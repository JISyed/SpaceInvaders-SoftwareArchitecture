using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.ImageSystem;

namespace SpaceInvaders.AnimationSystem
{
	sealed class AnimationUnitFrame : AnimationUnit
	{
		private Image imageReference;

		//
		// Constructors
		//

		public AnimationUnitFrame() : base()
		{
			this.imageReference = null;
		}



		//
		// Methods
		//

		/// <summary>
		///		Sets the image reference of this animation unit
		/// </summary>
		/// <param name="newImage"></param>
		public void SetImage(Image newImage)
		{
			this.imageReference = newImage;
		}



		//
		// Contracts
		//

		/// <summary>
		///		Used for Container searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			if (this.imageReference == null)
				return Image.Name.UNINITIALIZED;
			return this.imageReference.ImageName;
		}

		/// <summary>
		///		Clears the data in the unit
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			this.imageReference = null;
		}

		


		//
		// Properites
		//

		/// <summary>
		///		The image referenced by this animation frame
		/// </summary>
		public Image ImageReference
		{
			get
			{
				return this.imageReference;
			}
		}
	}
}
