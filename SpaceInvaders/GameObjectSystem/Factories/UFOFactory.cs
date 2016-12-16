using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;
using SpaceInvaders.CollisionSystem;

namespace SpaceInvaders.GameObjectSystem.Factories
{
	class UFOFactory
	{

		//
		// Methods
		//

		/// <summary>
		///		Create the GameObjects needed for UFO
		/// </summary>
		public void CreateObjects()
		{
			UFOCoordinator ufoCoordinator = new UFOCoordinator();


			// Register UFO collisions into the collision roots
			// AND subscribe the coordinator to those collisions
			var collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_UFO);
			collisionPair.SetSecondCollisonRoot(ufoCoordinator);
			collisionPair.Subscribe(ufoCoordinator);

			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_UFO);
			collisionPair.SetSecondCollisonRoot(ufoCoordinator);
			collisionPair.Subscribe(ufoCoordinator);



			// Add UFO root to GameObject manager
			GameObjectManager.Active.Attach(ufoCoordinator);
		}

	}
}
