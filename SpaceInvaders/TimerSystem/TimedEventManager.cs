using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.TimerSystem
{
	class TimedEventManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static TimedEventManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForTimedEvent;
			}
		}


		///////////////////////////////////////////////////////
		//
		// Manager Data
		//
		///////////////////////////////////////////////////////


		// Used to track the current time given by the game
		private float currentUpdateTime = 0.0f;
		// Used to identify TimedEvents
		private uint currentId = 0u;
		private bool isPaused = false;

		/// <summary>
		///		Make a manager WITH preallocation
		/// </summary>
		/// <param name="reserveSize"></param>
		/// <param name="reserveAddition"></param>
		public TimedEventManager(int reserveSize, int reserveAddition, float currentAzulTime)
			: base(reserveSize, reserveAddition)
		{
			// Set the Azul time
			this.currentUpdateTime = currentAzulTime;
		}



		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Updates the timed events to see if any need to be executed
		/// </summary>
		/// <param name="currentTime">
		///		Current Azul time of the game
		/// </param>
		public void Update(float currentTime)
		{
			// Only update if not paused
			if (this.isPaused == false)
			{
				// Store current Azul game time
				this.currentUpdateTime = currentTime;

				// Go though every node less than current time
				TimedEvent itr = this.activeList.Head as TimedEvent;
				while (itr != null)
				{
					// If the current event's stamp is less than the currTime
					if (itr.TimeStamp < currentTime)
					{
						// Execute the event's command
						itr.OnTimeIsUp();

						// Next node
						TimedEvent popped = this.activeList.PopFront() as TimedEvent;
						popped.Reset();
						this.reservedList.PushFront(popped);
						itr = this.activeList.Head as TimedEvent;
					}
					// Else if the event stamp is bigger than currTime
					else
					{
						// No need to check anymore
						break;
					}
				}
			}
		}

		/// <summary>
		///		Creates a new timed event given a command and time to execute
		/// </summary>
		/// <param name="newCommand"></param>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public TimedEvent Create(ICommandable newCommand, float seconds)
		{
			Debug.Assert(this.isPaused == false, "Trying to add a TimedEvent to a paused Timer!");

			TimedEvent newEvent = this.NewBaseCreate(seconds);
			newEvent.SetCommand(newCommand);
			return newEvent;
		}

		/// <summary>
		///		Recycles the given TimedEvent reference for object pooling
		/// </summary>
		/// <param name="oldTimedEvent"></param>
		/// <returns></returns>
		public bool Recycle(TimedEvent oldTimedEvent)
		{
			TimedEvent oldNode = this.NewBaseRecycle(oldTimedEvent);
			if(oldNode == null)   return false;
			oldNode.Reset();
			return true;
		}

		/// <summary>
		///		Pause this Timer
		/// </summary>
		public void Pause(float currentAzulTime)
		{
			this.isPaused = true;
			this.currentUpdateTime = currentAzulTime;
		}

		/// <summary>
		///		Unpaused this timer by updating all internal events
		///		given the correct current Azul time
		/// </summary>
		/// <param name="correctAzulTime"></param>
		public void Unpause(float correctAzulTime)
		{
			this.isPaused = false;

			// Calculate how much time elapsed since pausing
			float timeElapsed = correctAzulTime - this.currentUpdateTime;

			// Go through every TimedEvent and add this elapsed time
			TimedEvent itr = this.activeList.Head as TimedEvent;
			while(itr != null)
			{
				// Update the event
				itr.AddTime(timeElapsed);

				// Next event
				itr = itr.next as TimedEvent;
			}
		}




		///////////////////////////////////////////////////////
		//
		// Private Methods
		//
		///////////////////////////////////////////////////////


		// Bypass to AbstractManager's BaseCreate()
		// So that the new node is sorted in the active list
		private TimedEvent NewBaseCreate(float seconds)
		{
			// Refill reserves if it ran out
			if (this.ReservedSize <= 0)
			{
				this.FillReserve(this.AdditionalReserveSize);
				Debug.Assert(this.ReservedSize > 0);
			}

			// Move a node from the reserves to the active list
			TimedEvent newNode = this.reservedList.PopFront() as TimedEvent;
			newNode.SetId(this.currentId++);
			newNode.AddTime(seconds, this.currentUpdateTime);
			this.activeList.PushSorted(newNode);
			return newNode as TimedEvent;
		}

		// Bypass to AbstractManager's BaseFind()
		private TimedEvent NewBaseFind(TimedEvent query, LinkList list)
		{
			ListNode foundNode = null;

			ListNode itr = list.Head;
			while (itr != null)
			{
				if (ReferenceEquals(itr, query))
				{
					// Found node
					foundNode = itr;
					break;
				}

				// Next node
				itr = itr.next;
			}

			return foundNode as TimedEvent;
		}

		// Bypass to AbstractManager's BaseRecycle()
		private TimedEvent NewBaseRecycle(TimedEvent oldEvent)
		{
			// Look for the node to recycle
			ListNode queriedNode = this.NewBaseFind(oldEvent, this.activeList);

			// Give up if node wasn't found
			if (queriedNode == null) return null;

			// Clear data
			queriedNode.BaseReset();

			// Move it from the active to reserve
			queriedNode = this.activeList.Pop(queriedNode);
			this.reservedList.PushFront(queriedNode);

			return queriedNode as TimedEvent;
		}





		///////////////////////////////////////////////////////
		// 
		// Contracts
		// 
		///////////////////////////////////////////////////////

		protected override void FillReserve(int fillSize)
		{
			for (int i = fillSize; i > 0; i--)
			{
				TimedEvent newNode = new TimedEvent();
				this.reservedList.PushFront(newNode);
			}
		}



		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///		Get's the current time of the Azul Game
		/// </summary>
		public float LastTime
		{
			get
			{
				return this.currentUpdateTime;
			}
		}

		/// <summary>
		///		Is this Timer paused?
		/// </summary>
		public bool IsPaused
		{
			get
			{
				return this.isPaused;
			}
		}
	}
}
