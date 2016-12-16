using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;

namespace SpaceInvaders.GameObjectSystem.Factories
{
	class WallFactory
	{
		////////////////////////////////////////////////
		//
		// Data
		private readonly Azul.Color CollisonColor = Colors.Yellow;
		private readonly float HorizontalWallWidth = 606.0f;
		private readonly float HorizontalWallHeight = 40.0f;
		private readonly float VerticalWallWidth = 40.0f;
		private readonly float VerticalWallHeight = 768.0f;
		private readonly float HorizontalWallX = 336.0f;
		private readonly float CeilingY = 706.0f;
		private readonly float FloorY = 24.0f;
		private readonly float VerticalWallY = 384.0f;
		private readonly float LeftWallX = 12.0f;
		private readonly float RightWallX = 660.0f;
		//
		//
		////////////////////////////////////////////////


		//
		// Constructor 
		//

		public WallFactory()
		{
			
		}



		//
		// Methods
		//


		/// <summary>
		///		Create all four walls in the game
		/// </summary>
		public void CreateWalls()
		{
			this.CreateCeiling();
			this.CreateFloor();
			this.CreateSideWalls();
		}





		//
		// Private Methods
		//

		private void CreateCeiling()
		{
			Wall ceiling = new Wall(this.HorizontalWallWidth, this.HorizontalWallHeight);
			ceiling.Collider.Color = this.CollisonColor;
			ceiling.SetPosition(this.HorizontalWallX, this.CeilingY);
			GameObjectManager.Active.Attach(ceiling);

			// Set the collision root for this object
			var collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_WallTop);
			collisionPair.SetSecondCollisonRoot(ceiling);
		}

		private void CreateFloor()
		{
			Wall floor = new Wall(this.HorizontalWallWidth, this.HorizontalWallHeight);
			floor.Collider.Color = this.CollisonColor;
			floor.SetPosition(this.HorizontalWallX, this.FloorY);
			GameObjectManager.Active.Attach(floor);

			// Set the collision root for this object
			var collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.AlienLaser_vs_WallBottom);
			collisionPair.SetSecondCollisonRoot(floor);
		}

		private void CreateSideWalls()
		{
			// Create the root for the side walls
			EmptyObject wallRoot = new EmptyObject();
			GameObjectManager.Active.Attach(wallRoot);
			var collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_Alien);
			collisonPair.SetFirstCollisonRoot(wallRoot);
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_Player);
			collisonPair.SetFirstCollisonRoot(wallRoot);
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_UFO);
			collisonPair.SetFirstCollisonRoot(wallRoot);

			// Create the left wall
			Wall left = new Wall(this.VerticalWallWidth, this.VerticalWallHeight);
			left.Collider.Color = this.CollisonColor;
			left.SetPosition(this.LeftWallX, this.VerticalWallY);
			wallRoot.AddChild(left);
			GameObjectManager.Active.Attach(left);

			// Create the right wall
			Wall right = new Wall(this.VerticalWallWidth, this.VerticalWallHeight);
			right.Collider.Color = this.CollisonColor;
			right.SetPosition(this.RightWallX, this.VerticalWallY);
			wallRoot.AddChild(right);
			GameObjectManager.Active.Attach(right);

			// Recalculate collider of root
			wallRoot.RecalculateCollider();
		}
	}
}
