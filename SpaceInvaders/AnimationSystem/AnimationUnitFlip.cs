using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;

namespace SpaceInvaders.AnimationSystem
{
	class AnimationUnitFlip : AnimationUnit
	{
		private float xFlipFactor;
		private float yFlipFactor;



		//
		// Constructors
		//

		public AnimationUnitFlip() : base()
		{
			this.xFlipFactor = 1.0f;
			this.yFlipFactor = 1.0f;
		}




		//
		// Methods
		//

		/// <summary>
		///		Change if X and/or Y dimensions should flip in this unit
		/// </summary>
		/// <param name="shouldXFlip"></param>
		/// <param name="shouldYFlip"></param>
		public void SetFlipping(bool shouldXFlip, bool shouldYFlip)
		{
			if (shouldXFlip)
				this.xFlipFactor = -1.0f;
			else
				this.xFlipFactor = 1.0f;

			if (shouldYFlip)
				this.yFlipFactor = -1.0f;
			else
				this.yFlipFactor = 1.0f;
		}




		//
		// Contracts
		//

		/// <summary>
		///		Used for Container searches. NOT IMPLEMENTED!
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return SpriteSystem.Sprite.Name.UNINITIALIZED;
		}

		/// <summary>
		///		Clears the data in the unit
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			this.xFlipFactor = 1.0f;
			this.yFlipFactor = 1.0f;
		}





		//
		// Properties
		//

		/// <summary>
		///		Used by animator to apply flip in X dimension
		/// </summary>
		public float XFlipFactor
		{
			get
			{
				return this.xFlipFactor;
			}
		}
		
		/// <summary>
		///		Used by animator to apply flip in Y dimension
		/// </summary>
		public float YFlipFactor
		{
			get
			{
				return this.yFlipFactor;
			}
		}

		/// <summary>
		///		Will the animator flip in the X dimension?
		/// </summary>
		public bool DoesXFlip
		{
			get
			{
				return this.xFlipFactor == -1.0f;
			}
		}

		/// <summary>
		///		Will the animator flip in the Y dimension?
		/// </summary>
		public bool DoesYFlip
		{
			get
			{
				return this.yFlipFactor == -1.0f;
			}
		}
	}
}
