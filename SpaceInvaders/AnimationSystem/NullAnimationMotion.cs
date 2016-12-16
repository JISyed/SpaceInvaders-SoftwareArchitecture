using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.GameObjectSystem;

namespace SpaceInvaders.AnimationSystem
{
	class NullAnimationMotion : AnimationMotion
	{
		//
		// Constructors
		//

		public NullAnimationMotion() : base()
		{
		}



		//
		// Overrides
		//

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void ScheduleTimedAnimation()
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void ScheduleTimedAnimation(float secondsFromNow, bool shouldLoop)
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override bool CancelTimedAnimation()
		{
			// Do nothing
			return true;
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		protected override void ProgressToNextUnit()
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void SetTarget(GameObject newTarget)
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override AnimationUnitMotion Attach(float newDeltaX, float newDeltaY)
		{
			// Do nothing
			return null;
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void DetachEverything()
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		protected override void Animate()
		{
			// Don't do anything
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		protected override void UpdateIntervalTime()
		{
			// Don't do anything
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override Enum GetName()
		{
			return GameObject.Name.UNINITIALIZED;
		}

		protected override void FillReserve(int fillSize)
		{
			// "fillSize" not used. Just allocate space for one unit
			AnimationUnitMotion newNode = new AnimationUnitMotion();
			this.reservedList.PushFront(newNode);
			base.Attach(0.0f, 0.0f);
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void Reset()
		{
			// Do nothing
		}
	}
}
