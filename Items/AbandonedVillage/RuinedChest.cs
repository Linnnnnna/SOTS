using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SOTS.Items.AbandonedVillage
{
	public class RuinedChest : ModItem
	{
		public override void SetStaticDefaults() => this.SetResearchCost(1);
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Chest);
			Item.width = 32;
			Item.height = 28;
			Item.value = Item.sellPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.White;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<RuinedChestTile>();
		}
	}
	public class RuinedChestTile : Furniture.ContainerType
	{
		protected override string ChestName => Language.GetTextValue("Mods.SOTS.ItemName.RuinedChest");
		protected override int ChestDrop => ModContent.ItemType<RuinedChest>();
		protected override int ChestKey => ModContent.ItemType<OldKey>();
		protected override int DustType => 122;
		protected override void AddMapEntires()
		{
			ModTranslation name = CreateMapEntryName();
			AddMapEntry(new Color(180, 130, 100), name, MapChestName);

			name = CreateMapEntryName(Name + "_Locked"); // With multiple map entries, you need unique translation keys.
			AddMapEntry(new Color(180, 130, 100), name, MapChestName);
		}
	}
}