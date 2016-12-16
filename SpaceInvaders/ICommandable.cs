using SpaceInvaders.TimerSystem;

namespace SpaceInvaders
{
	interface ICommandable
	{
		void ExecuteCommand();

		TimedEvent TimerEvent
		{
			get;
		}
	}
}
