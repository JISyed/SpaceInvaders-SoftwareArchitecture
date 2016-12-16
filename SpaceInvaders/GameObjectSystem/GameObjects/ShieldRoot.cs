using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.CollisionSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class ShieldRoot : GameObject
	{
		//
		// Constructors
		//

		public ShieldRoot() : base()
		{
			this.SetName(Name.ShieldRoot);
			this.Collider.Color = Colors.Cyan;
		}

		




		//
		// Contracts
		//

		/// <summary>
		///		Start method
		/// </summary>
		public override void Start()
		{

		}

		/// <summary>
		///		The update loop routine
		/// </summary>
		public override bool Update()
		{
			bool keepObjectAlive = true;
			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine (after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{

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
			bool willBeDeleted = false;

			return willBeDeleted;
		}
	}
}
