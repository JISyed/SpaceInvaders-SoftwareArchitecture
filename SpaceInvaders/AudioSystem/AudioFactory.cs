using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.AudioSystem;

namespace SpaceInvaders.AudioSystem
{
	class AudioFactory
	{
		//
		// Constructor
		//

		public AudioFactory()
		{
		}


		//
		// Methods
		//

		/// <summary>
		///		Loads every AudioSource needed in the game
		/// </summary>
		public void LoadAllAudio()
		{
			AudioSourceManager.Self.Create(AudioSource.Name.Alien_A, "A.wav", false);
			AudioSourceManager.Self.Create(AudioSource.Name.Alien_B, "B.wav", false);
			AudioSourceManager.Self.Create(AudioSource.Name.Alien_C, "C.wav", false);
			AudioSourceManager.Self.Create(AudioSource.Name.Alien_D, "D.wav", false);
			AudioSourceManager.Self.Create(AudioSource.Name.DeathPlayer, "explosion.wav", false);
			AudioSourceManager.Self.Create(AudioSource.Name.DeathAlien, "shoot.wav", false);
			AudioSourceManager.Self.Create(AudioSource.Name.DeathUFO, "ufo_lowpitch.wav", false);
			AudioSourceManager.Self.Create(AudioSource.Name.UFOBeep, "ufo_highpitch.wav", true);
			AudioSourceManager.Self.Create(AudioSource.Name.LaserPlayer, "invaderkilled.wav", false);
		}
	}
}
