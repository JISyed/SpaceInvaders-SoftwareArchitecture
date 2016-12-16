using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.AnimationSystem
{
	class AnimationFlip : Animation
	{
		private SpriteEntity spriteTarget;


		//
		// Constructors
		//

		public AnimationFlip() : base()
		{
			this.spriteTarget = null;
		}



		//
		// Methods
		//

		/// <summary>
		///		Sets the new target of the animation
		/// </summary>
		/// <param name="newTarget"></param>
		public void SetTarget(SpriteEntity newTarget)
		{
			this.spriteTarget = newTarget;
		}

		/// <summary>
		///		Adds a boolean flag pair for flipping the sprite
		///		in the X dimension and Y dimension
		/// </summary>
		/// <param name="shouldXFlip"></param>
		/// <param name="shouldYFlip"></param>
		/// <returns></returns>
		public AnimationUnitFlip Attach(bool shouldXFlip, bool shouldYFlip)
		{
			AnimationUnitFlip newUnit = this.BaseAttach(0u) as AnimationUnitFlip;
			newUnit.SetFlipping(shouldXFlip, shouldYFlip);
			this.PointToStartingAnimationUnit();
			return newUnit;
		}


		//
		// Contracts
		//

		/// <summary>
		///		Implements what the animation will actually perform
		/// </summary>
		protected override void Animate()
		{
			// Flip the sprite
			AnimationUnitFlip currUnit = this.currentUnit as AnimationUnitFlip;
			Debug.Assert(currUnit != null);
			this.spriteTarget.ScaleX *= currUnit.XFlipFactor;
			this.spriteTarget.ScaleY *= currUnit.YFlipFactor;
		}

		/// <summary>
		///		If the animation interval changes, decide how here
		/// </summary>
		protected override void UpdateIntervalTime()
		{
			// Does nothing
		}

		// Prefills the pool with the given number of elements
		protected override void FillReserve(int fillSize)
		{
			for (int i = fillSize; i > 0; i--)
			{
				AnimationUnitFlip newNode = new AnimationUnitFlip();
				this.reservedList.PushFront(newNode);
			}
		}

		/// <summary>
		///		Used for AbstractManager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			if (this.spriteTarget == null)
				return Sprite.Name.UNINITIALIZED;
			return this.spriteTarget.SpriteName;
		}

		/// <summary>
		///		Clears the data in the Animation
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			this.spriteTarget = null;
			this.Destroy();
		}



		//
		// Properties
		//

		/// <summary>
		///		The sprite target for this animation
		/// </summary>
		public SpriteEntity Target
		{
			get
			{
				return this.spriteTarget;
			}
		}

		/// <summary>
		///		The name of the sprite being animated
		/// </summary>
		public Sprite.Name SpriteName
		{
			get
			{
				if (this.spriteTarget == null)
					return Sprite.Name.UNINITIALIZED;
				return this.spriteTarget.SpriteName;
			}
		}
	}
}
