using Debug = System.Diagnostics.Debug;
using SpaceInvaders.States.PlayerStates;

namespace SpaceInvaders.Strategies.PlayerStrategies
{
	abstract class PlayerStrategy
	{
		private PlayerState state;


		//
		// Constructors
		//

		private PlayerStrategy()
		{
			Debug.Assert(false, "Default Constructor for PlayerStrategy not allowed!");
		}

		public PlayerStrategy(PlayerState newState)
		{
			this.state = newState;
		}



		//
		// Contracts
		//

		/// <summary>
		///		Does the method defined by the strategy
		/// </summary>
		abstract public void PerformStrategy();



		//
		// Properties
		//

		/// <summary>
		///		The assigned state for this strategy. Read-only
		/// </summary>
		public PlayerState State
		{
			get
			{
				return this.state;
			}
		}
	}
}
