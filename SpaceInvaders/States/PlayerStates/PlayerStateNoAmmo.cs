using Debug = System.Diagnostics.Debug;
using SpaceInvaders.Strategies.PlayerStrategies;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.AudioSystem;
using PlayerShip = SpaceInvaders.GameObjectSystem.GameObjects.PlayerShip;
using SpaceInvaders.GameObjectSystem;

namespace SpaceInvaders.States.PlayerStates
{
	class PlayerStateNoAmmo : PlayerState
	{
		//
		// Constructors
		//

		private PlayerStateNoAmmo() : base(null)
		{
			// Not allowed
		}

		public PlayerStateNoAmmo(PlayerShip newContext)
			: base(newContext)
		{
			this.strMoveLeft = new PlayerStrategyMoveLeft(this);
			this.strMoveRight = new PlayerStrategyMoveRight(this);
			this.strShoot = new PlayerStrategyNone();
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
			this.Context.SetState(PlayerState.Dead);
		}

		/// <summary>
		///		State change for when the player laser collides
		/// </summary>
		override public void PlayerLaserCollided()
		{
			this.Context.SetState(PlayerState.Active);
		}

		/// <summary>
		///		State change for when the player respawns
		/// </summary>
		override public void Respawn()
		{

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
			this.Context.SetState(PlayerState.Standby);
		}

		/// <summary>
		///		Gets called when the Player changes state
		/// </summary>
		public override void OnContextChange()
		{
			
		}
	}
}
