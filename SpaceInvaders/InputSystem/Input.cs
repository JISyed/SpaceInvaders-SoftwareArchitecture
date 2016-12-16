namespace SpaceInvaders.InputSystem
{
	static class Input
	{
		/// <summary>
		///		Returns true if the key is currently pressed down
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetKey(Azul.AZUL_KEYS key)
		{
			return InputKeyController.GetKey(key);
		}

		/// <summary>
		///		Returns true at the moment the key is pressed down 
		///		(should only return true once per key press)
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetKeyDown(Azul.AZUL_KEYS key)
		{
			return InputKeyController.GetKeyDown(key);
		}

		/// <summary>
		///		Returns true the moment the key is released
		///		(should only return true once per key press)
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetKeyUp(Azul.AZUL_KEYS key)
		{
			return InputKeyController.GetKeyUp(key);
		}
	}
}
