using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.ContainerSystem
{
	sealed class ContainerList
	{
		private ContainerNode head = null;    // Head of linked list
		private int listSize = 0;

		// Getters
		public ContainerNode Head
		{
			get
			{
				return this.head;
			}
		}
		public int Size
		{
			get
			{
				return this.listSize;
			}
		}

		// Add to the front of the list
		public void PushFront(ContainerNode newNode)
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

		// Remove from the list
		public ContainerNode Pop(ContainerNode oldNode)
		{
			// No nodes at all
			if (this.listSize == 0)
			{
				return null;
			}

			// Search for the node to remove
			ContainerNode nodeToRemove = null;
			for (ContainerNode n = this.head; n != null; n = n.next)
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
				ContainerNode tail = nodeToRemove.prev;
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

		// Remove from the front of the list
		public ContainerNode PopFront()
		{
			// With no nodes
			if (this.listSize == 0)
			{
				return null;
			}
			// With 1 node
			else if (this.listSize == 1)
			{
				ContainerNode nodeToRemove = this.head;
				this.head = null;
				this.listSize--;
				return nodeToRemove;
			}

			// With multiple nodes
			ContainerNode nodeToRemoved = this.head;
			this.head = head.next;
			this.head.prev = null;
			nodeToRemoved.next = null;
			this.listSize--;

			return nodeToRemoved;
		}
	}
}
