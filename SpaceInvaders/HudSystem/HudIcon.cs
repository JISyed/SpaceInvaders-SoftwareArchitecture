using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.HudSystem
{
	class HudIcon : ListNode
	{
		private SpriteEntity spriteIcon;
		private Azul.Color color;
		private float x;
		private float y;
		private float iconGap;		// Gap between multiple icons
		private int numberOfIcons;


		//
		// Constructors
		//

		private HudIcon()
		{
			// Not allowed
		}

		public HudIcon(float newX, float newY, Azul.Color newColor, float iconSpacing, int numOfIcons, Sprite.Name newIconName)
		{
			this.spriteIcon = SpriteEntityManager.Self.Find(newIconName);
			this.color = newColor;
			this.x = newX;
			this.y = newY;
			this.iconGap = iconSpacing;
			this.numberOfIcons = numOfIcons;
			
			if(this.numberOfIcons < 0)
			{
				this.numberOfIcons = 0;
			}
		}




		//
		// Methods
		//

		/// <summary>
		///		Update the state of this HUD icon, given the number of icons to draw
		/// </summary>
		/// <param name="newNumberOfIcons">
		///		Cannot be negative
		/// </param>
		public void UpdateIcons(int newNumberOfIcons)
		{
			this.numberOfIcons = newNumberOfIcons;
			if(this.numberOfIcons < 0)
			{
				this.numberOfIcons = 0;
			}
		}

		/// <summary>
		///		Draw this HUD icon
		/// </summary>
		public void Draw()
		{
			if(this.numberOfIcons <= 0)
			{
				return;		// Skip
			}

			// The rest of this code assumes there is at least 1 icon being drawn
			this.spriteIcon.SetPosition(this.x, this.y);
			this.spriteIcon.SetColor(this.color);
			float xShift = (this.spriteIcon.ImageMap.Width * this.spriteIcon.ScaleX) + this.iconGap;
			for(int i = 0; i < this.numberOfIcons; i++)
			{
				this.spriteIcon.UpdateInternalData();
				this.spriteIcon.Draw();
				this.spriteIcon.SetPosition(spriteIcon.X + xShift, spriteIcon.Y);
			}
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
			this.spriteIcon = null;
			this.color = null;
		}
	}
}
