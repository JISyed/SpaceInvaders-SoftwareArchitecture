using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ImageSystem;

namespace SpaceInvaders.SpriteSystem
{
	/// <summary>
	///		Creates all SpriteEntites, but also creates all SpriteCollisions
	/// </summary>
	class SpriteEntityFactory
	{
		//
		// Data
		//

		// Player
		private readonly float PlayerFactoryScale = 0.6f;
		private readonly float ScalePlayerLaser = 0.75f;
		private readonly float ScalePlayer = 1.17f;
		private readonly float DeadPlayerScaleAdjustment = 0.9f;
		private readonly float AdditionalPlayerLaserScaleX = 0.75f;
		private readonly float PlayerLivesIconScale = 0.7f;

		// Blocks
		private readonly float BlockScale = 0.55f;

		// Aliens
		private readonly float AlienScale = 0.4f;
		private readonly float DeadAlienScaleAdjustment = 1.2f;
		private readonly float AlienLaserScaleX = 0.7f;
		private readonly float AlienLaserScaleY = 1.05f;

		// UFO
		private readonly float UFOScale = 0.4f;

		// Floor
		private readonly float FloorY = 45.0f;
		private readonly float FloorScaleX = 350.0f;
		private readonly float FloorScaleY = 1.5f;
		private readonly Azul.Color FloorColor = Colors.Green;


		//
		// Constructor
		//
		public SpriteEntityFactory()
		{
		}



		//
		// Methods
		//

		/// <summary>
		///		Creates all the SpriteEntities and SpriteCollisions ever neeed.
		///		This should call only once!
		/// </summary>
		public void Create()
		{
			SpriteEntity sprRef = null;

			// Shields
			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.ShieldPiece, Image.Name.Block);
			sprRef.ScaleX = this.BlockScale;
			sprRef.ScaleY = this.BlockScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.ShieldPiece);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.ShieldPieceUL, Image.Name.BlockUL);
			sprRef.ScaleX = this.BlockScale;
			sprRef.ScaleY = this.BlockScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.ShieldPieceUL);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.ShieldPieceUR, Image.Name.BlockUR);
			sprRef.ScaleX = this.BlockScale;
			sprRef.ScaleY = this.BlockScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.ShieldPieceUR);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.ShieldPieceBL, Image.Name.BlockBL);
			sprRef.ScaleX = this.BlockScale;
			sprRef.ScaleY = this.BlockScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.ShieldPieceBL);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.ShieldPieceBR, Image.Name.BlockBR);
			sprRef.ScaleX = this.BlockScale;
			sprRef.ScaleY = this.BlockScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.ShieldPieceBR);


			// Player
			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.Player, Image.Name.Player_W);
			sprRef.ScaleX = this.PlayerFactoryScale * this.ScalePlayer;
			sprRef.ScaleY = this.PlayerFactoryScale * this.ScalePlayer;
			SpriteCollisionManager.Self.Create(Sprite.Name.Player);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.LaserPlayer, Image.Name.LaserStraight_Alpha);
			sprRef.ScaleX = this.PlayerFactoryScale * this.ScalePlayerLaser * this.AdditionalPlayerLaserScaleX;
			sprRef.ScaleY = this.PlayerFactoryScale * this.ScalePlayerLaser;
			SpriteCollisionManager.Self.Create(Sprite.Name.LaserPlayer);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.DeadPlayer, Image.Name.DeadPlayer1_X);
			sprRef.ScaleX = this.PlayerFactoryScale * this.ScalePlayer * this.DeadPlayerScaleAdjustment;
			sprRef.ScaleY = this.PlayerFactoryScale * this.ScalePlayer * this.DeadPlayerScaleAdjustment;
			SpriteCollisionManager.Self.Create(Sprite.Name.DeadPlayer);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.SmallExplosionMissile, Image.Name.SmallExplosion_Delta);
			sprRef.ScaleX = this.PlayerFactoryScale;
			sprRef.ScaleY = this.PlayerFactoryScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.SmallExplosionMissile);
			
			// Player Lives icon
			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.HUD_Player, Image.Name.Player_W);
			sprRef.ScaleX = this.PlayerLivesIconScale;
			sprRef.ScaleY = this.PlayerLivesIconScale;

			// Aliens
			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.AlienCrab, Image.Name.AlienCrab_B);
			sprRef.ScaleX = this.AlienScale;
			sprRef.ScaleY = this.AlienScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.AlienCrab);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.AlienSquid, Image.Name.AlienSquid_D);
			sprRef.ScaleX = this.AlienScale;
			sprRef.ScaleY = this.AlienScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.AlienSquid);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.AlienOctopus, Image.Name.AlienOctopus_G);
			sprRef.ScaleX = this.AlienScale;
			sprRef.ScaleY = this.AlienScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.AlienOctopus);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.DeadAlien, Image.Name.DeadAlien_Z);
			sprRef.ScaleX = this.AlienScale * this.DeadAlienScaleAdjustment;
			sprRef.ScaleY = this.AlienScale * this.DeadAlienScaleAdjustment;
			SpriteCollisionManager.Self.Create(Sprite.Name.DeadAlien);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.SmallExplosionBomb, Image.Name.SmallExplosion_Delta);
			sprRef.ScaleX = this.AlienScale;
			sprRef.ScaleY = this.AlienScale;
			sprRef.SetColor(Colors.Green);
			SpriteCollisionManager.Self.Create(Sprite.Name.SmallExplosionBomb);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.LaserAlienStraight, Image.Name.LaserStraight_Alpha);
			sprRef.ScaleX = this.AlienScale * this.AlienLaserScaleX;
			sprRef.ScaleY = this.AlienScale * this.AlienLaserScaleY;
			SpriteCollisionManager.Self.Create(Sprite.Name.LaserAlienStraight);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.LaserAlienDagger, Image.Name.LaserDagger_Beta);
			sprRef.ScaleX = this.AlienScale * this.AlienLaserScaleX;
			sprRef.ScaleY = this.AlienScale * this.AlienLaserScaleY;
			SpriteCollisionManager.Self.Create(Sprite.Name.LaserAlienDagger);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.LaserAlienZigzag, Image.Name.LaserZigzag_Y);
			sprRef.ScaleX = this.AlienScale * this.AlienLaserScaleX;
			sprRef.ScaleY = this.AlienScale * this.AlienLaserScaleY;
			SpriteCollisionManager.Self.Create(Sprite.Name.LaserAlienZigzag);


			// UFO
			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.DeadUFO, Image.Name.SmallExplosion_Delta);
			sprRef.ScaleX = this.UFOScale;
			sprRef.ScaleY = this.UFOScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.DeadUFO);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.UFO, Image.Name.UFO_V);
			sprRef.ScaleX = this.UFOScale;
			sprRef.ScaleY = this.UFOScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.UFO);

			sprRef = SpriteEntityManager.Self.Create(Sprite.Name.UFOMissile, Image.Name.UFOMissile_Gamma);
			sprRef.ScaleX = this.UFOScale;
			sprRef.ScaleY = this.UFOScale;
			SpriteCollisionManager.Self.Create(Sprite.Name.UFOMissile);



			// Floor
			SpriteEntity floorSpr = SpriteEntityManager.Self.Create(Sprite.Name.Floor, Image.Name.Floor);
			floorSpr.SetPosition(Constants.SCREEN_CENTER_X, this.FloorY);
			floorSpr.ScaleX = this.FloorScaleX;
			floorSpr.ScaleY = this.FloorScaleY;
			floorSpr.SetColor(this.FloorColor);

		}
	}
}
