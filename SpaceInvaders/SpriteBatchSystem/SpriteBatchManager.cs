using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.SpriteBatchSystem
{
	sealed class SpriteBatchManager : AbstractManager
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static SpriteBatchManager Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.ManagerForSpriteBatch;
			}
		}


		///////////////////////////////////////////////////////
		//
		// Manager Data
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Make a manager WITH preallocation
		/// </summary>
		/// <param name="reserveSize"></param>
		/// <param name="reserveAddition"></param>
		public SpriteBatchManager(int reserveSize, int reserveAddition)
			: base(reserveSize, reserveAddition)
		{
			// Create CollisionSprite Batch now
			this.Create(SpriteBatch.Name.SpriteCollisions);
		}


        ///////////////////////////////////////////////////////
        //
        // Methods
        //
        ///////////////////////////////////////////////////////

		/// <summary>
		///		Initialize a sprite batch of the given name
		/// </summary>
		/// <param name="newName"></param>
		/// <returns></returns>
		public SpriteBatch Create(SpriteBatch.Name newName)
		{
			SpriteBatch newBatch = this.BaseCreate() as SpriteBatch;
			newBatch.SetName(newName);
			return newBatch;
		}

		/// <summary>
		///		Recycle the entire sprite batch for later pooling
		/// </summary>
		/// <param name="oldName"></param>
		/// <returns></returns>
		public bool Recycle(SpriteBatch.Name oldName)
		{
			SpriteBatch oldBatch = this.BaseRecycle(oldName) as SpriteBatch;
			if (oldBatch == null) return false;
			oldBatch.Reset();
			return true;
		}

		/// <summary>
		///		Finds a sprite batch by name and returns it
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public SpriteBatch Find(SpriteBatch.Name name)
		{
			if (name == SpriteBatch.Name.UNINITIALIZED)
				return null;
			return this.BaseFind(name, this.activeList) as SpriteBatch;
		}


		/// <summary>
		///		Convenience method to add a sprite to a sprite batch.
		///		The batch has to actually exist for this to work!
		/// </summary>
		/// <param name="batchName"></param>
		/// <param name="newSprite"></param>
		/// <returns></returns>
		public bool Attach(SpriteBatch.Name batchName, Sprite newSprite)
		{
			SpriteBatch batch = this.Find(batchName) as SpriteBatch;
			if (batch == null) return false;
			batch.Attach(newSprite, newSprite.Id);
			return true;
		}


		/// <summary>
		///		Draw all sprites from all sprite batches
		/// </summary>
		public void Draw()
		{
			SpriteBatch itr = this.activeList.Head as SpriteBatch;
			while(itr != null)
			{
				// Draw one sprite batch
				if (itr.IsEnabled)
				{
					itr.Draw();
				}

				// Next node
				itr = itr.next as SpriteBatch;
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

		protected override void FillReserve(int fillSize)
		{
			for (int i = fillSize; i > 0; i--)
			{
				SpriteBatch newNode = new SpriteBatch();
				newNode.Preallocate(4, 4);
				this.reservedList.PushFront(newNode);
			}
		}


		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////



	}
}
