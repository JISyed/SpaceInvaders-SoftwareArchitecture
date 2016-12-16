using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.AnimationSystem
{
	sealed class AnimationFlipManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static AnimationFlipManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForAnimationFlip;
			}
		}


		///////////////////////////////////////////////////////
		//
		// Manager Data
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Make a manager WITH preallocation
		/// </summary>
		/// <param name="reserveSize"></param>
		/// <param name="reserveAddition"></param>
		public AnimationFlipManager(int reserveSize, int reserveAddition)
			: base(reserveSize, reserveAddition)
		{
		}


		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Creates a new flipping animator for a SpriteEntity
		/// </summary>
		/// <param name="targetName"></param>
		/// <param name="intervalTimeInSeconds"></param>
		/// <param name="looping"></param>
		/// <returns></returns>
		public AnimationFlip Create(Sprite.Name targetName, float intervalTimeInSeconds, bool looping)
		{
			AnimationFlip newAnimation = this.BaseCreate() as AnimationFlip;
			SpriteEntity newTarget = SpriteEntityManager.Self.Find(targetName) as SpriteEntity;
			newAnimation.SetTarget(newTarget);
			newAnimation.IntervalTime = intervalTimeInSeconds;
			newAnimation.AssignNewIntervalTime(intervalTimeInSeconds);
			newAnimation.ScheduleTimedAnimation(intervalTimeInSeconds, looping);
			return newAnimation;
		}

		/// <summary>
		///		Recycles sprite's flip animator for object pooling
		/// </summary>
		/// <param name="targetName"></param>
		/// <returns></returns>
		public bool Recycle(Sprite.Name targetName)
		{
			AnimationFlip oldAnimation = this.BaseRecycle(targetName) as AnimationFlip;
			if (oldAnimation == null) return false;
			oldAnimation.Reset();
			return true;
		}

		/// <summary>
		///		Finds a flip animation given the name of its sprite target
		/// </summary>
		/// <param name="targetName"></param>
		/// <returns></returns>
		public AnimationFlip Find(Sprite.Name targetName)
		{
			return this.BaseFind(targetName, this.activeList) as AnimationFlip;
		}

		/// <summary>
		///		Convenience method to add parameters to the animator
		/// </summary>
		/// <param name="targetName"></param>
		/// <param name="shouldXFlip"></param>
		/// <param name="shouldYFlip"></param>
		/// <returns></returns>
		public bool Attach(Sprite.Name targetName, bool shouldXFlip, bool shouldYFlip)
		{
			AnimationFlip animation = this.Find(targetName);
			if (animation == null) return false;
			animation.Attach(shouldXFlip, shouldYFlip);
			return true;
		}





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
				AnimationFlip newNode = new AnimationFlip();
				newNode.Preallocate(3, 2);
				this.reservedList.PushFront(newNode);
			}
		}


		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////
	}
}
