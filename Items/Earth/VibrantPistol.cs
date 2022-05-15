using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SOTS.Void;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Projectiles.Earth;

namespace SOTS.Items.Earth
{
	public class VibrantPistol : VoidItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vibrant Pistol");
			Tooltip.SetDefault("Fires almost as fast as you can pull the trigger");
		}
		public override void SafeSetDefaults()
		{
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 22;
            Item.useTime = 5; 
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;    
            Item.noMelee = true;
			Item.knockBack = 2f;  
            Item.value = Item.sellPrice(0, 0, 80, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item91;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<VibrantBolt>(); 
            Item.shootSpeed = 24f;
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[Main.myPlayer];
			SOTSPlayer modPlayer = SOTSPlayer.ModPlayer(player);
			if (modPlayer.VibrantArmor)
			{
				Texture2D texture = Mod.Assets.Request<Texture2D>("Items/Earth/VibrantRifle").Value;
				Main.spriteBatch.Draw(texture, position - new Vector2((texture.Width - Item.width)/ 2 - 3.5f, 0), null, drawColor, 0f, origin, scale * 0.85f, SpriteEffects.None, 0f); //I had to position and draw this by testing values manually ughh
				return false;
			}
			return true;
		}
		public void triggerItemUpdates(Player player)
		{
			SOTSPlayer modPlayer = SOTSPlayer.ModPlayer(player);
			if (modPlayer.VibrantArmor)
			{
				Item.autoReuse = true;
				Item.noUseGraphic = true;
			}
			else
			{
				Item.autoReuse = false;
				Item.noUseGraphic = false;
			}
		}
		public override void GetWeaponKnockback(Player player, ref float knockback)
		{
			triggerItemUpdates(player);
			base.GetWeaponKnockback(player, ref knockback);
		}
		public override bool BeforeUseItem(Player player)
		{
			triggerItemUpdates(player);
			return base.BeforeUseItem(player);
		}
		public override int GetVoid(Player player)
		{
			return 1;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			SOTSPlayer modPlayer = SOTSPlayer.ModPlayer(player);
			triggerItemUpdates(player);
			if (modPlayer.VibrantArmor)
			{
				float mult = 1.33f;
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4.5f));
				Projectile.NewProjectile(position, Vector2.Zero, ModContent.ProjectileType<VibrantRifle>(), 0, 0, player.whoAmI, perturbedSpeed.ToRotation() - new Vector2(speedX, speedY).ToRotation());
				speedX = perturbedSpeed.X * mult;
				speedY = perturbedSpeed.Y * mult;
				//SoundEngine.PlaySound(SoundID.Item11, position);
			}
			return true; 
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<VibrantBar>(), 4).AddTile(TileID.Anvils).Register();
		}
	}
}
