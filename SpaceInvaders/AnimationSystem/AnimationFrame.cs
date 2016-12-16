using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.ImageSystem;

namespace SpaceInvaders.AnimationSystem
{
	class AnimationFrame : Animation
	{
		private SpriteEntity spriteTarget;

		//
		// Constructors
		//

		public AnimationFrame() : base()
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
		///		Attaches an Image to the frame animation
		/// </summary>
		/// <param name="imageName"></param>
		/// <returns></returns>
		public AnimationUnitFrame Attach(Image.Name imageName)
		{
			Image newImage = ImageManager.Self.Find(imageName);
			AnimationUnitFrame newUnit = this.BaseAttach(0u) as AnimationUnitFrame;
			newUnit.SetImage(newImage);
			this.PointToStartingAnimationUnit();
			return newUnit;
		}

		/// <summary>
		///		Attaches an Image to the frame animation
		/// </summary>
		/// <param name="newImage"></param>
		/// <returns></returns>
		public AnimationUnitFrame Attach(Image newImage)
		{
			AnimationUnitFrame newUnit = this.BaseAttach(0u) as AnimationUnitFrame;
			newUnit.SetImage(newImage);
			this.PointToStartingAnimationUnit();
			return newUnit;
		}

		/// <summary>
		///		Removes an Image from the frame animation
		/// </summary>
		/// <param name="oldName"></param>
		/// <returns></returns>
		public bool Detach(Image.Name oldName)
		{
			AnimationUnitFrame oldUnit = this.BaseDetach(oldName, 0u) as AnimationUnitFrame;
			if (oldUnit == null) return false;
			oldUnit.Reset();
			return true;
		}

		/// <summary>
		///		Searches for an AnimationUnit given the Image's name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public AnimationUnitFrame Find(Image.Name name)
		{
			return this.BaseFind(name, 0u, this.activeList) as AnimationUnitFrame;
		}




		//
		// Contracts
		//

		/// <summary>
		///		Implements what the animation will actually perform
		/// </summary>
		protected override void Animate()
		{
			// Change the SpriteEntity's Image
			AnimationUnitFrame currUnit = this.currentUnit as AnimationUnitFrame;
			Debug.Assert(currUnit != null);
			this.spriteTarget.SetMapping(currUnit.ImageReference);
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
				AnimationUnitFrame newNode = new AnimationUnitFrame();
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
