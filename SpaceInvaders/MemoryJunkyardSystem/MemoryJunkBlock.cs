using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.MemoryJunkyardSystem
{
	class MemoryJunkBlock : ListNode 
	{
		private object junk;


		//
		// Constructors
		//

		public MemoryJunkBlock() : base()
		{
			this.junk = null;
		}



		//
		// Methods
		//

		/// <summary>
		///		Assign the new object junk
		/// </summary>
		/// <param name="newJunk"></param>
		public void SetJunk(object newJunk)
		{
			this.junk = newJunk;
		}



		//
		// Contracts
		//

		/// <summary>
		///		Essentially does nothing
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return GameObjectSystem.GameObject.Name.UNINITIALIZED;
		}

		/// <summary>
		///		// Incinerates the junk
		/// </summary>
		public override void Reset()
		{
			this.junk = null;
		}
	}
}
