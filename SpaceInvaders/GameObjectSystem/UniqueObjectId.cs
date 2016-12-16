using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders.GameObjectSystem
{
	/// <summary>
	///		System for guaranteeing unique Ids for GameObjects
	/// </summary>
	static class UniqueObjectId
	{
		private static uint currentId = 1u;

		/// <summary>
		///		Get a unique Id as uint
		/// </summary>
		public static uint NewId
		{
			get
			{
				uint newId = UniqueObjectId.currentId;
				UniqueObjectId.currentId++;
				return newId;
			}
		}
	}
}
