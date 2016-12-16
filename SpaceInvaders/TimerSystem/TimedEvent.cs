using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.TimerSystem
{
	class TimedEvent : ListNode
	{
		private TimedEvent.Name name;
		private ICommandable command;
		private float timeStamp;


		//
		// Constructors
		//

		public TimedEvent() : base()
		{
			this.name = TimedEvent.Name.NoName;
			this.command = null;
			this.timeStamp = 0.0f;
		}



		//
		// Methods
		//

		/// <summary>
		///		Assign a new command
		/// </summary>
		/// <param name="newCommand"></param>
		public void SetCommand(ICommandable newCommand)
		{
			this.command = newCommand;
		}

		/// <summary>
		///		Called only by the TimedEventManager when the event's time is up
		/// </summary>
		public void OnTimeIsUp()
		{
			this.command.ExecuteCommand();
		}

		/// <summary>
		///		For manager use only! Makes it's time stamp the current time.
		/// </summary>
		public void SetTimeStamp(float currentTime)
		{
			this.timeStamp = currentTime;
		}

		/// <summary>
		///		Set the given currentTime to this event plus the additional seconds
		/// </summary>
		/// <param name="currentTime"></param>
		/// <param name="seconds"></param>
		public void AddTime(float seconds, float currentTime)
		{
			Debug.Assert(seconds >= 0.0f, "Attempting to add a timed event with negative time interval!");
			this.timeStamp = currentTime;
			this.timeStamp += seconds;
		}

		/// <summary>
		///		Adds a given number of seconds to the timed event's current time stamp
		/// </summary>
		/// <param name="seconds"></param>
		public void AddTime(float seconds)
		{
			this.timeStamp += seconds;
		}




		//
		// Contacts
		//

		/// <summary>
		///		Resets the timed event. Calls on a concrete command to reset.
		/// </summary>
		public override void Reset()
		{
			this.command = null;
		}

		/// <summary>
		///		Gets the name
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return this.name;
		}




		//
		// Operator overloads
		//

		/// <summary>
		///		If lhs is smaller, it's older than rhs
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator <(TimedEvent lhs, TimedEvent rhs)
		{
			return lhs.timeStamp < rhs.timeStamp;
		}

		/// <summary>
		///		If lhs is bigger, it's newer then rhs
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator >(TimedEvent lhs, TimedEvent rhs)
		{
			return lhs.timeStamp > rhs.timeStamp;
		}


		//
		// Virtual Overloads
		//

		public override bool LessThan(ListNode rhs)
		{
			TimedEvent right = rhs as TimedEvent;
			Debug.Assert(right != null);
			return this.timeStamp < right.timeStamp;
		}

		public override bool GreaterThan(ListNode rhs)
		{
			TimedEvent right = rhs as TimedEvent;
			Debug.Assert(right != null);
			return this.timeStamp > right.timeStamp;
		}


		//
		// Properties
		//

		public float TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}



		//
		// Nested Enums
		//

		/// <summary>
		///		Names are not really important for timed events
		/// </summary>
		public enum Name
		{
			NoName
		}
	}
}
