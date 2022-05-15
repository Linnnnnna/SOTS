using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using SOTS.Items.Fragments;

namespace SOTS.Items.Furniture.Earthen
{
	public class EarthenPlatingPiano : ModItem
	{
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.StoneBlock);
			Item.Size = new Vector2(38, 26);
			Item.rare = ItemRarityID.Blue;
			Item.createTile = ModContent.TileType<EarthenPlatingPianoTile>();
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<EarthenPlating>(), 15).AddIngredient(ItemID.Bone, 4).AddIngredient(ItemID.Book, 1).AddTile(TileID.Anvils).Register();
		}
	}
	public class EarthenPlatingPianoTile : Piano<EarthenPlatingPiano>
	{
		public override bool CanExplode(int i, int j)
		{
			return false;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D glowmask = (Texture2D)ModContent.Request<Texture2D>(this.GetPath("Glow"));
            SOTSTile.DrawSlopedGlowMask(i, j, -1, glowmask, Color.White, Vector2.Zero);
        }
    }
}