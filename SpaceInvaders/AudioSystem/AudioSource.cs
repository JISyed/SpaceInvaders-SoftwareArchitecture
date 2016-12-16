using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using Console = System.Console;

namespace SpaceInvaders.AudioSystem
{
	class AudioSource : ListNode
	{
		private Azul.Sound soundClip;
		private string fileName;
		private AudioSource.Name name;
		private bool willLoop;
		private bool isPaused;

		//
		// Constructors
		//

		public AudioSource() : base()
		{
			this.soundClip = null;
			this.fileName = null;
			this.name = Name.UNINITIALIZED;
			this.willLoop = false;
			this.isPaused = true;
		}




		//
		// Methods
		//

		/// <summary>
		///		Initialized the sound into memory. For manager use only.
		/// </summary>
		/// <param name="newFileName"></param>
		/// <param name="shouldLoop"></param>
		public void LoadAudio(Name newName, string newFileName, bool shouldLoop)
		{
			// Set the names
			this.name = newName;
			this.fileName = newFileName;
			this.willLoop = shouldLoop;
			this.isPaused = true;

			// Create the Azul.Sound for this source into memory
			this.soundClip = null;
			this.soundClip = Azul.Audio.playSound(this.fileName, this.willLoop, this.isPaused, true);
		}

		/// <summary>
		///		Set the volume of this AudioSource
		/// </summary>
		/// <param name="newVolume"></param>
		public void SetVolume(float newVolume)
		{
			Debug.Assert(this.soundClip != null, "Setting the volume of a null Azul.Sound!");
			this.soundClip.setVolume(newVolume);
		}

		/// <summary>
		///		Play a sound
		/// </summary>
		public void Play()
		{
			Debug.Assert(this.soundClip != null, "Playing an uninitialized sound!");

			this.isPaused = false;
			this.soundClip = Azul.Audio.playSound(this.fileName, this.willLoop, this.isPaused, true);
		}

		/// <summary>
		///		Stop a sound
		/// </summary>
		public void Stop()
		{
			this.isPaused = true;

			// Bail
			if(this.soundClip == null)
			{
				return;
			}

			this.Pause();

			this.soundClip = Azul.Audio.playSound(this.fileName, this.willLoop, this.isPaused, true);
		}

		/// <summary>
		///		Pauses a playing sound
		/// </summary>
		public void Pause()
		{
			Debug.Assert(this.soundClip != null, "Pausing an uninitialized sound!");

			this.isPaused = true;
			this.soundClip.Pause(this.isPaused);
		}

		/// <summary>
		///		Unpauses an already paused sound
		/// </summary>
		public void Unpause()
		{
			Debug.Assert(this.soundClip != null, "Unpausing an uninitialized sound!");

			this.isPaused = false;
			this.soundClip.Pause(this.isPaused);
		}




		//
		// Contracts
		//

		/// <summary>
		///		For Abstract Manager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return this.name;
		}

		/// <summary>
		///		For deleting a sound
		/// </summary>
		public override void Reset()
		{
			this.Stop();

			this.soundClip = null;
			this.fileName = null;
			this.name = Name.UNINITIALIZED;
			this.isPaused = true;
			this.willLoop = false;
		}



		

		//
		// Properties
		//

		/// <summary>
		///		The enum name of this AudioSource
		/// </summary>
		public Name AudioName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>
		///		Is this AudioSource paused at the moment?
		/// </summary>
		public bool IsPaused
		{
			get
			{
				return this.isPaused;
			}
		}





		//
		// Nested Enums
		//

		/// <summary>
		///		Possible names of an AudioSource
		/// </summary>
		public enum Name
		{
			UNINITIALIZED,
			Alien_A,
			Alien_B,
			Alien_C,
			Alien_D,
			DeathPlayer,
			DeathAlien,
			DeathUFO,
			UFOBeep,
			LaserPlayer
		}
	}
}
