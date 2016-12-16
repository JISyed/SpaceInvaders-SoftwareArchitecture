using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.TextureSystem;

namespace SpaceInvaders.ImageSystem
{
    sealed class Image : ListNode
    {
        private Azul.Rect imageMapping;
		private Texture texture;
        private Image.Name name;
		private float s;
		private float t;
		private float w;
		private float h;
		private bool stChanged;		// Did the image mapping change?
		private bool texChanged;	// Did the texture change?

        //
        // Constructors
        //

        public Image() : base()
        {
            this.imageMapping = new Azul.Rect(0.0f, 0.0f, 0.0f, 0.0f);
			this.texture = null;
			this.name = Image.Name.UNINITIALIZED;
			this.s = 0.0f;
			this.t = 0.0f;
			this.w = 0.0f;
			this.h = 0.0f;
			this.stChanged = false;
			this.texChanged = false;
        }


        //
        // Methods
        //

		/// <summary>
		///		Sets the enum name of the texture. For manager use.
		/// </summary>
		/// <param name="newName"></param>
		public void SetName(Image.Name newName)
        {
            this.name = newName;
        }

		/// <summary>
		///		Sets a new mapping for the image
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
        public void SetImageMapping(float s, float t, float width, float height)
        {
			this.imageMapping.x = s;
			this.imageMapping.y = t;
			this.imageMapping.w = width;
			this.imageMapping.h = height;
			this.s = s;
			this.t = t;
			this.w = width;
			this.h = height;
			this.stChanged = true;
        }

		/// <summary>
		///		Assigns the Texture reference
		/// </summary>
		/// <param name="newTexture"></param>
		public void SetTexture(Texture newTexture)
		{
			this.texture = newTexture;
			this.texChanged = true;
		}

		/// <summary>
		///		Only use this if the object using the mapping has resolved 
		///		changes in ST coordinates or if the texture reference changed
		/// </summary>
		public void DeclareChangeResolved()
		{
			if (this.stChanged)
			{
				this.imageMapping.x = this.s;
				this.imageMapping.y = this.t;
				this.imageMapping.w = this.w;
				this.imageMapping.h = this.h;
				this.stChanged = false;
			}
			if(this.texChanged)
			{
				this.texChanged = false;
			}
		}

        //
        // Contracts
        //

		/// <summary>
		///		Used for AbstractManager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return this.name;
		}

		/// <summary>
		///		Clears the data in the Image
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
        public override void Reset()
        {
            this.imageMapping.x = 0.0f;
			this.imageMapping.y = 0.0f;
			this.imageMapping.w = 0.0f;
			this.imageMapping.h = 0.0f;
			this.texture = null;
			this.name = Image.Name.UNINITIALIZED;
			this.stChanged = false;
			this.texChanged = false;
			this.s = 0.0f;
			this.t = 0.0f;
			this.w = 0.0f;
			this.h = 0.0f;
        }


        //
        // Properties
        //

		/// <summary>
		///		Returns the ST mapping of the image as an Azul Rect
		/// </summary>
		public Azul.Rect ImageMapping
		{
			get
			{
				return this.imageMapping;
			}
		}

		/// <summary>
		///		The horizontal anchor point
		/// </summary>
        public float S
        {
            get 
            {
				return this.s;
            }
			set
			{
				this.s = value;
				this.stChanged = true;
			}
        }

		/// <summary>
		///		The vertical anchor point
		/// </summary>
		public float T
		{
			get
			{
				return this.t;
			}
			set
			{
				this.t = value;
				this.stChanged = true;
			}
		}

		/// <summary>
		///		The width of the mapping in pixels
		/// </summary>
		public float Width
		{
			get
			{
				return this.w;
			}
			set
			{
				this.w = value;
				this.stChanged = true;
			}
		}

		/// <summary>
		///		The height of the mapping in pixels
		/// </summary>
		public float Height
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
				this.stChanged = true;
			}
		}

		/// <summary>
		///		Indicates if the image mapping changed
		/// </summary>
		public bool DidImageChange
		{
			get
			{
				return this.stChanged;
			}
		}

		/// <summary>
		///		Indicates if the texture changed
		/// </summary>
		public bool DidTextureChange
		{
			get
			{
				return this.texChanged;
			}
		}

		/// <summary>
		///		The enum name of the image
		/// </summary>
		public Image.Name ImageName
        {
            get
            {
                return this.name;
            }
        }
		
		/// <summary>
		///		The corresponding texture for the mapping
		/// </summary>
		public Texture Texture
		{
			get
			{
				return this.texture;
			}
		}

		//
		// Nested Enums
		//

		/// <summary>
		///		The possible names of an Image
		/// </summary>
		/// <remarks>
		///		// The letter of each image corresonds to the letter code in "invaders-from-space.tga"
		/// </remarks>
		public enum Name
		{
			UNINITIALIZED,
			AlienCrab_B,
			AlienCrab_C,
			AlienSquid_D,
			AlienSquid_E,
			AlienOctopus_F,
			AlienOctopus_G,
			UFO_V,
			Player_W,
			DeadPlayer1_X,
			DeadPlayer2_S,
			LaserZigzag_Y,
			DeadAlien_Z,
			LaserStraight_Alpha,
			LaserDagger_Beta,
			UFOMissile_Gamma,
			SmallExplosion_Delta,
			Block,
			BlockUL,
			BlockUR,
			BlockBL,
			BlockBR,
			Floor
		}
	}
}
