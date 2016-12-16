using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.AnimationSystem;
using SpaceInvaders.SpriteBatchSystem;
using SpaceInvaders.CollisionSystem;

namespace SpaceInvaders.GameObjectSystem
{
	abstract class GameObject : PCSTreeNode
	{
		private SpriteProxy sprite;
		private Collider collider;
		private GameObject.Name name;
		private SpriteBatch.Name batchName;
		private Azul.Color color;
		private float x;
		private float y;
		private uint instanceId;
		private bool wasAlreadyRemoved = false;




		//
		// Constructors
		//

		public GameObject() : base()
		{
			// Guaranteed unique Id, unless UINT_MAX objects are created
			this.ctor(UniqueObjectId.NewId);
		}

		private void ctor(uint newId)
		{
			this.sprite = SpriteProxyManager.Active.NullSpriteProxy;
			this.collider = new Collider();
			this.name = GameObject.Name.UNINITIALIZED;
			this.batchName = SpriteBatch.Name.UNINITIALIZED;
			this.color = Colors.White;
			this.x = 0.0f;
			this.y = 0.0f;
			this.instanceId = newId;

			// All GameObjects have collisions by default
			// Override by calling Collider.MakeColliderNull()
			//      in the derived class's constructor or Start() method
			if (this.Collider.IsNull)
				this.Collider.SetupCollisonSpriteProxy(5, 5, this.instanceId);
		}




		//
		// Destructor
		//

		~GameObject()
		{
			this.RemoveThisPCSNode();
		}




		//
		// Methods
		//

		/// <summary>
		///		Updates the sprite data of an individual GameObject
		/// </summary>
		public void UpdateInternalSpriteData()
		{
			Debug.Assert(this.sprite != null);

			// Copy GameObject data to Sprite
			this.sprite.X = this.x;
			this.sprite.Y = this.y;
			this.sprite.Color = this.color;

			// Update Collider
			this.RecalculateCollider();
		}

		/// <summary>
		///		Sets the enum name of the GameObject. For manager use.
		/// </summary>
		/// <param name="newName"></param>
		public void SetName(GameObject.Name newName)
		{
			this.name = newName;
		}

		/// <summary>
		///		Sets the new instance ID of the GameObject
		/// </summary>
		/// <param name="newId"></param>
		public void SetId(uint newId)
		{
			this.instanceId = newId;
			this.collider.SetId(newId);
			//this.sprite.SetId(newId);
			if(this.collider.SpriteReference != null)
			{
				this.collider.SpriteReference.SetId(newId);
			}
		}

		/// <summary>
		///		Changes the sprite reference
		/// </summary>
		/// <param name="newSprite"></param>
		public void SetSprite(SpriteBatch.Name newBatchName, SpriteEntity newSprite)
		{
			// Remove the old sprite from it's sprite batch
			SpriteBatch oldBatch = SpriteBatchManager.Active.Find(this.batchName);
			if (oldBatch != null)
			{
				bool su = oldBatch.Detach(this.sprite.SpriteName, this.sprite.Id);
				Debug.Assert(su, "The old SpriteBatch has to detach a valid (non-null) sprite.");
			}

			// Recycle old sprite
			SpriteProxyManager.Active.Recycle(this.sprite.SpriteName, this.sprite.Id);
			this.sprite = null;

			// Get new batch
			SpriteBatch newBatch = SpriteBatchManager.Active.Find(newBatchName);
			this.batchName = newBatchName;

			// If null, get null proxy and leave
			if(newBatch == null)
			{
				this.sprite = SpriteProxyManager.Active.NullSpriteProxy;
				return; // Leave
			}

			// Get new sprite and attach it
			this.sprite = SpriteProxyManager.Active.Create(newSprite.SpriteName, this.Id);
			newBatch.Attach(this.sprite, this.sprite.Id);

			this.collider.SetupCollisonSpriteProxy(this.sprite);
		}

		

		/// <summary>
		///		Sets the new position. Use X or Y properties to leave X or Y unchanged
		/// </summary>
		/// <param name="newX"></param>
		/// <param name="newY"></param>
		/// <remarks>
		///		Only AlienCoordinator is allowed to override this!
		/// </remarks>
		public void SetPosition(float newX, float newY)
		{
			float deltaX = newX - this.x;
			float deltaY = newY - this.y;
			this.x = newX;
			this.y = newY;
			this.collider.SetPosition(this.Collider.X + deltaX, this.Collider.Y + deltaY);

			GameObject itr = this.Child as GameObject;
			while(itr != null)
			{
				itr.SetPosition(itr.X + deltaX, itr.Y + deltaY);
				itr = itr.Sibling as GameObject;
			}

			// Call the OnMove event
			this.OnMove();
		}

		/// <summary>
		///		Set the new color of the GameObject
		/// </summary>
		/// <param name="newColor"></param>
		public void SetColor(Azul.Color newColor)
		{
			this.color = newColor;
		}


		/// <summary>
		///		Clears the data in the GameObject. Base must be called if overriding!
		/// </summary>
		public void Reset()
		{
			this.wasAlreadyRemoved = true;

			this.OnDestroy();

			// Get the parent of this GameObject
			GameObject parent = this.Parent as GameObject;

			this.RemoveThisPCSNode();

			// Do post-delete procedure of the parent's collider
			if (parent != null)
			{
				parent.ReevaluateCollider();
			}

			this.ResetAnimator();

			this.collider.Reset();

			// Remove the old sprite from it's sprite batch
			SpriteBatch oldBatch = SpriteBatchManager.Active.Find(this.batchName);
			if (oldBatch != null)
			{
				bool su = oldBatch.Detach(this.sprite.SpriteName, this.sprite.Id);
				Debug.Assert(su, "The old SpriteBatch has to detach a valid (non-null) sprite.");
			}

			SpriteProxyManager.Active.Recycle(this.sprite.SpriteName, this.sprite.Id);
			this.sprite = SpriteProxyManager.Active.NullSpriteProxy;

			this.name = GameObject.Name.UNINITIALIZED;
			this.batchName = SpriteBatch.Name.UNINITIALIZED;
			this.color = Colors.White;
			this.x = 0.0f;
			this.y = 0.0f;
			this.instanceId = 0u;
		}


		/// <summary>
		///		Resets animator information
		/// </summary>
		protected virtual void ResetAnimator()
		{
			// Does nothing here. See GameObjectMotionAnimated.cs.
		}

		/// <summary>
		///		Used to resize the collider if child objects change
		/// </summary>
		public void RecalculateCollider()
		{
			// Skip if this GameObject has no children
			if(this.Child == null)
			{
				return;
			}

			if(this.Collider.IsNull)
			{
				return;
			}

			// Find the first child with a valid collider
			GameObject itr = this.Child as GameObject;
			while(itr != null)
			{
				if(itr.Collider.IsNull == false)
				{
					break; // Found one!
				}

				itr = itr.Sibling as GameObject;
			}

			if(itr == null)
			{
				return;
			}

			// Set the first child's collider as yours
			this.Collider.SetPosition(itr.Collider.X, itr.Collider.Y);
			this.Collider.Resize(itr.Collider.Width, itr.Collider.Height);

			// Resize and reposition with every other sibling
			itr = itr.Sibling as GameObject;
			while(itr != null)
			{
				// Calculate the union of the colliders (parent with child)
				this.Collider.Union(itr.Collider);

				// Next sibling
				itr = itr.Sibling as GameObject;
			}
		}

		/// <summary>
		///		Check if the collider should be resized or nullified
		///		and does the appropriate action
		/// </summary>
		public void ReevaluateCollider()
		{
			// If no child
			if (this.Child == null)
			{
				// Nullify this object's collider
				this.collider.MakeColliderNull();
			}
			// If there is a child
			else
			{
				// Resize the collider
				this.RecalculateCollider();
			}

			// Reevaluate this object's parent, if any
			GameObject parent = this.Parent as GameObject;
			if(parent != null)
			{
				parent.ReevaluateCollider();
			}
		}






		//
		// Private Methods
		//





		//
		// New Contracts
		//

		/// <summary>
		///		Starting procedure when a GameObject is put into the game
		/// </summary>
		abstract public void Start();

		/// <summary>
		///		Custom update loop routine. Returns false if it wants to delete itself.
		/// </summary>
		abstract public bool Update();
		
		/// <summary>
		///		Get's called after base GameObject's Reset() method
		/// </summary>
		abstract protected void OnDestroy();

		/// <summary>
		///		Get's called when the GameObject's position is changed
		/// </summary>
		virtual protected void OnMove()
		{
			// Not implemented here
		}


		/// <summary>
		///		Executes when a collison occurs with another GameObject
		/// </summary>
		/// <param name="collisonName"></param>
		/// <param name="other"></param>
		/// <returns>
		///		<c>true</c> if the object is to be deleted
		///	</returns>
		abstract public bool OnCollide(CollisionPairEvaluator.Name collisonName, GameObject other);






		//
		// Contracts
		//

		/// <summary>
		///		Gets called when this GameObject is removed from
		///		a PCS hierarchy
		/// </summary>
		protected override void OnPCSUnlink()
		{
			if (this.wasAlreadyRemoved == false)
			{
				GameObjectManager.Active.Recycle(this.name, this.instanceId);
			}
		}

		/// <summary>
		///		Gets called when this GameObject adds a new child
		/// </summary>
		protected override void OnPCSNewChild(bool alreadyHadChildren)
		{
			// If this GameObject previously had no children...
			if(alreadyHadChildren == false)
			{
				// And is a null collider...
				if(this.Collider.IsNull)
				{
					// Make a new collider
					this.Collider.SetupCollisonSpriteProxy(5.0f, 5.0f, this.Id);
				}
			}
		}







		//
		// Properties
		//

		/// <summary>
		///		The sprite data (not Azul Sprite)
		/// </summary>
		public SpriteProxy SpriteReference
		{
			get
			{
				return this.sprite;
			}
		}

		/// <summary>
		///		The enum name of this GameObject
		/// </summary>
		public GameObject.Name ObjectName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>
		///		Current x-position
		/// </summary>
		public float X
		{
			get
			{
				return this.x;
			}
		}

		/// <summary>
		///		Current y-position
		/// </summary>
		public float Y
		{
			get
			{
				return this.y;
			}
		}

		/// <summary>
		///		Current color
		/// </summary>
		public Azul.Color Color
		{
			get
			{
				return this.color;
			}
		}

		/// <summary>
		///		Returns the name of the associated SpriteBatch's name
		/// </summary>
		public SpriteBatch.Name BatchName
		{
			get
			{
				return this.batchName;
			}
		}

		/// <summary>
		///		The Instance ID of the GameObject
		/// </summary>
		public uint Id
		{
			get
			{
				return this.instanceId;
			}
		}
		
		/// <summary>
		///		The GameObject's collison data
		/// </summary>
		public Collider Collider
		{
			get
			{
				return this.collider;
			}
		}

		/// <summary>
		///		The GameObject's width (based off collider's width)
		/// </summary>
		public float Width
		{
			get
			{
				return this.collider.Width;
			}
		}

		/// <summary>
		///		The GameObject's height (based off collider's height)
		/// </summary>
		public float Height
		{
			get
			{
				return this.collider.Height;
			}
		}


		//
		// Nested Enums
		//

		/// <summary>
		///		Possible names of a GameObject
		/// </summary>
		public enum Name
		{
			UNINITIALIZED,
			EMPTY,
			AlienCrab,
			AlienSquid,
			AlienOctopus,
			UFO,
			//UFOMissile,
			UFOCoordinator,
			Player,
			LaserAlien,
			LaserPlayer,
			AlienCoordinator,
			AlienColumn,
			Wall,
			ShieldPiece,
			ShieldPieceColumn,
			ShieldBarricade,
			ShieldRoot,
			DeadAlien,
			DeadPlayerLaser,
			//DeadAlienLaser,
			//DeadUFO
		}
	}
}
