
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.IceStuff
{
	public class HardIceBrickTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			minPick = 210;
			dustType = 34;
			drop = mod.ItemType("HardIceBrick");
			AddMapEntry(new Color(198, 249, 251));
			soundType = 21;
			soundStyle = 2;
		}
		public override bool CanExplode(int i, int j)
		{
			if (Main.tile[i, j].type == mod.TileType("HardIceBrickTile"))
			{
				return false;
			}
			return false;
		}
		public override bool Slope(int i, int j)
		{
			return false;
		}
	}
	public class HardIceBrickWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = false;
			AddMapEntry(new Color(144, 181, 181));
			dustType = 34;
		}
	}
	public class HardIceBrickWallItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hard Ice Wall Wall");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.StoneWall);
			item.createWall = ModContent.WallType<HardIceBrickWall>();
		}
	}
}