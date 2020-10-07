using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SOTS.Void;


namespace SOTS.Items.Fragments
{
	public class SoulEater : VoidItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Eater");
			Tooltip.SetDefault("Charge to increase damage up to 1000%\nKilled enemies regenerate mana and void");
		}
		public override void SafeSetDefaults()
		{
            item.damage = 10;
            item.melee = true;  
            item.width = 40;
            item.height = 40;  
            item.useTime = 24; 
            item.useAnimation = 24;
            item.useStyle = 5;    
            item.knockBack = 0f;
            item.value = Item.sellPrice(0, 0, 33, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item22;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SoulEaterArm"); 
            item.shootSpeed = 0f;
			item.channel = true;
            item.noUseGraphic = true; 
            item.noMelee = true;
			Item.staff[item.type] = true; 
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            SOTSPlayer modPlayer = (SOTSPlayer)player.GetModPlayer(mod, "SOTSPlayer");
				VoidPlayer voidPlayer = VoidPlayer.ModPlayer(player);
			
				bool summon = true;
				for (int l = 0; l < Main.projectile.Length; l++)
				{
					Projectile proj = Main.projectile[l];
					if(proj.active && proj.type == item.shoot && Main.player[proj.owner] == player)
					{
						summon = false;
					}
				}
			if(player.altFunctionUse != 2)
			{
				item.UseSound = SoundID.Item22;
				if(summon)
				{
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, 0, player.whoAmI);
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, 1, player.whoAmI);
				}
			}
              return false; 
		}
		public override void GetVoid(Player player)
		{
				voidMana = 2;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 12);
			recipe.AddIngredient(null, "FragmentOfEvil", 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}