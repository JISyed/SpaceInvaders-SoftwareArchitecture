using Enum = System.Enum;
using Debug = System.Diagnostics.Debug;
using SpaceInvaders.TextureSystem;

namespace SpaceInvaders.ImageSystem
{
	class ImageFactory
	{
		/// <summary>
		///		Make all the Images. Should only be called once per program run!
		/// </summary>
		public void Create()
		{
			ImageManager.Self.Create(Image.Name.UFO_V, Texture.Name.InvadersFromSpace, 122, 491, 95, 44);
			ImageManager.Self.Create(Image.Name.UFOMissile_Gamma, Texture.Name.InvadersFromSpace, 658, 469, 24, 65);
			ImageManager.Self.Create(Image.Name.SmallExplosion_Delta, Texture.Name.InvadersFromSpace, 684, 82, 41, 34);

			ImageManager.Self.Create(Image.Name.Block, Texture.Name.InvadersFromSpace, 163, 495, 13, 13);
			ImageManager.Self.Create(Image.Name.BlockUL, Texture.Name.InvadersFromSpace, 133, 490, 13, 13);
			ImageManager.Self.Create(Image.Name.BlockUR, Texture.Name.InvadersFromSpace, 193, 490, 13, 13);
			ImageManager.Self.Create(Image.Name.BlockBL, Texture.Name.InvadersFromSpace, 133, 522, 13, 13);
			ImageManager.Self.Create(Image.Name.BlockBR, Texture.Name.InvadersFromSpace, 193, 522, 13, 13);

			ImageManager.Self.Create(Image.Name.Player_W, Texture.Name.InvadersFromSpace, 243, 494, 57, 40);
			ImageManager.Self.Create(Image.Name.DeadPlayer1_X, Texture.Name.InvadersFromSpace, 330, 491, 81, 46);
			ImageManager.Self.Create(Image.Name.DeadPlayer2_S, Texture.Name.InvadersFromSpace, 520, 342, 82, 55);

			ImageManager.Self.Create(Image.Name.AlienCrab_B, Texture.Name.InvadersFromSpace, 136, 65, 85, 62);
			ImageManager.Self.Create(Image.Name.AlienCrab_C, Texture.Name.InvadersFromSpace, 253, 65, 85, 62);
			ImageManager.Self.Create(Image.Name.AlienSquid_D, Texture.Name.InvadersFromSpace, 370, 65, 62, 62);
			ImageManager.Self.Create(Image.Name.AlienSquid_E, Texture.Name.InvadersFromSpace, 465, 65, 62, 62);
			ImageManager.Self.Create(Image.Name.AlienOctopus_F, Texture.Name.InvadersFromSpace, 559, 65, 93, 62);
			ImageManager.Self.Create(Image.Name.AlienOctopus_G, Texture.Name.InvadersFromSpace, 27, 202, 93, 62);
			ImageManager.Self.Create(Image.Name.DeadAlien_Z, Texture.Name.InvadersFromSpace, 490, 489, 73, 44);
			ImageManager.Self.Create(Image.Name.LaserZigzag_Y, Texture.Name.InvadersFromSpace, 438, 491, 22, 39);
			ImageManager.Self.Create(Image.Name.LaserDagger_Beta, Texture.Name.InvadersFromSpace, 619, 491, 24, 41);
			ImageManager.Self.Create(Image.Name.LaserStraight_Alpha, Texture.Name.InvadersFromSpace, 590, 488, 10, 46);

			ImageManager.Self.Create(Image.Name.Floor, Texture.Name.InvadersFromSpace, 446, 316, 2, 2);
		}
	}
}
