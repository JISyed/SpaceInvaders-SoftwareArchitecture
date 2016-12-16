using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.GameObjectSystem
{
	sealed class GameObjectManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static GameObjectManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForGameObject;
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
		public GameObjectManager(int reserveSize, int reserveAddition)
			: base(reserveSize, reserveAddition)
		{
		}



		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Attaches an already created GameObject to the manager
		/// </summary>
		/// <param name="newGameObject"></param>
		public void Attach(GameObject newGameObject)
		{
			GameObjectHolder newGoHolder = this.BaseCreate(newGameObject.Id) as GameObjectHolder;
			newGoHolder.SetGameObject(newGameObject);
			newGameObject.Start();
		}

		/// <summary>
		///		Removes the given GameObject to be used again from the pool
		/// </summary>
		/// <param name="name"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Recycle(GameObject.Name name, uint id = 0u)
		{
			GameObjectHolder oldGoHolder = this.BaseRecycle(name, id) as GameObjectHolder;
			if (oldGoHolder == null) return false;
			oldGoHolder.Reset();
			return true;
		}

		/// <summary>
		///		Removes the given GameObject to be used again from the pool
		/// </summary>
		/// <param name="oldGameObject"></param>
		/// <returns></returns>
		public bool Recycle(GameObject oldGameObject)
		{
			GameObjectHolder oldGoHolder = this.BaseRecycle(oldGameObject.ObjectName, oldGameObject.Id) as GameObjectHolder;
			if (oldGoHolder == null) return false;
			oldGoHolder.Reset();
			return true;
		}

		/// <summary>
		///		Finds a GameObject with a name and ID and returns it
		/// </summary>
		/// <param name="name"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public GameObject Find(GameObject.Name name, uint id = 0u)
		{
			GameObjectHolder goHolder = this.BaseFind(name, id, this.activeList) as GameObjectHolder;
			if(goHolder != null)
			{
				return goHolder.GameObjectRef;
			}
			return null;
		}


		/// <summary>
		///		Updates every single GameObject in the loop
		/// </summary>
		public void Update()
		{
			GameObjectHolder itr = this.activeList.Head as GameObjectHolder;
			while(itr != null)
			{
				// Update a GameObject
				bool keepGameObject = itr.Update();

				if(keepGameObject == false)
				{
					// Begin removing this object
					GameObjectHolder oldHolder = itr;
					itr = itr.next as GameObjectHolder;
					this.Recycle(oldHolder.GameObjectRef);
					oldHolder = null;
				}
				else
				{
					// Keep updating the object
					itr.UpdateInternalSpriteData();

					// Next node
					itr = itr.next as GameObjectHolder;
				}
			}
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
				GameObjectHolder newNode = new GameObjectHolder();
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
