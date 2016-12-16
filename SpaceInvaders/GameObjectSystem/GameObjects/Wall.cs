using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class Wall : GameObject
	{

		//
		// Constructor
		//

		/// <summary>
		///		Create a wall
		/// </summary>
		/// <param name="newId">
		///		The ID of the Wall
		/// </param>
		/// <param name="isSideWall">
		///		This parameter is <c>true</c> if the wall is meant for the sides of the screen
		/// </param>
		public Wall(float width, float height) : base()
		{
			this.SetName(Name.Wall);
			this.Collider.Color = Colors.Yellow;
			this.Collider.SetupCollisonSpriteProxy(width, height, this.Id);
		}

		private Wall() : base()
		{
			Debug.Assert(false, "Do not call the default constructor of the Wall!");
		}



		//
		// Methods
		//





		//
		// Contracts
		//

		/// <summary>
		///		Start method of the GameObject
		/// </summary>
		public override void Start()
		{

		}

		/// <summary>
		///		The update loop routine of the GameObject
		/// </summary>
		public override bool Update()
		{
			bool keepObjectAlive = true;
			return keepObjectAlive;
		}

		/// <summary>
		///		Post-reset routine of the GameObject 
		///		(after base.Reset() is called)
		/// </summary>
		protected override void OnDestroy()
		{

		}

		/// <summary>
		///		Executes when a collison occurs with another GameObject
		/// </summary>
		/// <param name="collisonName"></param>
		/// <param name="other"></param>
		/// /// <returns>
		///		<c>true</c> if the object is to be deleted
		///	</returns>
		public override bool OnCollide(CollisionPairEvaluator.Name collisonName, GameObject other)
		{
			bool willBeDeleted = false;
			return willBeDeleted;
		}
	}
}
