using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.SpriteSystem
{
	// Same as Keenan's "SpriteBase"
	abstract class Sprite : ListNode
	{
		private Sprite.Name name;
		private Azul.Color color;
		private float x;
		private float y;



		//
		// Constructors
		//

		public Sprite() : base()
		{
			this.name = Sprite.Name.UNINITIALIZED;
			this.color = Colors.White;
			this.x = 0.0f;
			this.y = 0.0f;
		}
		


		//
		// Methods
		//

		/// <summary>
		///		Sets the enum name of the sprite. For manager use.
		/// </summary>
		/// <param name="newName"></param>
		virtual public void SetName(Sprite.Name newName)
		{
			this.name = newName;
		}

		/// <summary>
		///		Clears base sprite data
		/// </summary>
		virtual protected void SpriteReset()
		{
			this.name = Sprite.Name.UNINITIALIZED;
			this.color = Colors.White;
			this.x = 0.0f;
			this.y = 0.0f;
		}

		/// <summary>
		///		Sets the color of the sprite
		/// </summary>
		/// <param name="newColor"></param>
		public void SetColor(Azul.Color newColor)
		{
			this.Color = newColor;
		}

		/// <summary>
		///		Sets the position of the sprite
		/// </summary>
		/// <param name="newX"></param>
		/// <param name="newY"></param>
		public void SetPosition(float newX, float newY)
		{
			this.X = newX;
			this.Y = newY;
		}



		//
		// Contracts
		//

		/// <summary>
		///		Somehow updates internal data before drawing
		/// </summary>
		abstract public void UpdateInternalData();

		/// <summary>
		///		Draws an individual sprite
		/// </summary>
		abstract public void Draw();




		//
		// Properties
		//

		/// <summary>
		///		The enum name of the sprite
		/// </summary>
		public Sprite.Name SpriteName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>
		///		The x coordinate of the sprite in world space
		/// </summary>
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>
		///		The y coordinate of the sprite in world space
		/// </summary>
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>
		///		The normalized RGB color of the sprite
		/// </summary>
		public Azul.Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}




		//
		// Nested Enums
		//

		/// <summary>
		///		Possible names of a Sprite
		/// </summary>
		public enum Name
		{
			UNINITIALIZED,
			NULL,
			AlienCrab,
			AlienSquid,
			AlienOctopus,
			UFO,
			Player,
			LaserAlienZigzag,
			LaserAlienStraight,
			LaserAlienDagger,
			UFOMissile,
			LaserPlayer,
			DeadPlayer,
			DeadAlien,
			DeadUFO,
			SmallExplosionMissile,	// PlayerLaser
			SmallExplosionBomb, // AlienLaser
			Floor,
			ShieldPiece,
			ShieldPieceUL,
			ShieldPieceUR,
			ShieldPieceBL,
			ShieldPieceBR,
			HUD_Player
		}
	}
}
