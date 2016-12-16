using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using Console = System.Console;

namespace SpaceInvaders.AudioSystem
{
	class AudioSourceManager : AbstractManager
	{
		///////////////////////////////////////////////////////
        //
        // Singleton stuff
        //
        ///////////////////////////////////////////////////////

		private static AudioSourceManager instance = null;

		/// <summary>
		///		Singleton Instance
		/// </summary>
        public static AudioSourceManager Self
        {
			get
			{
				if (instance == null)
				{
					// Create the manager
					AudioSourceManager.instance = new AudioSourceManager();
				}

				return AudioSourceManager.instance;
			}
        }




        ///////////////////////////////////////////////////////
        //
        // Manager Data
        //
        ///////////////////////////////////////////////////////

        // Private Constructor
		private AudioSourceManager()
			: base()
        {

        }





		///////////////////////////////////////////////////////
		//
		// Methods
		//
		///////////////////////////////////////////////////////

		/// <summary>
		///		Creates an AudioSource ready to play
		/// </summary>
		/// <param name="newName"></param>
		/// <param name="fileName"></param>
		/// <param name="shouldLoop"></param>
		/// <returns></returns>
		public AudioSource Create(AudioSource.Name newName, string fileName, bool shouldLoop)
		{
			Debug.Assert(newName != AudioSource.Name.UNINITIALIZED, "Creating an UNINITIALIZED AudioSource!");
			Debug.Assert(string.IsNullOrEmpty(fileName) == false, "AudioSource's filename is null or empty!");

			// Create the AudioSource and load the sound data
			AudioSource audio = this.BaseCreate() as AudioSource;
			audio.LoadAudio(newName, fileName, shouldLoop);

			return audio;
		}

		/// <summary>
		///		Removes the given AudioSource to be used again from the pool
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool Recycle(AudioSource.Name name)
		{
			Debug.Assert(name != AudioSource.Name.UNINITIALIZED, "Recycling an UNINITIALIZED AudioSource!");

			AudioSource oldAudio = this.BaseRecycle(name) as AudioSource;
			if(oldAudio == null) return false;
			oldAudio.Reset();
			return true;
		}

		/// <summary>
		///		Finds an AudioSource and returns it
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public AudioSource Find(AudioSource.Name name)
		{
			Debug.Assert(name != AudioSource.Name.UNINITIALIZED, "Finding an UNINITIALIZED AudioSource!");

			return this.BaseFind(name, this.activeList) as AudioSource;
		}

		/// <summary>
		///		Set the master volume of all AudioSources in the game
		/// </summary>
		/// <param name="newMasterVolume"></param>
		public void SetMasterVolume(float newMasterVolume)
		{
			Azul.Audio.setMasterVolume(newMasterVolume);
		}

		/// <summary>
		///		Stops all playing audio
		/// </summary>
		public void StopAllAudio()
		{
			AudioSource itr = this.activeList.Head as AudioSource;
			while(itr != null)
			{
				// Stop sound
				if(itr.IsPaused == false)
				{
					itr.Stop();
				}

				// Next sound
				itr = itr.next as AudioSource;
			}
		}



		///////////////////////////////////////////////////////
		//
		// Private Methods
		//
		///////////////////////////////////////////////////////






		///////////////////////////////////////////////////////
		// 
		// Contracts
		// 
		///////////////////////////////////////////////////////

		protected override void FillReserve(int fillSize)
		{
			for (int i = fillSize; i > 0; i--)
			{
				AudioSource newNode = new AudioSource();
				this.reservedList.PushFront(newNode);
			}
		}



		///////////////////////////////////////////////////////
		// 
		// Properties
		// 
		///////////////////////////////////////////////////////





	}
}
