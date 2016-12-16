using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.HudSystem;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;
using SpaceInvaders.InputSystem;

namespace SpaceInvaders.SceneSystem.Scenes
{
	sealed class PlayerSelectScreen : Scene
	{
		SceneManager.NumberOfPlayers numOfPlayers;

		//
		// Constructor
		//

		public PlayerSelectScreen()
			: base()
		{
			this.name = Name.PlayerSelect;
			this.numOfPlayers = SceneManager.NumberOfPlayers.One;
		}



		//
		// Methods
		//




		//
		// Contracts
		//


		/// <summary>
		///		Create a unique HudDisplayer in the specialized class.
		///		Should only ever be called by base Scene!
		///		Also should not initialize the Hud here!
		/// </summary>
		/// <returns></returns>
		protected override HudDisplayer CreateHud()
		{
			return new PlayerSelectHudDisplayer();
		}

		/// <summary>
		///		Gets called when a Scene's Update loop begins
		/// </summary>
		protected override void OnSceneBeganUpdate()
		{
			if(Input.GetKeyDown(Azul.AZUL_KEYS.KEY_1))
			{
				this.numOfPlayers = SceneManager.NumberOfPlayers.One;
				SceneManager.Self.SetNumberOfPlayers(SceneManager.NumberOfPlayers.One);
				this.TransitionToNextScene();
			}
			else if(Input.GetKeyDown(Azul.AZUL_KEYS.KEY_2))
			{
				this.numOfPlayers = SceneManager.NumberOfPlayers.Two;
				SceneManager.Self.SetNumberOfPlayers(SceneManager.NumberOfPlayers.Two);
				this.TransitionToNextScene();
			}
		}

		/// <summary>
		///		Gets called when a Scene's Update loop ends
		/// </summary>
		protected override void OnSceneEndedUpdate()
		{

		}

		/// <summary>
		///		Each Scene has a different way of determining the next
		///		Scene to load. For Manager use only!!
		/// </summary>
		public override void OnLoadNextScene()
		{
			// Load only Player 1 game if 1 player was chosen
			if(this.numOfPlayers == SceneManager.NumberOfPlayers.One)
			{
				SceneManager.Self.LoadScene(new P1Scene());
			}
			// Otherwise create both Player1 and Player2 games and load P1
			else if(this.numOfPlayers == SceneManager.NumberOfPlayers.Two)
			{
				// Attach both scenes
				SceneManager.Self.AttachScene(new P1Scene());
				SceneManager.Self.AttachScene(new P2Scene());

				// Load player 1's scene
				SceneManager.Self.LoadScene(Name.GamePlayerOne);
			}
		}

		/// <summary>
		///		Gets called when a Scene is paused
		/// </summary>
		protected override void OnScenePaused()
		{
			
		}

		/// <summary>
		///		Gets called when a Scene is unpaused
		/// </summary>
		protected override void OnSceneUnpaused()
		{
			
		}

		/// <summary>
		///		Gets called when a Scene is unloaded off memory
		/// </summary>
		protected override void OnSceneUnload()
		{
			
		}

		/// <summary>
		///		Is this Scene a GameScene?
		/// </summary>
		/// <returns></returns>
		public override bool IsGameScene()
		{
			return false;
		}

		/// <summary>
		///		Gets called when a Scene is loaded into memory.
		///		Load all the stuff needed for Game Scenes
		/// </summary>
		protected override void OnSceneLoad()
		{
			
		}

	}
}
