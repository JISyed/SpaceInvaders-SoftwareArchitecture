using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.SpriteSystem
{
	class SpriteCollision : Sprite
	{
		private RectDrawable rect;


		//
        // Constructors
        //

		public SpriteCollision() : base()
        {
			this.rect = new RectDrawable(10, 10, 5, 5);	// Random data
        }



		//
		// Methods
		//

		/// <summary>
		///		Set the raw rectangle data for this sprite (in pixels)
		/// </summary>
		/// <param name="newX"></param>
		/// <param name="newY"></param>
		/// <param name="newW"></param>
		/// <param name="newH"></param>
		public void SetRectData(float newX, float newY, float newW, float newH)
		{
			this.X = newX;
			this.Y = newY;
			this.rect.SetDimensions(newW, newH);
		}

		/// <summary>
		///		Set the raw rectangle data for this sprite
		/// </summary>
		/// <param name="newRect"></param>
		public void SetRectData(Azul.Rect newRect)
		{
			this.SetRectData(newRect.x, newRect.y, newRect.w, newRect.h);
		}

		/// <summary>
		///		Resizes this sprite so that it's the same size as the given SpriteEntity
		/// </summary>
		/// <param name="sprite"></param>
		public void ResizeToSprite(SpriteEntity sprite)
		{
			this.rect.SetDimensions(sprite.ImageMap.Width * sprite.ScaleX,
									sprite.ImageMap.Height * sprite.ScaleY);
		}

		/// <summary>
		///		Set the new width and height of the sprite (in pixels)
		/// </summary>
		/// <param name="newW"></param>
		/// <param name="newH"></param>
		public void SetDimensions(float newW, float newH)
		{
			this.rect.SetDimensions(newW, newH);
		}

		





		//
		// Private Methods
		//




		//
		// Contracts
		//

		/// <summary>
		///		Updates all local data into RectDrawable
		///		Contrary to its name, this method is called in the Draw Loop
		/// </summary>
		public override void UpdateInternalData()
		{
			this.rect.SetPosition(this.X, this.Y);
			this.rect.LineColor = this.Color;
		}

		/// <summary>
		///		Draws the individual sprite
		/// </summary>
		public override void Draw()
		{
			this.rect.Draw();
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
			this.SpriteReset();
		}




		//
		// Properties
		//

		/// <summary>
		///		The width of the sprite in pixels
		/// </summary>
		public float Width
		{
			get
			{
				return this.rect.W;
			}
		}

		/// <summary>
		///		The height of the sprite in pixels
		/// </summary>
		public float Height
		{
			get
			{
				return this.rect.H;
			}
		}
	}
}
