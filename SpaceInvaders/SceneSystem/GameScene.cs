using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.HudSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.States.PlayerStates;
using SpaceInvaders.TimerSystem;
using SpaceInvaders.GameObjectSystem.Factories;

namespace SpaceInvaders.SceneSystem
{
	abstract class GameScene : Scene
	{
		protected GameObject playerRoot;
		protected GameObject playerLaserRoot;
		protected PlayerStateActive playerStateActive;
		protected PlayerStateDead playerStateDead;
		protected PlayerStateNoAmmo playerStateNoAmmo;
		protected PlayerStateStandby playerStateStandby;
		private bool isPlayerRootAlreadySet;
		private bool isLaserRootAlreadySet;

		//
		// Constructor
		//

		public GameScene() : base()
		{
			this.playerRoot = null;
			this.playerLaserRoot = null;
			this.playerStateActive = new PlayerStateActive(null);
			this.playerStateDead = new PlayerStateDead(null);
			this.playerStateNoAmmo = new PlayerStateNoAmmo(null);
			this.playerStateStandby = new PlayerStateStandby(null);
			this.isPlayerRootAlreadySet = false;
			this.isLaserRootAlreadySet = false;
		}



		//
		// Methods
		//

		/// <summary>
		///		Set the collision root for the Player. Can only be set once!
		/// </summary>
		/// <param name="newRoot"></param>
		public void SetPlayerRoot(GameObject newRoot)
		{
			if (this.isPlayerRootAlreadySet == false)
			{
				this.isPlayerRootAlreadySet = true;
				this.playerRoot = newRoot;
			}
			else
			{
				Debug.Assert(false, "A root object for Player object is being set in a GameScene when it's already set!");
			}
		}

		/// <summary>
		///		Set the collision root for PlayerLaser. Can only be set once!
		/// </summary>
		/// <param name="newRoot"></param>
		public void SetPlayerLaserRoot(GameObject newRoot)
		{
			if(this.isLaserRootAlreadySet == false)
			{
				this.isLaserRootAlreadySet = true;
				this.playerLaserRoot = newRoot;
			}
			else
			{
				Debug.Assert(false, "A root object for PlayerLaser object is being set in a GameScene when it's already set!");
			}
		}





		//
		// Contracts
		//

		/// <summary>
		///		Is this Scene a GameScene?
		/// </summary>
		/// <returns></returns>
		public override bool IsGameScene()
		{
			return true;
		}

		/// <summary>
		///		Gets called when a Scene is loaded into memory.
		///		Load all the stuff needed for Game Scenes
		/// </summary>
		protected override void OnSceneLoad()
		{
			// Get the green floor
			SpriteBatch floorBatch = this.ManagerForSpriteBatch.Find(SpriteBatch.Name.Shields);
			SpriteEntity floorSprite = SpriteEntityManager.Self.Find(Sprite.Name.Floor);
			floorBatch.Attach(floorSprite, floorSprite.Id);

			///// GameObject Factories ////////////////////////////

			// Create all the shields
			ShieldFactory shieldFactory = new ShieldFactory(SpriteBatch.Name.Shields);
			shieldFactory.CreateShields();

			// Create all the Walls
			WallFactory wallFactory = new WallFactory();
			wallFactory.CreateWalls();

			// Create all the Aliens
			AlienFactory alienFactory = new AlienFactory(SpriteBatch.Name.Aliens);
			alienFactory.LoadResources();
			alienFactory.CreateAliens();

			// Create the player
			PlayerFactory playerFactory = new PlayerFactory(this);
			playerFactory.InitializeResources();
			playerFactory.CreatePlayer();

			// Create the UFO
			UFOFactory ufoFactory = new UFOFactory();
			ufoFactory.CreateObjects();

		}




		//
		// Properties
		//

		/// <summary>
		///		Reference to the root of the Player. Get only.
		/// </summary>
		public GameObject PlayerRoot
		{
			get
			{
				return this.playerRoot;
			}
		}

		/// <summary>
		///		Reference to the root of the PlayerLaser. Get only.
		/// </summary>
		public GameObject PlayerLaserRoot
		{
			get
			{
				return this.playerLaserRoot;
			}
		}

		/// <summary>
		///		Get only reference to the Scene's PlayerActiveState
		/// </summary>
		public PlayerStateActive StatePlayerActive
		{
			get
			{
				return this.playerStateActive;
			}
		}

		/// <summary>
		///		Get only reference to the Scene's PlayerStateDead
		/// </summary>
		public PlayerStateDead StatePlayerDead
		{
			get
			{
				return this.playerStateDead;
			}
		}

		/// <summary>
		///		Get only reference to the Scene's PlayerStateNoAmmo
		/// </summary>
		public PlayerStateNoAmmo StatePlayerNoAmmo
		{
			get
			{
				return this.playerStateNoAmmo;
			}
		}

		/// <summary>
		///		Get only reference to the Scene's PlayerStateStandby
		/// </summary>
		public PlayerStateStandby StatePlayerStandby
		{
			get
			{
				return this.playerStateStandby;
			}
		}

	}
}
