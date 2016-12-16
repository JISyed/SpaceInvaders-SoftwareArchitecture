using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.ContainerSystem
{
	abstract class ContainerNode
	{
		public ContainerNode prev;
		public ContainerNode next;
		private uint instanceId;

        //
        // Constructors
        //

        public ContainerNode()
        {
            this.prev = null;
            this.next = null;
			this.instanceId = 0u;
        }


        //
        // Methods
        //

		public void SetId(uint newId)
		{
			this.instanceId = newId;
		}

        public void BaseReset()
        {
			this.instanceId = 0u;
        }


        //
        // Contracts
        //

        abstract public void Reset();

		abstract public Enum GetName();

        //
        // Properties
        //

		public uint Id
		{
			get
			{
				return this.instanceId;
			}
		}


	}
}
