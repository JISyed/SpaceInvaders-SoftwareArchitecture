using Debug = System.Diagnostics.Debug;
using Console = System.Console;
using Enum = System.Enum;
using SpaceInvaders.MemoryJunkyardSystem;

namespace SpaceInvaders.SceneSystem
{
	sealed class SceneHolder : ListNode
	{
		private Scene scene;


		// 
		// Constructors
		//

		public SceneHolder() : base()
		{
			this.scene = null;
		}




		//
		// Methods
		//

		/// <summary>
		///		Set the new scene of this holder
		/// </summary>
		/// <param name="newScene"></param>
		public void SetScene(Scene newScene)
		{
			Debug.Assert(this.scene == null, "SceneHolder is being set a new scene even though it already has one!");

			this.scene = newScene;
		}




		//
		// Contracts
		//

		/// <summary>
		///		Used for AbstractManager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			if(this.scene == null)
			{
				return Scene.Name.UNINITIALIZED;
			}

			return this.scene.SceneName;
		}

		/// <summary>
		///		Clears the data in this Scene's Holder
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			if(this.scene != null)
			{
				this.scene.UnloadScene();
				MemoryJunkyard.Self.DisposeObject(this.scene);
				this.scene = null;
			}
		}






		//
		// Properties
		//

		/// <summary>
		///		The reference to this holder's Scene. Get only.
		/// </summary>
		public Scene SceneRef
		{
			get
			{
				return this.scene;
			}
		}

		/// <summary>
		///		The name of the holder's Scene. Read only.
		/// </summary>
		public Scene.Name SceneName
		{
			get
			{
				if (this.scene == null)
				{
					return Scene.Name.UNINITIALIZED;
				}

				return this.scene.SceneName;
			}
		}
	}
}
