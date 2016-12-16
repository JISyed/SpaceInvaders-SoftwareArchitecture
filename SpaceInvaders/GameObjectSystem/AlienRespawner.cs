using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.GameObjectSystem.Factories;
using SpaceInvaders.GameObjectSystem.GameObjects;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.TimerSystem;

namespace SpaceInvaders.GameObjectSystem
{
	class AlienRespawner : ICommandable
	{
		private TimedEvent timedEvent;
		private float respawnWait;

		//
		// Constructors
		//
		public AlienRespawner()
		{
			// DATA SET
			this.respawnWait = 2.0f;


			// Add self to timer
			this.timedEvent = TimedEventManager.Active.Create(this, this.respawnWait);
		}


		//
		// Destructor
		//

		~AlienRespawner()
		{
			// Cancel event
			if (this.timedEvent != null)
			{
				TimedEventManager.Active.Recycle(this.timedEvent);
				this.timedEvent = null;
			}
		}



		//
		// Methods
		//

		public void ExecuteCommand()
		{
			// Increment the level
			GameSessionData.Active.IncrementLevel();

			// Create aliens
			AlienFactory factory = new AlienFactory(SpriteBatchSystem.SpriteBatch.Name.Aliens);
			factory.CreateAliens();

			// This thing is done
			this.timedEvent = null;
		}




		//
		// Properties
		//

		public TimedEvent TimerEvent
		{
			get
			{
				return this.timedEvent;
			}
		}
	}
}
