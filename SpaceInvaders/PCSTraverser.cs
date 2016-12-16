using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders
{
	/// <summary>
	///		Similar to an iterator but allows to go to next child or next sibling
	/// </summary>
	class PCSTraverser
	{
		private PCSTreeNode treeRoot;
		private PCSTreeNode currentNode;

		/// <summary>
		///		Initialize the traverser
		/// </summary>
		/// <param name="root">
		///		The root node of the PCS Tree
		/// </param>
		public PCSTraverser(PCSTreeNode root)
		{
			//Debug.Assert(root != null, "The PCS Root should not be null for the traverse to work!");

			this.treeRoot = root;
			this.currentNode = root;
		}

		/// <summary>
		///		Resets this traverser to a new tree given the new root
		/// </summary>
		/// <param name="newRoot"></param>
		public void Reassign(PCSTreeNode newRoot)
		{
			this.treeRoot = newRoot;
			this.currentNode = newRoot;
		}

		/// <summary>
		///		Moves this traverser back to the root of the PCS Tree
		/// </summary>
		public void StartOver()
		{
			this.currentNode = treeRoot;
		}

		/// <summary>
		///		Returns the node currently being pointed by the traverser
		/// </summary>
		/// <returns></returns>
		public PCSTreeNode GetCurrent()
		{
			return this.currentNode;
		}

		/// <summary>
		///		Returns the root of the PCS Tree currently being traversed 
		/// </summary>
		/// <returns></returns>
		public PCSTreeNode GetRoot()
		{
			return this.treeRoot;
		}

		/// <summary>
		///		Returns true if the node currently being traversed isn't null
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
		{
			return this.currentNode != null;
		}

		/// <summary>
		///		Returns true if the current node has a child
		/// </summary>
		/// <returns></returns>
		public bool IsThereChild()
		{
			return this.currentNode.Child != null;
		}

		/// <summary>
		///		Returns true if the current node has a sibling
		/// </summary>
		/// <returns></returns>
		public bool IsThereSibling()
		{
			return this.currentNode.Sibling != null;
		}

		/// <summary>
		///		Moves this traverser to the next sibling
		/// </summary>
		/// <returns></returns>
		public void NextSibling()
		{
			this.currentNode = this.currentNode.Sibling;
		}

		/// <summary>
		///		Moves this traverser to the next child
		/// </summary>
		/// <returns></returns>
		public void NextChild()
		{
			this.currentNode = this.currentNode.Child;
		}
	}
}
