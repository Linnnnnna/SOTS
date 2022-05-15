using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.Pyramid
{
	[AutoloadEquip(EquipType.Body)]
	public class PatchLeatherTunic : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 80, 0);
			Item.rare = 4;
			Item.defense = 4;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Patch Leather Tunic");
			Tooltip.SetDefault("Increases minion damage by 10%\nGrants immunity to venom and poison debuffs");
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Mod.Find<ModItem>("PatchLeatherHat") .Type&& legs.type == Mod.Find<ModItem>("PatchLeatherPants").Type;
        }
		public override void DrawHands(ref bool drawHands, ref bool drawArms) {
			drawHands = true;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) += 0.10f;
			player.buffImmune[BuffID.Venom] = true;
			player.buffImmune[BuffID.Poisoned] = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Snakeskin>(), 28).AddRecipeGroup("SOTS:EvilMaterial", 10).AddTile(TileID.Anvils).Register();
		}
	}
}