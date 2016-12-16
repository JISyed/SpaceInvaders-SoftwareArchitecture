using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjectSystem.Factories;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class ShieldBarricade : GameObject
	{
		// References
		private ShieldPieceColumn[] blockColumns;

		////////////////////////////////////////////////
		//
		// Data
		private readonly uint NumColumns = 9u;
		private readonly uint NumRows = 7u;
		//
		//
		////////////////////////////////////////////////



		//
		// Constructors
		//

		private ShieldBarricade() : base()
		{
			// Not in use
		}

		public ShieldBarricade(float newX, float newY) 
			: base()
		{
			this.SetName(Name.ShieldBarricade);
			this.Collider.Color = Colors.Red;
			this.SetPosition(newX, newY);
			this.CreateColumns();
			this.AssembleBlocks();
		}



		//
		// Methods
		//




		//
		// Private Methods
		//

		/// <summary>
		///		Initialize the ShieldPieceColumn objects
		/// </summary>
		private void CreateColumns()
		{
			this.blockColumns = new ShieldPieceColumn[this.NumColumns];
			for(uint i = 0u; i < this.NumColumns; i++)
			{
				this.blockColumns[i] = new ShieldPieceColumn();
				this.blockColumns[i].SetPosition(this.X + 10f * i, this.Y);
				this.AddChild(this.blockColumns[i]);
				GameObjectManager.Active.Attach(this.blockColumns[i]);
			}
		}


		/// <summary>
		///		Construct the barricade out of little blocks
		/// </summary>
		private void AssembleBlocks()
		{
			SpriteEntity centerBlock = SpriteEntityManager.Self.Find(Sprite.Name.ShieldPiece);
			SpriteEntity upperLeftBlock = SpriteEntityManager.Self.Find(Sprite.Name.ShieldPieceUL);
			SpriteEntity upperRightBlock = SpriteEntityManager.Self.Find(Sprite.Name.ShieldPieceUR);
			SpriteEntity bottomLeftBlock = SpriteEntityManager.Self.Find(Sprite.Name.ShieldPieceBL);
			SpriteEntity bottomRightBlock = SpriteEntityManager.Self.Find(Sprite.Name.ShieldPieceBR);
			SpriteEntity blockSprite = null;

			// Spawn all the blocks
			for(uint i = 0u; i < this.NumColumns; i++)
			{
				for(uint j = 0u; j < this.NumRows; j++)
				{
					if(j == 0u && i > 1u && i < 7u)
					{
						// Skip bottom middle 5 blocks
						continue;
					}
					else if(j == 1u && i > 2u && i < 6u)
					{
						// Skip second-bottom middle 3 blocks
						continue;
					}
					else if(j == 6u && (i == 0u || i == 8u))
					{
						// Skip the corners of the top row
						continue;
					}
					else if((j == 5u && i == 0u) || (j == 6u && i == 1u))
					{
						// top left corner blocks
						blockSprite = upperLeftBlock;
					}
					else if((j == 5u && i == 8u) || (j == 6u && i == 7u))
					{
						// top right corner blocks
						blockSprite = upperRightBlock;
					}
					else if(j == 1 && i == 2)
					{
						// bottom right blocks
						blockSprite = bottomRightBlock;
					}
					else if(j == 1 && i == 6)
					{
						// bottom left blocks
						blockSprite = bottomLeftBlock;
					}
					else
					{
						// Center block
						blockSprite = centerBlock;
					}

					ShieldPiece block = new ShieldPiece(blockSprite);
					block.SetPosition(this.X + block.Width * i, this.Y + block.Height * j);
					this.blockColumns[i].AddChild(block);
					GameObjectManager.Active.Attach(block);
				}
			}
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

			// Remove this barricade if no more columns left
			if(this.Child == null)
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
