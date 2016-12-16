using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.CollisionSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class ShieldPieceColumn : GameObject
	{
		//
		// Constructors
		//

		public ShieldPieceColumn() : base()
		{
			this.SetName(Name.ShieldPieceColumn);
			this.Collider.Color = Colors.Yellow;
		}




		//
		// Methods
		//




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

			// Remove this columns if there are no more blocks
			if(this.Collider.IsNull)
			{
				keepObjectAlive = false;
			}

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
