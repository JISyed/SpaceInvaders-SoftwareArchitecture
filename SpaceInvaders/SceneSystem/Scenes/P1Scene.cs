using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.HudSystem;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;

namespace SpaceInvaders.SceneSystem.Scenes
{
	sealed class P1Scene : GameScene
	{
		//
		// Constructor
		//

		public P1Scene() : base()
		{
			this.name = Name.GamePlayerOne;
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
			return new PlayerOneHudDisplayer();
		}

		/// <summary>
		///		Gets called when a Scene's Update loop begins
		/// </summary>
		protected override void OnSceneBeganUpdate()
		{
			
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
			Scene p2Scene = SceneManager.Self.FindScene(Name.GamePlayerTwo);
			
			// Don't do anything if P1 has lives left and there is no P2
			if(this.GameData.Lives > 0 && p2Scene == null)
			{
				// Don't swap scenes
				this.isMarkedForSceneChange = false;
				return;
			}

			// If P1 has lives and P2 exists
			else if(this.GameData.Lives > 0 && p2Scene != null)
			{
				// Swap to P2
				SceneManager.Self.SwapScene(p2Scene.SceneName);
			}

			// If P1 has no lives and P2 exist
			else if(this.GameData.Lives <= 0 && p2Scene != null)
			{
				// Mark P1 for removal and swap to P2
				this.DeleteScene();
				SceneManager.Self.SwapScene(p2Scene.SceneName);
			}

			// If P1 has no more lives and there is no P2
			else if(this.GameData.Lives <= 0 && p2Scene == null)
			{
				// Load the Start screen
				SceneManager.Self.LoadScene(new StartScene());
			}
			// Unhandled case
			else
			{
				Debug.Assert(false, "OnLoadNextScene for a GameScene caught an unknown condition that is not handled!");
			}

		}

		/// <summary>
		///		Gets called when a Scene is paused
		/// </summary>
		protected override void OnScenePaused()
		{
			// Send the current score to the Scene Manager
			SceneManager.Self.SetCachedScoreP1(this.GameData.Score);

			// Dispose temporary HudTexts from this scene
			this.HudDisplay.RemoveDisposableHudText();
		}

		/// <summary>
		///		Gets called when a Scene is unpaused
		/// </summary>
		protected override void OnSceneUnpaused()
		{
			// Make the aliens move again
			AlienCoordinator aliens = GameObjectManager.Active.Find(GameObject.Name.AlienCoordinator, this.AlienCoordinatorId) as AlienCoordinator;
			if(aliens != null)
			{
				aliens.StartMovingAgain();
			}

			// Make the UFO play a sound, if it exists
			UFO ufo = GameObjectManager.Active.Find(GameObject.Name.UFO, this.UfoId) as UFO;
			if (ufo != null)
			{
				ufo.PlayLoopingSound();
			}
		}

		/// <summary>
		///		Gets called when a Scene is unloaded off memory
		/// </summary>
		protected override void OnSceneUnload()
		{
			// Send the current score to the Scene Manager
			SceneManager.Self.SetCachedScoreP1(this.GameData.Score);
		}

		//protected override void OnSceneLoad()
		//{
		//	base.OnSceneLoad();

		//	string newText = "GET READY - PLAYER 1";
		//	HudText getReady = new HudText(205.0f, 70.0f, newText, Azul.AZUL_FONTS.Consolas24pt, Colors.Green);
		//	SceneManager.Self.ActiveScene.HudDisplay.AddHudText(getReady);
		//}
	}
}
