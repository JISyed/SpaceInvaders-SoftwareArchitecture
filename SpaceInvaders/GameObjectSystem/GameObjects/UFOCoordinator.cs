using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.TimerSystem;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.AudioSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class UFOCoordinator : GameObject, ICollisionSubscriber, ICommandable
	{
		////////////////////////////////
		//
		// Data Set
		private const float SpawnLeftX = 12.0f;
		private const float SpawnRightX = 660.0f;
		private const float SpawnY = 660.0f;
		private const float MinSpawnTime = 20.0f;
		private const float MaxSpawnTime = 35.0f;
		//
		//
		////////////////////////////////
		private AudioSource sndUfo;
		private AudioSource sndUfoDeath;
		private const int NUM_POINT_ENTIRES = 4;

		private TimedEvent ufoLaunchEvent;
		private UFO ufoReference;

		private int[] possiblePoints;

		//
		// Constructors
		//

		public UFOCoordinator() : base()
		{
			this.SetName(Name.UFOCoordinator);
			this.Collider.Color = Colors.Yellow;
			this.SetPosition(-30.0f, UFOCoordinator.SpawnY);
			this.ufoLaunchEvent = null;
			this.ufoReference = null;

			// Load the UFO sounds
			this.sndUfo = AudioSourceManager.Self.Find(AudioSource.Name.UFOBeep);
			this.sndUfoDeath = AudioSourceManager.Self.Find(AudioSource.Name.DeathUFO);

			// Initialize possible points table
			this.possiblePoints = new int[NUM_POINT_ENTIRES];
			this.possiblePoints[0] = 50;
			this.possiblePoints[1] = 100;
			this.possiblePoints[2] = 150;
			this.possiblePoints[3] = 300;
		}




		//
		// Methods
		//

		/// <summary>
		///		Removes the UFO launch event from the Timer (TimedEventManager)
		/// </summary>
		public void CancelLaunchingTheUFO()
		{
			Debug.Assert(this.ufoLaunchEvent != null, "The UFO launch event is null before canceling it!");
			TimedEventManager.Active.Recycle(this.ufoLaunchEvent);
		}




		//
		// Private Methods
		//

		/// <summary>
		///		Puts itself into the timer so that it can spawn a UFO later
		/// </summary>
		private void ScheduleNextUFOLaunch()
		{
			// Randomly decide the next launch event's time
			float launchTime = Randomizer.RandomRange(MinSpawnTime, MaxSpawnTime);

			// Create a new timed event
			Debug.Assert(this.ufoLaunchEvent == null, "A UFO launch TimedEvent already exists!");
			this.ufoLaunchEvent = TimedEventManager.Active.Create(this, launchTime);
		}


		/// <summary>
		///		Create a new UFO, does NOT schedule anything
		/// </summary>
		private void SpawnNewUFO()
		{
			// Decide randomly whether to spawn the UFO left or right
			HorizontalDirection.Name leftOrRight = HorizontalDirection.ConvertFromInt(Randomizer.RandomRange(0, 2));
			float xSpawn = 0.0f;
			if(leftOrRight == HorizontalDirection.Name.Left)
			{
				xSpawn = UFOCoordinator.SpawnLeftX;
			}
			else
			{
				xSpawn = UFOCoordinator.SpawnRightX;
			}

			// Create the UFO
			UFO ufo = new UFO(xSpawn, UFOCoordinator.SpawnY, leftOrRight, this.sndUfo, this.sndUfoDeath);
			this.AddChild(ufo);
			GameObjectManager.Active.Attach(ufo);

			// Keep a reference
			ufoReference = ufo;
		}

		/// <summary>
		///		Randonly return one of the possible mystery points
		///		rewarded for the UFO
		/// </summary>
		/// <returns></returns>
		private int ChooseRandomScore()
		{
			// Choose a number between 0 and 3 (indices in the point table)
			int randomIndex = Randomizer.RandomInt(NUM_POINT_ENTIRES);

			// Return the score at that index
			return this.possiblePoints[randomIndex];
		}






		//
		// Contracts
		//

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{
			this.ScheduleNextUFOLaunch();
		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			bool keepObjectAlive = true;

			// Create a new UFO launch event...
			// if there is no UFO, and no launch event
			if(this.ufoLaunchEvent == null && this.ufoReference == null)
			{
				this.ScheduleNextUFOLaunch();
			}

			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{
			this.CancelLaunchingTheUFO();

			this.sndUfo = null;
			this.sndUfoDeath = null;

			// Unsubscribe from collisions
			// Refer to UFOFactory.CreateObjects() for which collisions
			var collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_UFO);
			collisionPair.Unsubscribe(this);
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_UFO);
			collisionPair.Unsubscribe(this);
		}

		/// <summary>
		///		Executes when a collison occurs with another GameObject
		/// </summary>
		/// <param name="collisonName"></param>
		/// <param name="other"></param>
		/// <returns>
		///		<c>true</c> if the object is to be deleted
		///	</returns>
		public override bool OnCollide(CollisionPairEvaluator.Name collisonName, GameObject other)
		{
			// Does nothing...
			bool willBeDeleted = false;
			return willBeDeleted;
		}

		/// <summary>
		///		This method gets called when a collision occurs 
		///		that this object observes (subscribes to)
		/// </summary>
		/// <param name="collisionName"></param>
		public void OnCollisionNotified(CollisionPairEvaluator.Name collisionName)
		{
			// Assuming Wall_vs_UFO or PlayerLaser_vs_UFO (either way, the UFO died)

			// It no longer has a UFO reference
			this.ufoReference = null;

			if (collisionName == CollisionPairEvaluator.Name.PlayerLaser_vs_UFO)
			{
				// Award points
				GameSessionData.Active.AddScore(this.ChooseRandomScore());
			}

		}

		/// <summary>
		///		From ICommandable. Executes when its waiting-time is finished.
		/// </summary>
		public void ExecuteCommand()
		{
			this.SpawnNewUFO();
			this.ufoLaunchEvent = null;
		}





		//
		// Properties
		//

		public TimedEvent TimerEvent
		{
			get
			{
				return this.ufoLaunchEvent;
			}
		}
	}
}
