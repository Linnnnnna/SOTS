using SOTS.Items.Chaos;
using Microsoft.Xna.Framework;
using System;
using System.Threading;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using SOTS.Void;
using Terraria.ID;

namespace SOTS
{
    internal sealed class PhaseWorldgenHelper
    {
        public static bool Generating { get; private set; }

        public static string GetStatus()
        {
            return "Generating starlight...";
        }
        private static void ClearPreviousGen()
        {
            for(int i = 10; i < Main.maxTilesX - 10; i++)
            {
                for(int j = 10; j < Main.worldSurface * 0.35f + 40; j++)
                {
                    Tile tile = Main.tile[i, j];
                    if(tile.active() && tile.type == ModContent.TileType<PhaseOreTile>())
                    {
                        tile.active(false);
                    }
                }
            }
        }
        private static void DoGen(object state)
        {
            Generating = true;
            ClearPreviousGen();
            float worldPercent;
            int total = 6;
            int scattered = 60;
            if (Main.maxTilesX > 6000) //medium
            {
                total = 10;
                scattered = 90;
            }
            if (Main.maxTilesX > 8000) //large
            {
                total = 14;
                scattered = 120;
            }
            int amountInCluster = 14;
            float spread = 1f / total;
            for(int i = 1; i < total; i++)
            {
                if(i != total / 2)
                {
                    worldPercent = spread * i;
                    int xPos = (int)MathHelper.Lerp(40, Main.maxTilesX - 40, worldPercent);
                    int yPos = WorldGen.genRand.Next(80, (int)(Main.worldSurface * 0.25f));
                    SOTSWorldgenHelper.GeneratePhaseOre(xPos, yPos, 20, 2); //generate primary branches
                    int outwardsMax = 240;
                    for(int j = 0; j < amountInCluster; j++)
                    {
                        int newX = xPos + WorldGen.genRand.Next(-outwardsMax, outwardsMax);
                        yPos = WorldGen.genRand.Next(40, (int)(Main.worldSurface * 0.25f) + outwardsMax / 5);
                        if(SOTSWorldgenHelper.Empty(newX - 10, yPos - 10, 20, 20, 1))
                        {
                            SOTSWorldgenHelper.GeneratePhaseOre(newX, yPos, WorldGen.genRand.Next(12, 33), 0); //generate squigglies around the cluster
                        }
                        else
                        {
                            outwardsMax += 10;
                            if (outwardsMax > 320)
                                outwardsMax = 320;
                        }
                    }
                }
            }
            for (int j = 1; j < scattered; j++)
            {
                int newX = (int)MathHelper.Lerp(50, Main.maxTilesX - 50, j / (float)scattered) + WorldGen.genRand.Next(-20, 20);
                int minMax = 40;
                if (j % 5 == 0)
                    minMax = 80;
                int yPos = WorldGen.genRand.Next(minMax, (int)(Main.worldSurface * 0.3f) + (80 - minMax));
                if (SOTSWorldgenHelper.Empty(newX - 10, yPos - 10, 20, 20, 1))
                {
                    if (j % 4 == 0)
                    {
                        SOTSWorldgenHelper.GeneratePhaseOre(newX, yPos, (Main.rand.Next(4) + 1) * 4, 2);
                    }
                    else
                        SOTSWorldgenHelper.GeneratePhaseOre(newX, yPos, WorldGen.genRand.Next(12, 21), 0); //generate strangler squigglies
                }
            }
            string text = "Starlight solidifies in the upper atmosphere!";
            Generating = false;
            if (Main.netMode == NetmodeID.Server)
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), VoidPlayer.ChaosPink);
            else
                Main.NewText(text, VoidPlayer.ChaosPink);
        }
        public static void Generate()
        {
            ThreadPool.QueueUserWorkItem(DoGen, null);
        }
    }
}