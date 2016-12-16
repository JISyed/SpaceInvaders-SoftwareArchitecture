using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.TimerSystem;

namespace SpaceInvaders.SceneSystem
{
	class GameOverDelayEvent : ICommandable
	{
		private TimedEvent timedEvent;
		private float gameOverDelay;

		//
		// Constructor
		//

		public GameOverDelayEvent()
		{
			// DATA SET
			this.gameOverDelay = 2.0f;

			this.timedEvent = TimedEventManager.Active.Create(this, this.gameOverDelay);
		}


		//
		// Destructor
		//

		~GameOverDelayEvent()
		{
			// Cancel event
			if(this.timedEvent != null)
			{
				TimedEventManager.Active.Recycle(this.timedEvent);
				this.timedEvent = null;
			}
		}




		//
		// Contracts
		//

		public void ExecuteCommand()
		{
			// Go to the next scene.
			// Refer to OnLoadNextScene() in P1Scene and P2Scene
			//     for more information.
			SceneManager.Self.ActiveScene.TransitionToNextScene();
		}



		//
		// Properties
		//

		/// <summary>
		///		The timed event of this delay
		/// </summary>
		public TimedEvent TimerEvent
		{
			get
			{
				return this.timedEvent;
			}
		}

		/// <summary>
		///		How long to delay the GameOver screen
		/// </summary>
		public float GameOverDelayTime
		{
			get
			{
				return this.gameOverDelay;
			}
		}
	}
}
