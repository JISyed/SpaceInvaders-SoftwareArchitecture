using Debug = System.Diagnostics.Debug;
using Console = System.Console;

namespace SpaceInvaders.HudSystem
{
	class HudText : ListNode
	{
		private const Azul.AZUL_FONTS DEFAULT_FONT = Azul.AZUL_FONTS.Consolas36pt;

		private Azul.SpriteFont internalAzulText;



		//
		// Constructors
		//

		private HudText()
		{
			// Does nothing
		}

		public HudText(float x, float y, string text)
		{
			this.internalAzulText = new Azul.SpriteFont(text, HudText.DEFAULT_FONT, x, y);
			this.internalAzulText.setColor(Colors.White);
		}

		public HudText(float x, float y, string text, Azul.AZUL_FONTS font)
		{
			this.internalAzulText = new Azul.SpriteFont(text, font, x, y);
			this.internalAzulText.setColor(Colors.White);
		}

		public HudText(float x, float y, string text, Azul.AZUL_FONTS font, Azul.Color color)
		{
			this.internalAzulText = new Azul.SpriteFont(text, font, x, y);
			this.internalAzulText.setColor(color);
		}

		public HudText(float x, float y, string text, Azul.Color color)
		{
			this.internalAzulText = new Azul.SpriteFont(text, HudText.DEFAULT_FONT, x, y);
			this.internalAzulText.setColor(color);
		}


		//
		// Methods
		//

		/// <summary>
		///		Make this HudText render the given string
		/// </summary>
		/// <param name="newText"></param>
		public void UpdateText(string newText)
		{
			this.internalAzulText.Update(newText);
		}

		/// <summary>
		///		Draw the current text of this HudText on the screen
		/// </summary>
		public void Draw()
		{
			this.internalAzulText.Draw();
		}



		//
		// Contracts
		//

		/// <summary>
		///		NOT IMPLEMENTED! DO NOT USE.
		/// </summary>
		/// <returns></returns>
		public override System.Enum GetName()
		{
			throw new System.NotImplementedException();
		}

		public override void Reset()
		{
			this.internalAzulText = null;
		}
	}
}
