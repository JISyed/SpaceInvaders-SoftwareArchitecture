using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.GameObjectSystem;

namespace SpaceInvaders.AnimationSystem
{
	class AnimationMotion : Animation
	{
		private GameObject goTarget;
		private uint goId;

		//
		// Constructors
		//

		public AnimationMotion() : base()
		{
			this.goTarget = null;
			goId = 0u;
		}



		//
		// Methods
		//

		/// <summary>
		///		Sets the new target of the animation
		/// </summary>
		/// <param name="newTarget"></param>
		virtual public void SetTarget(GameObject newTarget)
		{
			this.goTarget = newTarget;
			this.goId = newTarget.Id;
		}

		/// <summary>
		///		Attaches a delta X-Y pair to the motion animation
		/// </summary>
		/// <param name="newDeltaX"></param>
		/// <param name="newDeltaY"></param>
		/// <returns></returns>
		virtual public AnimationUnitMotion Attach(float newDeltaX, float newDeltaY)
		{
			AnimationUnitMotion newUnit = this.BaseAttach(0u) as AnimationUnitMotion;
			newUnit.SetDeltas(newDeltaX, newDeltaY);
			this.PointToStartingAnimationUnit();
			return newUnit;
		}
		
		/// <summary>
		///		Change the X-motion of the animation
		/// </summary>
		/// <param name="newXChange"></param>
		public void AssignNewXChange(float newXChange)
		{
			AnimationUnitMotion itr = this.activeList.Head as AnimationUnitMotion;

			while(itr != null)
			{
				itr.SetDeltaX(newXChange);

				itr = itr.next as AnimationUnitMotion;
			}
		}



		//
		// Private Methods
		//






		//
		// Contracts
		//

		/// <summary>
		///		Implements what the animation will actually perform
		/// </summary>
		protected override void Animate()
		{
			AnimationUnitMotion currUnit = this.currentUnit as AnimationUnitMotion;
			// If this asset goes off, you probably forgot to attach animation units
			Debug.Assert(currUnit != null);
			this.goTarget.SetPosition(goTarget.X + currUnit.DeltaX, goTarget.Y + currUnit.DeltaY);
		}

		/// <summary>
		///		If the animation interval changes, decide how here
		/// </summary>
		protected override void UpdateIntervalTime()
		{
			// So far does nothing
		}

		/// <summary>
		///		Prefills the pool with the given number of elements
		/// </summary>
		/// <param name="fillSize"></param>
		protected override void FillReserve(int fillSize)
		{
			for (int i = fillSize; i > 0; i--)
			{
				AnimationUnitMotion newNode = new AnimationUnitMotion();
				this.reservedList.PushFront(newNode);
			}
		}

		/// <summary>
		///		Used for AbstractManager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			if (this.goTarget == null)
				return GameObject.Name.UNINITIALIZED;
			return this.goTarget.ObjectName;
		}

		/// <summary>
		///		Clears the data in the Animation
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			this.goTarget = null;
			this.goId = 0u;
			this.Destroy();
		}



		//
		// Properties
		//

		/// <summary>
		///		The GameObject target for this animation
		/// </summary>
		public GameObject Target
		{
			get
			{
				return this.goTarget;
			}
		}

		/// <summary>
		///		The name of the GameObject being animated
		/// </summary>
		public GameObject.Name ObjectName
		{
			get
			{
				if (this.goTarget == null)
					return GameObject.Name.UNINITIALIZED;
				return this.goTarget.ObjectName;
			}
		}

		/// <summary>
		///		The ID of the GameObject being animated
		/// </summary>
		public uint ObjectID
		{
			get
			{
				return this.goId;
			}
		}
	}
}
