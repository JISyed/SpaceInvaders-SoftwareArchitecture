using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.ContainerSystem;
using SpaceInvaders.SpriteSystem;

namespace SpaceInvaders.SpriteBatchSystem
{
	sealed class SpriteBatchElement : ContainerNode
	{
		private Sprite sprite;
		private Sprite.Name name;



		//
		// Constructors
		//

		public SpriteBatchElement() : base()
		{
			this.sprite = null;
			this.name = Sprite.Name.UNINITIALIZED;
		}




		//
		// Methods
		//

		/// <summary>
		///		Draws the sprite being referenced
		/// </summary>
		public void Draw()
		{
			this.sprite.UpdateInternalData();
			this.sprite.Draw();
		}

		/// <summary>
		///		Sets the sprite reference. This also sets the name.
		/// </summary>
		/// <param name="newSprite"></param>
		public void SetSprite(Sprite newSprite)
		{
			this.sprite = newSprite;
			this.name = newSprite.SpriteName;
			this.SetId(newSprite.Id);
		}




		//
		// Properties
		//

		/// <summary>
		///		The name of the sprite reference
		/// </summary>
		public Sprite.Name Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>
		///		The sprite reference
		/// </summary>
		public Sprite SpriteReference
		{
			get
			{
				return this.sprite;
			}
		}




		//
		// Contracts
		//

		/// <summary>
		///		Clears the data in the element
		/// </summary>
		/// <remarks>
		///		Does not clear base data. Use <c>BaseReset()</c> for that.
		/// </remarks>
		public override void Reset()
		{
			this.name = Sprite.Name.UNINITIALIZED;
			this.sprite = null;
		}

		/// <summary>
		///		Used for AbstractManager searches
		/// </summary>
		/// <returns></returns>
		public override Enum GetName()
		{
			return this.name;
		}
	}
}
