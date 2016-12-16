using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.CollisionSystem
{
	class CollisonPairManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static CollisonPairManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForCollisionPair;
			}
		}


		///////////////////////////////////////////////////////
		//
		// Manager Data
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Make a manager WITH preallocation
		/// </summary>
		/// <param name="reserveSize"></param>
		/// <param name="reserveAddition"></param>
		public CollisonPairManager(int reserveSize, int reserveAddition)
			: base(reserveSize, reserveAddition)
		{
		}





		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////


		/// <summary>
		///		Creates a new collison pair to be evaluated
		/// </summary>
		/// <param name="newName"></param>
		/// <returns></returns>
		public CollisionPairEvaluator Create(CollisionPairEvaluator.Name newName, int numOfChecks = 1)
		{
			Debug.Assert(newName != CollisionPairEvaluator.Name.UNINITIALIZED);

			CollisionPairEvaluator newCollison = this.BaseCreate() as CollisionPairEvaluator;
			newCollison.SetName(newName);
			newCollison.SetNumberOfChecks(numOfChecks);
			return newCollison;
		}

		/// <summary>
		///		Recycles the given collison pair into the object pool
		/// </summary>
		/// <param name="oldName"></param>
		/// <returns></returns>
		public bool Recycle(CollisionPairEvaluator.Name oldName)
		{
			Debug.Assert(oldName != CollisionPairEvaluator.Name.UNINITIALIZED);

			CollisionPairEvaluator oldCollison = this.BaseRecycle(oldName) as CollisionPairEvaluator;
			if (oldCollison == null) return false;
			oldCollison.Reset();
			return true;
		}

		/// <summary>
		///		Find the collison pair by name and return it
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public CollisionPairEvaluator Find(CollisionPairEvaluator.Name name)
		{
			Debug.Assert(name != CollisionPairEvaluator.Name.UNINITIALIZED);

			return this.BaseFind(name, this.activeList) as CollisionPairEvaluator;
		}

		/// <summary>
		///		Check all collisions in every collison pair
		/// </summary>
		public void Update()
		{
			CollisionPairEvaluator collisionPair = this.activeList.Head as CollisionPairEvaluator;
			
			// Loop through all collision pairs
			while (collisionPair != null)
			{
				collisionPair.Update();
				collisionPair = collisionPair.next as CollisionPairEvaluator;
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
				CollisionPairEvaluator newNode = new CollisionPairEvaluator();
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
