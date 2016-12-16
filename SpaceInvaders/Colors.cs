namespace SpaceInvaders
{
	/// <summary>
	///		A collection of premade Azul.Colors
	/// </summary>
	class Colors
	{
		private static Azul.Color white;
		private static Azul.Color black;

		private static Azul.Color red;
		private static Azul.Color green;
		private static Azul.Color blue;

		private static Azul.Color cyan;
		private static Azul.Color magenta;
		private static Azul.Color yellow;


		public static Azul.Color White
		{
			get
			{
				if(white == null)
				{
					Colors.white = new Azul.Color(1.0f, 1.0f, 1.0f);
				}
				return Colors.white;
			}
		}

		public static Azul.Color Black
		{
			get
			{
				if (Colors.black == null)
				{
					Colors.black = new Azul.Color(0.0f, 0.0f, 0.0f);
				}
				return Colors.black;
			}
		}

		public static Azul.Color Red
		{
			get
			{
				if (Colors.red == null)
				{
					Colors.red = new Azul.Color(1.0f, 0.0f, 0.0f);
				}
				return Colors.red;
			}
		}

		public static Azul.Color Green
		{
			get
			{
				if (Colors.green == null)
				{
					Colors.green = new Azul.Color(0.0f, 1.0f, 0.0f);
				}
				return Colors.green;
			}
		}

		public static Azul.Color Blue
		{
			get
			{
				if (Colors.blue == null)
				{
					Colors.blue = new Azul.Color(0.0f, 0.0f, 1.0f);
				}
				return Colors.blue;
			}
		}

		public static Azul.Color Cyan
		{
			get
			{
				if (Colors.cyan == null)
				{
					Colors.cyan = new Azul.Color(0.0f, 1.0f, 1.0f);
				}
				return Colors.cyan;
			}
		}

		public static Azul.Color Magenta
		{
			get
			{
				if (Colors.magenta == null)
				{
					Colors.magenta = new Azul.Color(1.0f, 0.0f, 1.0f);
				}
				return Colors.magenta;
			}
		}

		public static Azul.Color Yellow
		{
			get
			{
				if (Colors.yellow == null)
				{
					Colors.yellow = new Azul.Color(1.0f, 1.0f, 0.0f);
				}
				return Colors.yellow;
			}
		}
	}
}
