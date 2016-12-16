using Debug = System.Diagnostics.Debug;
using SpaceInvaders.States.PlayerStates;
using SpaceInvaders.InputSystem;

namespace SpaceInvaders.Strategies.PlayerStrategies
{
	class PlayerStrategyMoveRight : PlayerStrategy
	{
		//
		// Constructors
		//

		private PlayerStrategyMoveRight() : base(null)
		{
			// Not allowed
		}

		public PlayerStrategyMoveRight(PlayerState newState)
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
			if (Input.GetKey(Azul.AZUL_KEYS.KEY_RIGHT))
			{
				this.State.Context.SetPosition(
					this.State.Context.X + this.State.Context.Speed,
					this.State.Context.Y
				);
			}
		}
	}
}
