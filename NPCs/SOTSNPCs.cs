using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SOTS.NPCs
{
    public class SOTSNPCs : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
			Player player = Main.player[Main.myPlayer];
			if(npc.target <= 255)
			{
			player = Main.player[npc.target];
			}
            SOTSPlayer modPlayer = (SOTSPlayer)player.GetModPlayer(mod, "SOTSPlayer");	
			bool ZoneForest = !player.GetModPlayer<SOTSPlayer>().PyramidBiome && !player.ZoneDesert && !player.ZoneCorrupt && !player.ZoneDungeon && !player.ZoneDungeon && !player.ZoneHoly && !player.ZoneMeteor && !player.ZoneJungle && !player.ZoneSnow && !player.ZoneCrimson && !player.ZoneGlowshroom && !player.ZoneUndergroundDesert && (player.ZoneDirtLayerHeight || player.ZoneOverworldHeight) && !player.ZoneBeach;
				
			if(npc.lifeMax > 5)
			{
				if (Main.rand.Next(100) == 0 || (npc.type == 170 || npc.type == 171 || npc.type == 180)) { //guarenteed from pigrons
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AlmondMilk"), 1); 
				}
				if (Main.rand.Next(35) == 0) {
					//priorities: otherworld > tide > nature > permafrost > earth >  inferno
					//additional: evil & chaos (will not spawn in addition to forest)
					if(player.ZoneSkyHeight || player.ZoneMeteor)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfOtherworld"), Main.rand.Next(2) + 1); 
					}
					else if(player.ZoneBeach || player.ZoneDungeon)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfTide"), Main.rand.Next(2) + 1); 
					}
					else if(ZoneForest || player.ZoneJungle)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfNature"), Main.rand.Next(2) + 1); 
					}
					else if(player.ZoneSnow)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfPermafrost"), Main.rand.Next(2) + 1); 
					}
					else if(player.ZoneUndergroundDesert || player.ZoneDesert || player.GetModPlayer<SOTSPlayer>().PyramidBiome || player.ZoneRockLayerHeight)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfEarth"), Main.rand.Next(2) + 1); 
					}
					else if(player.ZoneUnderworldHeight)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfInferno"), Main.rand.Next(2) + 1); 
					}
				}
				else if(Main.rand.Next(34) == 0)
				{
					if(player.ZoneCorrupt || player.ZoneCrimson)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfEvil"), Main.rand.Next(2) + 1); 
					}
					if(player.ZoneHoly)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfChaos"), Main.rand.Next(2) + 1); 
					}
				}
				if (player.ZoneSnow && (Main.rand.Next(40) == 0 && !Main.expertMode)){
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StrawberryIcecream"),1);
				}
				if (player.ZoneSnow && (Main.rand.Next(50) == 0 && !Main.expertMode)){
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StrawberryIcecream"), 1);
				}
				if (npc.type == NPCID.WallofFlesh) {
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HungryHunter"), 1); 
				}
					
				if (npc.type == NPCID.WyvernHead) {
					if (Main.rand.Next(3) == 0) {
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GiantHarpyFeather, 1); 
					}
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FragmentOfOtherworld"), Main.rand.Next(2) + 1); 
				}
				
				if (npc.type == NPCID.SkeletronHead) {
				//	if (Main.rand.Next(10) == 0) 
						//Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BulletShark"), 1); 
				}
				if (npc.type == NPCID.GoblinPeon || npc.type == NPCID.GoblinArcher || npc.type == NPCID.GoblinWarrior || npc.type == NPCID.GoblinSorcerer) { //golbins
					if (Main.rand.Next(2) == 0 || Main.expertMode) {
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Goblinsteel"), Main.rand.Next(2) + 1); 
					}
				}
				if (npc.type == NPCID.PirateCaptain || npc.type == NPCID.PirateCorsair || npc.type == NPCID.PirateCrossbower || npc.type == NPCID.PirateDeadeye || npc.type == NPCID.Parrot) { //pirates
					if (Main.rand.Next(10) == 0) {
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Chocolate"), 1); 
					}
				}
				if (npc.type == NPCID.ElfCopter && Main.rand.Next(12) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HelicopterParts"), 1);
				}
				if (npc.type == NPCID.UndeadMiner) {
					if (Main.rand.Next(5) == 0) {          
						 Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ManicMiner"), 1); 
					}
				}
				if (npc.type == NPCID.BlueSlime) {
					if (Main.rand.Next(120) == 0) {
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireSpitter"), 1); 
					}
				}
				if (npc.type == NPCID.Crab) {
					if (Main.rand.Next(18) == 0) {
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CrabClaw"), 1); 
					}
				}
				if (npc.type == NPCID.Mothron && NPC.downedPlantBoss)
				{
					if (Main.rand.Next(5) == 0 && !Main.expertMode)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenVillainSword"), 1);
					}
					else if (Main.rand.Next(4) == 0 && Main.expertMode)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenVillainSword"), 1);
					}
				}
				if (npc.type == 64 && Main.rand.Next(60) == 0) {
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PinkJellyfishStaff"), 1); 
				}
				if (npc.type == 140 && Main.rand.Next(90) == 0) { //Possessed armor 
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PossessedHelmet"), 1); 
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PossessedChainmail"), 1); 
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PossessedGreaves"), 1); 
				}
				if ((npc.type == 63 || npc.type == 103) && Main.rand.Next(60) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlueJellyfishStaff"), 1); 
				}
			}
		}
		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns) 
		{
			if(player.GetModPlayer<SOTSPlayer>().PyramidBiome)
			{
				if(spawnRate > 60)
				{
					spawnRate = 60;
					maxSpawns = (int)(maxSpawns * 2f);
				}
			}
		}
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) 
		{
			if(spawnInfo.player.GetModPlayer<SOTSPlayer>().PyramidBiome && spawnInfo.spawnTileType == (ushort)mod.TileType("PyramidSlabTile"))
			{
				/*
				pool.Add(mod.NPCType("SnakePot"),0.5f);
				
				pool.Add(mod.NPCType("Snake"),1f);
				
				pool.Add(mod.NPCType("LostSoul"),0.7f);
				
				pool.Add(mod.NPCType("PyramidTreasureSlime"),0.4f);
				*/
				if(Main.hardMode)
				{
					pool.Add(NPCID.Mummy, 0.5f);
				}
			}
			
		}
	}
}