using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders
{
	/// <summary>
	///		An iterator to traverse the PCSTreeNode data structure.
	///		Note that this does child-first iteration.
	/// </summary>
	class PCSIterator
	{
		private PCSTreeNode treeRoot;
		private PCSTreeNode currentNode;

		/// <summary>
		///		Initialize the iterator
		/// </summary>
		/// <param name="root">
		///		The root node of the PCS Tree
		/// </param>
		public PCSIterator(PCSTreeNode root)
		{
			//Debug.Assert(root != null);

			this.treeRoot = root;
			this.currentNode = root;
		}

		/// <summary>
		///		Sets a new root for this iterator
		/// </summary>
		/// <param name="newRoot"></param>
		public void Reassign(PCSTreeNode newRoot)
		{
			this.treeRoot = newRoot;
			this.currentNode = newRoot;
		}

		/// <summary>
		///		Moves this iterator back to the root of the PCS Tree
		/// </summary>
		public void StartOver()
		{
			this.currentNode = treeRoot;
		}

		/// <summary>
		///		Returns the node currently being pointed by the iterator
		/// </summary>
		/// <returns></returns>
		public PCSTreeNode GetCurrent()
		{
			return this.currentNode;
		}

		/// <summary>
		///		Returns the root of the PCS Tree currently being iterated 
		/// </summary>
		/// <returns></returns>
		public PCSTreeNode GetRoot()
		{
			return this.treeRoot;
		}

		/// <summary>
		///		Returns true if this iterator is finished traversing the tree
		/// </summary>
		/// <returns></returns>
		public bool IsDone()
		{
			return this.currentNode == null;
		}

		/// <summary>
		///		Moves this iterator to the next PCSTreeNode and then returns it
		/// </summary>
		/// <returns></returns>
		public PCSTreeNode GetNext()
		{
			this.currentNode = this.privGetNext(this.currentNode);

			return this.currentNode;
		}




		//
		// Private Methods
		//

		/// <summary>
		///		Searches for the next node in the PCS Tree
		/// </summary>
		/// <param name="current"></param>
		/// <param name="canUseChild">
		///		Can the next node be a child node?
		/// </param>
		/// <returns></returns>
		private PCSTreeNode privGetNext(PCSTreeNode current, bool canUseChild = true)
		{
			PCSTreeNode theNextNode = null;

			// If there is a child and can use it
			if ((current.Child != null) && canUseChild)
			{
				theNextNode = current.Child;
			}
			// If there is a sibling (and no child)
			else if (current.Sibling != null)
			{
				theNextNode = current.Sibling;
			}
			// If there is a parent (and no child or sibling)
			else if (current.Parent != this.treeRoot)
			{
				// Search the parent's sibling
				theNextNode = this.privGetNext(current.Parent, false);
			}
			// If there is no parent, child or sibling
			else
			{
				theNextNode = null;
			}

			return theNextNode;
		}


	}
}
