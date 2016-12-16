using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.TimerSystem;

namespace SpaceInvaders
{
	class PrintStringCommand : ICommandable
	{
		private string statement;
		private TimedEvent timedEvent;

		public PrintStringCommand(string newString, TimedEvent newEvent)
		{
			this.statement = newString;
			this.timedEvent = newEvent;
		}

		private PrintStringCommand()
		{
		}


		//
		// Interface Contract
		//

		public void ExecuteCommand()
		{
			Console.WriteLine(this.statement);
		}

		public TimedEvent TimerEvent
		{
			get
			{
				return this.timedEvent;
			}
		}


		//
		// Properties
		//

		public string StringStatement
		{
			get
			{
				return this.statement;
			}
		}
	}
}
