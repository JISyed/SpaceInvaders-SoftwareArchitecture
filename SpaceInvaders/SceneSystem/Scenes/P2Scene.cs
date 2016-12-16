using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.HudSystem;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;

namespace SpaceInvaders.SceneSystem.Scenes
{
	sealed class P2Scene : GameScene
	{
		//
		// Constructor
		//

		public P2Scene()
			: base()
		{
			this.name = Name.GamePlayerTwo;
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
			return new PlayerTwoHudDisplayer();
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
			Scene p1Scene = SceneManager.Self.FindScene(Name.GamePlayerOne);

			// Don't do anything if P2 has lives left and there is no P1
			if (this.GameData.Lives > 0 && p1Scene == null)
			{
				// Don't swap scenes
				this.isMarkedForSceneChange = false;
				return;
			}

			// If P2 has lives and P1 exists
			else if (this.GameData.Lives > 0 && p1Scene != null)
			{
				// Swap to P1
				SceneManager.Self.SwapScene(p1Scene.SceneName);
			}

			// If P2 has no lives and P1 exist
			else if (this.GameData.Lives <= 0 && p1Scene != null)
			{
				// Mark P2 for removal and swap to P1
				this.DeleteScene();
				SceneManager.Self.SwapScene(p1Scene.SceneName);
			}

			// If P2 has no more lives and there is no P1
			else if (this.GameData.Lives <= 0 && p1Scene == null)
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
			SceneManager.Self.SetCachedScoreP2(this.GameData.Score);

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
			if (aliens != null)
			{
				aliens.StartMovingAgain();
			}

			// Make the UFO play a sound, if it exists
			UFO ufo = GameObjectManager.Active.Find(GameObject.Name.UFO, this.UfoId) as UFO;
 			if(ufo != null)
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
			SceneManager.Self.SetCachedScoreP2(this.GameData.Score);
		}
	}
}
