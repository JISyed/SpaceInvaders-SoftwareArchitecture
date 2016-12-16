using Debug = System.Diagnostics.Debug;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.AudioSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class PlayerLaser : GameObject
	{
		private static Azul.Color initialColor = Colors.White;

		private AudioSource shootingSound;
		private float speed = 13.0f;

		//
		// Constructor
		//

		private PlayerLaser() : base()
		{
			// Not in use
		}

		public PlayerLaser(float initX, float initY)
			: base()
		{
			this.SetName(Name.LaserPlayer);
			this.SetPosition(initX, initY);
			this.SetColor(initialColor);
			this.Collider.Color = Colors.Yellow;
			this.SetSprite(SpriteBatch.Name.Player, SpriteEntityManager.Self.Find(Sprite.Name.LaserPlayer));
			this.shootingSound = AudioSourceManager.Self.Find(AudioSource.Name.LaserPlayer);
			GameObjectManager.Active.Attach(this);
			
			// Add to PlayerLaser root
			GameScene scene = SceneManager.Self.ActiveScene as GameScene;
			Debug.Assert(scene != null, "The currently active scene is not a GameScene!");
			scene.PlayerLaserRoot.AddChild(this);
		}




		//
		// Contracts
		//

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{
			this.shootingSound.Play();
		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			this.SetPosition(this.X, this.Y + this.speed);

			bool keepObjectAlive = true;
			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{
			this.shootingSound = null;
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

			// Laser vs. Aliens
			if(collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_Alien)
			{
				willBeDeleted = true;
			}
			// Laser vs. Top Wall
			else if(collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_WallTop)
			{
				willBeDeleted = true;

				// Create small explosion
				DeadPlayerLaser smallExplosion = new DeadPlayerLaser(this.X, this.Y);
			}
			// Laser vs Shield
			else if(collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_Shield)
			{
				willBeDeleted = true;

				// Create small explosion
				DeadPlayerLaser smallExplosion = new DeadPlayerLaser(this.X, this.Y);
				smallExplosion.SetColor(Colors.Green);
			}
			// Laser vs. UFO
			else if (collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_UFO)
			{
				willBeDeleted = true;
			}
			// PlayerLaser vs AlienLaser
			else if (collisonName == CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser)
			{
				willBeDeleted = true;

				// Create small explosion
				DeadPlayerLaser smallExplosion = new DeadPlayerLaser(this.X, this.Y);
				smallExplosion.SetColor(Colors.White);
			}

			return willBeDeleted;
		}


	}
}
