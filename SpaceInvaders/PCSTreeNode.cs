using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders
{
	abstract class PCSTreeNode
	{
		private PCSTreeNode parent;
		private PCSTreeNode child;
		private PCSTreeNode sibling;



		//
		// Constructor
		//

		public PCSTreeNode()
		{
			this.parent = null;
			this.child = null;
			this.sibling = null;
		}



		//
		// Methods
		//

		

		/// <summary>
		///		Adds a new child to this node
		/// </summary>
		/// <param name="newChild"></param>
		public void AddChild(PCSTreeNode newChild)
		{
			Debug.Assert(newChild.sibling == null,
				"The new child shouldn't have any other siblings");

			// Add the new child if this has no children
			if(this.child == null)
			{
				// Point down to the child
				this.child = newChild;

				// Make child point up to this
				newChild.parent = this;

				// Tell GameObject that a new child was added
				this.OnPCSNewChild(false);
			}
			// If children already exist, add new child as
			// the new sibling of the existing children
			else
			{
				this.child.AddSibling(newChild);
				
				// Tell GameObject that a new child was added
				this.OnPCSNewChild(true);
			}

			
		}


		/// <summary>
		///		Removes itself from any existing PCS hierarchy, including children
		/// </summary>
		public void RemoveThisPCSNode()
		{
			// X Parent
			if(this.parent == null)
			{
				// X Parent, X Child
				if(this.child == null)
				{
					// X Parent, X Child, X Sibling
					if(this.sibling == null)
					{
						// Do nothing
					}
					// X Parent, X Child, O Sibling
					else
					{
						Debug.Assert(this.sibling.parent == null,
							"The sibling shouldn't have a parent if this node didn't either.");
						this.sibling = null;
					}
				}
				// X Parent, O Child
				else
				{
					// X Parent, O Child, X Sibling
					if (this.sibling == null)
					{
						this.RemoveChildrenNodes();
					}
					// X Parent, O Child, O Sibling
					else
					{
						Debug.Assert(this.sibling.parent == null,
							"The sibling shouldn't have a parent if this node didn't either.");
						this.sibling = null;

						this.RemoveChildrenNodes();
					}
				}
			}
			// O Parent
			else
			{
				// O Parent, X Child
				if (this.child == null)
				{
					// O Parent, X Child, X Sibling
					if (this.sibling == null)
					{
						// If this node is also the first and only
						if(ReferenceEquals(this, this.parent.child))
						{
							// This should be the end case of
							// recursive child unlinking
							this.parent.child = null;
							this.parent = null;
						}
						else
						{
							// Find the second to last sibling (backItr)
							PCSTreeNode backItr = this.parent.child;
							PCSTreeNode frontItr = backItr.sibling;
							while(ReferenceEquals(this, frontItr) == false)
							{
								frontItr = frontItr.sibling;
								backItr = backItr.sibling;
							}

							// Second to last node is now last node
							backItr.sibling = null;

							// This now doesn't have a parent
							this.parent = null;
						}
					}
					// O Parent, X Child, O Sibling
					else
					{
						// If parent points to this node (to be removed)
						if(ReferenceEquals(this.parent.child, this))
						{
							// Correct the child pointer for parent
							this.parent.child = this.sibling;
						}
						// If parent points to no child
						else if(this.parent.child == null)
						{
							// Do nothing??
						}
						// If parent points to another sibling
						else
						{
							// Find the sibling before this node (backItr)
							PCSTreeNode backItr = this.parent.child;
							PCSTreeNode forwardItr = backItr.sibling;
							while(ReferenceEquals(forwardItr, this) == false)
							{
								Debug.Assert(forwardItr != null);
								backItr = backItr.sibling;
								forwardItr = forwardItr.sibling;
							}

							// Point the previous sibling to the next sibling
							backItr.sibling = this.sibling;
						}

						// Unlink this parent and next sibling
						this.parent = null;
						this.sibling = null;
					}
				}
				// O Parent, O Child
				else
				{
					// O Parent, O Child, X Sibling
					if (this.sibling == null)
					{
						// If this node is also the first and only
						if (ReferenceEquals(this, this.parent.child))
						{
							this.parent.child = null;
							this.parent = null;
						}
						else
						{
							// Find the second to last sibling (backItr)
							PCSTreeNode backItr = this.parent.child;
							PCSTreeNode frontItr = backItr.sibling;
							while (ReferenceEquals(this, frontItr) == false)
							{
								frontItr = frontItr.sibling;
								backItr = backItr.sibling;
							}

							// Second to last node is now last node
							backItr.sibling = null;

							// This now doesn't have a parent
							this.parent = null;
						}

						this.RemoveChildrenNodes();
					}
					// O Parent, O Child, O Sibling
					else
					{
						// If parent points to this node (to be removed)
						if (ReferenceEquals(this.parent.child, this))
						{
							// Correct the child pointer for parent
							this.parent.child = this.sibling;
						}
						// If parent points to no child
						else if (this.parent.child == null)
						{
							// Do nothing??
						}
						// If parent points to another sibling
						else
						{
							// Find the sibling before this node (backItr)
							PCSTreeNode backItr = this.parent.child;
							PCSTreeNode forwardItr = backItr.sibling;
							while (ReferenceEquals(forwardItr, this) == false)
							{
								Debug.Assert(forwardItr != null);
								backItr = backItr.sibling;
								forwardItr = forwardItr.sibling;
							}

							// Point the previous sibling to the next sibling
							backItr.sibling = this.sibling;
						}

						// Unlink this parent and next sibling
						this.parent = null;
						this.sibling = null;


						this.RemoveChildrenNodes();
					}
				}
			}

			this.OnPCSUnlink();
		}





		//
		// Private Methods
		//

		/// <summary>
		///		Gives a new sibling node to this node. 
		///		Warning: assumes neither have next siblings
		/// </summary>
		/// <param name="newSibling"></param>
		private void AddSibling(PCSTreeNode newSibling)
		{
			Debug.Assert(newSibling.sibling == null,
				"The new sibling shouldn't have any other siblings");

			// Add new sibling if this has no sibling
			if (this.sibling == null)
			{
				this.sibling = newSibling;

				// Give the new sibling the same parent
				newSibling.parent = this.parent;
			}
			// See if the next sibling can add the new sibling (recursive!)
			else
			{
				this.sibling.AddSibling(newSibling);
			}
		}

		/// <summary>
		///		Removes all children of this node (and their siblings)
		/// </summary>
		public void RemoveChildrenNodes()
		{
			// Removing this child will make this node 
			// point to the the next sibling until
			// there are no siblings left
			while(this.child != null)
			{
				this.child.RemoveThisPCSNode();
			}
		}





		//
		// Contracts
		//

		/// <summary>
		///		Gets called when this node is removed from a PCS hierarchy
		/// </summary>
		abstract protected void OnPCSUnlink();

		/// <summary>
		///		Gets called when a parent adds a new child
		/// </summary>
		abstract protected void OnPCSNewChild(bool alreadyHadChildren);


		//
		// Properties
		//

		/// <summary>
		///		The parent node of this PCS node
		/// </summary>
		public PCSTreeNode Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>
		///		The first child node of this PCS node
		/// </summary>
		public PCSTreeNode Child
		{
			get
			{
				return this.child;
			}
		}

		/// <summary>
		///		The next sibling node of this PCS node
		/// </summary>
		public PCSTreeNode Sibling
		{
			get
			{
				return this.sibling;
			}
		}
	}
}
