using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.SpriteSystem
{
	class SpriteCollisonProxyManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static SpriteCollisonProxyManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForSpriteCollisonProxy;
			}
		}



        ///////////////////////////////////////////////////////
        //
        // Manager Data
        //
        ///////////////////////////////////////////////////////

		/// <summary>
		///		Make a manager WITH preallocation
		/// </summary>
		/// <param name="reserveSize"></param>
		/// <param name="reserveAddition"></param>
		public SpriteCollisonProxyManager(int reserveSize, int reserveAddition)
			: base(reserveSize, reserveAddition)
		{
		}


        ///////////////////////////////////////////////////////
        //
        // Methods
        //
        ///////////////////////////////////////////////////////

		/// <summary>
		///		Create a new flyweight sprite referencing a model SpriteCollision
		/// </summary>
		/// <param name="spriteName"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public SpriteCollisionProxy Create(Sprite.Name spriteName, uint id)
		{
			Debug.Assert(spriteName != Sprite.Name.UNINITIALIZED);
			//Debug.Assert(spriteName != Sprite.Name.NULL);

			SpriteCollision modelSprite = SpriteCollisionManager.Self.Find(spriteName);
			Debug.Assert(modelSprite != null, "For some reason, the model collision sprite could not be found!");
			SpriteCollisionProxy newProxy = this.BaseCreate(id) as SpriteCollisionProxy;
			newProxy.SetName(spriteName);
			newProxy.SetModelSprite(modelSprite);
			return newProxy;
		}

		/// <summary>
		///		Removes the flyweight sprite to be used again from the pool
		/// </summary>
		/// <param name="name"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Recycle(Sprite.Name name, uint id)
		{
			Debug.Assert(name != Sprite.Name.UNINITIALIZED);
			//Debug.Assert(name != Sprite.Name.NULL);

			SpriteCollisionProxy oldProxy = this.BaseRecycle(name, id) as SpriteCollisionProxy;
			if (oldProxy == null) return false;
			oldProxy.Reset();
			return true;
		}

		/// <summary>
		///		Finds a flyweight sprite and returns it
		/// </summary>
		/// <param name="name"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public SpriteCollisionProxy Find(Sprite.Name name, uint id)
		{
			Debug.Assert(name != Sprite.Name.UNINITIALIZED);
			//Debug.Assert(name != Sprite.Name.NULL);

			return this.BaseFind(name, id, this.activeList) as SpriteCollisionProxy;
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
				SpriteCollisionProxy newNode = new SpriteCollisionProxy();
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
