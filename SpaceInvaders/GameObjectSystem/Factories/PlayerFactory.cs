using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ImageSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.AnimationSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.GameObjectSystem.Factories
{
	class PlayerFactory
	{
		////////////////////////////////////////////////
		//
		// Data
		private readonly Azul.Color CollisonColor = Colors.Red;
		private readonly float DeathAnimationInterval = 0.15f;
		private readonly float InitX = 55.0f;
		private readonly float InitY = 110.0f;
		//
		//
		////////////////////////////////////////////////

		private GameScene sceneRef;


		//
		// Constructor
		//

		private PlayerFactory()
		{
			// Not used
		}

		public PlayerFactory(GameScene scene)
		{
			this.sceneRef = scene;
		}




		//
		// Methods
		//

		/// <summary>
		///		Initialize needed resources before making player
		/// </summary>
		public void InitializeResources()
		{
			this.LoadSpriteAnimations();
			this.SetupCollisionHierarchy();
		}

		/// <summary>
		///		Create a PlayerShip GameObject and return it
		/// </summary>
		/// <returns></returns>
		public PlayerShip CreatePlayer()
		{
			SpriteBatch batch = SpriteBatchManager.Active.Find(SpriteBatch.Name.Player);

			PlayerShip player = new PlayerShip();

			player.SetPosition(this.InitX, this.InitY);
			player.SetColor(Colors.Green);
			player.Collider.Color = this.CollisonColor;
			player.SetSprite(SpriteBatch.Name.Player, SpriteEntityManager.Self.Find(Sprite.Name.Player));
			GameObjectManager.Active.Attach(player);


			// Player listens for PlayerLaser vs. WallTop
			var collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_WallTop);
			collisionPair.Subscribe(player);
			// And PlayerLaser vs. Aliens
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Alien);
			collisionPair.Subscribe(player);
			// And PlayerLaser vs. Shields
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Shield);
			collisionPair.Subscribe(player);
			// PlayerLaser vs. UFO
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_UFO);
			collisionPair.Subscribe(player);
			// PlayerLaser vs. AlienLaser
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser);
			collisionPair.Subscribe(player);

			return player;
		}






		//
		// Private Methods
		//

		/// <summary>
		///		Loads all the animators for the Player
		/// </summary>
		private void LoadSpriteAnimations()
		{
			AnimationFrameManager.Active.Create(Sprite.Name.DeadPlayer, this.DeathAnimationInterval, true);
			AnimationFrameManager.Active.Attach(Sprite.Name.DeadPlayer, Image.Name.DeadPlayer1_X);
			AnimationFrameManager.Active.Attach(Sprite.Name.DeadPlayer, Image.Name.DeadPlayer2_S);
		}

		/// <summary>
		///		Setup the collision pairs for the player
		/// </summary>
		private void SetupCollisionHierarchy()
		{
			CollisionPairEvaluator collisionPair = null;

			// Create the collision root for player laser
			EmptyObject playerLaserRoot = new EmptyObject();
			playerLaserRoot.SetPosition(-10f, -10f);	// Throw to a bottom corner
			GameObjectManager.Active.Attach(playerLaserRoot);
			
			// Set the root of this collision
			this.sceneRef.SetPlayerLaserRoot(playerLaserRoot);


			// Find all collision pairs the laser is a part of
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_WallTop);
			collisionPair.SetFirstCollisonRoot(playerLaserRoot);
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Alien);
			collisionPair.SetFirstCollisonRoot(playerLaserRoot);
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Shield);
			collisionPair.SetFirstCollisonRoot(playerLaserRoot);
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_UFO);
			collisionPair.SetFirstCollisonRoot(playerLaserRoot);
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser);
			collisionPair.SetFirstCollisonRoot(playerLaserRoot);



			//////////////////////////////////////////////



			// Create the collision root for the player
			EmptyObject playerRoot = new EmptyObject();
			playerRoot.SetPosition(this.InitX, this.InitY);
			GameObjectManager.Active.Attach(playerRoot);

			// Set the root of this collision
			this.sceneRef.SetPlayerRoot(playerRoot);



			// Find all the collision pairs the player is a part of

			// Wall vs Player
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_Player);
			collisionPair.SetSecondCollisonRoot(playerRoot);

			// Player vs Alien
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Player_vs_Alien);
			collisionPair.SetFirstCollisonRoot(playerRoot);

			// Player vs AlienLaser
			collisionPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Player_vs_AlienLaser);
			collisionPair.SetFirstCollisonRoot(playerRoot);


			// Don't need the reference anymore
			this.sceneRef = null;
		}




	}
}
