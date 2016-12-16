namespace SpaceInvaders.InputSystem
{
	class InputKeyListNode : SingleListNode
	{
		private InputKey inputKey;

		public InputKeyListNode(InputKey newKey)
		{
			this.inputKey = newKey;
		}

		private InputKeyListNode()
		{
			// Not Implemented
		}

		/// <summary>
		///		Update the state of the currently referenced key
		/// </summary>
		public void Update()
		{
			this.inputKey.Update();
		}
	}
}
