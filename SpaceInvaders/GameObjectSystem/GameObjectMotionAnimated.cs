using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.AnimationSystem;

namespace SpaceInvaders.GameObjectSystem
{
	/// <summary>
	///		Defines GameObjects that can be animated with AnimationMotion
	/// </summary>
	abstract class GameObjectMotionAnimated : GameObject
	{
		private AnimationMotion animator;


		//
		// Constructors
		//

		public GameObjectMotionAnimated() : base()
		{
			this.animator = AnimationMotionManager.Active.NullAnimation;
		}


		//
		// Methods
		//

		/// <summary>
		///		Sets the given motion animator to this GameObject
		/// </summary>
		/// <param name="newAnimator"></param>
		public void SetAnimator(AnimationMotion newAnimator)
		{
			this.animator.CancelTimedAnimation();
			AnimationMotionManager.Active.Recycle(this.ObjectName, Id);
			this.animator = newAnimator;
			this.animator.SetTarget(this);
			newAnimator.ScheduleTimedAnimation();
		}

		/// <summary>
		///		Resets animator information
		/// </summary>
		protected override void ResetAnimator()
		{
			this.animator.CancelTimedAnimation();
			AnimationMotionManager.Active.Recycle(this.ObjectName, Id);
			this.animator = AnimationMotionManager.Active.NullAnimation;
		}




		//
		// Properties
		//

		/// <summary>
		///		The motion animator of this GameObject
		/// </summary>
		public AnimationMotion Animator
		{
			get
			{
				return this.animator;
			}
		}
	}
}
