using Debug = System.Diagnostics.Debug;
using Enum = System.Enum;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class AlienLaser : GameObject
	{
		private static Azul.Color initialColor = Colors.White;

		private float speed = 3.95f;


		//
		// Constructor
		//

		private AlienLaser() : base()
		{
			// Not used
		}

		public AlienLaser(float initX, float initY, GameObject laserRoot) 
			: base()
		{
			this.SetName(Name.LaserAlien);
			this.SetPosition(initX, initY);
			this.SetColor(initialColor);
			this.Collider.Color = Colors.Magenta;

			// Decide randomly which sprite to choose
			int random = Randomizer.RandomInt(3);
			Sprite.Name newSpriteName = Sprite.Name.UNINITIALIZED;
			switch(random)
			{
				case 0:
					newSpriteName = Sprite.Name.LaserAlienZigzag;
					break;
				case 1:
					newSpriteName = Sprite.Name.LaserAlienStraight;
					break;
				case 2:
					newSpriteName = Sprite.Name.LaserAlienDagger;
					break;
				default:
					Debug.Assert(false, "AlienLaser: Randomizer choose a number out of range!");
					break;
			}
			this.SetSprite(SpriteBatch.Name.Aliens, SpriteEntityManager.Self.Find(newSpriteName));

			GameObjectManager.Active.Attach(this);
			laserRoot.AddChild(this);
		}

		//
		// Contracts
		//

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{

		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			this.SetPosition(this.X, this.Y - this.speed);

			bool keepObjectAlive = true;
			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
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

			// PlayerLaser vs AlienLaser
			if(collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser)
			{
				willBeDeleted = true;
			}
			// Player vs AlienLaser
			else if(collisonName == CollisionPairEvaluator.Name.Player_vs_AlienLaser)
			{
				willBeDeleted = true;

				// Create small explosion
				DeadPlayerLaser smallExplosion = new DeadPlayerLaser(this.X, this.Y);
				smallExplosion.SetColor(Colors.Green);

				// Move up. This is important!
				// Removing this line will make the Aliens rain laser everywhere!
				GameObject parent = this.Parent as GameObject;
				parent.SetPosition(parent.X, parent.Y + this.Height);
			}
			// AlienLaser vs Shields
			else if (collisonName == CollisionPairEvaluator.Name.AlienLaser_vs_Shield)
			{
				willBeDeleted = true;

				// Create small explosion
				DeadPlayerLaser smallExplosion = new DeadPlayerLaser(this.X, this.Y);
				smallExplosion.SetColor(Colors.Green);
			}
			// AlienLaser vs WallBottom
			else if (collisonName == CollisionPairEvaluator.Name.AlienLaser_vs_WallBottom)
			{
				willBeDeleted = true;

				// Create small explosion
				DeadPlayerLaser smallExplosion = new DeadPlayerLaser(this.X, this.Y);
				smallExplosion.SetColor(Colors.Green);
			}


			return willBeDeleted;
		}
	}
}
