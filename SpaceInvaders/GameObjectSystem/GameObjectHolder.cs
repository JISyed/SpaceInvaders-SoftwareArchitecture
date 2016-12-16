using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.MemoryJunkyardSystem;

namespace SpaceInvaders.GameObjectSystem
{
	sealed class GameObjectHolder : ListNode
	{
		private GameObject gameObject;


		//
		// Constructors
		//

		public GameObjectHolder()
			: base()
		{
			this.gameObject = null;
		}



		//
		// Methods
		//

		/// <summary>
		///		Set the new GameObject reference
		/// </summary>
		/// <param name="newGameObject"></param>
		public void SetGameObject(GameObject newGameObject)
		{
			// If there was already a GameObject here, throw it into the junkyard
			this.Reset();

			// Set new GameObject
			this.gameObject = newGameObject;
			this.SetId(newGameObject.Id);
			
		}


		/// <summary>
		///		Updates the sprite data of an individual GameObject
		/// </summary>
		public void UpdateInternalSpriteData()
		{
			Debug.Assert(this.gameObject != null);
			this.gameObject.UpdateInternalSpriteData();
		}

		/// <summary>
		///		Perform the GameObject's custom update loop routine.
		///		Returns false if the object decides to delete itself.
		/// </summary>
		public bool Update()
		{
			Debug.Assert(this.gameObject != null);
			return this.gameObject.Update();
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
			if(this.gameObject == null)
			{
				return GameObject.Name.UNINITIALIZED;
			}
			return this.gameObject.ObjectName;
		}

		/// <summary>
		///		Clears the data in the GameObject
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			if (this.gameObject != null)
			{
				this.gameObject.Reset();
				MemoryJunkyard.Self.DisposeObject(this.gameObject);
				this.gameObject = null;
			}
		}





		//
		// Properties
		//

		/// <summary>
		///		The GameObject being referenced
		/// </summary>
		public GameObject GameObjectRef
		{
			get
			{
				return this.gameObject;
			}
		}

		/// <summary>
		///		The name of the GameObject
		/// </summary>
		public GameObject.Name ObjectName
		{
			get
			{
				if (this.gameObject == null)
				{
					return GameObject.Name.UNINITIALIZED;
				}
				return this.gameObject.ObjectName;
			}
		}
	}
}
