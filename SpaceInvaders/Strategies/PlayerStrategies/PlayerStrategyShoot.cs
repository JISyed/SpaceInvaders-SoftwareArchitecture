using Debug = System.Diagnostics.Debug;
using SpaceInvaders.States.PlayerStates;
using SpaceInvaders.InputSystem;
using PlayerLaser = SpaceInvaders.GameObjectSystem.GameObjects.PlayerLaser;

namespace SpaceInvaders.Strategies.PlayerStrategies
{
	class PlayerStrategyShoot : PlayerStrategy
	{
		//
		// Constructors
		//

		private PlayerStrategyShoot() : base(null)
		{
			// Not allowed
		}

		public PlayerStrategyShoot(PlayerState newState)
			: base(newState)
		{
			
		}



		//
		// Contracts
		//

		/// <summary>
		///		Move right
		/// </summary>
		public override void PerformStrategy()
		{
			if (Input.GetKeyDown(Azul.AZUL_KEYS.KEY_SPACE))
			{
				PlayerLaser laser = new PlayerLaser(
					this.State.Context.X, 
					this.State.Context.Y
				);

				this.State.Context.SetState(PlayerState.NoAmmo);
			}
		}
	}
}
