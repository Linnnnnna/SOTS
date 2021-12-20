using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Dusts;
using SOTS.Items.Otherworld.FromChests;
using SOTS.Items.Otherworld.Furniture;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.Fragments
{
	public abstract class PlatingTile : ModTile
	{
		public virtual Texture2D glowTexture => mod.GetTexture("Items/Fragments/PermafrostPlatingTileGlow");
		public sealed override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileBrick[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			drop = ModContent.ItemType<PermafrostPlating>();
			AddMapEntry(new Color(165, 179, 198));
			mineResist = 1.5f;
			soundType = SoundID.Tink;
			soundStyle = 2;
			dustType = DustID.Silver;
			SafeSetDefaults();
		}
		public virtual void SafeSetDefaults()
		{

		}
		public virtual bool canGlow(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int frameX = tile.frameX / 18;
			int frameY = tile.frameY / 18;
			if (frameX == 5 && (frameY >= 0 && frameY <= 2))
			{
				return false;
			}
			if (frameX >= 6 && frameX <= 8 && (frameY == 0 || frameY == 3))
				return false;
			return true;
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (canGlow(i, j))
			{
				r = 0.2f;
				g = 0.25f;
				b = 0.25f;
			}
			else
			{
				r = 0;
				g = 0;
				b = 0;
			}
			base.ModifyLight(i, j, ref r, ref g, ref b);
		}
		public sealed override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if(canGlow(i, j))
				DrawLights(i, j, spriteBatch);
		}
		public void DrawLights(int i, int j, SpriteBatch spriteBatch)
		{
			float uniquenessCounter = Main.GlobalTime * -100 + (i + j) * 5;
			Tile tile = Main.tile[i, j];
			Texture2D texture = glowTexture;
			Rectangle frame = new Rectangle(tile.frameX, tile.frameY, 16, 16);
			Color color;
			color = WorldGen.paintColor((int)Main.tile[i, j].color()) * (100f / 255f);
			color.A = 0;
			float alphaMult = 0.55f + 0.45f * (float)Math.Sin(MathHelper.ToRadians(uniquenessCounter));
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			for (int k = 0; k < 3; k++)
			{
				Vector2 pos = new Vector2((i * 16 - (int)Main.screenPosition.X), (j * 16 - (int)Main.screenPosition.Y)) + zero;
				Vector2 offset = new Vector2(Main.rand.NextFloat(-1, 1f), Main.rand.NextFloat(-1, 1f)) * 0.25f * k;
				Main.spriteBatch.Draw(texture, pos + offset, frame, color * alphaMult * 0.6f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
		}
		public sealed override bool CanExplode(int i, int j)
		{
			return false;
		}
		public sealed override bool Slope(int i, int j)
		{
			return false;
		}
	}
	public class NaturePlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nature Plating");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.StoneBlock);
			item.rare = ItemRarityID.Blue;
			item.createTile = ModContent.TileType<NaturePlatingTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FragmentOfNature>(), 1);
			recipe.AddRecipeGroup("SOTS:PHMOre", 1);
			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
		}
	}
	public class NaturePlatingTile : PlatingTile
	{
		public override Texture2D glowTexture => mod.GetTexture("Items/Fragments/NaturePlatingTileGlow");
		public override void SafeSetDefaults()
		{
			drop = ModContent.ItemType<NaturePlating>();
			AddMapEntry(new Color(158, 177, 171));
			mineResist = 1.5f;
			soundType = SoundID.Tink;
			soundStyle = 2;
			dustType = DustID.Tungsten;
		}
		public override bool canGlow(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int frameX = tile.frameX / 18;
			int frameY = tile.frameY / 18;
			if (frameX >= 1 && frameX <= 3 && (frameY == 0 || frameY == 2))
			{
				return true;
			}
			if (frameX >= 0 && frameX <= 5 && (frameY == 3 || frameY == 4))
				return true;
			return false;
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (canGlow(i, j))
			{
				r = 0.15f;
				g = 0.2f;
				b = 0.13f;
			}
			else
			{
				r = 0;
				g = 0;
				b = 0;
			}
			base.ModifyLight(i, j, ref r, ref g, ref b);
		}
	}
	public class EarthenPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Plating");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.StoneBlock);
			item.rare = ItemRarityID.Blue;
			item.createTile = ModContent.TileType<EarthenPlatingTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FragmentOfEarth>(), 1);
			recipe.AddRecipeGroup("SOTS:PHMOre", 1);
			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
		}
	}
	public class EarthenPlatingTile : PlatingTile
	{
		public override Texture2D glowTexture => mod.GetTexture("Items/Fragments/EarthenPlatingTileGlow");
		public override void SafeSetDefaults()
		{
			drop = ModContent.ItemType<EarthenPlating>();
			AddMapEntry(new Color(112, 90, 86));
			mineResist = 1.5f;
			soundType = SoundID.Tink;
			soundStyle = 2;
			dustType = DustID.Iron;
		}
		public override bool canGlow(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int frameX = tile.frameX / 18;
			int frameY = tile.frameY / 18;
			if (frameX == 0 || frameX == 4 || frameX == 5)
			{
				return true;
			}
			if (frameX >= 0 && frameX <= 5 && (frameY == 3 || frameY == 4))
				return true;
			if (frameX >= 6 && frameX <= 8 && (frameY == 0 || frameY == 3))
				return true;
			return false;
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (canGlow(i, j))
			{
				r = 0.25f;
				g = 0.22f;
				b = 0.11f;
			}
			else
			{
				r = 0;
				g = 0;
				b = 0;
			}
			base.ModifyLight(i, j, ref r, ref g, ref b);
		}
	}
	public class PermafrostPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Permafrost Plating");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.StoneBlock);
			item.rare = ItemRarityID.Blue;
			item.createTile = ModContent.TileType<PermafrostPlatingTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FragmentOfPermafrost>(), 1);
			recipe.AddRecipeGroup("SOTS:PHMOre", 1);
			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
		}
	}
	public class PermafrostPlatingTile : PlatingTile
	{
		public override Texture2D glowTexture => mod.GetTexture("Items/Fragments/PermafrostPlatingTileGlow");
		public override void SafeSetDefaults()
		{
			drop = ModContent.ItemType<PermafrostPlating>();
			AddMapEntry(new Color(165, 179, 198));
			mineResist = 1.5f;
			soundType = SoundID.Tink;
			soundStyle = 2;
			dustType = DustID.Silver;
		}
		public override bool canGlow(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int frameX = tile.frameX / 18;
			int frameY = tile.frameY / 18;
			if (frameX == 5 && (frameY >= 0 && frameY <= 2))
			{
				return false;
			}
			if (frameX >= 6 && frameX <= 8 && (frameY == 0 || frameY == 3))
				return false;
			return true;
		}
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			if(canGlow(i, j))
			{
				r = 0.2f;
				g = 0.25f;
				b = 0.25f;
			}
			else
            {
				r = 0;
				g = 0;
				b = 0;
            }
            base.ModifyLight(i, j, ref r, ref g, ref b);
        }
	}
}