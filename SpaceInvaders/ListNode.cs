using Enum = System.Enum;

namespace SpaceInvaders
{
    abstract class ListNode
    {
        public ListNode prev;
        public ListNode next;
        private uint instanceId;


        //
        // Constructors
        //

        public ListNode()
        {
            this.prev = null;
            this.next = null;
            this.instanceId = 0u;
        }

        public ListNode(uint newId)
        {
            this.prev = null;
            this.next = null;
            this.instanceId = newId;
        }

        public ListNode(ListNode newPrev, ListNode newNext)
        {
            this.prev = newPrev;
            this.next = newNext;
            this.instanceId = 0u;
        }

        public ListNode(ListNode newPrev, ListNode newNext, uint newId)
        {
            this.prev = newPrev;
            this.next = newNext;
            this.instanceId = newId;
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

		public static bool operator <(ListNode lhs, ListNode rhs)
		{
			return lhs.LessThan(rhs);
		}

		public static bool operator >(ListNode lhs, ListNode rhs)
		{
			return lhs.GreaterThan(rhs);
		}


		//
		// Virtuals
		//

		virtual public bool LessThan(ListNode rhs)
		{
			return this.instanceId < rhs.instanceId;
		}

		virtual public bool GreaterThan(ListNode rhs)
		{
			return this.instanceId > rhs.instanceId;
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
