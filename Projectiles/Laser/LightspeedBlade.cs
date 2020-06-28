using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Projectiles.Laser
{
	public class LightspeedBlade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightspeed Blade");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 240;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 48;
			projectile.height = 48;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.extraUpdates = 16;
			projectile.timeLeft = 6000;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 16;
			height = 16;
			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			triggerUpdate();
			return false;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = mod.GetTexture("Projectiles/Laser/LightspeedTrail");
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Color color = Color.Black;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin;
				color = projectile.GetAlpha(color) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length) * 0.5f;
				for (int j = 0; j < 7; j++)
				{
					float x = Main.rand.Next(-10, 11) * 0.15f;
					float y = Main.rand.Next(-10, 11) * 0.15f;
					if(!projectile.oldPos[k].Equals(projectile.position))
					{
						Main.spriteBatch.Draw(texture, drawPos + new Vector2(x, y), null, color, projectile.rotation, drawOrigin, (projectile.oldPos.Length - k) / (float)projectile.oldPos.Length, SpriteEffects.None, 0f);
					}
				}
			}
			return false;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			Color color = Color.Black;
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < 7; k++)
			{
				float x = Main.rand.Next(-10, 11) * 0.15f;
				float y = Main.rand.Next(-10, 11) * 0.15f;
				if (projectile.ai[0] != 1)
					Main.spriteBatch.Draw(texture, new Vector2((float)(projectile.Center.X - (int)Main.screenPosition.X) + x, (float)(projectile.Center.Y - (int)Main.screenPosition.Y) + y), null, color * (1f - (projectile.alpha / 255f)), projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
			}
			base.PostDraw(spriteBatch, drawColor);
		}
		int inititate = 0;
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			Lighting.AddLight(projectile.Center, 1f, 0.5f, 1f);
			if (inititate == 0)
			{
				inititate++;
				//Main.PlaySound(2, projectile.Center, 60);
				Main.PlaySound(2, player.Center, 60);
			}
			if(!projectile.velocity.Equals(new Vector2(0, 0)))
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

			if(projectile.velocity.X > 0)
			{
				if(projectile.Center.X > projectile.ai[1])
				{
					projectile.tileCollide = true;
				}
			}
			if (projectile.velocity.X < 0)
			{
				if (projectile.Center.X < projectile.ai[1])
				{
					projectile.tileCollide = true;
				}
			}

			if (projectile.ai[0] == 1)
			{
				projectile.alpha += 2;
				projectile.friendly = false;
				if (projectile.alpha >= 255)
				{
					projectile.Kill();
				}
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 0;
			triggerUpdate();
		}
		public void triggerUpdate()
		{
			projectile.ai[0] = 1;
			projectile.velocity *= 0;
			projectile.friendly = false;
			if (projectile.owner == Main.myPlayer)
			{
				projectile.netUpdate = true;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("VoidRing"), projectile.damage, 0, Main.myPlayer);
			}
		}
	}
}