using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.TimerSystem;

namespace SpaceInvaders.AnimationSystem
{
	abstract class Animation : Container, ICommandable
	{
		protected AnimationUnit currentUnit;
		protected TimedEvent currentEvent;
		private float intervalTime;
		private float requestedNewIntervalTime;
		private bool isLooping;



		//
		// Constructor
		//

		public Animation() : base()
		{
			this.currentUnit = null;
			this.currentEvent = null;
			this.intervalTime = 1.0f;
			this.requestedNewIntervalTime = this.intervalTime;
			this.isLooping = false;
		}




		//
		// Methods
		//


		/// <summary>
		///		Cancel the timed event created by this animation, if any
		/// </summary>
		/// <returns></returns>
		virtual public bool CancelTimedAnimation()
		{
			// Don't do anything if there is no timed event
			if(this.currentEvent == null)
			{
				return true;				
			}

			// Removed the animation's timed event from the Timer Manager
			bool success = TimedEventManager.Active.Recycle(this.currentEvent);
			this.currentEvent = null;
			return success;
		}


		/// <summary>
		///		Schedule one unit of this animation on the Timer System
		/// </summary>
		/// <param name="secondsFromNow"></param>
		/// <param name="shouldLoop"></param>
		/// <remarks>
		///		Only Null Object should override this method!
		/// </remarks>
		virtual public void ScheduleTimedAnimation(float secondsFromNow, bool shouldLoop)
		{
			this.isLooping = shouldLoop;
			this.currentEvent = TimedEventManager.Active.Create(this, secondsFromNow);
		}

		/// <summary>
		///		Schedule one unit of this animation on the Timer System based on internal intervalTime
		/// </summary>
		virtual public void ScheduleTimedAnimation()
		{
			this.currentEvent = TimedEventManager.Active.Create(this, this.intervalTime);
		}


		/// <summary>
		///		Call this method after attaching animation units
		/// </summary>
		protected void PointToStartingAnimationUnit()
		{
			this.currentUnit = this.activeList.Head as AnimationUnit;
		}

		/// <summary>
		///		Assign a new interval time for the animation.
		///		Animation takes care of changing its internal timing.
		/// </summary>
		/// <param name="newInterval"></param>
		public void AssignNewIntervalTime(float newInterval)
		{
			Debug.Assert(newInterval >= 0.0f, "Attempting to assign a negative interval to an Animation");
			this.requestedNewIntervalTime = newInterval;
		}



		//
		// Virtual Methods
		//

		/// <summary>
		///		Go to the next unit (aka "frame") of the animation
		/// </summary>
		/// <remarks>
		///		Only Null Object should override this method!
		/// </remarks>
		virtual protected void ProgressToNextUnit()
		{
			// Go to the next animation unit
			AnimationUnit oldUnit = this.currentUnit;
			this.currentUnit = this.currentUnit.next as AnimationUnit;
			
			// Change the interval time if requested
			this.intervalTime = this.requestedNewIntervalTime;

			// If there are still animation units left
			if(currentUnit != null)
			{
				// Reschedule
				this.ScheduleTimedAnimation(this.intervalTime, this.isLooping);
			}
			// Else if there are no more units left
			else
			{
				// Reschedule if this animation loops
				if(this.isLooping)
				{
					this.currentUnit = this.activeList.Head as AnimationUnit;
					this.ScheduleTimedAnimation(this.intervalTime, true);
				}
				else
				{
					// Set it back to the previous animation unit
					this.currentUnit = null;
				}
			}
		}

		/// <summary>
		///		Detaches all animation units from the animator.
		///		Also cancels the current animation from the Timer.
		/// </summary>
		virtual public void DetachEverything()
		{
			this.CancelTimedAnimation();
			AnimationUnit itr = this.activeList.Head as AnimationUnit;
			while (this.activeList.Head != null)
			{
				itr.Reset();
				this.activeList.PopFront();
			}
		}




		//
		// Interface Contracts
		//

		/// <summary>
		///		Called by the TimedEvent holding this animation
		/// </summary>
		public void ExecuteCommand()
		{
			this.Animate();
			this.UpdateIntervalTime();
			this.currentEvent = null;
			this.ProgressToNextUnit();
		}




		//
		// Contracts
		//

		/// <summary>
		///		Implements what the animation will actually perform
		/// </summary>
		protected abstract void Animate();

		/// <summary>
		///		If the animation interval changes, decide how here
		/// </summary>
		protected abstract void UpdateIntervalTime();




		//
		// Properties
		//

		/// <summary>
		///		Is the animation looping?
		/// </summary>
		public bool IsLooping
		{
			get
			{
				return this.isLooping;
			}
			set
			{
				this.isLooping = value;
			}
		}

		/// <summary>
		///		In time in seconds between each animation interval
		/// </summary>
		public float IntervalTime
		{
			get
			{
				return this.intervalTime;
			}
			set
			{
				this.intervalTime = value;
			}
		}

		/// <summary>
		///		Returns true the non-looping animation ended
		/// </summary>
		public bool DidAnimationEnd
		{
			get
			{
				return this.currentUnit == null;
			}
		}

		/// <summary>
		///		The current timed event. Read only.
		/// </summary>
		public TimedEvent TimerEvent
		{
			get
			{
				return this.currentEvent;
			}
		}

		
	}
}
