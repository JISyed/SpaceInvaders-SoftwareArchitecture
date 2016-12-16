using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.SpriteSystem
{
	/// <summary>
	///		Serves as a flyweight to SpriteEntity
	/// </summary>
	class SpriteProxy : Sprite
	{
		private SpriteEntity modelSprite;

		//
		// Constructors
		//

		public SpriteProxy() : base()
		{
			this.modelSprite = null;
			this.SetName(Sprite.Name.UNINITIALIZED);
		}


		//
		// Methods
		//

		/// <summary>
		///		Sets the sprite used as a model by the flyweight proxy sprite
		/// </summary>
		/// <param name="newSprite"></param>
		public void SetModelSprite(SpriteEntity newSprite)
		{
			if(this.SpriteName == Sprite.Name.NULL)
			{
				// Do nothing
				return;
			}
			this.modelSprite = newSprite;
			this.SetName(newSprite.SpriteName);
		}


		//
		// Private Methods
		//




		//
		// Contracts
		//

		/// <summary>
		///		Maps the extrinsic flyweight data to the model sprite entity
		/// </summary>
		public override void UpdateInternalData()
		{
			this.modelSprite.X = this.X;
			this.modelSprite.Y = this.Y;
			this.modelSprite.Color = this.Color;
		}

		/// <summary>
		///		Draws the model sprite entity. Call UpdateInternalData() first.
		/// </summary>
		public override void Draw()
		{
			this.modelSprite.UpdateInternalData();
			this.modelSprite.Draw();
		}

		/// <summary>
		///		Used for AbstractManager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return this.SpriteName;
		}

		/// <summary>
		///		Clears the data in the Sprite
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			this.modelSprite = null;
			this.SpriteReset();
		}


		//
		// Properties
		//

		/// <summary>
		///		The sprite used as a model by the flyweight proxy sprite
		/// </summary>
		public SpriteEntity ModelSprite
		{
			get
			{
				return this.modelSprite;
			}
		}
	}
}
