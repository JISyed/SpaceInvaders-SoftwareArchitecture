using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.SpriteSystem
{
	sealed class SpriteProxyManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static SpriteProxyManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForSpriteProxy;
			}
		}



        ///////////////////////////////////////////////////////
        //
        // Manager Data
        //
        ///////////////////////////////////////////////////////

		private SpriteProxy nullSpriteProxy;

		/// <summary>
		///		Make a manager WITH preallocation
		/// </summary>
		/// <param name="reserveSize"></param>
		/// <param name="reserveAddition"></param>
		public SpriteProxyManager(int reserveSize, int reserveAddition)
			: base(reserveSize, reserveAddition)
		{
			this.nullSpriteProxy = new SpriteProxy();
			this.nullSpriteProxy.SetName(Sprite.Name.NULL);
			this.nullSpriteProxy.SetModelSprite(SpriteEntityManager.Self.NullSprite);
		}



		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Create a new flyweight sprite referencing a model sprite entity
		/// </summary>
		/// <param name="proxyName"></param>
		/// <param name="entityName">The entity's ID should be 0</param>
		/// <param name="id"></param>
		/// <returns></returns>
		public SpriteProxy Create(Sprite.Name spriteName, uint id)
		{
			if (spriteName == Sprite.Name.NULL || spriteName == Sprite.Name.UNINITIALIZED)
			{
				return this.nullSpriteProxy;
			}
			SpriteEntity modelSprite = SpriteEntityManager.Self.Find(spriteName);
			SpriteProxy newProxy = this.BaseCreate(id) as SpriteProxy;
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
			if (name == Sprite.Name.NULL || name == Sprite.Name.UNINITIALIZED)
				return false;
			SpriteProxy oldProxy = this.BaseRecycle(name, id) as SpriteProxy;
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
		public SpriteProxy Find(Sprite.Name name, uint id)
		{
			if (name == Sprite.Name.NULL || name == Sprite.Name.UNINITIALIZED)
				return this.nullSpriteProxy;
			return this.BaseFind(name, id, this.activeList) as SpriteProxy;
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
				SpriteProxy newNode = new SpriteProxy();
				this.reservedList.PushFront(newNode);
			}
		}



		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///		Returns a SpriteProxy that references a null sprite
		/// </summary>
		public SpriteProxy NullSpriteProxy
		{
			get
			{
				return this.nullSpriteProxy;
			}
		}
	}
}
