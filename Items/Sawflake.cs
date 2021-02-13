using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using SOTS.Void;
 
namespace SOTS.Items
{
    public class Sawflake : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sawflake");
			Tooltip.SetDefault("Throw a Sawflake that exudes a spiral of razor ice mist in every direction");
		}
        public override void SetDefaults()
        {
            item.damage = 70;
            item.width = 50;
            item.height = 50;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = ItemRarityID.Yellow;
            item.noMelee = true;
            item.useStyle = 1;
            item.useAnimation = 16;
            item.useTime = 16;
            item.knockBack = 7f;
            item.noUseGraphic = true; 
            item.shoot = mod.ProjectileType("Sawflake");
            item.shootSpeed = 13.5f;
            item.UseSound = SoundID.Item1;
            item.melee = true; 
            item.autoReuse = true;
        }
    }
}