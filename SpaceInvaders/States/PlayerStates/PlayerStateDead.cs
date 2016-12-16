using Debug = System.Diagnostics.Debug;
using SpaceInvaders.Strategies.PlayerStrategies;
using SpaceInvaders.SpriteSystem;
using PlayerShip = SpaceInvaders.GameObjectSystem.GameObjects.PlayerShip;
using SpaceInvaders.TimerSystem;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.AudioSystem;
using SpaceInvaders.HudSystem;

namespace SpaceInvaders.States.PlayerStates
{
	class PlayerStateDead : PlayerState, ICommandable
	{
		private TimedEvent timedEvent;
		private AudioSource deathSound;

		//
		// Constructors
		//

		private PlayerStateDead() : base(null)
		{
			// Not allowed
		}

		public PlayerStateDead(PlayerShip newContext)
			: base(newContext)
		{
			this.strMoveLeft = new PlayerStrategyNone();
			this.strMoveRight = new PlayerStrategyNone();
			this.strShoot = new PlayerStrategyNone();
			this.timedEvent = null;
			this.deathSound = AudioSourceManager.Self.Find(AudioSource.Name.DeathPlayer);
		}



		//
		// Destructor
		//

		~PlayerStateDead()
		{
			this.deathSound = null;

			if (this.timedEvent != null)
			{
				// Cancel
				TimedEventManager.Active.Recycle(this.timedEvent);
				this.timedEvent = null;
			}
		}



		//
		// Contracts
		//

		/// <summary>
		///		State change for when the player shoots
		/// </summary>
		public override void Shoot()
		{
			
		}

		/// <summary>
		///		State change for when the player gets killed
		/// </summary>
		override public void PlayerGotKilled()
		{
			
		}

		/// <summary>
		///		State change for when the player laser collides
		/// </summary>
		override public void PlayerLaserCollided()
		{

		}

		/// <summary>
		///		State change for when the player respawns
		/// </summary>
		override public void Respawn()
		{
			this.Context.SetSprite(this.Context.BatchName, SpriteEntityManager.Self.Find(Sprite.Name.Player));
			this.Context.SetState(PlayerState.Active);
		}

		/// <summary>
		///		State change for when the game begins
		/// </summary>
		override public void Begin()
		{
			
		}

		/// <summary>
		///		State change for when the game restarts
		/// </summary>
		override public void Restart()
		{
			this.Context.SetSprite(this.Context.BatchName, SpriteEntityManager.Self.Find(Sprite.Name.Player));
			this.Context.SetState(PlayerState.Standby);
		}

		/// <summary>
		///		Gets called when the Player changes state
		/// </summary>
		public override void OnContextChange()
		{
			// Player just died

			// Change the sprite of the player
			this.Context.SetSprite(this.Context.BatchName, SpriteEntityManager.Self.Find(Sprite.Name.DeadPlayer));
			this.deathSound.Play();

			// Create the respawner
			PlayerRespawner respawner = new PlayerRespawner();

			// Add self to the timer
			TimedEventManager.Active.Create(this, respawner.RespawnWait * 0.5f);
		}

		/// <summary>
		///		From ICommandable. Executes when it's timer event goes off.
		///		This does NOT respawn the player.
		/// </summary>
		public void ExecuteCommand()
		{
			// Lose a life
			GameSessionData.Active.DecrementLives();
			
			// Player will now disappear
			this.Context.MarkForDeletion();

			// Prepare the "Get Ready" text, but only in 2-player mode
			// And only if not Game Over
			if(SceneManager.Self.TotalNumberOfPlayers == SceneManager.NumberOfPlayers.Two
				&& GameSessionData.Active.Lives > 0)
			{
				string newText = " ";

				if(SceneManager.Self.ActiveSceneName == Scene.Name.GamePlayerOne)
				{
					newText = "GET READY - PLAYER 2";
				}
				else if(SceneManager.Self.ActiveSceneName == Scene.Name.GamePlayerTwo)
				{
					newText = "GET READY - PLAYER 1";
				}

				// Create the HUD text
				HudText getReady = new HudText(205.0f, 70.0f, newText, Azul.AZUL_FONTS.Consolas24pt, Colors.Green);
				SceneManager.Self.ActiveScene.HudDisplay.AddDisposableHudText(getReady);
			}

			this.timedEvent = null;
		}

		/// <summary>
		///		The TimedEvent for this command
		/// </summary>
		public TimedEvent TimerEvent
		{
			get
			{
				return this.timedEvent;
			}
		}
	}

	
}
