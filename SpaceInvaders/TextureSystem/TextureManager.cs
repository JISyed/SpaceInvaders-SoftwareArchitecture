using Enum = System.Enum;

namespace SpaceInvaders.TextureSystem
{
    sealed class TextureManager : AbstractManager
    {
        ///////////////////////////////////////////////////////
        //
        // Singleton stuff
        //
        ///////////////////////////////////////////////////////

        private static TextureManager instance = null;

		/// <summary>
		///		Singleton Instance
		/// </summary>
        public static TextureManager Self
        {
			get
			{
				if (instance == null)
				{
					// Create the manager
					TextureManager.instance = new TextureManager();
				}

				return TextureManager.instance;
			}
        }

        ///////////////////////////////////////////////////////
        //
        // Manager Data
        //
        ///////////////////////////////////////////////////////

        // Private Constructor
        private TextureManager() : base()
        {
        }


        ///////////////////////////////////////////////////////
        //
        // Methods
        //
        ///////////////////////////////////////////////////////

        /// <summary>
        ///     Create a new texture from the object pool
        /// </summary>
        /// <param name="name">Identification of the texture</param>
        /// <param name="fileName">Local file name of the texture</param>
        /// <returns></returns>
		public Texture Create(Texture.Name name, string fileName)
        {
            Texture newNode = this.BaseCreate() as Texture;
            newNode.SetName(name);
            newNode.SetTexture(fileName);
            return newNode;
        }

        /// <summary>
        ///     Removes the given texture to be used again from the pool
        /// </summary>
        /// <param name="name"></param>
        /// <returns> <c>true</c> if a texture could be found </returns>
		public bool Recycle(Texture.Name name)
        {
            Texture oldNode = this.BaseRecycle(name) as Texture;
            if (oldNode == null) return false; 
            oldNode.Reset();
            return true;
        }

        /// <summary>
        ///     Finds the texture node and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
		public Texture Find(Texture.Name name)
        {
            return this.BaseFind(name, this.activeList) as Texture;
        }
        

        ///////////////////////////////////////////////////////
        //
        // Private Methods
        //
        ///////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////
        // 
        // Contracts
        // 
        ///////////////////////////////////////////////////////

        protected override void FillReserve(int fillSize)
        {
            for (int i = fillSize; i > 0; i--)
            {
                Texture newNode = new Texture();
                this.reservedList.PushFront(newNode);
            }
        }

        ///////////////////////////////////////////////////////
        // 
        // Properties
        // 
        ///////////////////////////////////////////////////////
    }
}
