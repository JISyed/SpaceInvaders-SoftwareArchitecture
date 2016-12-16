using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ImageSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.AnimationSystem;
using SpaceInvaders.GameObjectSystem.GameObjects;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.GameObjectSystem.Factories
{
	public enum AlienName
	{
		Crab,
		Squid,
		Octopus
	}


	class AlienFactory
	{
		// References
		private SpriteBatch.Name batchName;
		

		////////////////////////////////////////////////
		//
		// Data
		public const int MaxDissent = 4;
		private readonly Azul.Color CollisonColor = Colors.Magenta;
		private readonly float FrameInterval = AlienCoordinator.OriginalInterval;
		private readonly uint Rows = 5u;
		private readonly uint Columns = 11u;
		private readonly float InitX = 80.0f;
		private readonly float InitY = 590.0f;
		private readonly float XGap = 50.0f;
		private readonly float YGap = 50.0f;
		//
		//
		////////////////////////////////////////////////



		//
		// Constructor
		//

		public AlienFactory(SpriteBatch.Name newBatchName)
		{
			this.batchName = newBatchName;
		}

		private AlienFactory()
		{
			// Do nothing
		}




		//
		// Methods
		//

		/// <summary>
		///		Load sprites and other resources needed for Aliens
		/// </summary>
		public void LoadResources()
		{
			this.LoadFlipAnimations();
			this.LoadSpriteAnimations();
		}

		/// <summary>
		///		Create 5 x 11 Aliens
		/// </summary>
		public void CreateAliens()
		{
			this.LoadGameObjects();
		}





		//
		// Private methods
		//

		/// <summary>
		///		Loads all the frame animators for the Aliens' SpriteEnties
		/// </summary>
		private void LoadSpriteAnimations()
		{
			// Frame animations
			AnimationFrameManager.Active.Create(Sprite.Name.AlienCrab, this.FrameInterval, true);
			AnimationFrameManager.Active.Create(Sprite.Name.AlienSquid, this.FrameInterval, true);
			AnimationFrameManager.Active.Create(Sprite.Name.AlienOctopus, this.FrameInterval, true);

			AnimationFrameManager.Active.Attach(Sprite.Name.AlienCrab, Image.Name.AlienCrab_B);
			AnimationFrameManager.Active.Attach(Sprite.Name.AlienCrab, Image.Name.AlienCrab_C);
			AnimationFrameManager.Active.Attach(Sprite.Name.AlienSquid, Image.Name.AlienSquid_D);
			AnimationFrameManager.Active.Attach(Sprite.Name.AlienSquid, Image.Name.AlienSquid_E);
			AnimationFrameManager.Active.Attach(Sprite.Name.AlienOctopus, Image.Name.AlienOctopus_F);
			AnimationFrameManager.Active.Attach(Sprite.Name.AlienOctopus, Image.Name.AlienOctopus_G);

			
		}

		/// <summary>
		///		Load the flip animations of the sprites
		/// </summary>
		private void LoadFlipAnimations()
		{
			// Flip animations (for lasers)
			AnimationFlip flipAni = null;

			flipAni = AnimationFlipManager.Active.Create(Sprite.Name.LaserAlienDagger, this.FrameInterval * 0.2f, true);
			flipAni.Attach(false, true);

			flipAni = AnimationFlipManager.Active.Create(Sprite.Name.LaserAlienZigzag, this.FrameInterval * 0.2f, true);
			flipAni.Attach(true, false);
		}

		/// <summary>
		///		Load the Alien GameObjects
		/// </summary>
		private void LoadGameObjects()
		{
			AlienName[] alienNames = new AlienName[this.Rows];
			alienNames[0] = AlienName.Squid;
			alienNames[1] = AlienName.Crab;
			alienNames[2] = AlienName.Crab;
			alienNames[3] = AlienName.Octopus;
			alienNames[4] = AlienName.Octopus;

			AlienCoordinator coordinator = new AlienCoordinator(Rows, Columns, InitX, InitY, XGap, YGap);

			// Add coordinator as root of collision checking
			this.AddAlienRootToCollisionSystem(coordinator);
			
			// Attach coordinator last to call it's Start event
			GameObjectManager.Active.Attach(coordinator);

			// Create all the Aliens (bottom row first)
			for (int i = (int) this.Rows-1; i >= 0; i--)
			{
				this.LoadAlienRow(alienNames[i],
								  this.Columns,
								  this.InitX,
								  this.InitY - this.YGap * (uint) i,
								  this.XGap,
								  coordinator
				);
			}

			// Calculate colliders of columns
			for(uint j = 0; j < this.Rows; j++)
			{
				coordinator.Columns[j].RecalculateCollider();
			}

			// Calculate collider of coodinator
			coordinator.RecalculateCollider();
			
		}


		private void LoadAlienRow(AlienName alienName, uint numOfAliens, float initX, float initY, float xSpacing, AlienCoordinator coordinator)
		{
			// Determine correct enum names
			GameObject.Name objectName = GameObject.Name.UNINITIALIZED;
			Sprite.Name spriteName = Sprite.Name.UNINITIALIZED;
			switch (alienName)
			{
				case AlienName.Crab:
					objectName = GameObject.Name.AlienCrab;
					spriteName = Sprite.Name.AlienCrab;
					break;
				case AlienName.Squid:
					objectName = GameObject.Name.AlienSquid;
					spriteName = Sprite.Name.AlienSquid;
					break;
				case AlienName.Octopus:
					objectName = GameObject.Name.AlienOctopus;
					spriteName = Sprite.Name.AlienOctopus;
					break;
				default:
					break;
			}

			// Use a loop to create a row of aliens
			// Each "i" is a column
			for(uint i = 0; i < numOfAliens; i++)
			{
				Aliens alien = this.LoadIndividualAlien(objectName, 
														spriteName,
														alienName,
														initX + xSpacing * i, 
														initY
				);
				coordinator.Columns[i].AddChild(alien);
			}
		}


		/// <summary>
		///		Create one Alien
		/// </summary>
		/// <param name="alienName"></param>
		/// <param name="alienId"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		private Aliens LoadIndividualAlien(GameObject.Name objectName, Sprite.Name spriteName, AlienName alienName, float x, float y)
		{
			// Aliens spawn on y differently depending on level
			int currentLevel = GameSessionData.Active.Level;
			float yOffset = (currentLevel % MaxDissent) * this.YGap; 

			Aliens newAlien = new Aliens(alienName);
			newAlien.SetName(objectName);
			newAlien.SetPosition(x, y - yOffset);
			newAlien.SetColor(Colors.White);
			newAlien.Collider.Color = this.CollisonColor;
			newAlien.SetSprite(this.batchName, SpriteEntityManager.Self.Find(spriteName));
			GameObjectManager.Active.Attach(newAlien);
			return newAlien;
		}

		/// <summary>
		///		Add the given root as the root of collision checking on behalf of all aliens
		/// </summary>
		/// <param name="newRoot"></param>
		private void AddAlienRootToCollisionSystem(AlienCoordinator newRoot)
		{
			CollisionPairEvaluator collisonPair = null;

			// Alien collisions =================

			// Wall Vs Alien
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_Alien);
			collisonPair.SetSecondCollisonRoot(newRoot);

			// PlayerLaser vs Alien
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Alien);
			collisonPair.SetSecondCollisonRoot(newRoot);

			// Player vs Alien
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Player_vs_Alien);
			collisonPair.SetSecondCollisonRoot(newRoot);

			// Alien vs Shield
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Alien_vs_Shield);
			collisonPair.SetFirstCollisonRoot(newRoot);


			// AlienLaser collisions ==============

			GameObject alienLaserRoot = newRoot.AlienLaserRoot;

			// PlayerLaser vs AlienLaser
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser);
			collisonPair.SetSecondCollisonRoot(alienLaserRoot);

			// Player vs AlienLaser
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Player_vs_AlienLaser);
			collisonPair.SetSecondCollisonRoot(alienLaserRoot);

			// AlienLaser vs Shields
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.AlienLaser_vs_Shield);
			collisonPair.SetFirstCollisonRoot(alienLaserRoot);

			// AlienLaser vs WallBottom
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.AlienLaser_vs_WallBottom);
			collisonPair.SetFirstCollisonRoot(alienLaserRoot);

		}


	}
}
