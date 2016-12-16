using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.CollisionSystem
{
	class CollisionPairFactory
	{
		public CollisionPairFactory()
		{
		}

		public void CreatePairs()
		{
			this.FactoryMethod();
		}


		private void FactoryMethod()
		{
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.Wall_vs_Player);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.Wall_vs_UFO);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.PlayerLaser_vs_Alien);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.PlayerLaser_vs_Shield);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.PlayerLaser_vs_UFO);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.PlayerLaser_vs_WallTop);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.Player_vs_Alien);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.Player_vs_AlienLaser);
			var cp = CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.Alien_vs_Shield, 5);
			cp.ShouldScanEntireFirstTree = true;
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.AlienLaser_vs_Shield, 3);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.AlienLaser_vs_WallBottom, 3);
			CollisonPairManager.Active.Create(CollisionPairEvaluator.Name.Wall_vs_Alien);
		}

	}
}
