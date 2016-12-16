using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.HudSystem
{
	sealed class PlayerSelectHudDisplayer : HudDisplayer
	{
		//////////////////////////
		//
		// Constants

		private const float LabelChooseX = 210.0f;
		private const float LabelChooseY = 440.0f;
		private const float LabelPressX = 300.0f;
		private const float LabelPressY = 330.0f;
		private const float LabelForPlayerX = 175.0f;
		private const float LabelForPlayer1Y = 250.0f;
		private const float LabelForPlayer2Y = 170.0f;

		//
		//////////////////////////

		//
		// Constructor
		//

		public PlayerSelectHudDisplayer()
			: base()
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
			// Add "Choose Players" text
			this.AddHudText(LabelChooseX, LabelChooseY, "CHOOSE PLAYERS", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Add "Press" text
			this.AddHudText(LabelPressX, LabelPressY, "PRESS", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Add "<1> FOR 1 PLAYER" text
			this.AddHudText(LabelForPlayerX, LabelForPlayer1Y, "<1>  FOR   1 PLAYER", Azul.AZUL_FONTS.Consolas36pt, Colors.White);

			// Add "<2> FOR 2 PLAYER" text
			this.AddHudText(LabelForPlayerX, LabelForPlayer2Y, "<2>  FOR   2 PLAYER", Azul.AZUL_FONTS.Consolas36pt, Colors.Green);

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
