using Debug = System.Diagnostics.Debug;
using SpaceInvaders.States.PlayerStates;
using SpaceInvaders.InputSystem;

namespace SpaceInvaders.Strategies.PlayerStrategies
{
	class PlayerStrategyMoveLeft : PlayerStrategy
	{
		//
		// Constructors
		//

		private PlayerStrategyMoveLeft() : base(null)
		{
			// Not allowed
		}

		public PlayerStrategyMoveLeft(PlayerState newState)
			: base(newState)
		{
			
		}



		//
		// Contracts
		//

		/// <summary>
		///		Move left
		/// </summary>
		public override void PerformStrategy()
		{
			if (Input.GetKey(Azul.AZUL_KEYS.KEY_LEFT))
			{
				this.State.Context.SetPosition(
					this.State.Context.X - this.State.Context.Speed,
					this.State.Context.Y
				);
			}
		}
	}
}
