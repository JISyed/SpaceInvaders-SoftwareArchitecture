using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.AnimationSystem;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjectSystem;
using SpaceInvaders.HudSystem;
using SpaceInvaders.InputSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.TimerSystem;

namespace SpaceInvaders.SceneSystem
{
	abstract class Scene
	{
		// All the Managers
		private AnimationFlipManager manAnimationFlip;
		private AnimationFrameManager manAnimationFrame;
		private AnimationMotionManager manAnimationMotion;
		private CollisonPairManager manCollisionPair;
		private GameObjectManager manGameObject;
		private SpriteBatchManager manSpriteBatch;
		private SpriteProxyManager manSpriteProxy;
		private SpriteCollisonProxyManager manSpriteColProxy;
		private TimedEventManager manTimedEvent;

		// Other containers
		private GameSessionData gameData;
		private HudDisplayer hudDisplay;
		private SpriteBatch collisionBatch;

		// Scene specific data
		protected Scene.Name name;
		private float lastAzulTime;
		private uint alienCoordinatorId;
		private uint ufoId;
		private bool isLoaded;
		private bool isPaused;
		protected bool isMarkedForSceneChange;
		private bool isMarkedForRemoval;


		//
		// Constructors
		//

		public Scene()
		{
			// Make all the managers
			this.manAnimationFlip = new AnimationFlipManager(2, 1);
			this.manAnimationFrame = new AnimationFrameManager(4, 1);
			this.manAnimationMotion = new AnimationMotionManager(1, 1);
			this.manCollisionPair = new CollisonPairManager(13, 1);
			this.manGameObject = new GameObjectManager(50, 10);
			this.manSpriteBatch = new SpriteBatchManager(7, 1);
			this.manSpriteProxy = new SpriteProxyManager(50, 10);
			this.manSpriteColProxy = new SpriteCollisonProxyManager(50, 10);
			this.manTimedEvent = new TimedEventManager(10, 1, SceneManager.Self.AzulClockTime);
			
			// Make all the other containers
			this.gameData = new GameSessionData();
			this.hudDisplay = this.CreateHud();
			this.collisionBatch = this.manSpriteBatch.Find(SpriteBatch.Name.SpriteCollisions);
			this.collisionBatch.IsEnabled = false;

			// Set scene data
			this.name = Name.UNINITIALIZED;
			this.lastAzulTime = 0.0f;
			this.alienCoordinatorId = 0u;
			this.ufoId = 0u;
			this.isLoaded = false;
			this.isPaused = false;
			this.isMarkedForSceneChange = false;
			this.isMarkedForRemoval = false;
		}



		//
		// Methods
		//

		/// <summary>
		///		Updates everything in the current Scene. For Manager use only!
		/// </summary>
		/// <param name="currentAzulTime"></param>
		public void Update(float currentAzulTime)
		{
			this.lastAzulTime = currentAzulTime;

			// Pre-update
			this.OnSceneBeganUpdate();
			/////////////////////////


			// Timer Update
			this.manTimedEvent.Update(currentAzulTime);

			// Collision SpriteBatch toggle
			if(Input.GetKeyDown(Azul.AZUL_KEYS.KEY_D))
			{
				this.collisionBatch.IsEnabled = !this.collisionBatch.IsEnabled;
			}

			// Collision Pair checks
			this.manCollisionPair.Update();

			// GameObject Update
			this.manGameObject.Update();

			// HUD Update
			this.hudDisplay.Update();


			/////////////////////////
			// Post-update
			this.OnSceneEndedUpdate();
		}

		/// <summary>
		///		Draws everything in the current Scene. For Manager use only!
		/// </summary>
		/// <remarks>
		///		What gets drawn last gets drawn on top of everyone else
		/// </remarks>
		public void Draw()
		{
			// Draw SpriteBatches
			this.manSpriteBatch.Draw();
			
			// Draw HUD
			this.hudDisplay.Draw();
		}

		/// <summary>
		///		Allocates all the object specific to this Scene. For Manager use only!
		/// </summary>
		public void LoadScene()
		{
			this.isLoaded = true;
			this.isMarkedForSceneChange = false;

			// Load scene

			// Initialize the HUD
			this.hudDisplay.Initialize();

			// Make all collision pairs
			CollisionPairFactory collisonFactory = new CollisionPairFactory();
			collisonFactory.CreatePairs();

			// Make all the sprite batches
			BatchFactory spriteBatchFactory = new BatchFactory();
			spriteBatchFactory.CreateBatches();


			// Call event
			this.OnSceneLoad();
		}

		/// <summary>
		///		Unloads the contents of this scene. For Manager use only!
		/// </summary>
		public void UnloadScene()
		{
			// Call event
			this.OnSceneUnload();

			// Unload scene
			this.hudDisplay.Destroy();
			this.manAnimationMotion.Destroy();
			this.manGameObject.Destroy();
			this.manCollisionPair.Destroy();
			this.manSpriteBatch.Destroy();
			this.manSpriteColProxy.Destroy();
			this.manSpriteProxy.Destroy();
			this.manAnimationFrame.Destroy();
			this.manAnimationFlip.Destroy();
			this.manTimedEvent.Destroy();
		}

		/// <summary>
		///		Pauses the current scene. For Manager use only!
		/// </summary>
		/// <param name="currentAzulTime"></param>
		public void PauseScene()
		{
			this.isPaused = true;

			// Call event
			this.OnScenePaused();

			// Pause the Timer in this Scene
			this.manTimedEvent.Pause(this.lastAzulTime);
		}

		/// <summary>
		///		Unpauses the current scene. For Manager use only!
		/// </summary>
		public void UnpauseScene(float correctAzulTime)
		{
			this.isPaused = false;
			this.isMarkedForSceneChange = false;

			// Unpause the Timer in this Scene
			this.manTimedEvent.Unpause(correctAzulTime);

			// Call event
			this.OnSceneUnpaused();
		}

		/// <summary>
		///		Declare that this scene will go to the next one.
		///		Refer to OnLoadNextScene() for how it determines the next scene
		/// </summary>
		public void TransitionToNextScene()
		{
			this.isMarkedForSceneChange = true;
		}

		/// <summary>
		///		Store the current Id of the AlienCoodinator in this Scene
		/// </summary>
		/// <param name="newId"></param>
		public void SetAlienCoordiatorId(uint newId)
		{
			this.alienCoordinatorId = newId;
		}

		/// <summary>
		///		Store the current Id of the UFO in this Scene
		/// </summary>
		/// <param name="newId"></param>
		public void SetUfoId(uint newId)
		{
			this.ufoId = newId;
		}

		/// <summary>
		///		Mark this scene for removal. Only Scenes should call this
		///		at OnLoadNextScene() !
		/// </summary>
		public void DeleteScene()
		{
			this.isMarkedForRemoval = true;
		}


		//
		// Private Methods
		//





		//
		// Contracts
		//

		/// <summary>
		///		Each Scene has a different way of determining the next
		///		Scene to load. For Manager use only!!
		/// </summary>
		public abstract void OnLoadNextScene();

		/// <summary>
		///		Gets called when a Scene is loaded into memory
		/// </summary>
		protected abstract void OnSceneLoad();

		/// <summary>
		///		Gets called when a Scene is unloaded off memory
		/// </summary>
		protected abstract void OnSceneUnload();

		/// <summary>
		///		Gets called when a Scene is paused
		/// </summary>
		protected abstract void OnScenePaused();

		/// <summary>
		///		Gets called when a Scene is unpaused
		/// </summary>
		protected abstract void OnSceneUnpaused();

		/// <summary>
		///		Gets called when a Scene's Update loop begins
		/// </summary>
		protected abstract void OnSceneBeganUpdate();

		/// <summary>
		///		Gets called when a Scene's Update loop ends
		/// </summary>
		protected abstract void OnSceneEndedUpdate();

		/// <summary>
		///		Is this Scene a GameScene?
		/// </summary>
		/// <returns></returns>
		public abstract bool IsGameScene();

		/// <summary>
		///		Create a unique HudDisplayer in the specialized class.
		///		Should only ever be called by base Scene!
		///		Also should not initialize the Hud here!
		/// </summary>
		/// <returns></returns>
		protected abstract HudDisplayer CreateHud();





		//
		// Properties
		//

		/// <summary>
		///		The enum name of this Scene
		/// </summary>
		public Name SceneName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>
		///		Did this Scene load?
		/// </summary>
		public bool IsLoaded
		{
			get
			{
				return this.isLoaded;
			}
		}

		/// <summary>
		///		Is this Scene paused?
		/// </summary>
		public bool IsPaused
		{
			get
			{
				return this.isPaused;
			}
		}

		/// <summary>
		///		Get the last Azul time recorded before scene-pausing.
		///		If the scene is not paused, it's probably the latest.
		/// </summary>
		protected float LastAzulTime
		{
			get
			{
				return this.lastAzulTime;
			}
		}

		/// <summary>
		///		The AnimationFlipManager of this Scene
		/// </summary>
		public AnimationFlipManager ManagerForAnimationFlip
		{
			get
			{
				return this.manAnimationFlip;
			}
		}

		/// <summary>
		///		The AnimationFrameManager of this Scene
		/// </summary>
		public AnimationFrameManager ManagerForAnimationFrame
		{
			get
			{
				return this.manAnimationFrame;
			}
		}

		/// <summary>
		///		The AnimationMotionManager of this Scene
		/// </summary>
		public AnimationMotionManager ManagerForAnimationMotion
		{
			get
			{
				return this.manAnimationMotion;
			}
		}

		/// <summary>
		///		The CollisonPairManager of this Scene
		/// </summary>
		public CollisonPairManager ManagerForCollisionPair
		{
			get
			{
				return this.manCollisionPair;
			}
		}

		/// <summary>
		///		The GameObjectManager of this Scene
		/// </summary>
		public GameObjectManager ManagerForGameObject
		{
			get
			{
				return this.manGameObject;
			}
		}

		/// <summary>
		///		The SpriteBatchManager of this Scene
		/// </summary>
		public SpriteBatchManager ManagerForSpriteBatch
		{
			get
			{
				return this.manSpriteBatch;
			}
		}

		/// <summary>
		///		The SpriteProxyManager of this Scene
		/// </summary>
		public SpriteProxyManager ManagerForSpriteProxy
		{
			get
			{
				return this.manSpriteProxy;
			}
		}

		/// <summary>
		///		The SpriteCollisonProxyManager of this Scene
		/// </summary>
		public SpriteCollisonProxyManager ManagerForSpriteCollisonProxy
		{
			get
			{
				return this.manSpriteColProxy;
			}
		}

		/// <summary>
		///		The TimedEventManager of this Scene
		/// </summary>
		public TimedEventManager ManagerForTimedEvent
		{
			get
			{
				return this.manTimedEvent;
			}
		}

		/// <summary>
		///		The GameSessionData of this Scene
		/// </summary>
		public GameSessionData GameData
		{
			get
			{
				return this.gameData;
			}
		}

		/// <summary>
		///		Will this scene transition to another soon?
		/// </summary>
		public bool IsMarkedForSceneChange
		{
			get
			{
				return this.isMarkedForSceneChange;
			}
		}

		/// <summary>
		///		The GameObject ID of the AlienCoordinator
		/// </summary>
		public uint AlienCoordinatorId
		{
			get
			{
				return this.alienCoordinatorId;
			}
		}

		/// <summary>
		///		The GameObject ID of the UFO
		/// </summary>
		public uint UfoId
		{
			get
			{
				return this.ufoId;
			}
		}

		/// <summary>
		///		Get only reference to this scene's HUD
		/// </summary>
		public HudDisplayer HudDisplay
		{
			get
			{
				return this.hudDisplay;
			}
		}

		/// <summary>
		///		Will this scene get deleted soon?
		/// </summary>
		public bool IsMarkedForRemoval
		{
			get
			{
				return this.isMarkedForRemoval;
			}
		}



		//
		// Nested Enums
		//

		/// <summary>
		///		Names for the Scenes
		/// </summary>
		public enum Name
		{
			UNINITIALIZED,
			StartScreen,
			PlayerSelect,
			GamePlayerOne,
			GamePlayerTwo
		}
	}
}
