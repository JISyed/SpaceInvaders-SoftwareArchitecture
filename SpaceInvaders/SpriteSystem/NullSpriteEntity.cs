using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ImageSystem;

namespace SpaceInvaders.SpriteSystem
{
	class NullSpriteEntity : SpriteEntity
	{
		//
		// Constructor
		//

		public NullSpriteEntity() : base()
		{
			base.SetName(Sprite.Name.NULL);
		}



		//
		// Overrides
		//

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void SetName(Sprite.Name newName)
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		protected override void SpriteReset()
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void UpdateInternalData()
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override Enum GetName()
		{
			return Sprite.Name.UNINITIALIZED;
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void Reset()
		{
			// Do nothing
		}

		/// <summary>
		///		Warning: Does nothing
		/// </summary>
		public override void Draw()
		{
			// Does nothing
		}
	}
}
