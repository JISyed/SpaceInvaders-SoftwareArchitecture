using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.CollisionSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	/// <summary>
	///		An empty GameObject. Usually serves as a root object for collisions.
	/// </summary>
	sealed class EmptyObject : GameObject
	{
		//
		// Constructor
		//

		public EmptyObject() : base()
		{
			this.SetName(GameObject.Name.EMPTY);
		}




		//
		// Contracts
		//

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{
			// Purposefully does nothing
		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			// Purposefully does nothing
			bool keepObjectAlive = true;
			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{
			// Purposefully does nothing
		}

		/// <summary>
		///		Executes when a collison occurs with another GameObject
		/// </summary>
		/// <param name="collisonName"></param>
		/// <param name="other"></param>
		/// <returns>
		///		<c>true</c> if the object is to be deleted
		///	</returns>
		public override bool OnCollide(CollisionPairEvaluator.Name collisonName, GameObject other)
		{
			// Purposefully does nothing
			bool willBeDeleted = false;
			return willBeDeleted;
		}
	}
}
