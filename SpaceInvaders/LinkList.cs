using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders
{
    sealed class LinkList
    {
        private ListNode head = null;    // Head of linked list
        private int listSize = 0;

        // Getters

		/// <summary>
		///		The head node of the list
		/// </summary>
        public ListNode Head
        {
            get
            {
                return this.head;
            }
        }

		/// <summary>
		///		The current number of nodes in the list
		/// </summary>
        public int Size
        {
            get
            {
                return this.listSize;
            }
        }

        /// <summary>
		///		Add to the front of the list
        /// </summary>
        /// <param name="newNode"></param>
        public void PushFront(ListNode newNode)
        {
            // Empty list
            if (this.listSize == 0)
            {
                this.head = newNode;
            }
            // Non-empty list
            else
            {
                // Push new node to front
                newNode.prev = null;
                newNode.next = this.head;
                this.head.prev = newNode;
                this.head = newNode;
            }

            // Increment count
            this.listSize++;
        }


		/// <summary>
		///		Pushes into the list in sorted order. Order is defined by the nodes themselves.
		///		Only use this with lists that were meant to be ordered. Does not check
		///		if the list is sorted.
		/// </summary>
		/// <param name="newNode"></param>
		public void PushSorted(ListNode newNode)
		{
			// Empty list
			if (this.listSize == 0)
			{
				this.head = newNode;
			}
			// Non-empty list
			else
			{
				Debug.Assert(this.head != null);

				// If the new node is "smaller" than the head
				if (newNode < this.head)
				{
					// Push new node to front (as the "head" node)
					newNode.prev = null;
					newNode.next = this.head;
					this.head.prev = newNode;
					this.head = newNode;
				}
				// In all cases where the new node is "bigger" than head
				else
				{
					// Look at all connections after node
					ListNode itr = head;
					while(itr != null)
					{
						// If there is no next node...
						if(itr.next == null)
						{
							// Add the new node after itr (as the "tail" node)
							itr.next = newNode;
							newNode.prev = itr;
							newNode.next = null;
							break;
						}
						else if(itr.next > newNode)
						{
							// Put the new node *between* itr and itr.next
							newNode.prev = itr;
							newNode.next = itr.next;
							itr.next.prev = newNode;
							itr.next = newNode;
							break;
						}
						/*
						else
						{
							// Mystery case
							Debug.Assert(false);
						}
						//*/

						// Move to the next node
						itr = itr.next;
					}
				}
			}

			// Increment count
			this.listSize++;
		}



        /// <summary>
		///		Remove from the list
        /// </summary>
        /// <param name="oldNode"></param>
        /// <returns></returns>
        public ListNode Pop(ListNode oldNode)
        {
            // No nodes at all
            if (this.listSize == 0)
            {
                return null;
            }

            // Search for the node to remove
            ListNode nodeToRemove = null;
            for (ListNode n = this.head; n != null; n = n.next)
            {
                if (ReferenceEquals(n, oldNode))
                {
                    nodeToRemove = n;
                    break;
                }
            }

            // Queried node not found
            if (nodeToRemove == null)
            {
                return null;
            }

            // Queried node is only one found
            if (this.listSize == 1)
            {
                this.head = null;
                this.listSize--;
                return nodeToRemove;
            }

            // Queried node is in front
            else if (nodeToRemove.prev == null)
            {
                nodeToRemove = this.head;
                this.head = head.next;
                this.head.prev = null;
                nodeToRemove.next = null;
                this.listSize--;
                return nodeToRemove;
            }

            // Queried node is in back
            else if (nodeToRemove.next == null)
            {
                ListNode tail = nodeToRemove.prev;
                tail.next = null;
                nodeToRemove.prev = null;
                this.listSize--;
                return nodeToRemove;
            }

            // Queried node is in the middle
            nodeToRemove.prev.next = nodeToRemove.next;
            nodeToRemove.next.prev = nodeToRemove.prev;
            nodeToRemove.prev = null;
            nodeToRemove.next = null;
            this.listSize--;
            return nodeToRemove;
        }


        /// <summary>
		///		Remove from the front of the list
        /// </summary>
        /// <returns></returns>
        public ListNode PopFront()
        {
            // With no nodes
            if (this.listSize == 0)
            {
                return null;
            }
            // With 1 node
            else if (this.listSize == 1)
            {
                ListNode nodeToRemove = this.head;
                this.head = null;
                this.listSize--;
                return nodeToRemove;
            }

            // With multiple nodes
            ListNode nodeToRemoved = this.head;
            this.head = head.next;
            this.head.prev = null;
            nodeToRemoved.next = null;
            this.listSize--;

            return nodeToRemoved;
        }

		/// <summary>
		///		Returns the head of the list without removing it
		/// </summary>
		/// <returns></returns>
		public ListNode PeekFront()
		{
			// With no nodes
			if (this.listSize == 0)
			{
				return null;
			}

			// Else with any other number of nodes
			return this.head;
		}

		
    }
}
