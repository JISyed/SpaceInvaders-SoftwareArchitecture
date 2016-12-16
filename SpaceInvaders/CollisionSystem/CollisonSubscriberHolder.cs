using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;

namespace SpaceInvaders.CollisionSystem
{
	class CollisonSubscriberHolder : ContainerNode
	{
		ICollisionSubscriber subscriber;

		//
		// Constructor
		//

		public CollisonSubscriberHolder(ICollisionSubscriber newSubscriber) 
			: base()
		{
			this.subscriber = newSubscriber;
		}



		//
		// Methods
		//

		/// <summary>
		///		Notifies the subscriber that a collison happened
		/// </summary>
		/// <param name="collisonName"></param>
		public void NotifySubscriber(CollisionPairEvaluator.Name collisonName)
		{
			this.subscriber.OnCollisionNotified(collisonName);
		}




		//
		// Private Methods
		//




		//
		// Contracts
		//

		/// <summary>
		///		NOT IN USE
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		///		Removes the subscriber from the subscription
		/// </summary>
		public override void Reset()
		{
			this.subscriber = null;
		}


		//
		// Properties
		//

		/// <summary>
		///		The subscriber of the collison
		/// </summary>
		public ICollisionSubscriber Subscriber
		{
			get
			{
				return this.subscriber;
			}
		}
	}
}
