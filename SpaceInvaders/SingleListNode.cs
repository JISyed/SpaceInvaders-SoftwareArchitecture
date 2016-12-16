namespace SpaceInvaders
{
	abstract class SingleListNode
	{
		public SingleListNode next;

		public SingleListNode()
		{
			this.next = null;
		}

		public SingleListNode(SingleListNode newNextNode)
		{
			this.next = newNextNode;
		}
	}
}
