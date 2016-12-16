using Debug = System.Diagnostics.Debug;
using SpaceInvaders.SpriteSystem;
using SpaceInvaders.SpriteBatchSystem;

namespace SpaceInvaders.GameObjectSystem
{
	class Collider
	{
		private SpriteCollisionProxy collisionSprite;
		private Azul.Rect colliderBoundary;
		private Azul.Color color;
		private uint gameObjectId;

		//
		// Constructor
		//

		public Collider()
		{
			this.collisionSprite = null;
			this.colliderBoundary = new Azul.Rect();
			this.gameObjectId = 0u;
			this.color = Colors.Green;
		}




		//
		// Methods
		//

		/// <summary>
		///		Set this Collider's GameObject ID
		/// </summary>
		/// <param name="newId"></param>
		public void SetId(uint newId)
		{
			this.gameObjectId = newId;
		}


		/// <summary>
		///		Reset function that can only be called by the base GameObject!
		/// </summary>
		public void Reset()
		{
			// Remove previous SpriteCollisionProxy if any
			if (this.collisionSprite != null)
			{
				SpriteBatch colliderBatch = SpriteBatchManager.Active.Find(SpriteBatch.Name.SpriteCollisions);
				bool success = colliderBatch.Detach(collisionSprite.SpriteName, collisionSprite.Id);
				Debug.Assert(success, "The collision sprite proxy wasn't found during collider's reset!");
				Debug.Assert(this.collisionSprite.SpriteName != Sprite.Name.UNINITIALIZED, "The collision sprite proxy already seems to be uninitialized, but it's probably not");
				SpriteCollisonProxyManager.Active.Recycle(collisionSprite.SpriteName, collisionSprite.Id);
				this.collisionSprite = null;
			}
			
			this.gameObjectId = 0u;
			this.color = Colors.Yellow;
		}

		/// <summary>
		///		Changes the width and height of this Collider
		/// </summary>
		/// <param name="newWidth"></param>
		/// <param name="newHeight"></param>
		public void Resize(float newWidth, float newHeight)
		{
			this.colliderBoundary.w = newWidth;
			this.colliderBoundary.h = newHeight;
			if (this.collisionSprite != null)
				this.collisionSprite.Resize(newWidth, newHeight);
		}

		/// <summary>
		///		Setup the boundaries and position of this collider given a sprite
		/// </summary>
		/// <param name="theProxy"></param>
		public void SetupCollisonSpriteProxy(SpriteProxy theProxy)
		{
			SpriteBatch colliderBatch = SpriteBatchManager.Active.Find(SpriteBatch.Name.SpriteCollisions);

			// Remove previous SpriteCollisionProxy if any
			if (this.collisionSprite != null)
			{
				bool oldProxyRemoved = colliderBatch.Detach(collisionSprite.SpriteName, collisionSprite.Id);
				Debug.Assert(oldProxyRemoved, "The old collision sprite proxy wasn't removed upon setting a new proxy!");
				Debug.Assert(this.collisionSprite.SpriteName != Sprite.Name.UNINITIALIZED);
				SpriteCollisonProxyManager.Active.Recycle(collisionSprite.SpriteName, collisionSprite.Id);
				this.collisionSprite = null;
			}

			// Create a new proxy
			this.collisionSprite = SpriteCollisonProxyManager.Active.Create(theProxy.SpriteName, theProxy.Id);
			this.collisionSprite.SetId(theProxy.Id);
			this.collisionSprite.SetPosition(this.colliderBoundary.x, this.colliderBoundary.y);
			this.colliderBoundary.w = this.collisionSprite.ModelSprite.Width;
			this.colliderBoundary.h = this.collisionSprite.ModelSprite.Height;
			this.collisionSprite.SetColor(this.color);

			// Attach it to the batch
			colliderBatch.Attach(this.collisionSprite, this.collisionSprite.Id);
		}

		/// <summary>
		///		Setup the boundaries and position of this collider given a width and height
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void SetupCollisonSpriteProxy(float width, float height, uint newId)
		{
			SpriteBatch colliderBatch = SpriteBatchManager.Active.Find(SpriteBatch.Name.SpriteCollisions);

			// Remove previous SpriteCollisionProxy if any
			if (this.collisionSprite != null)
			{
				bool oldRemSuccess = colliderBatch.Detach(collisionSprite.SpriteName, collisionSprite.Id);
				Debug.Assert(oldRemSuccess, "Upon setting a new collider sprite proxy, the old one was not deleted!");
				Debug.Assert(this.collisionSprite.SpriteName != Sprite.Name.UNINITIALIZED);
				SpriteCollisonProxyManager.Active.Recycle(collisionSprite.SpriteName, collisionSprite.Id);
				this.collisionSprite = null;
			}

			// Create a new proxy
			this.collisionSprite = SpriteCollisonProxyManager.Active.Create(Sprite.Name.NULL, newId);
			this.collisionSprite.SetId(newId);
			this.collisionSprite.SetPosition(this.colliderBoundary.x, this.colliderBoundary.y);
			this.collisionSprite.Resize(width, height);
			this.colliderBoundary.w = width;
			this.colliderBoundary.h = height;
			this.collisionSprite.SetColor(this.color);

			// Attach it to the batch
			colliderBatch.Attach(this.collisionSprite, this.collisionSprite.Id);
		}


		/// <summary>
		///		Makes the collider null so that it doesn't 
		///		render a SpriteCollison or check collisions
		/// </summary>
		public void MakeColliderNull()
		{
			// Remove previous SpriteCollisionProxy if any
			if (this.collisionSprite != null)
			{
				SpriteBatch colliderBatch = SpriteBatchManager.Active.Find(SpriteBatch.Name.SpriteCollisions);
				bool colSprRemoved = colliderBatch.Detach(collisionSprite.SpriteName, collisionSprite.Id);
				Debug.Assert(colSprRemoved, "This collision sprite proxy wasn't removed for some reason!");
				Debug.Assert(this.collisionSprite.SpriteName != Sprite.Name.UNINITIALIZED);
				SpriteCollisonProxyManager.Active.Recycle(collisionSprite.SpriteName, collisionSprite.Id);
				this.collisionSprite = null;
			}
		}

		/// <summary>
		///		Changes the position of this Collider's boundaries and its SpriteCollision
		/// </summary>
		/// <param name="newX"></param>
		/// <param name="newY"></param>
		public void SetPosition(float newX, float newY)
		{
			this.colliderBoundary.x = newX;
			this.colliderBoundary.y = newY;
			if(this.collisionSprite != null)
				this.collisionSprite.SetPosition(newX, newY);
		}


		/// <summary>
		///		Compute the boundaries of this parent collider with a child collider
		/// </summary>
		/// <param name="childCollider"></param>
		public void Union(Collider childCollider)
		{
			// Leave if the child collider is null
			if (childCollider.IsNull)
				return;	

			float pLeft = this.X - (this.Width / 2.0f);
			float pRight = this.X + (this.Width / 2.0f);
			float pBottom = this.Y - (this.Height / 2.0f);
			float pTop = this.Y + (this.Height / 2.0f);

			float cLeft = childCollider.X - (childCollider.Width / 2.0f);
			float cRight = childCollider.X + (childCollider.Width / 2.0f);
			float cBottom = childCollider.Y - (childCollider.Height / 2.0f);
			float cTop = childCollider.Y + (childCollider.Height / 2.0f);

			float l, r, t, b;

			if(pLeft < cLeft)
			{
				l = pLeft;
			}
			else
			{
				l = cLeft;
			}

			if(cRight > pRight)
			{
				r = cRight;
			}
			else
			{
				r = pRight;
			}

			if(pBottom < cBottom)
			{
				b = pBottom;
			}
			else
			{
				b = cBottom;
			}

			if(pTop > cTop)
			{
				t = pTop;
			}
			else
			{
				t = cTop;
			}

			this.Resize(r - l, t - b);
			this.SetPosition(l + ((r - l) / 2.0f), b + ((t - b) / 2.0f));
		}




		//
		// Static Methods
		//

		/// <summary>
		///		Check to see if two colliders intersect
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		/// <remarks>
		///		Assumes that rectangle's pivot is at the center
		/// </remarks>
		static public bool Intersect(Collider first, Collider second)
		{
			// No collisions if the colliders are null
			if (first.IsNull || second.IsNull)
				return false;

			// Check if twice the x line between the two centers are 
			// less than the widths combined
			// And if twice the y line between the two centers are
			// less then the heights combined
			return (Mathf.Abs(first.X - second.X) * 2.0f < (first.Width + second.Width)) 
				&& (Mathf.Abs(first.Y - second.Y) * 2.0f < (first.Height + second.Height));
		}




		//
		// Private Methods
		//




		//
		// Properties
		//

		/// <summary>
		///		The CollisionSpriteProxy of this Collider
		/// </summary>
		public SpriteCollisionProxy SpriteReference
		{
			get
			{
				return this.collisionSprite;
			}
		}

		/// <summary>
		///		The Id of this Collider's GameObject
		/// </summary>
		public uint Id
		{
			get
			{
				return this.gameObjectId;
			}
		}

		/// <summary>
		///		The color of this Collider
		/// </summary>
		public Azul.Color Color
		{
			get
			{
				return this.color;
			}

			set
			{
				this.color = value;
				if(this.collisionSprite != null)
					this.collisionSprite.SetColor(value);
			}
		}

		/// <summary>
		///		Determines if this Collider is null
		/// </summary>
		/// <remarks>
		///		This is determined if it doesn't have a SpriteCollisionProxy
		/// </remarks>
		public bool IsNull
		{
			get
			{
				return this.collisionSprite == null;
			}
		}

		/// <summary>
		///		X position of this collider. Read only
		/// </summary>
		public float X
		{
			get
			{
				return this.colliderBoundary.x;
			}
		}

		/// <summary>
		///		Y position of this collider. Read only
		/// </summary>
		public float Y
		{
			get
			{
				return this.colliderBoundary.y;
			}
		}

		/// <summary>
		///		Width of this collider. Read only
		/// </summary>
		public float Width
		{
			get
			{
				return this.colliderBoundary.w;
			}
		}
		
		/// <summary>
		///		Height of this collider. Read only
		/// </summary>
		public float Height
		{
			get
			{
				return this.colliderBoundary.h;
			}
		}

	}
}
