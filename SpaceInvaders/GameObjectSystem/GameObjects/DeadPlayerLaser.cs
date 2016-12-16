using Debug = System.Diagnostics.Debug;
using Enum = System.Enum;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.TimerSystem;
using SpaceInvaders.AudioSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class DeadPlayerLaser : GameObject, ICommandable
	{
		private TimedEvent timedEvent = null;
		private float lifeTime = 0.1f;


		//
		// Constructor
		//

		private DeadPlayerLaser() : base()
		{
			// Not allowed
		}

		public DeadPlayerLaser(float initX, float initY) 
			: base()
		{
			this.SetName(Name.DeadPlayerLaser);
			this.SetPosition(initX, initY);
			this.SetColor(Colors.Red);
			this.SetSprite(SpriteBatch.Name.Explosions, SpriteEntityManager.Self.Find(Sprite.Name.SmallExplosionMissile));
			GameObjectManager.Active.Attach(this);
		}

		public DeadPlayerLaser(float initX, float initY, float lifeSpan)
			: base()
		{
			this.SetName(Name.DeadPlayerLaser);
			this.SetPosition(initX, initY);
			this.SetColor(Colors.Red);
			this.lifeTime = lifeSpan;
			this.SetSprite(SpriteBatch.Name.Explosions, SpriteEntityManager.Self.Find(Sprite.Name.SmallExplosionMissile));
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
