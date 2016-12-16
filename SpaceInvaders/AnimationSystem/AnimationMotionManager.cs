using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.AnimationSystem
{
	sealed class AnimationMotionManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static AnimationMotionManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForAnimationMotion;
			}
		}

		///////////////////////////////////////////////////////
		//
		// Manager Data
		//
		///////////////////////////////////////////////////////

		private NullAnimationMotion nullAnimator = null;

		/// <summary>
		///		Make a manager WITH preallocation
		/// </summary>
		/// <param name="reserveSize"></param>
		/// <param name="reserveAddition"></param>
		public AnimationMotionManager(int reserveSize, int reserveAddition)
			: base(reserveSize, reserveAddition)
		{
			this.nullAnimator = new NullAnimationMotion();
		}




		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Creates a new motion animation for a GameObject. Must pass this into GameObject::SetAnimator()
		/// </summary>
		/// <param name="targetName">
		///		The name of the GameObject to be animated. A corresponding "animator" will be created for it.
		/// </param>
		/// <param name="intervalTimeInSeconds"></param>
		/// <param name="looping"></param>
		/// <returns></returns>
		public AnimationMotion Create(GameObject.Name targetName, uint targetId, float intervalTimeInSeconds, bool looping)
		{
			if (targetName == GameObject.Name.UNINITIALIZED) return nullAnimator;
			AnimationMotion newAnimation = this.BaseCreate(targetId) as AnimationMotion;
			newAnimation.IntervalTime = intervalTimeInSeconds;
			newAnimation.AssignNewIntervalTime(intervalTimeInSeconds);
			newAnimation.IsLooping = looping;
			return newAnimation;
		}

		/// <summary>
		///		Recycles GameObject's motion animator for object pooling
		/// </summary>
		/// <param name="targetName"></param>
		/// <returns></returns>
		public bool Recycle(GameObject.Name targetName, uint targetId)
		{
			if (targetName == GameObject.Name.UNINITIALIZED) return false;
			AnimationMotion oldAnimation = this.BaseRecycle(targetName, targetId) as AnimationMotion;
			if (oldAnimation == null) return false;
			oldAnimation.Reset();
			return true;
		}

		/// <summary>
		///		Finds a motion animation given the name of its GameObject target
		/// </summary>
		/// <param name="targetName"></param>
		/// <returns></returns>
		public AnimationMotion Find(GameObject.Name targetName, uint targetId)
		{
			if (targetName == GameObject.Name.UNINITIALIZED) return nullAnimator;
			return this.BaseFind(targetName, targetId, this.activeList) as AnimationMotion;
		}

		/// <summary>
		///		Convenience method to add an delta X-Y pair to the animator
		/// </summary>
		/// <param name="targetName">The name of the GameObject target</param>
		/// <param name="newDeltaX"></param>
		/// <param name="newDeltaY"></param>
		/// <returns></returns>
		public bool Attach(GameObject.Name targetName, uint targetId, float newDeltaX, float newDeltaY)
		{
			AnimationMotion animation = this.Find(targetName, targetId);
			if (animation == null) return false;
			animation.Attach(newDeltaX, newDeltaY);
			return true;
		}
		
		/*
		/// <summary>
		///		Check if the given motion animator is a null animation 
		/// </summary>
		/// <param name="animatorToCheck"></param>
		/// <returns></returns>
		public bool IsNull(AnimationMotion animatorToCheck)
		{
			return ReferenceEquals(animatorToCheck, AnimationMotionManager.Self.NullAnimation);
		}
		//*/



		///////////////////////////////////////////////////////
		//
		// Private Methods
		//
		///////////////////////////////////////////////////////




		///////////////////////////////////////////////////////
		// 
		// Contracts
		// 
		///////////////////////////////////////////////////////

		protected override void FillReserve(int fillSize)
		{
			for (int i = fillSize; i > 0; i--)
			{
				AnimationMotion newNode = new AnimationMotion();
				newNode.Preallocate(2, 2);
				this.reservedList.PushFront(newNode);
			}
		}


		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///		Returns a motion animation that does nothing
		/// </summary>
		public AnimationMotion NullAnimation
		{
			get
			{
				return this.nullAnimator;
			}
		}
	}
}
