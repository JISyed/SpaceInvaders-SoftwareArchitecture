using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.TextureSystem;
using SpaceInvaders.ImageSystem;

namespace SpaceInvaders.SpriteSystem
{
    sealed class SpriteEntityManager : AbstractManager
    {
        ///////////////////////////////////////////////////////
        //
        // Singleton stuff
        //
        ///////////////////////////////////////////////////////

        private static SpriteEntityManager instance = null;
		private NullSpriteEntity nullSprite = null;

		/// <summary>
		///		Singleton Instance
		/// </summary>
        public static SpriteEntityManager Self
        {
			get
			{
				if (instance == null)
				{
					// Create the manager
					SpriteEntityManager.instance = new SpriteEntityManager();
				}

				return SpriteEntityManager.instance;
			}
        }




        ///////////////////////////////////////////////////////
        //
        // Manager Data
        //
        ///////////////////////////////////////////////////////

        // Private Constructor
        private SpriteEntityManager() : base()
        {
			this.nullSprite = new NullSpriteEntity();
        }




        ///////////////////////////////////////////////////////
        //
        // Methods
        //
        ///////////////////////////////////////////////////////

        /// <summary>
		///		Creates the sprite from the memory pool
        /// </summary>
        /// <param name="newName"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
		public SpriteEntity Create(Sprite.Name newName, Image.Name imageName)
        {
			if (newName == Sprite.Name.NULL || newName == Sprite.Name.UNINITIALIZED) 
				return nullSprite;
            Image image = ImageManager.Self.Find(imageName);
            SpriteEntity newSprite = this.BaseCreate() as SpriteEntity;
			newSprite.SetName(newName);
			newSprite.SetMapping(image);
			Azul.Sprite2D sprite = new Azul.Sprite2D(image.Texture.TextureMap,
													 image.ImageMapping,
													 new Azul.Rect(0, 0, image.Width, image.Height)
			);
			newSprite.SetSprite(sprite);
            return newSprite;
        }


        /// <summary>
		///		Removes the given sprite to be used again from the pool
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
		public bool Recycle(Sprite.Name name)
        {
			if (name == Sprite.Name.NULL || name == Sprite.Name.UNINITIALIZED) 
				return false;
            SpriteEntity oldNode = this.BaseRecycle(name) as SpriteEntity;
            if (oldNode == null) return false;
            oldNode.Reset();
            return true;
        }
		

        /// <summary>
		///		Finds a sprite and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
		public SpriteEntity Find(Sprite.Name name)
        {
			if (name == Sprite.Name.NULL || name == Sprite.Name.UNINITIALIZED) 
				return nullSprite;
            return this.BaseFind(name, this.activeList) as SpriteEntity;
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
                SpriteEntity newNode = new SpriteEntity();
                this.reservedList.PushFront(newNode);
            }
        }




        ///////////////////////////////////////////////////////
        // 
        // Properties
        // 
        ///////////////////////////////////////////////////////

		/// <summary>
		///		Returns a sprite that does nothing
		/// </summary>
		public SpriteEntity NullSprite
		{
			get
			{
				return nullSprite;
			}
		}
		
    }
}
