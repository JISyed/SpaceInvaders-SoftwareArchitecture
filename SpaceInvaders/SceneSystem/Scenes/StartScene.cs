using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.HudSystem;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;
using SpaceInvaders.InputSystem;

namespace SpaceInvaders.SceneSystem.Scenes
{
	sealed class StartScene : Scene
	{
		//
		// Constructor
		//

		public StartScene()
			: base()
		{
			this.name = Name.StartScreen;
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
			return new StartScreenHudDisplayer();
		}

		/// <summary>
		///		Gets called when a Scene's Update loop begins
		/// </summary>
		protected override void OnSceneBeganUpdate()
		{
			if(Input.GetKeyDown(Azul.AZUL_KEYS.KEY_ENTER))
			{
				// Move to the next scene
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
			// This should load the Player Select Scene
			SceneManager.Self.LoadScene(new PlayerSelectScreen());
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
		///		Load all the stuff needed for Scenes
		/// </summary>
		protected override void OnSceneLoad()
		{
			// Reset the trans-scene score tracker
			SceneManager.Self.SetCachedScoreP1(0);
			SceneManager.Self.SetCachedScoreP2(0);
		}
	}
}
