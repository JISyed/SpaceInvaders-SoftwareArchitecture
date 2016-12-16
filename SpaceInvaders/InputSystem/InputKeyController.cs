using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.InputSystem
{
	/// <summary>
	///		Used by the system to handle keyboard input
	/// </summary>
	static class InputKeyController
	{
		private static InputKeyMap keyMap = null;
		private static bool alreadySetup = false;


		//
		// Methods
		//

		/// <summary>
		///		Setup all the state data needed for input to work
		/// </summary>
		/// <remarks>
		///		To define allowed keys, go to InputKeyMap.CreateAllInputKeys()
		/// </remarks>
		public static void Initialize()
		{
			if (InputKeyController.alreadySetup == false)
			{
				InputKeyController.alreadySetup = true;

				InputKeyController.keyMap = new InputKeyMap();
				InputKeyController.keyMap.Initialize();
			}
			else
			{
				Debug.Assert(false, "The InputKeyController is being initialized more than once!");
			}
		}

		/// <summary>
		///		Update the state of all input keys
		/// </summary>
		public static void Update()
		{
			InputKeyController.keyMap.Update();
		}

		/// <summary>
		///		Returns true if the key is currently pressed down
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetKey(Azul.AZUL_KEYS key)
		{
			return InputKeyController.keyMap[key].GetKey();
		}

		/// <summary>
		///		Returns true at the moment the key is pressed down 
		///		(should only return true once per key press)
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetKeyDown(Azul.AZUL_KEYS key)
		{
			return InputKeyController.keyMap[key].GetKeyDown();
		}

		/// <summary>
		///		Returns true the moment the key is released
		///		(should only return true once per key press)
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetKeyUp(Azul.AZUL_KEYS key)
		{
			return InputKeyController.keyMap[key].GetKeyUp();
		}


	}
}
