using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;

namespace SpaceInvaders.AnimationSystem
{
	sealed class AnimationUnitMotion : AnimationUnit
	{
		private float deltaX;
		private float deltaY;


		//
		// Constructors
		//

		public AnimationUnitMotion() : base()
		{
			this.deltaX = 0.0f;
			this.deltaY = 0.0f;
		}



		//
		// Methods
		//

		/// <summary>
		///		Sets both the X and Y change for this animation unit
		/// </summary>
		/// <param name="newDeltaX"></param>
		/// <param name="newDeltaY"></param>
		public void SetDeltas(float newDeltaX, float newDeltaY)
		{
			this.deltaX = newDeltaX;
			this.deltaY = newDeltaY;
		}

		/// <summary>
		///		Sets the X change for this animation unit
		/// </summary>
		/// <param name="newDeltaX"></param>
		public void SetDeltaX(float newDeltaX)
		{
			this.deltaX = newDeltaX;
		}

		/// <summary>
		///		Sets the Y change for this animation unit
		/// </summary>
		/// <param name="newDeltaY"></param>
		public void SetDeltaY(float newDeltaY)
		{
			this.deltaY = newDeltaY;
		}




		//
		// Contracts
		//

		/// <summary>
		///		Used for Container searches. NOT IMPLEMENTED!
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return GameObjectSystem.GameObject.Name.UNINITIALIZED;
		}

		/// <summary>
		///		Clears the data in the unit
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			this.deltaX = 0.0f;
			this.deltaY = 0.0f;
		}
		


		
		//
		// Properites
		//

		/// <summary>
		///		The x-change in this animation unit
		/// </summary>
		public float DeltaX
		{
			get
			{
				return this.deltaX;
			}
		}

		/// <summary>
		///		The y-change in this animation unit
		/// </summary>
		public float DeltaY
		{
			get
			{
				return this.deltaY;
			}
		}
	}
}
