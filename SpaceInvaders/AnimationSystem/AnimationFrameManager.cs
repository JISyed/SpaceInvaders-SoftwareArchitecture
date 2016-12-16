using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.ImageSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.AnimationSystem
{
	sealed class AnimationFrameManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static AnimationFrameManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForAnimationFrame;
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
		public AnimationFrameManager(int reserveSize, int reserveAddition)
			: base(reserveSize, reserveAddition)
		{
		}



		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////


		/// <summary>
		///		Creates a new frame animation for a SpriteEntity
		/// </summary>
		/// <param name="targetName">
		///		The name of the sprite to be animated. A corresponding "animator" will be created for it.
		/// </param>
		/// <param name="intervalTimeInSeconds"></param>
		/// <param name="looping"></param>
		/// <returns></returns>
		public AnimationFrame Create(Sprite.Name targetName, float intervalTimeInSeconds, bool looping)
		{
			AnimationFrame newAnimation = this.BaseCreate() as AnimationFrame;
			SpriteEntity newTarget = SpriteEntityManager.Self.Find(targetName) as SpriteEntity;
			newAnimation.SetTarget(newTarget);
			newAnimation.IntervalTime = intervalTimeInSeconds;
			newAnimation.AssignNewIntervalTime(intervalTimeInSeconds);
			newAnimation.ScheduleTimedAnimation(intervalTimeInSeconds, looping);
			return newAnimation;
		}

		/// <summary>
		///		Recycles sprite's frame animator for object pooling
		/// </summary>
		/// <param name="targetName"></param>
		/// <returns></returns>
		public bool Recycle(Sprite.Name targetName)
		{
			AnimationFrame oldAnimation = this.BaseRecycle(targetName) as AnimationFrame;
			if (oldAnimation == null) return false;
			oldAnimation.Reset();
			return true;
		}

		/// <summary>
		///		Finds a frame animation given the name of its sprite target
		/// </summary>
		/// <param name="targetName"></param>
		/// <returns></returns>
		public AnimationFrame Find(Sprite.Name targetName)
		{
			return this.BaseFind(targetName, this.activeList) as AnimationFrame;
		}

		/// <summary>
		///		Convenience method to add an image to the animator
		/// </summary>
		/// <param name="targetName">The name of the sprite target</param>
		/// <param name="imageName">The name of the new image</param>
		/// <returns><c>true</c> if it could find an animator for the target</returns>
		public bool Attach(Sprite.Name targetName, Image.Name imageName)
		{
			AnimationFrame animation = this.Find(targetName);
			if (animation == null) return false;
			Image newImage = ImageManager.Self.Find(imageName) as Image;
			if (newImage == null) return false;
			animation.Attach(newImage);
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
				AnimationFrame newNode = new AnimationFrame();
				newNode.Preallocate(2, 2);
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
