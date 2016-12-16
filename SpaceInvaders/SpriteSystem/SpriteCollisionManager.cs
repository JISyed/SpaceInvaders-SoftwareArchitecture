using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.SpriteSystem
{
	sealed class SpriteCollisionManager : AbstractManager
	{
		///////////////////////////////////////////////////////
        //
        // Singleton stuff
        //
        ///////////////////////////////////////////////////////

        private static SpriteCollisionManager instance = null;

		/// <summary>
		///		Singleton Instance
		/// </summary>
        public static SpriteCollisionManager Self
        {
			get
			{
				if (instance == null)
				{
					// Create the manager
					SpriteCollisionManager.instance = new SpriteCollisionManager();
				}

				return SpriteCollisionManager.instance;
			}
        }




        ///////////////////////////////////////////////////////
        //
        // Manager Data
        //
        ///////////////////////////////////////////////////////

        // Private Constructor
		private SpriteCollisionManager() : base()
        {
			
        }




        ///////////////////////////////////////////////////////
        //
        // Methods
        //
        ///////////////////////////////////////////////////////

		/// <summary>
		///		Create the sprite from the memory pool
		/// </summary>
		/// <param name="newName"></param>
		/// <returns></returns>
		public SpriteCollision Create(Sprite.Name newName)
		{
			Debug.Assert(newName != Sprite.Name.UNINITIALIZED);
			//Debug.Assert(newName != Sprite.Name.NULL);

			if(newName == Sprite.Name.NULL)
			{
				SpriteCollision newNullSprite = this.BaseCreate() as SpriteCollision;
				newNullSprite.SetName(newName);
				newNullSprite.SetPosition(0, 0);
				newNullSprite.SetDimensions(5, 5);
				return newNullSprite;
			}

			SpriteEntity referenceSprite = SpriteEntityManager.Self.Find(newName);

			SpriteCollision newSprite = this.BaseCreate() as SpriteCollision;
			newSprite.SetName(newName);
			newSprite.SetPosition(referenceSprite.X, referenceSprite.Y);
			newSprite.ResizeToSprite(referenceSprite);
			return newSprite;
		}

		/// <summary>
		///		Removes the given sprite to be used again from the pool
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool Recycle(Sprite.Name name)
		{
			Debug.Assert(name != Sprite.Name.UNINITIALIZED);
			//Debug.Assert(name != Sprite.Name.NULL);

			SpriteCollision oldNode = this.BaseRecycle(name) as SpriteCollision;
			if (oldNode == null) return false;
			oldNode.Reset();
			return true;
		}

		/// <summary>
		///		Finds a sprite and returns it
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public SpriteCollision Find(Sprite.Name name)
		{
			Debug.Assert(name != Sprite.Name.UNINITIALIZED);
			//Debug.Assert(name != Sprite.Name.NULL);

			return this.BaseFind(name, this.activeList) as SpriteCollision;
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
				SpriteCollision newNode = new SpriteCollision();
				this.reservedList.PushFront(newNode);
			}
		}

	}
}
