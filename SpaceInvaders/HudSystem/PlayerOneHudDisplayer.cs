using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.HudSystem
{
	sealed class PlayerOneHudDisplayer : HudDisplayer
	{
		//
		// Constructor
		//

		public PlayerOneHudDisplayer() : base()
		{
		}


		//
		// Contracts
		//

		/// <summary>
		///		Decide what data gets updated in the HUD
		/// </summary>
		protected override void OnUpdate()
		{
			this.internalHighscore = SceneManager.Self.GlobalHighScore;
			this.internalScoreP1 = GameSessionData.Active.Score;
			this.internalScoreP2 = SceneManager.Self.CachedScoreP2;
			this.internalLives = GameSessionData.Active.Lives;
		}

		/// <summary>
		///		Decide what additional things to draw
		/// </summary>
		/// <remarks>
		///		If you add HudText to the list, they are automatically drawn
		/// </remarks>
		protected override void OnDraw()
		{
			// Does nothing
		}

		/// <summary>
		///		Custom initialization routine. You must define how the 
		///		HUD will look here.
		/// </summary>
		protected override void OnInit()
		{
			// Does nothing
		}

		/// <summary>
		///		Additional removal of object on derived HudDisplayers
		/// </summary>
		protected override void OnDestroy()
		{
			// Does nothing
		}

	}
}
