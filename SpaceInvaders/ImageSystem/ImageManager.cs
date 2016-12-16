using Enum = System.Enum;

using SpaceInvaders.TextureSystem;

namespace SpaceInvaders.ImageSystem
{
    sealed class ImageManager : AbstractManager
    {
        ///////////////////////////////////////////////////////
        //
        // Singleton stuff
        //
        ///////////////////////////////////////////////////////

        private static ImageManager instance = null;

		/// <summary>
		///		Singleton Instance
		/// </summary>
        public static ImageManager Self
        {
			get
			{
				if (instance == null)
				{
					// Create the manager
					ImageManager.instance = new ImageManager();
				}

				return ImageManager.instance;
			}
        }

        ///////////////////////////////////////////////////////
        //
        // Manager Data
        //
        ///////////////////////////////////////////////////////

        // Private Constructor
        private ImageManager() : base()
        {
        }

        ///////////////////////////////////////////////////////
        //
        // Methods
        //
        ///////////////////////////////////////////////////////

        /// <summary>
		///		Create a new image map from the object pool
        /// </summary>
        /// <param name="newName"></param>
        /// <param name="textureName"></param>
        /// <returns></returns>
		public Image Create(Image.Name newName, Texture.Name textureName)
        {
            Azul.Rect stMap = this.LookupImageMapping(newName);
            Image newImage = this.BaseCreate() as Image;
            newImage.SetName(newName);
			newImage.SetImageMapping(stMap.x, stMap.y, stMap.w, stMap.h);
			newImage.SetTexture(TextureManager.Self.Find(textureName));
            return newImage;
        }

		/// <summary>
		///		Create a new image map from the object pool
		/// </summary>
		/// <param name="newName"></param>
		/// <param name="texName"></param>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public Image Create(Image.Name newName, Texture.Name texName, float s, float t, float width, float height)
		{
			Image newImage = this.BaseCreate() as Image;
			newImage.SetName(newName);
			newImage.SetImageMapping(s, t, width, height);
			newImage.SetTexture(TextureManager.Self.Find(texName));
			return newImage;
		}

        /// <summary>
		///		Removes the given image to be used again from the pool
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
		public bool Recycle(Image.Name name)
        {
            Image oldNode = this.BaseRecycle(name) as Image;
            if (oldNode == null) return false;
            oldNode.Reset();
            return true;
        }

        /// <summary>
		///		Finds the image node and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

		public Image Find(Image.Name name)
        {
            return this.BaseFind(name, this.activeList) as Image;
        }


        ///////////////////////////////////////////////////////
        //
        // Private Methods
        //
        ///////////////////////////////////////////////////////

        // Returns a image mapping given the image's enum name
		private Azul.Rect LookupImageMapping(Image.Name name)
        {
            switch (name)
            {
				case Image.Name.AlienCrab_B:
                    return new Azul.Rect(136, 65, 85, 62);
				case Image.Name.AlienCrab_C:
                    return new Azul.Rect(253, 65, 85, 62);
				case Image.Name.AlienSquid_D:
                    return new Azul.Rect(370, 65, 62, 62);
				case Image.Name.AlienSquid_E:
                    return new Azul.Rect(465, 65, 62, 62);
				case Image.Name.AlienOctopus_F:
                    return new Azul.Rect(559, 65, 93, 62);
				case Image.Name.AlienOctopus_G:
                    return new Azul.Rect(27, 202, 93, 62);
				case Image.Name.UFO_V:
                    return new Azul.Rect(122, 491, 95, 44);
				case Image.Name.Player_W:
                    return new Azul.Rect(243, 494, 57, 40);
				case Image.Name.DeadPlayer1_X:
                    return new Azul.Rect(329, 483, 82, 55);
				case Image.Name.LaserZigzag_Y:
                    return new Azul.Rect(438, 491, 22, 39);
				case Image.Name.DeadAlien_Z:
                    return new Azul.Rect(490, 489, 73, 44);
                default:
                    return null;
            }

            // Old look up table so I don't have to write it again
            //invadersSpriteMapping = new Dictionary<string, Azul.Rect>();
            //invadersSpriteMapping.Add("A", new Azul.Rect(27, 81, 80, 36));
            //invadersSpriteMapping.Add("B", new Azul.Rect(136, 65, 85, 62));
            //invadersSpriteMapping.Add("C", new Azul.Rect(253, 65, 85, 62));
            //invadersSpriteMapping.Add("D", new Azul.Rect(370, 65, 62, 62));
            //invadersSpriteMapping.Add("E", new Azul.Rect(465, 65, 62, 62));
            //invadersSpriteMapping.Add("F", new Azul.Rect(559, 65, 93, 62));
            //invadersSpriteMapping.Add("G", new Azul.Rect(27, 202, 93, 62));
            //invadersSpriteMapping.Add("H", new Azul.Rect(153, 202, 78, 62));
            //invadersSpriteMapping.Add("I", new Azul.Rect(263, 209, 78, 54));
            //invadersSpriteMapping.Add("J", new Azul.Rect(373, 202, 54, 61));
            //invadersSpriteMapping.Add("K", new Azul.Rect(459, 202, 54, 61));
            //invadersSpriteMapping.Add("L", new Azul.Rect(546, 202, 69, 61));
            //invadersSpriteMapping.Add("M", new Azul.Rect(647, 202, 69, 61));
            //invadersSpriteMapping.Add("N", new Azul.Rect(27, 339, 77, 61));
            //invadersSpriteMapping.Add("O", new Azul.Rect(138, 339, 77, 61));
            //invadersSpriteMapping.Add("P", new Azul.Rect(248, 339, 61, 61));
            //invadersSpriteMapping.Add("Q", new Azul.Rect(342, 339, 62, 61));
            //invadersSpriteMapping.Add("R", new Azul.Rect(437, 354, 54, 46));
            //invadersSpriteMapping.Add("S", new Azul.Rect(523, 346, 69, 54));
            //invadersSpriteMapping.Add("T", new Azul.Rect(625, 339, 61, 61));
            //invadersSpriteMapping.Add("U", new Azul.Rect(27, 475, 62, 62));
            //invadersSpriteMapping.Add("V", new Azul.Rect(122, 491, 95, 44));
            //invadersSpriteMapping.Add("W", new Azul.Rect(243, 494, 57, 40));
            //invadersSpriteMapping.Add("X", new Azul.Rect(330, 491, 82, 55));
            //invadersSpriteMapping.Add("Y", new Azul.Rect(438, 491, 22, 39)); // Zigzag Laser
            //invadersSpriteMapping.Add("Z", new Azul.Rect(490, 489, 73, 44));

			//invadersSpriteMapping.Add("new S", new Azul.Rect(520, 342, 82, 55)); // Player dead 2

			//invadersSpriteMapping.Add("Alpha", new Azul.Rect(590, 488, 10, 46)); // Straight bullet
			//invadersSpriteMapping.Add("Beta", new Azul.Rect(619, 491, 24, 41)); // Dagger bullet
			
			//invadersSpriteMapping.Add("Gamma", new Azul.Rect(658, 469, 24, 65)); // UFO missile
			//invadersSpriteMapping.Add("Delta", new Azul.Rect(684, 82, 41, 34)); // Small explosion

			//invadersSpriteMapping.Add("Block", new Azul.Rect(163, 495, 13, 13)); // Block
			// BlockUL:  133, 490, 13, 13
			// BlockUR:  193, 490, 13, 13
			// BlockBL:  133, 522, 13, 13
			// BlockBR:  193, 522, 13, 13
        }


        ///////////////////////////////////////////////////////
        // 
        // Contracts
        // 
        ///////////////////////////////////////////////////////

        protected override void FillReserve(int fillSize)
        {
            for (int i = fillSize; i > 0; i--)
            {
                Image newNode = new Image();
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
