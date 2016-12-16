using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SpriteBatchSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class ShieldPiece : GameObject
	{
		//
		// Constructors
		//

		private ShieldPiece() : base()
		{
			// Not in use
		}

		public ShieldPiece(SpriteEntity blockSprite) : base()
		{
			this.SetName(Name.ShieldPiece);
			this.Collider.Color = Colors.Magenta;
			this.SetColor(Colors.Green);
			this.SetSprite(SpriteBatch.Name.Shields, blockSprite);
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

			// PlayerLaser vs Shield
			if(collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_Shield)
			{
				willBeDeleted = true;
			}
			// Alien vs Shield
			else if(collisonName == CollisionPairEvaluator.Name.Alien_vs_Shield)
			{
				willBeDeleted = true;
			}
			// AlienLaser vs Shield
			else if(collisonName == CollisionPairEvaluator.Name.AlienLaser_vs_Shield)
			{
				willBeDeleted = true;
			}

			return willBeDeleted;
		}
	}
}
