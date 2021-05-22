using SOTS.NPCs.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.GelGear
{
	public class PinkyBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("Right Click to open");
		}
		public override void SetDefaults()
		{

			item.width = 40;
			item.height = 32;
			item.value = 0;
			item.rare = 6;
			item.expert = true;
			item.maxStack = 99;
			item.consumable = true;
			//bossBagNPC = mod.NPCType("PutridPinky2Head");
		}
		public override int BossBagNPC => ModContent.NPCType<PutridPinkyPhase2>();
		public override bool CanRightClick()
		{
			return true;
		}
		public override void OpenBossBag(Player player)
		{
			player.QuickSpawnItem(mod.ItemType("PutridEye"));
			player.QuickSpawnItem(ModContent.ItemType<VialofAcid>(), Main.rand.Next(20, 30));
			player.QuickSpawnItem(ItemID.PinkGel,Main.rand.Next(40, 60));
			player.QuickSpawnItem(mod.ItemType("Wormwood"), Main.rand.Next(20, 30));
				
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("GelWings"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodParasite"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodHelix"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodCrystal"),Main.rand.Next(200, 500));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodHook"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodCollapse"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodScepter"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodStaff"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodSpike"));
		}
	}
}