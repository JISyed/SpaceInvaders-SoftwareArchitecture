using Debug = System.Diagnostics.Debug;
using Enum = System.Enum;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.TimerSystem;
using SpaceInvaders.AudioSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class DeadAlien : GameObject, ICommandable
	{
		private TimedEvent timedEvent = null;
		private AudioSource alienDeathSound;
		private float lifeTime = 0.2f;


		//
		// Constructor
		//

		private DeadAlien() : base()
		{
			// Not allowed
		}

		public DeadAlien(float initX, float initY)
			: base()
		{
			this.SetName(Name.DeadAlien);
			this.SetPosition(initX, initY);
			this.SetColor(Colors.White);
			this.SetSprite(SpriteBatch.Name.Explosions, SpriteEntityManager.Self.Find(Sprite.Name.DeadAlien));
			this.alienDeathSound = AudioSourceManager.Self.Find(AudioSource.Name.DeathAlien);
			GameObjectManager.Active.Attach(this);
		}




		//
		// Methods
		//

		/// <summary>
		///		Called when the timer goes off
		/// </summary>
		public void OnTimeUp()
		{
			// Destroy yourself
			GameObjectManager.Active.Recycle(this);
		}



		//
		// Contracts
		//

		/// <summary>
		///		From ICommandable
		/// </summary>
		public void ExecuteCommand()
		{
			this.OnTimeUp();
		}

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{
			// Add self to a timer
			this.timedEvent = TimedEventManager.Active.Create(this, this.lifeTime);

			// Play a sound
			this.alienDeathSound.Play();
		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			// Purposefully does nothing
			bool keepObjectAlive = true;
			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{
			this.timedEvent = null;
			this.alienDeathSound = null;
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
			// Purposefully does nothing
			bool willBeDeleted = false;
			return willBeDeleted;
		}




		//
		// Properties
		//

		public TimedEvent TimerEvent
		{
			get
			{
				return this.timedEvent;
			}
		}
	}
}
