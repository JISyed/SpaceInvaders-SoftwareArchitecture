using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.GameObjectSystem.Factories;
using SpaceInvaders.GameObjectSystem.GameObjects;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.TimerSystem;
using SpaceInvaders.HudSystem;

namespace SpaceInvaders.GameObjectSystem
{
	class PlayerRespawner : ICommandable
	{
		private TimedEvent timedEvent;
		private float respawnWait;

		//
		// Constructor
		//

		public PlayerRespawner()
		{
			// DATA SET
			this.respawnWait = 2.5f;

			// Add self to timer
			this.timedEvent = TimedEventManager.Active.Create(this, this.respawnWait);
		}


		//
		// Destructor
		//

		~PlayerRespawner()
		{
			// Cancel event
			if (this.timedEvent != null)
			{
				TimedEventManager.Active.Recycle(this.timedEvent);
				this.timedEvent = null;
			}
		}



		//
		// Methods
		//

		public void ExecuteCommand()
		{
			// Not Game Over yet
			if (GameSessionData.Active.Lives > 0)
			{
				// Make the aliens move again
				AlienCoordinator aliens = GameObjectManager.Active.Find(GameObject.Name.AlienCoordinator, SceneManager.Self.ActiveScene.AlienCoordinatorId) as AlienCoordinator;
				Debug.Assert(aliens != null, "The AlienCoordinator could not be found upon respawning the Player!");
				aliens.StartMovingAgain();

				// Respawn player
				GameScene scene = SceneManager.Self.ActiveScene as GameScene;
				Debug.Assert(scene != null, "Trying to respawn a player in a non-GameScene!");
				PlayerFactory factory = new PlayerFactory(scene);
				factory.CreatePlayer();

				// Go to the next scene. See OnLoadNextScene() in P1Scene and P2Scene
				SceneManager.Self.ActiveScene.TransitionToNextScene();
			}
			// Game Over
			else
			{
				// Create a new Game Over text for the HUD
				HudText gameOverText = new HudText(HudDisplayer.LabelGameOverX, HudDisplayer.LabelGameOverY, "GAME OVER", Azul.AZUL_FONTS.Consolas36pt, Colors.Red);
				SceneManager.Self.ActiveScene.HudDisplay.AddHudText(gameOverText);

				// Test for new highscore
				bool isNewHighscore = SceneManager.Self.DetermineNewHighscore(GameSessionData.Active.Score);
				if(isNewHighscore)
				{
					HudText highScoreLabel = new HudText(HudDisplayer.LabelGameOverX + 27.0f, HudDisplayer.LabelGameOverY + 70.0f, "NEW HIGHSCORE", Azul.AZUL_FONTS.Consolas12pt, Colors.White);
					SceneManager.Self.ActiveScene.HudDisplay.AddHudText(highScoreLabel);
				}

				// Create a GameOver delay. Which will go to the next scene. 
				GameOverDelayEvent gameOver = new GameOverDelayEvent();
			}

			// This thing is done
			this.timedEvent = null;
		}



		//
		// Properties
		//

		/// <summary>
		///		The TimedEvent of this respawner
		/// </summary>
		public TimedEvent TimerEvent
		{
			get
			{
				return this.timedEvent;
			}
		}

		/// <summary>
		///		How long will it take to respawn the player? (In seconds)
		/// </summary>
		public float RespawnWait
		{
			get
			{
				return this.respawnWait;
			}
		}
	}
}
