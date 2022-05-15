using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Projectiles.Otherworld
{    
    public class CodeBurst : ModProjectile 
    {	
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Code Burst");
		}
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
			Projectile.timeLeft = 72;
            Projectile.penetrate = 1; 
            Projectile.friendly = true; 
            Projectile.tileCollide = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.alpha = 255;
		}
		public override void Kill(int timeLeft)
		{
			if (Projectile.ai[1] == -1)
            {
				Projectile.extraUpdates = 3;
				// Projectile.magic = false /* tModPorter - this is redundant, for more info see https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide#damage-classes */ ;
            }
			for (int i = 0; i < 30; i++)
			{
				int type = Mod.Find<ModDust>("CodeDust").Type;
				if (Projectile.ai[1] == -1)
					type = Mod.Find<ModDust>("CodeDust2").Type;
				int num = Dust.NewDust(new Vector2(Projectile.position.X - 4, Projectile.position.Y - 4), Projectile.width, Projectile.height, type);
				Dust dust = Main.dust[num];
				dust.velocity *= 1.3f;
				dust.velocity += Projectile.velocity * 0.3f;
				dust.noGravity = true;
				dust.scale *= 2.75f;
			}
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return true;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
			width = 16;
			height = 16;
            return true;
        }
		public override void AI() //The projectile's AI/ what the projectile does
		{
			int type = Mod.Find<ModDust>("CodeDust").Type;
			if (Projectile.ai[1] == -1)
				type = Mod.Find<ModDust>("CodeDust2").Type;
			for (int i = 0; i < Main.rand.Next(2) + 1; i++)
			{
				int num = Dust.NewDust(new Vector2(Projectile.Center.X - 4, Projectile.Center.Y - 4), 4, 4, type);
				Dust dust = Main.dust[num];
				dust.velocity *= 0.6f;
				dust.velocity += Projectile.velocity * 0.1f;
				dust.noGravity = true;
				dust.scale *= 1.75f;
			}
		}
	}
}