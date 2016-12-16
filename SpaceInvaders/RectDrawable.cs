using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders
{
	/// <summary>
	///		Behaves similarly to Azul.Rect but can be drawn. Pivot is in the center!
	/// </summary>
	class RectDrawable
	{
		private Azul.Line outline;
		private Azul.Color lineColor;
		private float x;
		private float y;
		private float width;
		private float height;
		private float halfWidth;	// Recaulcated only when width changes
		private float halfHeight;	// Recaulcated only when height changes


		//
		// Constructors
		//

		/// <summary>
		///		Private common constructor
		/// </summary>
		private void ctor(float x, float y, float w, float h, Azul.Color color, float thickness)
		{
			this.x = x;
			this.y = y;
			this.width = w;
			this.height = h;
			this.halfWidth = w / 2.0f;
			this.halfHeight = h / 2.0f;
			this.lineColor = color;
			this.outline = new Azul.Line(thickness, color, x, y, w, h);
		}

		private RectDrawable()
		{
			// Private, not implemented
			this.outline = null;
		}

		public RectDrawable(float x, float y, float w, float h)
		{
			this.outline = null;
			this.ctor(x, y, w, h, Colors.White, 1.0f);
		}

		public RectDrawable(float x, float y, float w, float h, Azul.Color color)
		{
			this.outline = null;
			this.ctor(x, y, w, h, color, 1.0f);
		}

		public RectDrawable(float x, float y, float w, float h, Azul.Color color, float thickness)
		{
			//this.outline = null;
			//this.ctor(x, y, w, h, color, thickness);
			this.x = x;
			this.y = y;
			this.width = w;
			this.height = h;
			this.halfWidth = w / 2.0f;
			this.halfHeight = h / 2.0f;
			this.lineColor = color;
			this.outline = new Azul.Line(thickness, color, x, y, w, h);
		}



		//
		// Methods
		//

		/// <summary>
		///		Draw the Rect's outline
		/// </summary>
		public void Draw()
		{
			float top = this.y + this.halfHeight;
			float bottom = this.y - this.halfHeight;
			float left = this.x - this.halfWidth;
			float right = this.x + this.halfWidth;
			this.outline.Draw(left, top, right, top, this.lineColor);		// Top line
			this.outline.Draw(left, bottom, right, bottom, this.lineColor);	// Bottom line
			this.outline.Draw(left, bottom, left, top, this.lineColor);		// Left line 
			this.outline.Draw(right, bottom, right, top, this.lineColor);	// Right line
		}

		/// <summary>
		///		Change the position of the Rect
		/// </summary>
		/// <param name="newX"></param>
		/// <param name="newY"></param>
		public void SetPosition(float newX, float newY)
		{
			this.x = newX;
			this.y = newY;
		}

		/// <summary>
		///		Change position, width, and height of the Rect
		/// </summary>
		/// <param name="newX"></param>
		/// <param name="newY"></param>
		/// <param name="newW"></param>
		/// <param name="newH"></param>
		public void SetRect(float newX, float newY, float newW, float newH)
		{
			this.x = newX;
			this.y = newY;
			this.width = newW;
			this.height = newH;
			this.halfWidth = newW / 2.0f;
			this.halfHeight = newH / 2.0f;
		}

		/// <summary>
		///		Change position, width, and height of the Rect
		/// </summary>
		/// <param name="newRect"></param>
		public void SetRect(Azul.Rect newRect)
		{
			this.SetRect(newRect.x, newRect.y, newRect.w, newRect.h);
		}

		/// <summary>
		///		Change only the width and the height of the Rect
		/// </summary>
		/// <param name="newWidth"></param>
		/// <param name="newHeight"></param>
		public void SetDimensions(float newWidth, float newHeight)
		{
			this.width = newWidth;
			this.halfWidth = newWidth / 2.0f;
			this.height = newHeight;
			this.halfHeight = newHeight / 2.0f;
		}






		//
		// Properties
		//

		/// <summary>
		///		Rect's X position
		/// </summary>
		public float X
		{
			get
			{
				return this.x;
			}

			set
			{
				this.x = value;
			}
		}

		/// <summary>
		///		Rect's Y position
		/// </summary>
		public float Y
		{
			get
			{
				return this.y;
			}

			set
			{
				this.y = value;
			}
		}

		/// <summary>
		///		Rect's Width
		/// </summary>
		public float W
		{
			get
			{
				return this.width;
			}

			set
			{
				this.width = value;
				this.halfWidth = this.width / 2.0f;
			}
		}

		/// <summary>
		///		Rect's Height
		/// </summary>
		public float H
		{
			get
			{
				return this.height;
			}

			set
			{
				this.height = value;
				this.halfHeight = this.height / 2.0f;
			}
		}

		/// <summary>
		///		The line color of the Rect
		/// </summary>
		public Azul.Color LineColor
		{
			get
			{
				return this.lineColor;
			}

			set
			{
				this.lineColor = value;
			}
		}
		
	}
}
