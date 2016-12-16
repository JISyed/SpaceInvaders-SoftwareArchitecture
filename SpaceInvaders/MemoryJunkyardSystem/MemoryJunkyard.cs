using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.MemoryJunkyardSystem
{
	/// <summary>
	///		Used to store disposed objects, thus evading garbage collection
	/// </summary>
	class MemoryJunkyard : AbstractManager
	{
		///////////////////////////////////////////////////////
		//
		// Singleton stuff
		//
		///////////////////////////////////////////////////////

		private static MemoryJunkyard instance = null;

		private bool wasNewJunkAdded = false;


		/// <summary>
		///		Singleton Instance
		/// </summary>
		public static MemoryJunkyard Self
		{
			get
			{
				if (instance == null)
				{
					// Create the manager
					MemoryJunkyard.instance = new MemoryJunkyard();
				}

				return MemoryJunkyard.instance;
			}
		}


		///////////////////////////////////////////////////////
		//
		// Manager Data
		//
		///////////////////////////////////////////////////////

		// Private Constructor
		private MemoryJunkyard()
			: base()
        {
        }




		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Puts a new object in the junkyard to evade the garbage collector
		/// </summary>
		/// <param name="newJunk"></param>
		public void DisposeObject(object newJunk)
		{
			MemoryJunkBlock newBlock = this.BaseCreate() as MemoryJunkBlock;
			newBlock.SetJunk(newJunk);
			this.wasNewJunkAdded = true;
		}

		/// <summary>
		///		Destroys every object in the junk yard
		/// </summary>
		public void RecycleYard()
		{
			if (this.wasNewJunkAdded)
			{
				this.wasNewJunkAdded = false;

				MemoryJunkBlock itr = this.activeList.Head as MemoryJunkBlock;

				while (itr != null)
				{
					itr.Reset();
					this.activeList.PopFront();

					// Next node
					itr = this.activeList.Head as MemoryJunkBlock;
				}

				// Call the garbage collector
				System.GC.Collect();
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
				MemoryJunkBlock newNode = new MemoryJunkBlock();
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
