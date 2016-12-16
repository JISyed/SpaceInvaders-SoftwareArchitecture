using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.SpriteSystem
{
	/// <summary>
	///		Serves as a flyweight to SpriteCollision
	/// </summary>
	class SpriteCollisionProxy : Sprite
	{
		public SpriteCollision modelSprite;
		public float width;
		public float height;

		//
        // Constructors
        //

		public SpriteCollisionProxy() : base()
        {
			this.modelSprite = null;
			this.width = 5;
			this.height = 5;
        }




		//
		// Methods
		//

		/// <summary>
		///		Sets the sprite used as a model by the flyweight proxy sprite
		/// </summary>
		/// <param name="newSprite"></param>
		public void SetModelSprite(SpriteCollision newSprite)
		{
			this.modelSprite = newSprite;
			this.SetName(newSprite.SpriteName);
		}

		/// <summary>
		///		Resizes the model CollisionSprite
		/// </summary>
		/// <param name="newWidth"></param>
		/// <param name="newHeight"></param>
		public void Resize(float newWidth, float newHeight)
		{
			if(this.SpriteName == Name.NULL)
			{
				this.width = newWidth;
				this.height = newHeight;
			}
			else
			{
				this.modelSprite.SetDimensions(newWidth, newHeight);
			}
			
		}




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
			if (this.SpriteName == Name.NULL)
				this.modelSprite.SetDimensions(this.width, this.height);
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
			//this.SpriteReset();
		}

		



		//
		// Properties
		//

		/// <summary>
		///		The sprite used as a model by the flyweight proxy sprite
		/// </summary>
		public SpriteCollision ModelSprite
		{
			get
			{
				return this.modelSprite;
			}
		}
	}
}
