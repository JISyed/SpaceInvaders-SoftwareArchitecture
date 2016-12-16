using Debug = System.Diagnostics.Debug;
using Enum = System.Enum;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.States.PlayerStates;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class PlayerShip : GameObject, ICollisionSubscriber
	{
		private PlayerState currentState;
		private float speed = 4.2f;
		private bool shouldBeDeleted = false;

		//
		// Constructors
		//

		public PlayerShip() : base()
		{
			this.SetName(GameObject.Name.Player);

			GameScene scene = SceneManager.Self.ActiveScene as GameScene;
			Debug.Assert(scene != null, "The currently active scene is not a GameScene!");
			scene.PlayerRoot.AddChild(this);
			
			this.SetState(PlayerState.Active);
		}



		//
		// Methods
		//

		/// <summary>
		///		Sets the new state for the player
		/// </summary>
		/// <param name="newState"></param>
		public void SetState(PlayerState newState)
		{
			this.currentState = newState;
			newState.SetContext(this);
		}

		/// <summary>
		///		Delete this Player. GameObjectManager takes care of the details
		/// </summary>
		public void MarkForDeletion()
		{
			this.shouldBeDeleted = true;
		}


		//
		// Contracts
		//

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{

		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			bool keepObjectAlive = !shouldBeDeleted;

			this.currentState.MoveLeft();
			this.currentState.MoveRight();
			this.currentState.Shoot();

			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{
			// Unsubscribe from all collisons
			// PlayerLaser vs. Wall
			var collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_WallTop);
			collisionPair.Unsubscribe(this);
			// PlayerLaser vs. Aliens
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Alien);
			collisionPair.Unsubscribe(this);
			// PlayerLaser vs. Shields
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Shield);
			collisionPair.Unsubscribe(this);
			// PlayerLaser vs. UFO
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_UFO);
			collisionPair.Unsubscribe(this);
			// PlayerLaser vs. AlienLaser
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser);
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
			bool willBeDeleted = false;

			// If player collided with wall (Wall_vs_Player)
			if(collisonName == CollisionPairEvaluator.Name.Wall_vs_Player)
			{
				// Check if it is the left wall
				if(other.X < Constants.SCREEN_CENTER_X)
				{
					// Find the left wall's right edge
					float rightEdgeX = other.X + (other.Collider.Width * 0.5f);
					float playerHalfWidth = this.Collider.Width * 0.5f;

					this.SetPosition(rightEdgeX + playerHalfWidth + 0.1f, this.Y);
				}
				// It's probably the right wall
				else
				{
					// Find the right wall's left edge
					float leftEdgeX = other.X - (other.Collider.Width * 0.5f);
					float playerHalfWidth = this.Collider.Width * 0.5f;

					this.SetPosition(leftEdgeX - playerHalfWidth - 0.1f, this.Y);
				}
			}
			// Player vs AlienLaser
			else if(collisonName == CollisionPairEvaluator.Name.Player_vs_AlienLaser)
			{
				this.currentState.PlayerGotKilled();
			}
			// Player vs Alien
			else if (collisonName == CollisionPairEvaluator.Name.Player_vs_Alien)
			{
				// Decrease the lives to 0 to invoke a Game Over event
				while(GameSessionData.Active.Lives > 0)
				{
					GameSessionData.Active.DecrementLives();
				}

				this.currentState.PlayerGotKilled();
			}

			return willBeDeleted;
		}

		/// <summary>
		///		Gets called when this object is notified about a collision
		/// </summary>
		/// <param name="collisonName"></param>
		public void OnCollisionNotified(CollisionPairEvaluator.Name collisonName)
		{
			// Assumes the following collison pairs:
			//		-- PlayerLaser vs WallTop
			//		-- PlayerLaser vs Aliens
			//		-- PlayerLaser vs Shields
			//		-- PlayerLaser vs UFO
			//		-- PlayerLaser vs AlienLaser
			this.currentState.PlayerLaserCollided();
		}




		//
		// Properties
		//

		/// <summary>
		///		The movement speed of the player ship
		/// </summary>
		public float Speed
		{
			get
			{
				return this.speed;
			}
		}

	}
}
