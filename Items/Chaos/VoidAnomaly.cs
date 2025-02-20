using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SOTS.Void;

namespace SOTS.Items.Chaos
{
	public class VoidAnomaly : ModItem
	{
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetStaticDefaults()
		{
			this.SetResearchCost(1);
		}
		public override void SetDefaults()
		{
            Item.width = 44;     
            Item.height = 26;   
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
			Item.expert = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SOTSPlayer modPlayer = SOTSPlayer.ModPlayer(player);
			VoidPlayer vPlayer = VoidPlayer.ModPlayer(player);
			vPlayer.bonusVoidGain += 10f;
			vPlayer.voidRegenSpeed += 0.1f;
			//modPlayer.VMincubator = true;
			modPlayer.VoidAnomaly = true;
		}
	}
}

