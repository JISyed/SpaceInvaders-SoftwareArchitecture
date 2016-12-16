using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.AudioSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class UFO : GameObject
	{
		/////////////////////////
		//
		// UFO Data
		private float speed = 2.75f;		// Absolute speed
		//
		//
		/////////////////////////
		private AudioSource sndUfo;
		private AudioSource sndDeath;


		//
		// Constructors
		//

		private UFO() : base()
		{
			Debug.Assert(false, "Default constructor for UFO not allowed!");
		}

		public UFO(float initX, float initY, HorizontalDirection.Name leftOrRight, AudioSource newUfoSound, AudioSource newDeathSound) 
			: base()
		{
			SceneManager.Self.ActiveScene.SetUfoId(this.Id);
			this.SetName(Name.UFOCoordinator);
			this.Collider.Color = Colors.Yellow;
			this.SetPosition(initX, initY);

			this.SetColor(Colors.Red);
			this.SetSprite(SpriteBatch.Name.Aliens, SpriteEntityManager.Self.Find(Sprite.Name.UFO));

			// Set the sounds
			this.sndUfo = newUfoSound;
			this.sndDeath = newDeathSound;

			// Factor the direction of the UFO into the speed
			// The extra -1 is to make sure left-spawned UFOs go right, and vice versa
			this.speed *= HorizontalDirection.ConvertToFloat(leftOrRight) * -1.0f;
		}




		//
		// Methods
		//

		/// <summary>
		///		Plays that obnoxious looping sound
		/// </summary>
		public void PlayLoopingSound()
		{
			if (this.sndUfo.IsPaused)
			{
				this.sndUfo.Play();
			}
		}



		//
		// Private Methods
		//




		//
		// Contracts
		//

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{
			// Start playing that obnoxious sound
			this.PlayLoopingSound();
		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			bool keepObjectAlive = true;


			// Change position
			this.SetPosition(this.X + this.speed, this.Y);


			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{
			// Stop the obnoxious sound
			this.sndUfo.Stop();

			this.sndDeath = null;
			this.sndUfo = null;
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
			bool willBeDeleted = false;

			// Wall vs UFO
			if(collisonName == CollisionPairEvaluator.Name.Wall_vs_UFO)
			{
				// Destroy self...

				// But only if the UFO is moving right toward the right wall
				bool approachingRightWall = this.speed > 0.0f && this.X < other.X;
				// Or if the UFO is moving left toward the left wall
				bool approachingLeftWall = this.speed < 0.0f && this.X > other.X;

				if (approachingLeftWall || approachingRightWall)
				{
					willBeDeleted = true;
				}
			}
			// PlayerLaser vs UFO
			else if(collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_UFO)
			{
				willBeDeleted = true;

				// Play the death sound
				this.sndDeath.Play();

				// Create small explosion
				DeadPlayerLaser smallExplosion = new DeadPlayerLaser(this.X, this.Y, 1.0f);
			}

			return willBeDeleted;
		}




	}
}
