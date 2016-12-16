using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.HudSystem
{
	sealed class StartScreenHudDisplayer : HudDisplayer
	{
		//////////////////////////
		//
		// Constants

		private const float LabelPlayX = 315.0f;
		private const float LabelPlayY = 560.0f;
		private const float LabelInvadersX = 200.0f;
		private const float LabelInvadersY = 490.0f;
		private const float LabelScoreX = 130.0f;
		private const float LabelScoreY = 390.0f;
		private const float LabelPointsX = 270.0f;
		private const float LabelPointsY = 340.0f;
		private const float TableLineSpacing = 50.0f;
		private const float IconPointsX = 230.0f;

		//
		//////////////////////////

		private HudIcon iconUfo;
		private HudIcon iconSquid;
		private HudIcon iconCrab;
		private HudIcon iconOctopus;


		//
		// Constructor
		//

		public StartScreenHudDisplayer() : base()
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
			// Does nothing
		}

		/// <summary>
		///		Decide what additional things to draw
		/// </summary>
		/// <remarks>
		///		If you add HudText to the list, they are automatically drawn
		/// </remarks>
		protected override void OnDraw()
		{
			this.iconUfo.Draw();
			this.iconSquid.Draw();
			this.iconCrab.Draw();
			this.iconOctopus.Draw();
		}

		/// <summary>
		///		Custom initialization routine. You must define how the 
		///		HUD will look here.
		/// </summary>
		protected override void OnInit()
		{
			// Create the "Press Enter" text
			this.AddHudText(HudDisplayer.LabelPressEnterX, HudDisplayer.LabelPressEnterY, "Press Enter", Azul.AZUL_FONTS.Consolas24pt, Colors.Green);

			// Create the "Play" text
			this.AddHudText(LabelPlayX, LabelPlayY, "PLAY", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Create the "Space Invaders" text
			this.AddHudText(LabelInvadersX, LabelInvadersY, "SPACE   INVADERS", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Create the "*Score Advance Table*" text
			this.AddHudText(LabelScoreX, LabelScoreY, "*SCORE  ADVANCE  TABLE*", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Create the "=? Mystery" text
			this.AddHudText(LabelPointsX, LabelPointsY, "=?  MYSTERY", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Create the "=30  Points" text
			this.AddHudText(LabelPointsX, LabelPointsY - TableLineSpacing, "=30  POINTS", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Create the "=20  Points" text
			this.AddHudText(LabelPointsX, LabelPointsY - TableLineSpacing * 2, "=20  POINTS", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Create the "=10  Points" text
			this.AddHudText(LabelPointsX, LabelPointsY - TableLineSpacing * 3, "=10  POINTS", Azul.AZUL_FONTS.Consolas36pt, Colors.Green);

			// Create the UFO icon
			this.iconUfo = new HudIcon(IconPointsX, LabelPointsY, Colors.White, 1.0f, 1, Sprite.Name.UFO);

			// Create the Squid icon
			this.iconSquid = new HudIcon(IconPointsX, LabelPointsY - TableLineSpacing, Colors.White, 1.0f, 1, Sprite.Name.AlienSquid);

			// Create the Crab icon
			this.iconCrab = new HudIcon(IconPointsX, LabelPointsY - TableLineSpacing * 2, Colors.White, 1.0f, 1, Sprite.Name.AlienCrab);

			// Create the Octopus icon
			this.iconOctopus = new HudIcon(IconPointsX, LabelPointsY - TableLineSpacing * 3, Colors.Green, 1.0f, 1, Sprite.Name.AlienOctopus);

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
