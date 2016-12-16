namespace SpaceInvaders
{
	/// <summary>
	///		Static class to associate left and right with float numbers
	/// </summary>
	static class HorizontalDirection
	{
		/// <summary>
		///		Can either be Left or Right
		/// </summary>
		public enum Name
		{
			Left,
			Right
		}

		/// <summary>
		///		Returns -1f if Left, or 1f if Right
		/// </summary>
		/// <param name="name">Can either be Left or Right</param>
		/// <returns></returns>
		public static float ConvertToFloat(Name name)
		{
			float direction = 0f;

			switch (name)
			{
				case Name.Left:
					direction = -1.0f;
					break;
				case Name.Right:
					direction = 1.0f;
					break;
				default:
					break;
			}

			return direction;
		}

		/// <summary>
		///		Converts an int (preferably 0 or 1) to Left or Right.
		///		Left maps to 0, and Right maps to 1.
		/// </summary>
		/// <param name="intValue"></param>
		/// <returns></returns>
		public static Name ConvertFromInt(int intValue)
		{
			Name name = Name.Left;

			// 0 or less
			if(intValue < 1)
			{
				name = Name.Left;
			}
			// 1 or greater
			else
			{
				name = Name.Right;
			}

			return name;
		}
	}
}
