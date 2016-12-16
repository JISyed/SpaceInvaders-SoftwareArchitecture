using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.ContainerSystem
{
	abstract class Container : ListNode
	{
		protected ContainerList activeList;
		protected ContainerList reservedList;
		private int initialReserveSize = 10;
		private int additionalReserveSize = 5;
		private bool alreadyPreallocated = false;

		///////////////////////////////////////////////////////
		//
		// Constructors
		//
		///////////////////////////////////////////////////////

		public Container() : base()
        {
			this.activeList = new ContainerList();
			this.reservedList = new ContainerList();
        }

		public Container(uint newId)
			: base(newId)
        {
			this.activeList = new ContainerList();
			this.reservedList = new ContainerList();
        }

		public Container(ListNode newPrev, ListNode newNext)
			: base(newPrev, newNext)
        {
			this.activeList = new ContainerList();
			this.reservedList = new ContainerList();
        }

		public Container(ListNode newPrev, ListNode newNext, uint newId)
			: base(newPrev, newNext, newId)
        {
			this.activeList = new ContainerList();
			this.reservedList = new ContainerList();
        }


		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///     Preallocate the reserve pool for the objects being managed
		/// </summary>
		/// <param name="reserveSize">
		///     The initial size of the reserve pool
		/// </param>
		/// <param name="reserveAddition">
		///     The amount of nodes to add to the reserve pool if the pool runs out
		/// </param>
		public void Preallocate(int reserveSize, int reserveAddition)
		{
			if (this.alreadyPreallocated == false)
			{
				this.alreadyPreallocated = true;

				// Check for legit value and set the reserve data
				if (reserveSize > 0)
				{
					this.initialReserveSize = reserveSize;
				}
				if (reserveAddition > 0)
				{
					this.additionalReserveSize = reserveAddition;
				}

				// Preallocate the reserves list
				this.FillReserve(this.initialReserveSize);
			}
		}


		/// <summary>
		///     Releases all pool resources for garbage collection
		/// </summary>
		public void Destroy()
		{
			this.DestroyEntireList(this.activeList);
			this.DestroyEntireList(this.reservedList);
		}

		///////////////////////////////////////////////////////
		// 
		// Base Methods
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///     Takes a node from the reserves and adds it to the active list
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		protected ContainerNode BaseAttach(uint id)
		{
			// Refill reserves if it ran out
			if (this.ReservedSize <= 0)
			{
				this.FillReserve(this.AdditionalReserveSize);
				Debug.Assert(this.ReservedSize > 0);
			}

			// Move a node from the reserves to the active list
			ContainerNode newNode = this.reservedList.PopFront();
			this.activeList.PushFront(newNode);
			newNode.SetId(id);
			return newNode;
		}

		/// <summary>
		///     Takes a node from active list, clears data, and moves it to reserves
		/// </summary>
		/// <param name="query"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		protected ContainerNode BaseDetach(Enum query, uint id)
		{
			// Look for the node to recycle
			ContainerNode queriedNode = this.BaseFind(query, id, this.activeList);

			// Give up if node wasn't found
			if (queriedNode == null) return null;

			// Clear data
			queriedNode.BaseReset();

			// Move it from the active to reserve
			this.Move(queriedNode, this.activeList, this.reservedList);

			return queriedNode;
		}

		/// <summary>
		///     Iterate through a list and retrieve it's inqured enum value with instance ID
		/// </summary>
		/// <param name="query"></param>
		/// <param name="id"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		protected ContainerNode BaseFind(Enum query, uint id, ContainerList list)
		{
			ContainerNode foundNode = null;

			ContainerNode itr = list.Head;
			while (itr != null)
			{
				if (itr.GetName().Equals(query))
				{
					if (itr.Id == id)
					{
						// Found node
						foundNode = itr;
						break;
					}
				}

				// Next node
				itr = itr.next;
			}

			return foundNode;
		}

		///////////////////////////////////////////////////////
		//
		// Private Methods
		//
		///////////////////////////////////////////////////////

		// Removes the first node in the given list and clears its data
		private void DestroyFrontOfList(ContainerList list)
		{
			if (list.Size <= 0) return;

			ContainerNode toBeDeleted = list.PopFront();
			toBeDeleted.Reset();
			toBeDeleted.BaseReset();
			toBeDeleted.next = null;
			toBeDeleted.prev = null;
			toBeDeleted = null;
		}

		// Removes every node in the given list
		private void DestroyEntireList(ContainerList list)
		{
			while (list.Size != 0)
			{
				this.DestroyFrontOfList(list);
			}
		}

		// Move the given node from one list to another
		private void Move(ContainerNode node, ContainerList fromList, ContainerList toList)
		{
			Debug.Assert(ReferenceEquals(fromList, toList) == false);
			node = fromList.Pop(node);
			toList.PushFront(node);
		}


		///////////////////////////////////////////////////////
		// 
		// Contracts
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///     Adds the given amount of nodes to the reserve pool
		/// </summary>
		/// <param name="fillSize">Number of nodes to add</param>
		abstract protected void FillReserve(int fillSize);


		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		/// The size of the active pool
		/// </summary>
		public int ActiveSize
		{
			get
			{
				return this.activeList.Size;
			}
		}

		/// <summary>
		/// The size of the reserved pool
		/// </summary>
		public int ReservedSize
		{
			get
			{
				return this.reservedList.Size;
			}
		}

		/// <summary>
		/// The total size of all pools in the manager
		/// </summary>
		public int TotalSize
		{
			get
			{
				return this.activeList.Size + this.reservedList.Size;
			}
		}

		/// <summary>
		/// The original size of the reserved pool
		/// </summary>
		public int InitialReserveSize
		{
			get
			{
				return this.initialReserveSize;
			}
		}

		/// <summary>
		/// How many new objects to allocate to the reserved pool if it runs out
		/// </summary>
		public int AdditionalReserveSize
		{
			get
			{
				return this.additionalReserveSize;
			}
		}
	}
}
