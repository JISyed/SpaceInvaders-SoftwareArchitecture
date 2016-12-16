using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.GameObjectSystem;

namespace SpaceInvaders.CollisionSystem
{
	class CollisionPairEvaluator : ListNode
	{
		private CollisionSubscription subscription;
		private CollisionPairEvaluator.Name name;
		private GameObject collisionTreeRootFirst;
		private GameObject collisionTreeRootSecond;
		private PCSTraverser firstTvr;
		private PCSTraverser secondTvr;
		private PCSIterator iterator;
		private int collisionChecks;
		private bool scanEntireFirstTree;

		//
		// Constructors
		//

		public CollisionPairEvaluator() : base()
		{
			this.subscription = new CollisionSubscription();
			this.name = Name.UNINITIALIZED;
			this.collisionTreeRootFirst = null;
			this.collisionTreeRootSecond = null;
			this.firstTvr = new PCSTraverser(null);
			this.secondTvr = new PCSTraverser(null);
			this.iterator = new PCSIterator(null);
			this.collisionChecks = 1;
			this.scanEntireFirstTree = false;
		}




		//
		// Methods
		//

		/// <summary>
		///		Check all collisions
		/// </summary>
		public void Update()
		{
			// Iterate through the entire first tree...
			if (this.ShouldScanEntireFirstTree)
			{
				// ... But only if the two roots collide

				// Disclaimer: This loop only works if nothing from
				// the first tree will ever be deleted. This is true
				// for Aliens vs Shields (Aliens don't get deleted)

				firstTvr.Reassign(this.collisionTreeRootFirst);
				secondTvr.Reassign(this.collisionTreeRootSecond);

				// Leave if either roots are null
				if (firstTvr.IsValid() == false || secondTvr.IsValid() == false)
				{
					return;
				}

				if (Collider.Intersect(collisionTreeRootFirst.Collider, collisionTreeRootSecond.Collider))
				{
					this.iterator.Reassign(this.collisionTreeRootFirst);

					// For every collision check allowed...
					for(int i = 0; i < this.collisionChecks; i++)
					{
						// For every node in the first tree...
						for(this.iterator.StartOver(); this.iterator.IsDone() == false; this.iterator.GetNext())
						{
							// Collisions must be checked against a leaf node
							var currentNode = this.iterator.GetCurrent() as GameObject;
							if(currentNode.Child == null)
							{
								// Move second tree's traverser to the root
								this.secondTvr.StartOver();

								// Scan the second tree against the first collidee
								GameObject secondCollidee = this.NarrowDownCollisonTree(secondTvr, currentNode);

								// A collision occurred
								if(secondCollidee != null)
								{
									// If we are here, there was a collision!
									this.DeclareCollision(currentNode, secondCollidee);
								}
							}
						}
					}
				}
			}
			
			// Don't iterate through the entire first tree. Just narrow it down.
			else
			{
				for (int i = 0; i < this.collisionChecks; i++)
				{
					firstTvr.Reassign(this.collisionTreeRootFirst);
					secondTvr.Reassign(this.collisionTreeRootSecond);

					// Leave if either roots are null
					if (firstTvr.IsValid() == false || secondTvr.IsValid() == false)
					{
						return;
					}


					// Scan the first tree against the entire second root
					GameObject firstCollidee = this.NarrowDownCollisonTree(firstTvr, this.collisionTreeRootSecond);
					if (firstCollidee == null)
					{
						// Leave if no collison was found
						return;
					}

					// Scan the second tree against the first collidee
					GameObject secondCollidee = this.NarrowDownCollisonTree(secondTvr, firstCollidee);
					if (secondCollidee == null)
					{
						// Leave if no collison was found
						return;
					}


					// If we are here, there was a collision!
					this.DeclareCollision(firstCollidee, secondCollidee);
				}
			}

		}

		/// <summary>
		///		Set the name of this collision pair
		/// </summary>
		/// <param name="newName"></param>
		public void SetName(CollisionPairEvaluator.Name newName)
		{
			this.name = newName;
			this.subscription.SetName(newName);
		}

		/// <summary>
		///		Adds a new observer to this collison pair
		/// </summary>
		/// <param name="newSubscriber"></param>
		public void Subscribe(ICollisionSubscriber newSubscriber)
		{
			this.subscription.Register(newSubscriber);
		}

		/// <summary>
		///		Removes an observer from this collison pair
		/// </summary>
		/// <param name="oldSubscriber"></param>
		/// <returns></returns>
		public bool Unsubscribe(ICollisionSubscriber oldSubscriber)
		{
			return this.subscription.Unregister(oldSubscriber);
		}

		/// <summary>
		///		Sets the root of the first collision tree
		///		to check collisions with
		/// </summary>
		/// <param name="newRoot"></param>
		public void SetFirstCollisonRoot(GameObject newRoot)
		{
			this.collisionTreeRootFirst = newRoot;
		}

		/// <summary>
		///		Sets the root of the second collision tree
		///		to check collisions with
		/// </summary>
		/// <param name="newRoot"></param>
		public void SetSecondCollisonRoot(GameObject newRoot)
		{
			this.collisionTreeRootSecond = newRoot;
		}

		/// <summary>
		///		Set how many collision checks to do per pair per frame
		/// </summary>
		/// <param name="newNumOfChecks"></param>
		public void SetNumberOfChecks(int newNumOfChecks)
		{
			this.collisionChecks = newNumOfChecks;
		}

		//
		// Private Methods
		//

		/// <summary>
		///		Declare the given two GameObjects as having collided with each other
		/// </summary>
		/// <param name="firstObject"></param>
		/// <param name="secondObject"></param>
		private void DeclareCollision(GameObject firstObject, GameObject secondObject)
		{
			// Callback to the GameObjects
			bool deleteFirst = firstObject.OnCollide(this.name, secondObject);
			bool deleteSecond = secondObject.OnCollide(this.name, firstObject);

			// Notify all observers (subscribers) of this collison
			this.subscription.Notify();

			if(deleteFirst)
			{
				GameObjectManager.Active.Recycle(firstObject);
			}

			if(deleteSecond)
			{
				GameObjectManager.Active.Recycle(secondObject);
			}
		}

		/// <summary>
		///		Checks collisons with every object in the tree 
		///		against only the root of the other tree
		/// </summary>
		/// <param name="treeTvr">
		///		The tree to narrow down (as a PCS traverser)
		/// </param>
		/// <param name="otherRoot">
		///		The other tree to check against
		///	</param>
		/// <returns>A GameObject that collides with other tree, otherwise null</returns>
		private GameObject NarrowDownCollisonTree(PCSTraverser treeTvr, GameObject otherRoot)
		{
			// Make sure the traverser starts at the root
			treeTvr.StartOver();

			// Loop through (almost) every object in this tree (pointed by the traverser)
			while(treeTvr.IsValid())
			{
				GameObject current = treeTvr.GetCurrent() as GameObject;
				
				// Check if the current object collides with the other root
				if(Collider.Intersect(current.Collider, otherRoot.Collider))
				{
					// If the current object has children
					if (treeTvr.IsThereChild())
					{
						// Check collisons with the children
						treeTvr.NextChild();
					}
					// The current object is at the bottom of the
					// collison hierarchy (it is a last-child node)
					else
					{
						// A collison was found!
						break;
					}
				}
				// No collision
				else
				{
					// Go to the next sibling
					// If there are no more siblings, this loop breaks
					treeTvr.NextSibling();
				}
			}

			// Return the current object. This can be null.
			return treeTvr.GetCurrent() as GameObject;
		}



		//
		// Contracts
		//

		/// <summary>
		///		Used for manager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return this.name;
		}

		/// <summary>
		///		Reset this collison pair
		/// </summary>
		public override void Reset()
		{
			this.name = Name.UNINITIALIZED;
			this.subscription.UnregisterAll();
			this.subscription.SetName(Name.UNINITIALIZED);
			this.collisionTreeRootFirst = null;
			this.collisionTreeRootSecond = null;
		}




		//
		// Properties
		//

		/// <summary>
		///		The name of this collison pair
		/// </summary>
		public Name CollisionName
		{
			get
			{
				return this.name;
			}
		}


		/// <summary>
		///		The root of the first collision tree
		/// </summary>
		public GameObject FirstCollisionRoot
		{
			get
			{
				return this.collisionTreeRootFirst;
			}
		}

		/// <summary>
		///		The root of the second collision tree
		/// </summary>
		public GameObject SecondCollisionRoot
		{
			get
			{
				return this.collisionTreeRootSecond;
			}
		}

		/// <summary>
		///		Should every object the the first tree be checked
		///		for collisions even if most don't collide?
		/// </summary>
		public bool ShouldScanEntireFirstTree
		{
			get
			{
				return this.scanEntireFirstTree;
			}

			set
			{
				this.scanEntireFirstTree = value;
			}
		}




		//
		// Nested Enums
		//

		/// <summary>
		///		Possible names for the collison pair. 
		///		They are always in order of first then second.
		/// </summary>
		public enum Name
		{
			UNINITIALIZED,

			Wall_vs_Alien,
			Wall_vs_Player,
			Wall_vs_UFO,

			PlayerLaser_vs_Alien,
			PlayerLaser_vs_Shield,
			PlayerLaser_vs_AlienLaser,
			PlayerLaser_vs_UFO,
			PlayerLaser_vs_WallTop,

			Player_vs_Alien,
			Player_vs_AlienLaser,

			Alien_vs_Shield,
			
			AlienLaser_vs_Shield,
			AlienLaser_vs_WallBottom,
		}
	}
}
