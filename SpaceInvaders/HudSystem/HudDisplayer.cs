using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.HudSystem
{
	abstract class HudDisplayer
	{
		//////////////////////////
		//
		// Constants

		public const float LabelGameOverX = 260.0f;
		public const float LabelGameOverY = 600.0f;
		public const float LabelPressEnterX = 265.0f;
		public const float LabelPressEnterY = 60.0f;
		private const float HighTextY = 745.0f;
		private const float MostlyHighTextY = 700.0f;
		private const float LowTextY = 27.0f;
		private const float CreditZeroX = 460.0f;
		private const float LabelP2X = 470.0f;
		private const float LabelHighscoreX = 275.0f;
		private const float LabelP1X = 35.0f;
		private const float TextLivesX = 28.0f;
		private const float TextScoreP1X = 90.0f;
		private const float TextScoreP2X = 520.0f;
		private const float TextHighscoreX = 310.0f;
		private const float IconLivesX = 90.0f;
		private const float IconLivesY = LowTextY + 3.0f;
		private const float IconLivesSpacing = 5.0f;

		//
		//////////////////////////

		private LinkList hudTextList;
		private LinkList disposableHudTextList;

		private HudText textScoreP1;
		private HudText textScoreP2;
		private HudText textHighScore;
		private HudText textLives;
		private HudIcon iconLives;

		protected int internalHighscore;
		protected int internalLives;
		protected int internalScoreP1;
		protected int internalScoreP2;

		public HudDisplayer()
		{
			this.hudTextList = new LinkList();
			this.disposableHudTextList = new LinkList();

			this.internalHighscore = SceneManager.Self.GlobalHighScore;
			this.internalLives = 3;
			this.internalScoreP1 = 0;
			this.internalScoreP2 = 0;
		}


		//
		// Methods
		//

		/// <summary>
		///		Initialize the layout and objects to display on the HUD
		/// </summary>
		public void Initialize()
		{
			// Create the lives icon
			this.iconLives = new HudIcon(IconLivesX,
										 IconLivesY,
										 Colors.Green,
										 IconLivesSpacing,
										 this.internalLives-1,
										 Sprite.Name.HUD_Player
			);


			// Still Text
			HudText p1StillText = new HudText(LabelP1X, HighTextY, "SCORE< 1 >");
			this.AddHudText(p1StillText);

			HudText highScoreStillText = new HudText(LabelHighscoreX, HighTextY, "HI-SCORE");
			this.AddHudText(highScoreStillText);

			HudText p2StillText = new HudText(LabelP2X, HighTextY, "SCORE< 2 >");
			this.AddHudText(p2StillText);

			HudText creditStillText = new HudText(CreditZeroX, LowTextY, "CREDIT  00");
			this.AddHudText(creditStillText);


			// Updating Text
			this.textScoreP1 = new HudText(TextScoreP1X, MostlyHighTextY, this.internalScoreP1.ToString());
			this.AddHudText(this.textScoreP1);

			this.textScoreP2 = new HudText(TextScoreP2X, MostlyHighTextY, this.internalScoreP2.ToString());
			this.AddHudText(this.textScoreP2);

			this.textHighScore = new HudText(TextHighscoreX, MostlyHighTextY, this.internalHighscore.ToString());
			this.AddHudText(this.textHighScore);

			this.textLives = new HudText(TextLivesX, LowTextY, this.internalLives.ToString());
			this.AddHudText(this.textLives);


			// Additional initialization if requested by derived class
			this.OnInit();
		}

		/// <summary>
		///		Update all elements of the HUD
		/// </summary>
		public void Update()
		{
			this.OnUpdate();

			// Always update the highscore
			this.internalHighscore = SceneManager.Self.GlobalHighScore;

			// D4 allows additional 0's in front
			this.textScoreP1.UpdateText(this.internalScoreP1.ToString("D4"));
			this.textScoreP2.UpdateText(this.internalScoreP2.ToString("D4"));
			this.textHighScore.UpdateText(this.internalHighscore.ToString("D4"));
			this.textLives.UpdateText(this.internalLives.ToString());
			this.iconLives.UpdateIcons(this.internalLives - 1);
		}

		/// <summary>
		///		Draw every element in this HUD display
		/// </summary>
		public void Draw()
		{
			// Draw the icons
			this.iconLives.Draw();

			// Draw all the text
			HudText itr = this.hudTextList.Head as HudText;
			while (itr != null)
			{
				// Reset
				itr.Draw();

				// Next node
				itr = itr.next as HudText;
			}

			// Draw all the disposable text
			itr = this.disposableHudTextList.Head as HudText;
			while (itr != null)
			{
				// Reset
				itr.Draw();

				// Next node
				itr = itr.next as HudText;
			}

			// Draw additional things if need
			this.OnDraw();
		}

		/// <summary>
		///		Deletes everything in this HUD display
		/// </summary>
		public void Destroy()
		{
			// Run derived removal
			this.OnDestroy();

			// Run base removal
			if (this.iconLives != null)
			{
				this.iconLives.Reset();
				this.iconLives = null;
			}

			ListNode itr = this.hudTextList.Head;
			while(itr != null)
			{
				// Reset
				itr.Reset();
				
				// Remove and Next Node
				this.hudTextList.PopFront();
				itr = this.hudTextList.Head;
			}

			this.RemoveDisposableHudText();
		}

		/// <summary>
		///		Add a new HudText instance to the list
		/// </summary>
		/// <param name="newText"></param>
		public void AddHudText(HudText newText)
		{
			this.hudTextList.PushFront(newText);
		}

		/// <summary>
		///		Add a new HudText instance to the disposable list
		/// </summary>
		/// <param name="newText"></param>
		public void AddDisposableHudText(HudText newText)
		{
			this.disposableHudTextList.PushFront(newText);
		}


		/// <summary>
		///		Construct a new HudText internally and automatically add it to the internal list
		/// </summary>
		/// <param name="newX"></param>
		/// <param name="newY"></param>
		/// <param name="newText"></param>
		/// <param name="newFont"></param>
		/// <param name="newColor"></param>
		public HudText AddHudText(float newX, float newY, string newText, Azul.AZUL_FONTS newFont, Azul.Color newColor)
		{
			HudText newHudText = new HudText(newX, newY, newText, newFont, newColor);
			this.AddHudText(newHudText);
			return newHudText;
		}

		/// <summary>
		///		Removes all the disposable HudTexts
		/// </summary>
		public void RemoveDisposableHudText()
		{
			ListNode itr = this.disposableHudTextList.Head;
			while (itr != null)
			{
				// Reset
				itr.Reset();

				// Remove and Next Node
				this.disposableHudTextList.PopFront();
				itr = this.disposableHudTextList.Head;
			}
		}



		//
		// Contracts
		//

		/// <summary>
		///		Decide what data gets updated in the HUD
		/// </summary>
		abstract protected void OnUpdate();
		
		/// <summary>
		///		Decide what additional things to draw
		/// </summary>
		/// <remarks>
		///		If you add HudText to the list, they are automatically drawn
		/// </remarks>
		abstract protected void OnDraw();

		/// <summary>
		///		Custom initialization routine. You must define how the 
		///		HUD will look here.
		/// </summary>
		abstract protected void OnInit();

		/// <summary>
		///		Additional removal of object on derived HudDisplayers
		/// </summary>
		abstract protected void OnDestroy();


	}
}
