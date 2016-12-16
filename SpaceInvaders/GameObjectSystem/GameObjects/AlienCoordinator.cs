using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.AnimationSystem;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.GameObjectSystem.Factories;
using SpaceInvaders.AudioSystem;

// Formulas were made with the help of "Desmos Graphing Calculator" online.

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class AlienCoordinator : GameObjectMotionAnimated, ICollisionSubscriber
	{
		/////////////////////////////
		//
		// Speed increase formula constants
		private const float SI_Coefficient = (64.0f / 1024.0f);
		private const float SI_ExpBase = 1.0507007f;
		private const float ProI_Coefficient = (1.0f / 99900099900099.0f);
		private const float ProI_ExpBase = 1.81f; // 1.87
		private const uint ProI_Threshold = 46u;
		public const float OriginalInterval = 0.85f;
		private const float OriginalSteps = 6.0f;
		private const int OriginalMaxAllowedLasers = 1;
		//
		//
		/////////////////////////////

		private AnimationFrame AnimatorSquid;
		private AnimationFrame AnimatorCrab;
		private AnimationFrame AnimatorOctopus;

		private AudioSource sndAlienA;
		private AudioSource sndAlienB;
		private AudioSource sndAlienC;
		private AudioSource sndAlienD;
		private int currSoundIndex = 0;

		private GameObject alienLaserRoot;	// Is not a child of this
		private AlienColumn[] columns;
		private float progressionSteps; // Should never be 0
		private float progressionTime;
		private float dropSteps;
		private readonly uint TotalAliens;
		private uint numberOfAliens;
		private int numberOfLasers;
		private int maxAllowedLasers;
		private bool isGoingDown;
		private bool isMoving;

		private readonly uint _numCols;
		private readonly float _initX;
		private readonly float _initY;
		private readonly float _xGap;

		//
		// Constructors
		//

		public AlienCoordinator(uint numRow, uint numColumns, float initX, float initY, float xGap, float yGap)
			: base()
		{
			SceneManager.Self.ActiveScene.SetAlienCoordiatorId(this.Id);
			this.SetName(GameObject.Name.AlienCoordinator);
			this.isGoingDown = false;
			this.isMoving = true;

			// Setup the marching sounds
			this.sndAlienA = AudioSourceManager.Self.Find(AudioSource.Name.Alien_A);
			this.sndAlienB = AudioSourceManager.Self.Find(AudioSource.Name.Alien_B);
			this.sndAlienC = AudioSourceManager.Self.Find(AudioSource.Name.Alien_C);
			this.sndAlienD = AudioSourceManager.Self.Find(AudioSource.Name.Alien_D);


			// Set the position depending on the current level
			int currentLevel = GameSessionData.Active.Level;
			int levelingFactor = currentLevel % AlienFactory.MaxDissent;
			int levelingRatio = currentLevel / AlienFactory.MaxDissent;
			float yOffset = levelingFactor * yGap;
			
			this.SetPosition(initX, initY - yOffset);
			this.maxAllowedLasers = AlienCoordinator.OriginalMaxAllowedLasers + levelingRatio;

			this.dropSteps = -yGap * 0.5f;
			this.progressionSteps = AlienCoordinator.OriginalSteps;
			this.progressionTime = AlienCoordinator.OriginalInterval;

			this.TotalAliens = numColumns * numRow;
			this.numberOfAliens = this.TotalAliens;
			this.numberOfLasers = 0;
			
			this._numCols = numColumns;
			this._initX = initX;
			this._initY = initY;
			this._xGap = xGap;

			this.Collider.Color = Colors.Cyan;



			// Subscribe AlienCoordinator to collisions that occur between Aliens and Walls
			var collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_Alien);
			collisonPair.Subscribe(this);
			// Subscribe AlienCoordinator to PlayerLaser vs Alien collision
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Alien);
			collisonPair.Subscribe(this);
			// And Player vs Aliens
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Player_vs_Alien);
			collisonPair.Subscribe(this);

			// And AlienLaser_vs_Shield
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.AlienLaser_vs_Shield);
			collisonPair.Subscribe(this);
			// And AlienLaser_vs_WallBottom
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.AlienLaser_vs_WallBottom);
			collisonPair.Subscribe(this);
			// And Player_vs_AlienLaser
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Player_vs_AlienLaser);
			collisonPair.Subscribe(this);
			// And Player_vs_AlienLaser
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser);
			collisonPair.Subscribe(this);



			// Get animator references
			this.AnimatorSquid = AnimationFrameManager.Active.Find(Sprite.Name.AlienSquid);
			this.AnimatorCrab = AnimationFrameManager.Active.Find(Sprite.Name.AlienCrab);
			this.AnimatorOctopus = AnimationFrameManager.Active.Find(Sprite.Name.AlienOctopus);
			
			// Resync animation
			this.ResyncFrameAnimation(AlienCoordinator.OriginalInterval);


			// Create the root for AlienLaser, add to manager on Start()
			this.alienLaserRoot = new EmptyObject();

			Debug.Assert(this.progressionSteps != 0.0f, "AlienCoordinator.progressionSteps should NOT be 0.");

		}

		private AlienCoordinator()
			: base()
		{
			Debug.Assert(false, "No default constructor for AlienCoordinator!");
		}


		
		//
		// Methods
		//



		//
		// Private methods
		//

		/// <summary>
		///		Make all columns for the coordinator.
		///		Should only run once!
		/// </summary>
		private void CreateAllColumns(uint numColumns, float initX, float initY, float xGap)
		{
			// Create all the Alien Columns
			this.columns = new AlienColumn[numColumns];
			for(uint i = 0; i < numColumns; i++)
			{
				// Create all the Alien Columns
				this.columns[i] = new AlienColumn();
				this.columns[i].SetPosition(initX + xGap*i, initY);
				this.columns[i].Collider.Color = Colors.Red;

				// Links up the PCS nodes of the columns
				this.AddChild(this.columns[i]);

				// Add the column to GameObjectManager
				GameObjectManager.Active.Attach(this.columns[i]);
			}
		}


		/// <summary>
		///		Move the coordinator downward
		/// </summary>
		private void MoveDownward()
		{
			this.isGoingDown = true;
			this.Animator.DetachEverything();
			this.Animator.Attach(0.0f, this.dropSteps);
			this.Animator.ScheduleTimedAnimation(this.progressionTime, false);
		}

		/// <summary>
		///		Switch horizontal direction and make it 
		///		move horizontally again
		/// </summary>
		private void SwitchHorizontalDirection()
		{
			this.isGoingDown = false;
			this.progressionSteps *= -1.0f;	// Flip direction
			this.Animator.DetachEverything();
			this.Animator.Attach(this.progressionSteps, 0.0f);
			this.Animator.ScheduleTimedAnimation(this.progressionTime, true);
		}

		/// <summary>
		///		Returns the new time interval for the motion animation
		/// </summary>
		/// <remarks>
		///		Formula's format:
		///		
		///			-coef * base^aliensKilled + (interval + coef * interval)
		///			
		/// </remarks>
		/// <param name="totalAliensKilled"></param>
		private float SpeedUpFormula(uint totalAliensKilled)
		{
			return (-SI_Coefficient * (Mathf.Pow(SI_ExpBase, totalAliensKilled))) + (OriginalInterval + SI_Coefficient * OriginalInterval);
		}

		/// <summary>
		///		Returns the new delta of the progression steps
		/// </summary>
		/// <remarks>
		///		Formula's format:
		///		
		///			coef * base^aliensKilled
		/// 
		/// </remarks>
		/// <returns></returns>
		/// <param name="totalAliensKilled"></param>
		private float ProgressionIncreaseFormula(uint totalAliensKilled)
		{
			if (totalAliensKilled < ProI_Threshold)
				return 0.0f;
			else if (totalAliensKilled == 54)	// One more alien left
			{
				return 0.0f;
			}

			return ProI_Coefficient * Mathf.Pow(ProI_ExpBase, totalAliensKilled);
		}

		/// <summary>
		///		Returns the time interval of the sprite animation
		///		back to the default values
		/// </summary>
		private void ResetSpriteAnimationIntervals()
		{
			this.AnimatorSquid.AssignNewIntervalTime(OriginalInterval);
			this.AnimatorCrab.AssignNewIntervalTime(OriginalInterval);
			this.AnimatorOctopus.AssignNewIntervalTime(OriginalInterval);
		}

		/// <summary>
		///		Chooses which column to shoot the laser from
		/// </summary>
		private void ShootLaser()
		{
			// Only shoot if more are allowed
			if(this.numberOfLasers < this.maxAllowedLasers)
			{
				// Gather the number of columns
				int numOfCol = 0;
				for(GameObject itr = this.Child as GameObject; 
					itr != null; 
					itr = itr.Sibling as GameObject)
				{
					numOfCol++;
				}

				// Skip if no columns
				if(numOfCol == 0)
				{
					return;
				}

				// Pick a random number to represent the random column that shoots
				int random = Randomizer.RandomInt(numOfCol);

				// Now find the random column to shoot from
				// The child object MUST be AlienColumn!
				AlienColumn cItr = this.Child as AlienColumn;
				Debug.Assert(cItr != null, "AlienCoordinator has a child that is NOT an AlienColumn!");
				for(int i = 0; i < random; i++)
				{
					cItr = cItr.Sibling as AlienColumn;
				}

				Debug.Assert(cItr != null, "The AlienColumn to fire laser from is null!");

				// Shoot the laser from this column
				bool alienFound = cItr.ShootLaser(this.alienLaserRoot);

				if (alienFound)
				{
					this.numberOfLasers++;
				}
			}
		}

		/// <summary>
		///		Play the next marching sound out of 4 possible sounds
		/// </summary>
		private void PlayNextMarchingSound()
		{
			// Pick the next sound
			AudioSource nextSound = null;
			switch(this.currSoundIndex)
			{
				case 0:
					nextSound = this.sndAlienA;
					this.currSoundIndex = 1;
					break;
				case 1:
					nextSound = this.sndAlienB;
					this.currSoundIndex = 2;
					break;
				case 2:
					nextSound = this.sndAlienC;
					this.currSoundIndex = 3;
					break;
				case 3:
					nextSound = this.sndAlienD;
					this.currSoundIndex = 0;
					break;
				default:
					Debug.Assert(false, "Alien marching sound is reaching an index outside of the [0-3] range!");
					break;
			}

			// Play the sound
			nextSound.Play();
			nextSound = null;
		}

		/// <summary>
		///		Makes the aliens stop moving
		/// </summary>
		private void StopMoving()
		{
			this.isMoving = false;

			this.alienLaserRoot.RemoveChildrenNodes();

			this.Animator.DetachEverything();
		}

		/// <summary>
		///		Makes the aliens move again
		/// </summary>
		public void StartMovingAgain()
		{
			this.isMoving = true;

			if(this.isGoingDown)
			{
				this.Animator.DetachEverything();
				this.Animator.Attach(0.0f, this.dropSteps);
				this.Animator.ScheduleTimedAnimation(this.progressionTime, false);
			}
			else
			{
				this.Animator.DetachEverything();
				this.Animator.Attach(this.progressionSteps, 0.0f);
				this.Animator.ScheduleTimedAnimation(this.progressionTime, true);
			}

			this.ResyncFrameAnimation(this.progressionTime);
		}

		/// <summary>
		///		Makes sure that the frame animators are in sync with
		///		the motion animator
		/// </summary>
		private void ResyncFrameAnimation(float newInterval)
		{
			this.AnimatorSquid.CancelTimedAnimation();
			this.AnimatorCrab.CancelTimedAnimation();
			this.AnimatorOctopus.CancelTimedAnimation();
			this.AnimatorSquid.AssignNewIntervalTime(newInterval);
			this.AnimatorCrab.AssignNewIntervalTime(newInterval);
			this.AnimatorOctopus.AssignNewIntervalTime(newInterval);
			this.AnimatorSquid.ScheduleTimedAnimation();
			this.AnimatorCrab.ScheduleTimedAnimation();
			this.AnimatorOctopus.ScheduleTimedAnimation();
		}



		//
		// Contracts
		//

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{
			// Add the alien root to the manager so that
			// it appears before this coordinator in the list
			GameObjectManager.Active.Attach(alienLaserRoot);

			// Same for all the columns
			this.CreateAllColumns(_numCols, _initX, _initY, _xGap);

			// Add the motion animator
			this.SetAnimator(AnimationMotionManager.Active.Create(Name.AlienCoordinator, 
																this.Id, 
																this.progressionTime, 
																true)
			);
			this.Animator.Attach(this.progressionSteps, 0.0f);

			this.ShootLaser();
		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			bool keepObjectAlive = true;

			if (this.isMoving)
			{

				// If aliens are moving down
				if (this.isGoingDown)
				{
					// Aliens only move down once.
					// Are they done yet?
					if (this.Animator.DidAnimationEnd)
					{
						// Stop moving down and move sideways again
						this.SwitchHorizontalDirection();
					}
				}

				// If there are no more aliens
				if (this.numberOfAliens == 0u)
				{
					this.Animator.AssignNewIntervalTime(OriginalInterval);
					this.Animator.DetachEverything();


					// Create a respawner
					AlienRespawner respawner = new AlienRespawner();

					// Destroy coordinator
					keepObjectAlive = false;
				}


				// Shoot laser if enough laser are allowed
				if (this.numberOfLasers < this.maxAllowedLasers)
				{
					this.ShootLaser();
				}
			}

			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{
			// Remove current motion animation
			this.ResetAnimator();



			// Unsubsribe from any collisions this is watching
			var collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Wall_vs_Alien);
			collisonPair.Unsubscribe(this);
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_Alien);
			collisonPair.Unsubscribe(this);
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Player_vs_Alien);
			collisonPair.Unsubscribe(this);

			// And AlienLaser_vs_Shield
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.AlienLaser_vs_Shield);
			collisonPair.Unsubscribe(this);
			// And AlienLaser_vs_WallBottom
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.AlienLaser_vs_WallBottom);
			collisonPair.Unsubscribe(this);
			// And Player_vs_AlienLaser
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.Player_vs_AlienLaser);
			collisonPair.Unsubscribe(this);
			// And Player_vs_AlienLaser
			collisonPair = CollisonPairManager.Active.Find(CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser);
			collisonPair.Unsubscribe(this);



			// Reset the sprite data it probably modified
			this.ResetSpriteAnimationIntervals();
			this.AnimatorCrab = null;
			this.AnimatorOctopus = null;
			this.AnimatorSquid = null;


			// Remove audio references
			this.sndAlienA = null;
			this.sndAlienB = null;
			this.sndAlienC = null;
			this.sndAlienD = null;

			// Remove the AlienLaser root
			if (this.alienLaserRoot != null)
			{
				bool found = GameObjectManager.Active.Recycle(this.alienLaserRoot);
				//Debug.Assert(found, "AlienLaser's root was not found on AlienCoordinator's destruction!");
				this.alienLaserRoot = null;
			}
		}


		/// <summary>
		///		Get's called when the GameObject's position is changed
		/// </summary>
		protected override void OnMove()
		{
			this.PlayNextMarchingSound();
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
			// Does nothing...
			bool willBeDeleted = false;
			return willBeDeleted;
		}

		/// <summary>
		///		This method gets called when a collision occurs 
		///		that this object observes (subscribes to)
		/// </summary>
		/// <param name="collisionName"></param>
		public void OnCollisionNotified(CollisionPairEvaluator.Name collisionName)
		{
			// Walls vs Aliens
			if(collisionName == CollisionPairEvaluator.Name.Wall_vs_Alien)
			{
				if (this.isGoingDown == false)
				{
					// Prepare the aliens to move downward

					// But only if the coordinator is moving right toward the right wall
					bool approachingRightWall = this.progressionSteps > 0.0f && this.Collider.X > Constants.SCREEN_CENTER_X;
					// Or if the coordinator is moving left toward the left wall
					bool approachingLeftWall = this.progressionSteps < 0.0f && this.Collider.X < Constants.SCREEN_CENTER_X;

					if (approachingLeftWall || approachingRightWall)
					{
						this.MoveDownward();
					}
				}
			}
			// PlayerLaser vs Alien
			else if(collisionName == CollisionPairEvaluator.Name.PlayerLaser_vs_Alien)
			{
				// An alien died
				this.numberOfAliens--;

				// Get the new progression time
				this.progressionTime = this.SpeedUpFormula(this.TotalAliens - this.numberOfAliens);
				if(this.progressionTime < 0.0f)
				{
					this.progressionTime = 0.1f;
				}
				this.Animator.AssignNewIntervalTime(this.progressionTime);

				// Get new progression step
				float newProgressionDelta = this.ProgressionIncreaseFormula(this.TotalAliens - this.numberOfAliens);
				if (progressionSteps > 0.0f)	// positive
				{
					this.progressionSteps += newProgressionDelta;
				}
				else		// negative
				{
					this.progressionSteps -= newProgressionDelta;
				}
				if (this.isGoingDown == false)
				{
					this.Animator.AssignNewXChange(this.progressionSteps);
				}
				
				// Sync the movement with the sprite animations
				this.AnimatorSquid.AssignNewIntervalTime(this.progressionTime);
				this.AnimatorCrab.AssignNewIntervalTime(this.progressionTime);
				this.AnimatorOctopus.AssignNewIntervalTime(this.progressionTime);
			}
			// Player vs Alien
			else if(collisionName == CollisionPairEvaluator.Name.Player_vs_Alien)
			{
				// Player just died. Stop moving.
				this.StopMoving();
			}


			// For AlienLaser vs anything
			else if(collisionName == CollisionPairEvaluator.Name.PlayerLaser_vs_AlienLaser
				|| collisionName == CollisionPairEvaluator.Name.Player_vs_AlienLaser
				|| collisionName == CollisionPairEvaluator.Name.AlienLaser_vs_Shield
				|| collisionName == CollisionPairEvaluator.Name.AlienLaser_vs_WallBottom)
			{
				// A laser died. Allow more laser in the future.
				this.numberOfLasers--;
				if(this.numberOfLasers < 0)
				{
					this.numberOfLasers = 0;
				}

				// If specifically Player vs. AlienLaser
				if(collisionName == CollisionPairEvaluator.Name.Player_vs_AlienLaser)
				{
					// Player just died. Stop moving.
					this.StopMoving();
				}
			}

		}


		//
		// Properties
		//

		/// <summary>
		///		The number of columns in the Alien formation
		/// </summary>
		public int NumberOfColumns
		{
			get
			{
				return this.columns.Length;
			}
		}

		/// <summary>
		///		The array of AlienColumn references
		/// </summary>
		public AlienColumn[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		/// <summary>
		///		Amount of horrizontal movement every interval
		/// </summary>
		public float ProgressionSteps
		{
			get
			{
				return this.progressionSteps;
			}
		}

		/// <summary>
		///		How long to wait before next progression, in seconds
		/// </summary>
		public float ProgressionTime
		{
			get
			{
				return this.progressionTime;
			}
		}

		/// <summary>
		///		How far should the aliens drop when hitting the edge
		/// </summary>
		public float DropSteps
		{
			get
			{
				return this.dropSteps;
			}
		}

		/// <summary>
		///		Get only reference to the root of the AlienLaser collision tree
		/// </summary>
		public GameObject AlienLaserRoot
		{
			get
			{
				return this.alienLaserRoot;
			}
		}
		
	}
}
