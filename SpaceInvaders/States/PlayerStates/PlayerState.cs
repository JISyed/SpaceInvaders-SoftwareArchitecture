using Debug = System.Diagnostics.Debug;
using SpaceInvaders.Strategies.PlayerStrategies;
using SpaceInvaders.SceneSystem;
using PlayerShip = SpaceInvaders.GameObjectSystem.GameObjects.PlayerShip;

namespace SpaceInvaders.States.PlayerStates
{
	abstract class PlayerState
	{
		// Instance data
		private PlayerShip context;
		protected PlayerStrategy strMoveLeft;
		protected PlayerStrategy strMoveRight;
		protected PlayerStrategy strShoot;

		//
		// Constructors
		//

		private PlayerState()
		{
			Debug.Assert(false, "Default Constructor for PlayerState now allowed!");
		}

		public PlayerState(PlayerShip newContext)
		{
			this.context = newContext;
		}



		//
		// Methods
		//

		public void SetContext(PlayerShip newContext)
		{
			this.context = newContext;
			this.OnContextChange();
		}


		/// <summary>
		///		Defines how the player should move left
		/// </summary>
		public void MoveLeft()
		{
			this.strMoveLeft.PerformStrategy();
		}

		/// <summary>
		///		Defines how the player should move right
		/// </summary>
		public void MoveRight()
		{
			this.strMoveRight.PerformStrategy();
		}




		//
		// Virtuals - Must be ammended
		//

		/// <summary>
		///		Defines how the player should shoot weapons
		/// </summary>
		virtual public void Shoot()
		{
			this.strShoot.PerformStrategy();
		}



		//
		// Contracts
		//

		/// <summary>
		///		Gets called when the Player changes state
		/// </summary>
		abstract public void OnContextChange();

		/// <summary>
		///		State change for when the player gets killed
		/// </summary>
		abstract public void PlayerGotKilled();

		/// <summary>
		///		State change for when the player laser collides
		/// </summary>
		abstract public void PlayerLaserCollided();

		/// <summary>
		///		State change for when the player respawns
		/// </summary>
		abstract public void Respawn();

		/// <summary>
		///		State change for when the game begins
		/// </summary>
		abstract public void Begin();

		/// <summary>
		///		State change for when the game restarts
		/// </summary>
		abstract public void Restart();



		//
		// Properties
		//

		/// <summary>
		///		Read-only getter to the player ship
		/// </summary>
		public PlayerShip Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>
		///		Get the Player's active state
		/// </summary>
		public static PlayerState Active
		{
			get
			{
				GameScene scene = SceneManager.Self.ActiveScene as GameScene;
				Debug.Assert(scene != null, "The scene being retrieved in the PlayerState isn't a GameScene!");
				return scene.StatePlayerActive;
			}
		}

		/// <summary>
		///		Get the Player's active state
		/// </summary>
		public static PlayerState NoAmmo
		{
			get
			{
				GameScene scene = SceneManager.Self.ActiveScene as GameScene;
				Debug.Assert(scene != null, "The scene being retrieved in the PlayerState isn't a GameScene!");
				return scene.StatePlayerNoAmmo;
			}
		}

		/// <summary>
		///		Get the Player's active state
		/// </summary>
		public static PlayerState Standby
		{
			get
			{
				GameScene scene = SceneManager.Self.ActiveScene as GameScene;
				Debug.Assert(scene != null, "The scene being retrieved in the PlayerState isn't a GameScene!");
				return scene.StatePlayerStandby;
			}
		}

		/// <summary>
		///		Get the Player's active state
		/// </summary>
		public static PlayerState Dead
		{
			get
			{
				GameScene scene = SceneManager.Self.ActiveScene as GameScene;
				Debug.Assert(scene != null, "The scene being retrieved in the PlayerState isn't a GameScene!");
				return scene.StatePlayerDead;
			}
		}

	}
}
