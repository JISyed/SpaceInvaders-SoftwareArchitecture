using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.SpriteBatchSystem
{
	class BatchFactory
	{
		//
		// Constructor
		//

		public BatchFactory()
		{
			
		}



		//
		// Methods
		//

		/// <summary>
		///		Creates the SpriteBatches. Control the ordering here.
		///		Never create a SpriteCollisions Batch. It's automatically made beforehand
		/// </summary>
		/// <remarks>
		///		Hint: the first batch made renders on top of everyone else
		/// </remarks>
		public void CreateBatches()
		{
			SpriteBatchManager.Active.Create(SpriteBatch.Name.HUD);
			SpriteBatchManager.Active.Create(SpriteBatch.Name.Aliens);
			SpriteBatchManager.Active.Create(SpriteBatch.Name.Explosions);
			SpriteBatchManager.Active.Create(SpriteBatch.Name.Player);
			SpriteBatchManager.Active.Create(SpriteBatch.Name.Lasers);
			SpriteBatchManager.Active.Create(SpriteBatch.Name.Shields);
		}
	}
}
