using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;


namespace SOTS.Items.SpecialDrops
{
    public class FireSpitter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Spitter");
        }
        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.width = 46;
            item.height = 16;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = 85;
            item.shootSpeed = 5.5f;
            item.useAmmo = 23; 
        }
    }
}
