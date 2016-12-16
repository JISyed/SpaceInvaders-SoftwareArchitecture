using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.InputSystem
{
	/// <summary>
	///		Represents an individual key on the keyboard with
	///		a previously pressed state and currently pressed state
	/// </summary>
	class InputKey
	{
		private Azul.AZUL_KEYS name;
		private bool previousState;
		private bool currentState;


		//
		// Constructors
		//

		private InputKey()
		{
			Debug.Assert(false, "Default constructor not allowed for InputKey");
		}

		public InputKey(Azul.AZUL_KEYS keyName)
		{
			this.name = keyName;
			this.previousState = false;
			this.currentState = false;
		}


		//
		// Methods
		//

		/// <summary>
		///		Determine the current and previous state of this key
		/// </summary>
		public void Update()
		{
			// Set the previous state
			this.previousState = this.currentState;

			// Set the current state
			this.currentState = Azul.Input.GetKeyState(this.name);
		}

		/// <summary>
		///		Returns true if the key is currently pressed down
		/// </summary>
		/// <returns></returns>
		public bool GetKey()
		{
			return this.currentState;
		}

		/// <summary>
		///		Returns true at the moment the key is pressed down 
		///		(should only return true once per key press)
		/// </summary>
		/// <returns></returns>
		public bool GetKeyDown()
		{
			if(this.previousState == false && this.currentState == true)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		///		Returns true the moment the key is released
		///		(should only return true once per key press)
		/// </summary>
		/// <returns></returns>
		public bool GetKeyUp()
		{
			if(this.previousState == true && this.currentState == false)
			{
				return true;
			}

			return false;
		}
	}
}
