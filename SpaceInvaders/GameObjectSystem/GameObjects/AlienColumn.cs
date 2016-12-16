using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.CollisionSystem;

namespace SpaceInvaders.GameObjectSystem.GameObjects
{
	class AlienColumn : GameObject
	{


		//
		// Constructor
		//

		public AlienColumn()
			: base()
		{
			this.SetName(GameObject.Name.AlienColumn);
		}



		
		//
		// Methods
		//

		/// <summary>
		///		Shoot laser from the bottom alien of this column
		/// </summary>
		/// <param name="laserRoot"></param>
		public bool ShootLaser(GameObject laserRoot)
		{
			// Get the bottom child of the alien column
			// This should be the immediate child!
			Aliens bottomAlien = this.Child as Aliens;

			// Abort
			if(bottomAlien == null)
			{
				return false;
			}

			// Make the alien shoot
			bottomAlien.ShootLaser(laserRoot);

			return true;
		}



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

			// Delete the column if no more aliens exist in it
			if(this.Child == null)
			{
				keepObjectAlive = false;
			}

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
		/// <returns>
		///		<c>true</c> if the object is to be deleted
		///	</returns>
		public override bool OnCollide(CollisionPairEvaluator.Name collisonName, GameObject other)
		{
			bool willBeDeleted = false;
			return willBeDeleted;
		}
	}
}
