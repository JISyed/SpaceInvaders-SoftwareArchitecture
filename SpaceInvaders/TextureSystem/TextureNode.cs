using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.TextureSystem
{
    sealed class Texture : ListNode
    {
        private Azul.Texture textureMap;
        private Texture.Name name;


        //
        // Constructors
        //

        public Texture() : base()
        {
			this.name = Texture.Name.UNINITIALIZED;
            this.textureMap = null;
        }


        //
        // Methods
        //

		/// <summary>
		///		Sets the enum name of the texture. For manager use.
		/// </summary>
		/// <param name="newName"></param>
		public void SetName(Texture.Name newName)
        {
            this.name = newName;
        }

		/// <summary>
		///		Resets the texture data. For manager use.
		/// </summary>
		/// <remarks>
		///		Dynamically creates a new Azul Texture!
		/// </remarks>
		/// <param name="newTextureFileName"></param>
        public void SetTexture(string newTextureFileName)
        {
            this.textureMap = new Azul.Texture(newTextureFileName);
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
		///		Clears the data in the Texture
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
        public override void Reset()
        {
            this.textureMap = null;
			this.name = Texture.Name.UNINITIALIZED;
        }


        //
        // Properties
        //

		/// <summary>
		///		The raw Azul Texture data
		/// </summary>
        public Azul.Texture TextureMap
        {
            get
            {
                return this.textureMap;
            }
        }

		/// <summary>
		///		The enum name of the texture
		/// </summary>
		public Texture.Name TextureName
        {
            get
            {
                return this.name;
            }
        }

		//
		// Nested Enum
		//

		/// <summary>
		///		The possible names of a Texture
		/// </summary>
		public enum Name
		{
			UNINITIALIZED,
			InvadersFromSpace       // Main texture (invaders-from-space.tga)
		}
	}
}
