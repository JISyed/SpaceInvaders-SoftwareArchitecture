using Debug = System.Diagnostics.Debug;
using SpaceInvaders.States.PlayerStates;

namespace SpaceInvaders.Strategies.PlayerStrategies
{
	class PlayerStrategyNone : PlayerStrategy
	{
		//
		// Constructors
		//

		public PlayerStrategyNone() : base(null)
		{
			
		}


		//
		// Contracts
		//

		public override void PerformStrategy()
		{
			// Do nothing!
		}
	}
}
