using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using SpaceInvaders.MemoryJunkyardSystem;
using SpaceInvaders.TextureSystem;
using SpaceInvaders.ImageSystem;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.InputSystem;
using SpaceInvaders.AudioSystem;
using SpaceInvaders.SceneSystem;
using SpaceInvaders.SceneSystem.Scenes;

namespace SpaceInvaders
{
    class Game : Azul.Engine
    {
        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
			// Set up Azul's internal graphics handlings
			this.setClearBufferColor(Colors.Black);
            Azul.Camera.setWindowSize(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT);

			// To define allowed keys, go to InputKeyMap.CreateAllInputKeys()
			InputKeyController.Initialize();

			// Create the managers not stuck to Scenes
			Randomizer.Initialize();
			AudioSourceManager.Self.Preallocate(7, 1);
			MemoryJunkyard.Self.Preallocate(12, 4);
			TextureManager.Self.Preallocate(1, 1);
			ImageManager.Self.Preallocate(11, 5);
			SpriteEntityManager.Self.Preallocate(2, 4);
			
			// Scene allocation
			SceneManager.Self.Preallocate(2, 1);

        }



        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
			// Moving this line into Initialize() causes
			// stupid AccessViolationExceptions
			SpriteCollisionManager.Self.Preallocate(2, 4);
			// The NULL one is for resizable sprites
			SpriteCollisionManager.Self.Create(Sprite.Name.NULL);

			// Create all the AudioSources
			AudioFactory audioFactory = new AudioFactory();
			audioFactory.LoadAllAudio();

			// Initilize the texture system
			TextureManager.Self.Create(Texture.Name.InvadersFromSpace, "invaders-from-space.tga");

			// Create all the Images ever needed
			ImageFactory imageFactory = new ImageFactory();
			imageFactory.Create();

			// Create all the SpriteEntites and SpriteCollisions ever needed
			SpriteEntityFactory spriteFactory = new SpriteEntityFactory();
			spriteFactory.Create();


			Console.WriteLine(" ");
			Console.WriteLine("//////////////////////////////////////////////////");
			Console.WriteLine("//          INSTRUCTIONS                        //");
			Console.WriteLine("//                                              //");
			Console.WriteLine("//    Left Arrow  = Go Left                     //");
			Console.WriteLine("//    Right Arrow = Go Right                    //");
			Console.WriteLine("//    Space       = Fire Missile                //");
			Console.WriteLine("//    D           = Toggle Collider Rendering   //");
			Console.WriteLine("//////////////////////////////////////////////////");
			Console.WriteLine(" ");


			///////////////////
			// Begin Scene Setup
			///////////////////

			// Create first scene
			StartScene firstScene = new StartScene();
			SceneManager.Self.LoadScene(firstScene);
        }
       



        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update(float totalTime)
        {
			// Update all input states
			InputKeyController.Update();

			// Update the currently active scene
			SceneManager.Self.Update(totalTime);

        }




        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
			// Draw the currently active scene
			SceneManager.Self.Draw();
        }




        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
			// Delete Scenes
			SceneManager.Self.UnloadEverything();

			// Delete scene-independent Managers
			SpriteCollisionManager.Self.Destroy();
			SpriteEntityManager.Self.Destroy();
			ImageManager.Self.Destroy();
			TextureManager.Self.Destroy();
			AudioSourceManager.Self.Destroy();
			MemoryJunkyard.Self.Destroy();
        }




		//-----------------------------------------------------------------------------
		// Game::EscapeTriggerFunc()
		//       can set the way to end the game
		//-----------------------------------------------------------------------------
		public override bool EscapeTriggerFunc()
		{
			//return  Azul.Input.GetKeyState(Azul.AZUL_KEYS.KEY_ESCAPE);
			return Input.GetKey(Azul.AZUL_KEYS.KEY_ESCAPE);
		}
    }
}
