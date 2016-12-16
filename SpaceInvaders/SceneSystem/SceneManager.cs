using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using Enum = System.Enum;
using SpaceInvaders.MemoryJunkyardSystem;
using SpaceInvaders.AudioSystem;


namespace SpaceInvaders.SceneSystem
{
	class SceneManager : AbstractManager
	{
		///////////////////////////////////////////////////////
		//
		// Singleton stuff
		//
		///////////////////////////////////////////////////////

		private static SceneManager instance = null;

		/// <summary>
		///		Singleton Instance
		/// </summary>
		public static SceneManager Self
		{
			get
			{
				if (instance == null)
				{
					// Create the manager
					SceneManager.instance = new SceneManager();
				}

				return SceneManager.instance;
			}
		}


		///////////////////////////////////////////////////////
		//
		// Manager Data
		//
		///////////////////////////////////////////////////////

		private Scene activeScene;
		private NumberOfPlayers totalNumberOfPlayers;
		private Azul.AZUL_KEYS colliderToggleKey;
		private float internalClock;
		private int globalHighScore;
		private int cachedPlayerOneScore;
		private int cachedPlayerTwoScore;

		// Private Constructor
		private SceneManager()
			: base()
        {
			this.activeScene = null;
			this.totalNumberOfPlayers = NumberOfPlayers.One;
			this.colliderToggleKey = Azul.AZUL_KEYS.KEY_D;
			this.internalClock = 0.0f;
			this.globalHighScore = 0;
			this.cachedPlayerOneScore = 0;
			this.cachedPlayerTwoScore = 0;
        }





		///////////////////////////////////////////////////////
		//
		// Manager Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Attaches an already created Scene to the manager
		/// </summary>
		/// <param name="newScene"></param>
		public void AttachScene(Scene newScene)
		{
			SceneHolder newHolder = this.BaseCreate() as SceneHolder;
			newHolder.SetScene(newScene);
		}

		/// <summary>
		///		Removes a Scene from the manager, given the scene's name
		/// </summary>
		/// <param name="oldName"></param>
		/// <returns></returns>
		public bool UnloadScene(Scene.Name oldName)
		{
			SceneHolder oldHolder = this.BaseRecycle(oldName) as SceneHolder;
			if (oldHolder == null) return false;
			
			// Make the active scene null if it's being removed
			//if(this.activeScene == oldHolder.SceneRef)
			//{
			//	this.activeScene = null;
			//}
			
			// Reset the holder, which unloads the Scene internally
			oldHolder.Reset();
			AudioSourceManager.Self.StopAllAudio();

			// Good time to call Garbage collector
			MemoryJunkyard.Self.RecycleYard();

			return true;
		}

		/// <summary>
		///		Finds a Scene in the manager and returns it
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Scene FindScene(Scene.Name name)
		{
			Debug.Assert(name != Scene.Name.UNINITIALIZED, "Trying to find an unitialized Scene!");

			SceneHolder holder = this.BaseFind(name, this.activeList) as SceneHolder;
			if (holder == null) return null;
			return holder.SceneRef;
		}



		///////////////////////////////////////////////////////
		//
		// Other Public Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Set the key that should toggle the render of collision boxes.
		///		You must go to InputKeyMap.CreateAllInputKeys() for supported Keys!
		/// </summary>
		/// <param name="newKey"></param>
		public void SetColliderToggleKey(Azul.AZUL_KEYS newKey)
		{
			this.colliderToggleKey = newKey;
		}

		/// <summary>
		///		Update everything in the current active Scene
		/// </summary>
		/// <param name="azulClockTime"></param>
		public void Update(float azulClockTime)
		{
			this.internalClock = azulClockTime;

			// Update only the currently active scene
			this.activeScene.Update(azulClockTime);

			// Determine if this scene requested a scene change
			if(this.activeScene.IsMarkedForSceneChange)
			{
				// Perform a scene change
				this.activeScene.OnLoadNextScene();
			}
		}

		/// <summary>
		///		Draw everything in the current active Scene
		/// </summary>
		public void Draw()
		{
			// Draw only the currently active Scene
			this.activeScene.Draw();
		}

		/// <summary>
		///		Load an already attached Scene. Makes it the active Scene.
		///		Removes the old active scene.
		/// </summary>
		/// <param name="sceneName"></param>
		public void LoadScene(Scene.Name newScenesName)
		{
			// Find the new scene
			Scene newScene = this.FindScene(newScenesName);
			Debug.Assert(newScene != null, "The Scene could not be found upon loading a scene by name!");

			// Unload the old active scene if any
			if(activeScene != null)
			{
				bool wasRemoved = this.UnloadScene(ActiveSceneName);
				Debug.Assert(wasRemoved, "Upon loading a new Scene, the old Scene was not successfuly removed!");
				this.activeScene = null;
			}

			// Make the new scene the active scene
			this.activeScene = newScene;
			Debug.Assert(newScene.IsPaused == false, "Loading a new Scene should not be paused!");
			this.activeScene.LoadScene();
		}

		/// <summary>
		///		Load the given Scene. Automatically attaches it and make it active.
		///		Removes the old active scene.
		/// </summary>
		/// <param name="newScene"></param>
		public void LoadScene(Scene newScene)
		{
			// Attach it first
			this.AttachScene(newScene);

			// Load it
			this.LoadScene(newScene.SceneName);
		}

		/// <summary>
		///		Pauses the current scene and makes the given scene the new 
		///		active scene. It MUST be already attached to the manager.
		///		Does NOT remove the old active scene.
		///		If the new scene is unloaded, it will be automatically loaded.
		/// </summary>
		/// <param name="newScenesName"></param>
		public void SwapScene(Scene.Name newScenesName)
		{
			Scene newScene = this.FindScene(newScenesName);
			Debug.Assert(newScene != null, "The Scene could not be found upon swapping a scene!");

			// Pause the old Scene
			Debug.Assert(this.activeScene != null, "Swapping a Scene requires an Active Scene to exist!");
			this.activeScene.PauseScene();
			AudioSourceManager.Self.StopAllAudio();
			
			// Delete the old Scene if it was marked for deletion
			if(this.activeScene.IsMarkedForRemoval)
			{
				this.UnloadScene(this.ActiveSceneName);
			}
			this.activeScene = null;

			// Load or unpause the new Scene
			this.activeScene = newScene;
			if(newScene.IsLoaded)
			{
				if (newScene.IsPaused)
				{
					this.activeScene.UnpauseScene(this.AzulClockTime);
				}
			}
			else
			{
				this.activeScene.LoadScene();
			}
		}
		
		/// <summary>
		///		Given a new final score, it will be set as the new 
		///		highscore and return true.
		///		Otherwise, the highscore will not change and return false.
		/// </summary>
		/// <param name="newScore"></param>
		/// <returns></returns>
		public bool DetermineNewHighscore(int newScore)
		{
			if (newScore > this.globalHighScore)
			{
				// New highscore
				this.globalHighScore = newScore;
				Console.WriteLine("New High Score! ({0})", this.globalHighScore);
				return true;
			}

			// No new highscore
			return false;
		}

		/// <summary>
		///		Stores the cached score for Player 1. For Scene use only!
		/// </summary>
		/// <param name="newCachedScore"></param>
		public void SetCachedScoreP1(int newCachedScore)
		{
			this.cachedPlayerOneScore = newCachedScore;
		}

		/// <summary>
		///		Stores the cached score for Player 2. For Scene use only!
		/// </summary>
		/// <param name="newCachedScore"></param>
		public void SetCachedScoreP2(int newCachedScore)
		{
			this.cachedPlayerTwoScore = newCachedScore;
		}

		/// <summary>
		///		Sets the total number of players. This should only
		///		be called by PlayerSelectScreen scene!
		/// </summary>
		/// <param name="howMany"></param>
		public void SetNumberOfPlayers(NumberOfPlayers howMany)
		{
			this.totalNumberOfPlayers = howMany;
		}


		/// <summary>
		///		This is the prefered method to destroy all the scenes.
		///		Do NOT use SceneManager.Destroy()!
		/// </summary>
		public void UnloadEverything()
		{
			// Loop though every scene in the active list
			SceneHolder itr = this.activeList.Head as SceneHolder;
			while(itr != null)
			{
				// Make this scene the active scene
				this.activeScene = itr.SceneRef;

				// Destroy it
				bool success = this.UnloadScene(this.ActiveSceneName);
				Debug.Assert(success, "The Scene was not successfully unloaded in UnloadEverything()");

				// Next scene
				itr = this.activeList.Head as SceneHolder;
			}

			// Now destroy the nodes holding the scenes
			this.Destroy();
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
		///		Used for AbstractManager memory filling
		/// </summary>
		/// <param name="fillSize"></param>
		protected override void FillReserve(int fillSize)
		{
			for (int i = fillSize; i > 0; i--)
			{
				SceneHolder newNode = new SceneHolder();
				this.reservedList.PushFront(newNode);
			}
		}



		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///		Get the number of player sessions currently active.
		///		NOT an indicator for the current player!
		/// </summary>
		public NumberOfPlayers TotalNumberOfPlayers
		{
			get
			{
				return this.totalNumberOfPlayers;
			}
		}

		/// <summary>
		///		Get the current timestamp from Azul. Read only.
		/// </summary>
		public float AzulClockTime
		{
			get
			{
				return this.internalClock;
			}
		}

		/// <summary>
		///		A get-only reference to the currently active Scene
		/// </summary>
		public Scene ActiveScene
		{
			get
			{
				return this.activeScene;
			}
		}

		/// <summary>
		///		Get the name of the currently active Scene
		/// </summary>
		public Scene.Name ActiveSceneName
		{
			get
			{
				if(this.activeScene == null)
				{
					return Scene.Name.UNINITIALIZED;
				}

				return this.activeScene.SceneName;
			}
		}

		/// <summary>
		///		Get the current highscore of this running session. Read only.
		/// </summary>
		public int GlobalHighScore
		{
			get
			{
				return this.globalHighScore;
			}
		}

		/// <summary>
		///		The current score of Player 1
		/// </summary>
		public int CachedScoreP1
		{
			get
			{
				return this.cachedPlayerOneScore;
			}
		}

		/// <summary>
		///		The current score of Player 2
		/// </summary>
		public int CachedScoreP2
		{
			get
			{
				return this.cachedPlayerTwoScore;
			}
		}




		///////////////////////////////////////////////////////
		// 
		// Nested Enums
		// 
		///////////////////////////////////////////////////////

		/// <summary>
		///		Type-safe indicator of how players are currently playing.
		///		NOT an indicator for the current player!
		/// </summary>
		public enum NumberOfPlayers
		{
			One = 1,
			Two = 2
		}


	}
}
