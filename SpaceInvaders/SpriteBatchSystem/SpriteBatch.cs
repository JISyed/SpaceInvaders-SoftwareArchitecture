using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.SpriteBatchSystem
{
	sealed class SpriteBatch : Container
	{
		private SpriteBatch.Name name;
		private bool enabled;

		// Constructor
		public SpriteBatch() : base()
		{
			this.name = SpriteBatch.Name.UNINITIALIZED;
			this.enabled = true;
		}



		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Attaches a sprite to the batch using a batch element
		/// </summary>
		/// <param name="newSprite"></param>
		/// <param name="newId"></param>
		/// <returns></returns>
		public SpriteBatchElement Attach(Sprite newSprite, uint newId)
		{
			SpriteBatchElement newElement = this.BaseAttach(newId) as SpriteBatchElement;
			newElement.SetSprite(newSprite);
			return newElement;
		}

		/// <summary>
		///		Detaches a sprite from the batch
		/// </summary>
		/// <param name="oldName"></param>
		/// <param name="oldId"></param>
		/// <returns></returns>
		public bool Detach(Sprite.Name oldName, uint oldId)
		{
			SpriteBatchElement oldNode = this.BaseDetach(oldName, oldId) as SpriteBatchElement;
			if (oldNode == null) return false;
			oldNode.Reset();
			return true;
		}

		/// <summary>
		///		Finds a batch element by its sprite's name and id
		/// </summary>
		/// <param name="name"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public SpriteBatchElement Find(Sprite.Name name, uint id)
		{
			return this.BaseFind(name, id, this.activeList) as SpriteBatchElement;
		}

		/// <summary>
		///		Sets the enum name for the batch
		/// </summary>
		/// <param name="newName"></param>
		public void SetName(SpriteBatch.Name newName)
		{
			this.name = newName;
		}

		/// <summary>
		///		Draws all sprites in the batch
		/// </summary>
		public void Draw()
		{
			SpriteBatchElement itr = this.activeList.Head as SpriteBatchElement;
			while(itr != null)
			{
				// Draw one sprite
				itr.Draw();

				// Next node
				itr = itr.next as SpriteBatchElement;
			}
		}




		///////////////////////////////////////////////////////
		//
		// Private Methods
		//
		///////////////////////////////////////////////////////




		///////////////////////////////////////////////////////
		// 
		// Contracts
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///		Clears the data in the SpriteBatch
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			this.name = SpriteBatch.Name.UNINITIALIZED;
			this.Destroy();
		}

		/// <summary>
		///		Used for AbstractManager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return this.name;
		}

		// Prefills the pool with the given number of elements
		protected override void FillReserve(int fillSize)
		{
			for (int i = fillSize; i > 0; i--)
			{
				SpriteBatchElement newNode = new SpriteBatchElement();
				this.reservedList.PushFront(newNode);
			}
		}




		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///		The name of the sprite batch
		/// </summary>
		public SpriteBatch.Name BatchName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>
		///		Checks if the SpriteBatch will draw
		/// </summary>
		public bool IsEnabled
		{
			get
			{
				return this.enabled;
			}

			set
			{
				this.enabled = value;
			}
		}




		///////////////////////////////////////////////////////
		// 
		// Nested Enums
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///		The possible names of a SpriteBatch
		/// </summary>
		public enum Name
		{
			UNINITIALIZED,
			Aliens,
			Player,
			Shields,
			Lasers,
			Explosions,
			SpriteCollisions,	// Automatically created!
			HUD
		}
	}
}
