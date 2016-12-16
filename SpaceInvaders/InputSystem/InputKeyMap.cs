using Debug = System.Diagnostics.Debug;
using Enum = System.Enum;

namespace SpaceInvaders.InputSystem
{
	/// <summary>
	///		A Hashmap for all InputKeys
	/// </summary>
	class InputKeyMap
	{
		private InputKey[] hashBucket;
		private InputKeyListNode iterationListHead;
		private readonly int hashBucketSize;
		private bool isMapLocked;
		

		//
		// Constructor
		//

		public InputKeyMap()
		{
			// This number is determined by looking for the maximum
			// Possible value in Azul.AZUL_KEYS, which appears to be 348
			int azulKeyMaxValue = this.GetLargestValueInAzulKeysEnum();
			this.hashBucketSize = azulKeyMaxValue + 1;

			this.isMapLocked = false;
			this.hashBucket = null;
			this.iterationListHead = null;
		}



		//
		// Methods
		//

		/// <summary>
		///		Setup the hashtable of keys
		/// </summary>
		/// <remarks>
		///		To define allowed keys, go to InputKeyMap.CreateAllInputKeys()
		/// </remarks>
		public void Initialize()
		{
			if(this.isMapLocked == false)
			{
				this.isMapLocked = true;

				this.hashBucket = new InputKey[this.hashBucketSize];
				this.CreateAllInputKeys();
				this.SetupIterationList();
			}
		}

		/// <summary>
		///		Indexer for getting an InputKey given an AZUL_KEY in constant time
		/// </summary>
		/// <param name="keyName"></param>
		/// <returns></returns>
		public InputKey this[Azul.AZUL_KEYS keyName]
		{
			get
			{
				int index = (int)keyName;
				InputKey key = this.hashBucket[index];
				string assetMessage = "";
				if(key == null)
				{
					assetMessage = "The key \"" + keyName.ToString() 
					+ "\" is not supported. Please add it to InputKeyMap.CreateAllInputKeys()";
				}
				Debug.Assert(key != null, assetMessage);
				return key;
			}
		}

		/// <summary>
		///		Updates all InputKeys in the hashtable
		/// </summary>
		public void Update()
		{
			// Loop through the iteration list of valid key entries
			// This is much faster then looping through the whole hashtable
			InputKeyListNode itr = this.iterationListHead;
			while(itr != null)
			{
				// Update the state of individual keys
				itr.Update();

				// Next key
				itr = itr.next as InputKeyListNode;
			}
		}



		//
		// Private Methods
		//

		/// <summary>
		///		Used to get the max value of AZUL_KEYS enum
		/// </summary>
		/// <returns></returns>
		private int GetLargestValueInAzulKeysEnum()
		{
			int[] values = (int[]) Enum.GetValues(typeof(Azul.AZUL_KEYS));
			int highestValue = values[0];
			for(int i = 1; i < values.Length; i++)
			{
				if(highestValue < values[i])
				{
					highestValue = values[i];
				}
			}
			return highestValue;
		}

		
		/// <summary>
		///		Sets up a linked list to iterate though valid entries of the hashtable.
		///		Must call only after this.CreateAllInputKeys() is called!
		/// </summary>
		private void SetupIterationList()
		{
			// Search for the first valid entry
			int i;
			for(i = 0; i < this.hashBucketSize; i++)
			{
				if(this.hashBucket[i] != null)
				{
					break;	// Found it
				}
			}

			Debug.Assert(i < this.hashBucketSize, "InputKeyMap does not have any entries!");

			// Create the head of the iteration list
			this.iterationListHead = new InputKeyListNode(this.hashBucket[i]);
			this.iterationListHead.next = null;

			// Now loop through the rest of the hashtable
			for(i = i + 1; i < this.hashBucketSize; i++)
			{
				if(this.hashBucket[i] != null)
				{
					this.AddIterationNode(this.hashBucket[i]);
				}
			}
		}

		/// <summary>
		///		Add a new node to the front of the linked list
		/// </summary>
		/// <param name="newNode"></param>
		private void AddIterationNode(InputKey newInputKey)
		{
			Debug.Assert(newInputKey != null, "Trying to add a null key into the iteration list!");
			InputKeyListNode newNode = new InputKeyListNode(newInputKey);
			newNode.next = this.iterationListHead;
			this.iterationListHead = newNode;
		}

		/// <summary>
		///		Create a new entry in the hashtable given the Azul Key
		/// </summary>
		/// <param name="?"></param>
		private void NewEntry(Azul.AZUL_KEYS newKeyName)
		{
			int index = (int)newKeyName;
			this.hashBucket[index] = new InputKey(newKeyName);
		}

		/// <summary>
		///		Factory method for InputKeys. 
		///		Here you would put all the keys you want to support in the game.
		/// </summary>
		private void CreateAllInputKeys()
		{
			this.NewEntry(Azul.AZUL_KEYS.KEY_SPACE);
			this.NewEntry(Azul.AZUL_KEYS.KEY_LEFT);
			this.NewEntry(Azul.AZUL_KEYS.KEY_RIGHT);
			this.NewEntry(Azul.AZUL_KEYS.KEY_D);
			this.NewEntry(Azul.AZUL_KEYS.KEY_ESCAPE);
			this.NewEntry(Azul.AZUL_KEYS.KEY_T);
			this.NewEntry(Azul.AZUL_KEYS.KEY_ENTER);
			this.NewEntry(Azul.AZUL_KEYS.KEY_1);
			this.NewEntry(Azul.AZUL_KEYS.KEY_2);
		}
	}
}
