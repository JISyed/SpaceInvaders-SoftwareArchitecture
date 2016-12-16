using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.SceneSystem;

namespace SpaceInvaders.SceneSystem
{
	class GameSessionData
	{
		/// <summary>
		///		The currently active instance based off active scene
		/// </summary>
		public static GameSessionData Active
		{
			get
			{
				return SceneManager.Self.ActiveScene.GameData;
			}
		}



		///////////////////////////////////////////////////////
		//
		// Class Data
		//
		///////////////////////////////////////////////////////

		private const int MAX_SCORE = 9999;
		private const int MAX_LIVES = 7;
		private const int STARTING_LIVES = 3;
		public const float BreakTime = 2.0f;

		//
		// Data
		//

		private int score = 0;
		private int lives = STARTING_LIVES;
		private int level = 0;	// Level number isn't displayed, so start with 0





		//
		// Constructors
		//

		public GameSessionData()
		{
		}




		//
		// Methods
		//

		/// <summary>
		///		Sets the score back to 0. Does NOT reset the highscore.
		/// </summary>
		public void ResetScore()
		{
			this.score = 0;
		}

		/// <summary>
		///		Adds <c>additionalPoints</c> to the score
		/// </summary>
		/// <param name="additionalPoints"></param>
		public void AddScore(int additionalPoints)
		{
			this.score += additionalPoints;
			if(this.score > GameSessionData.MAX_SCORE)
			{
				this.score = GameSessionData.MAX_SCORE;
			}
			//Console.WriteLine("Score: {0}", this.score);
		}

		/// <summary>
		///		Return the game to the first level
		/// </summary>
		public void ResetLevel()
		{
			this.level = 0;
		}

		/// <summary>
		///		Go to the next level
		/// </summary>
		public void IncrementLevel()
		{
			this.level++;
			Console.WriteLine("LEVEL {0}", this.level);
		}

		/// <summary>
		///		Reset lives to the default amount
		/// </summary>
		public void ResetLives()
		{
			this.lives = GameSessionData.STARTING_LIVES;
		}

		/// <summary>
		///		Loose a life
		/// </summary>
		public void DecrementLives()
		{
			this.lives--;
			if(this.lives < 0)
			{
				this.lives = 0;
			}
			//Console.WriteLine("Lives: {0}", this.lives);
		}

		/// <summary>
		///		Get an extra life
		/// </summary>
		public void IncrementLives()
		{
			this.lives++;
			if(this.lives > GameSessionData.MAX_LIVES)
			{
				this.lives = GameSessionData.MAX_LIVES;
			}
			//Console.WriteLine("Lives: {0}", this.lives);
		}




		//
		// Properties
		//

		/// <summary>
		///		The current number of lives. Read only.
		/// </summary>
		public int Lives
		{
			get
			{
				return this.lives;
			}
		}

		/// <summary>
		///		The current score. Read only.
		/// </summary>
		public int Score
		{
			get
			{
				return this.score;
			}
		}

		/// <summary>
		///		The current level. Starts at 0. Read only.
		/// </summary>
		public int Level
		{
			get
			{
				return this.level;
			}
		}

	}
}
