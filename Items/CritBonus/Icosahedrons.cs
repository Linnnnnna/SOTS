using SOTS.Items.Fragments;
using SOTS.Items.Permafrost;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.CritBonus
{
	public class BorealisIcosahedron : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Borealis Icosahedron");
			Tooltip.SetDefault("Critical strikes may cause a frostburn explosion, dealing 100% critical damage\n3% increased crit chance");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = Item.sellPrice(0, 1, 50, 0);
			Item.rare = ItemRarityID.Lime;
			Item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SOTSPlayer modPlayer = SOTSPlayer.ModPlayer(player);
			modPlayer.CritFrost = true;
			player.GetCritChance(DamageClass.Melee) += 3;
			player.GetCritChance(DamageClass.Ranged) += 3;
			player.GetCritChance(DamageClass.Magic) += 3;
			player.GetCritChance(DamageClass.Throwing) += 3;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<DissolvingAurora>(), 1).AddIngredient(ItemID.FrostCore, 1).AddIngredient(ModContent.ItemType<AbsoluteBar>(), 6).AddTile(TileID.MythrilAnvil).Register();
		}
	}
	public class CursedIcosahedron : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Icosahedron");
			Tooltip.SetDefault("Critical strikes may cause a release of cursed thunder, dealing 50% critical damage\nCritical strikes may also cause frostburn or flaming explosions, dealing 50% critical damage\n3% increased crit chance");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = Item.sellPrice(0, 5, 50, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SOTSPlayer modPlayer = SOTSPlayer.ModPlayer(player);
			modPlayer.CritCurseFire = true;
			player.GetCritChance(DamageClass.Melee) += 3;
			player.GetCritChance(DamageClass.Ranged) += 3;
			player.GetCritChance(DamageClass.Magic) += 3;
			player.GetCritChance(DamageClass.Throwing) += 3;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<BorealisIcosahedron>(), 1).AddIngredient(ModContent.ItemType<HellfireIcosahedron>(), 1).AddIngredient(ModContent.ItemType<DissolvingUmbra>(), 1).AddIngredient(ItemID.CursedFlame, 10).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
	public class HellfireIcosahedron : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellfire Icosahedron");
			Tooltip.SetDefault("Critical strikes may cause a flaming explosion, dealing 50% critical damage\n3% increased crit chance");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = Item.sellPrice(0, 0, 80, 0);
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SOTSPlayer modPlayer = SOTSPlayer.ModPlayer(player);
			modPlayer.CritFire = true;
			player.GetCritChance(DamageClass.Melee) += 3;
			player.GetCritChance(DamageClass.Ranged) += 3;
			player.GetCritChance(DamageClass.Magic) += 3;
			player.GetCritChance(DamageClass.Throwing) += 3;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.HellstoneBar, 6).AddIngredient(ModContent.ItemType<FragmentOfInferno>(), 6).AddTile(TileID.Anvils).Register();
		}
	}
}
