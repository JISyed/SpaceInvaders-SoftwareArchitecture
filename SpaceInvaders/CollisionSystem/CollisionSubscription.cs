using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;

namespace SpaceInvaders.CollisionSystem
{
	class CollisionSubscription
	{
		private ContainerList listOfSubscribers;
		private CollisionPairEvaluator.Name collisionName;



		//
		// Constructors
		//

		public CollisionSubscription() : base()
		{
			this.listOfSubscribers = new ContainerList();
			this.collisionName = CollisionPairEvaluator.Name.UNINITIALIZED;
		}





		//
		// Methods
		//

		/// <summary>
		///		Sets the name of this collison subscription
		/// </summary>
		/// <param name="newName"></param>
		public void SetName(CollisionPairEvaluator.Name newName)
		{
			this.collisionName = newName;
		}

		/// <summary>
		///		Adds a new subscriber to this Collision Subscription
		/// </summary>
		/// <param name="newSubscriber"></param>
		/// <returns></returns>
		public CollisonSubscriberHolder Register(ICollisionSubscriber newSubscriber)
		{
			CollisonSubscriberHolder newHolder = new CollisonSubscriberHolder(newSubscriber);
			this.listOfSubscribers.PushFront(newHolder);
			return newHolder;
		}

		/// <summary>
		///		Remove a subscriber from this subscription
		/// </summary>
		/// <param name="oldSubscriber"></param>
		/// <returns></returns>
		public bool Unregister(ICollisionSubscriber oldSubscriber)
		{
			CollisonSubscriberHolder oldHolder = this.Search(oldSubscriber);
			Debug.Assert(oldHolder != null);
			oldHolder.Reset();
			this.listOfSubscribers.Pop(oldHolder);
			oldHolder = null;

			return true;
		}

		/// <summary>
		///		Notify every subscriber that a collision occured
		/// </summary>
		public void Notify()
		{
			CollisonSubscriberHolder itr = this.listOfSubscribers.Head as CollisonSubscriberHolder;
			while (itr != null)
			{
				// Notify the individual subscriber
				itr.NotifySubscriber(collisionName);

				// Next subscriber
				itr = itr.next as CollisonSubscriberHolder;
			}
		}

		/// <summary>
		///		Remove every subscriber from this subscription
		/// </summary>
		public void UnregisterAll()
		{
			CollisonSubscriberHolder itr = this.listOfSubscribers.Head as CollisonSubscriberHolder;
			while(itr != null)
			{
				this.listOfSubscribers.PopFront();
				itr = this.listOfSubscribers.Head as CollisonSubscriberHolder;
			}
		}






		//
		// Private Methods
		//

		/// <summary>
		///		Return a CollisonSubscriberHolder given an ICollisionScriber
		/// </summary>
		/// <param name="queriedSubscriber"></param>
		/// <returns></returns>
		private CollisonSubscriberHolder Search(ICollisionSubscriber queriedSubscriber)
		{
			CollisonSubscriberHolder itr = this.listOfSubscribers.Head as CollisonSubscriberHolder;

			while(itr != null)
			{
				if(ReferenceEquals(itr.Subscriber, queriedSubscriber))
				{
					break;	// Found it
				}

				itr = itr.next as CollisonSubscriberHolder;
			}

			return itr;
		}
		





		//
		// Contracts
		//





		//
		// Properties
		//

		/// <summary>
		///		The name of the collison pair this subscription represents
		/// </summary>
		public CollisionPairEvaluator.Name CollisionName
		{
			get
			{
				return this.collisionName;
			}
		}



	}
}
