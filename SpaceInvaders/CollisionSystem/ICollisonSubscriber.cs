using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.CollisionSystem
{
	interface ICollisionSubscriber
	{
		void OnCollisionNotified(CollisionPairEvaluator.Name collisonName);
	}
}
