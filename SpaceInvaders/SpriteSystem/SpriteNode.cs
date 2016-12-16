using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ImageSystem;

namespace SpaceInvaders.SpriteSystem
{
    class SpriteEntity : Sprite
    {
        private Azul.Sprite2D sprite;
        private Image imageMapping;
		private float scaleX;
		private float scaleY;
		private float angle;



        //
        // Constructors
        //

        public SpriteEntity() : base()
        {
            this.imageMapping = null;
            this.sprite = null;
			this.scaleX = 1.0f;
			this.scaleY = 1.0f;
			this.angle = 0.0f;
        }
		


        //
        // Methods
        //

		/// <summary>
		///		Changes the Azul Sprite reference
		/// </summary>
		/// <param name="newSprite"></param>
        public void SetSprite(Azul.Sprite2D newSprite)
        {
            this.sprite = newSprite;
        }

		/// <summary>
		///		Changes the image ST mapping of the sprite
		/// </summary>
		/// <param name="newMapping"></param>
        public void SetMapping(Image newMapping)
        {
            this.imageMapping = newMapping;
			if(this.sprite != null)
			{
				this.sprite.swapImage(newMapping.ImageMapping);
			}
        }
		


		//
		// Private Methods
		//

		// Update the image mapping and texture if they changed
		private void UpdateImageAndTexture()
		{
			// Check if the image changed
			if (imageMapping.DidImageChange)
			{
				this.imageMapping.DeclareChangeResolved();
				this.sprite.swapImage(this.imageMapping.ImageMapping);
			}
		}



        //
        // Contracts
        //

		/// <summary>
		///		Updates all local sprite data into Azul Sprite data.
		///		Contrary to its name, this method is called in the Draw Loop
		/// </summary>
		public override void UpdateInternalData()
		{
			this.UpdateImageAndTexture();

			this.sprite.x = this.X;
			this.sprite.y = this.Y;
			this.sprite.sx = this.scaleX;
			this.sprite.sy = this.scaleY;
			this.sprite.angle = this.angle;
			this.sprite.setColor(this.Color);
		}

		/// <summary>
		///		Draws the individual sprite entity
		/// </summary>
		public override void Draw()
		{
			this.sprite.Draw();
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
            this.sprite = null;
            this.imageMapping = null;
			this.scaleX = 1.0f;
			this.scaleY = 1.0f;
			this.angle = 0.0f;
			this.SpriteReset();
        }




        //
        // Properties
        //

		/// <summary>
		///		The Azul Sprite data
		/// </summary>
        public Azul.Sprite2D AzulSprite
        {
            get
            {
                return this.sprite;
            }
        }

		/// <summary>
		///		The normalized x scaling of the sprite
		/// </summary>
		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		/// <summary>
		///		The normalized y scaling of the sprite
		/// </summary>
		public float ScaleY
		{
			get
			{
				return this.scaleY;
			}
			set
			{
				this.scaleY = value;
			}
		}

		/// <summary>
		///		The rotation of the sprite
		/// </summary>
		public float Angle
		{
			get
			{
				return this.angle;
			}
			set
			{
				this.angle = value;
			}
		}

		/// <summary>
		///		The ST image mapping of the sprite
		/// </summary>
		public Image ImageMap
		{
			get
			{
				return this.imageMapping;
			}
		}
    }
}
