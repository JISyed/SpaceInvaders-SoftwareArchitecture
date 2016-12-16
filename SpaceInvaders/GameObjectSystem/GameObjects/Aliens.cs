using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjectSystem.Factories;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class Aliens : GameObject
	{
		private int pointsRewarded;
		private float laserYOffset;

		//
		// Constructor
		//

		private Aliens() : base()
		{
			// Not in use
		}


		public Aliens(AlienName newName) : base()
		{
			this.laserYOffset = 5.0f;

			switch (newName)
			{
				case AlienName.Crab:
					this.pointsRewarded = 20;
					break;
				case AlienName.Squid:
					this.pointsRewarded = 30;
					break;
				case AlienName.Octopus:
					this.pointsRewarded = 10;
					break;
				default:
					break;
			}
		}



		
		//
		// Methods
		//

		/// <summary>
		///		Make an individual alien shoot laser
		/// </summary>
		/// <param name="laserRoot"></param>
		public void ShootLaser(GameObject laserRoot)
		{
			// Create laser
			AlienLaser laser = new AlienLaser(this.X, this.Y + this.laserYOffset, laserRoot);
		}



		//
		// Contracts
		//

		/// <summary>
		///		Start method of the Alien
		/// </summary>
		public override void Start()
		{
			
		}

		/// <summary>
		///		The update loop routine of the Alien
		/// </summary>
		public override bool Update()
		{
			bool keepObjectAlive = true;
			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the Alien (after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{
			
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

			// PlayerLaser vs Alien
			if(collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_Alien)
			{
				willBeDeleted = true;

				DeadAlien explosion = new DeadAlien(this.X, this.Y);

				// Reward points
				GameSessionData.Active.AddScore(this.pointsRewarded);
			}

			return willBeDeleted;
		}
	}
}
