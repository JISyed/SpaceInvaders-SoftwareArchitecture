using Debug = System.Diagnostics.Debug;
using SpaceInvaders.Strategies.PlayerStrategies;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.AudioSystem;
using PlayerShip = SpaceInvaders.GameObjectSystem.GameObjects.PlayerShip;

namespace SpaceInvaders.States.PlayerStates
{
	class PlayerStateStandby : PlayerState
	{
		//
		// Constructors
		//

		private PlayerStateStandby() : base(null)
		{
			// Not allowed
		}

		public PlayerStateStandby(PlayerShip newContext)
			: base(newContext)
		{
			this.strMoveLeft = new PlayerStrategyNone();
			this.strMoveRight = new PlayerStrategyNone();
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

		}

		/// <summary>
		///		State change for when the game begins
		/// </summary>
		override public void Begin()
		{
			this.Context.SetState(PlayerState.Active);
		}

		/// <summary>
		///		State change for when the game restarts
		/// </summary>
		override public void Restart()
		{

		}

		/// <summary>
		///		Gets called when the Player changes state
		/// </summary>
		public override void OnContextChange()
		{

		}
	}
}
