using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;
using SpaceInvaders.CollisionSystem;

namespace SpaceInvaders.GameObjectSystem.Factories
{
	class ShieldFactory
	{
		// References
		private SpriteBatch.Name batchName;


		////////////////////////////////////////////////
		//
		// Data
		private readonly uint NumOfBarricades = 4u;
		private readonly float InitX = 100.0f;
		private readonly float InitY = 160.0f;
		private readonly float XGap = 140.0f;
		//
		//
		////////////////////////////////////////////////



		//
		// Constructor
		//

		private ShieldFactory()
		{
			// Does nothing
		}

		public ShieldFactory(SpriteBatch.Name newBatchName)
		{
			this.batchName = newBatchName;
		}




		//
		// Methods
		//

		/// <summary>
		///		Create shields
		/// </summary>
		public void CreateShields()
		{
			this.LoadGameObjects();
		}






		//
		// Private Methods
		//

		/// <summary>
		///		Setup all the collisions for shields
		/// </summary>
		/// <param name="collisionRoot"></param>
		private void SetupCollisions(GameObject collisionRoot)
		{
			CollisionPairEvaluator colPair = null;
			
			// PlayerLaser vs Shield
			colPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Shield);
			colPair.SetSecondCollisonRoot(collisionRoot);

			// Alien vs Shield
			colPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Alien_vs_Shield);
			colPair.SetSecondCollisonRoot(collisionRoot);

			// AlienLaser vs Shield
			colPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.AlienLaser_vs_Shield);
			colPair.SetSecondCollisonRoot(collisionRoot);

		}

		/// <summary>
		///		Load all the shield GameObjects
		/// </summary>
		private void LoadGameObjects()
		{
			// Create the root of the shields
			ShieldRoot shieldRoot = new ShieldRoot();
			this.SetupCollisions(shieldRoot);
			GameObjectManager.Active.Attach(shieldRoot);
			
			// Create all the shield barricades
			for(uint i = 0; i < this.NumOfBarricades; i++)
			{
				ShieldBarricade barricade = new ShieldBarricade(
						this.InitX + this.XGap*i, 
						this.InitY
				);
				shieldRoot.AddChild(barricade);
				GameObjectManager.Active.Attach(barricade);
			}

		}

		
	}
}
