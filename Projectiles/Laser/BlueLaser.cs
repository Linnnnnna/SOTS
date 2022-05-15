﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace SOTS.Projectiles.Laser
{
    public class BlueLaser : ModProjectile
    {
        private Vector2 _targetPos;         //Ending position of the laser beam
        private int _charge;                //The charge level of the weapon
        private float _moveDist = 45f;       //The distance charge particle from the player center
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pure Mana");
			
		}
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;     //this defines if the projectile is frendly
            Projectile.penetrate = -1;  //this defines the projectile penetration, -1 = infinity
            Projectile.tileCollide = false;   //this defines if the tile can colide with walls
            Projectile.DamageType = DamageClass.Magic;
            Projectile.hide = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (_charge == 100)
            {
                Vector2 unit = _targetPos - Main.player[Projectile.owner].Center;
                unit.Normalize();
                DrawLaser(spriteBatch, Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, Main.player[Projectile.owner].Center, unit, 5, Projectile.damage, -1.57f, 1, 1000, Color.Red, 45);
            }
            return false;

        }

        /// <summary>
        /// The core function of drawing a laser
        /// </summary>
        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 200f, Color color = default(Color), int transDist = 50)
        {
            Vector2 origin = start;
            float r = unit.ToRotation() + rotation;

            #region Draw laser body
            for (float i = transDist; i <= _moveDist; i += step)
            {
                Color c = Color.White;
                origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                    new Rectangle(0, 26, 28, 26), i < transDist ? Color.White : c, r,
                    new Vector2(28 / 2, 26 / 2), scale, 0, 0);
            }
            #endregion

            #region Draw laser head
            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 / 2, 26 / 2), scale, 0, 0);
            #endregion

            #region Draw laser tail
            spriteBatch.Draw(texture, start + (_moveDist + step) * unit - Main.screenPosition,
                new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 / 2, 26 / 2), scale, 0, 0);
            #endregion
        }

        /// <summary>
        /// Change the way of collision check of the projectile
        /// </summary>
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (_charge == 100)
            {
                Player p = Main.player[Projectile.owner];
                Vector2 unit = (Main.player[Projectile.owner].Center - _targetPos);
                unit.Normalize();
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), p.Center - 45f * unit, p.Center - unit * _moveDist, 22, ref point))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Change the behavior after hit a NPC
        /// </summary>
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 4;
        }

        /// <summary>
        /// The AI of the projectile
        /// </summary>
        public override void AI()
        {

            Vector2 mousePos = Main.MouseWorld;
            Player player = Main.player[Projectile.owner];
			
            #region Set projectile position
            if (Projectile.owner == Main.myPlayer) // Multiplayer support
            {
                Vector2 diff = mousePos - player.Center;
                diff.Normalize();
                Projectile.position = player.Center + diff * _moveDist;
                Projectile.timeLeft = 2;
                int dir = Projectile.position.X > player.position.X ? 1 : -1;
                player.ChangeDir(dir);
                player.heldProj = Projectile.whoAmI;
                player.itemTime = 2;
                player.itemAnimation = 2;
                player.itemRotation = (float)Math.Atan2(diff.Y * dir, diff.X * dir);
                Projectile.soundDelay--;


                #endregion
            }


            #region Charging process
            // Kill the projectile if the player stops channeling
            if (!player.channel)
            {
                Projectile.Kill();
            }
            else
            {

                if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                {
                    Projectile.Kill();
                }
                if (_charge < 100)
                {
                    _charge++;
                }

                
            }
            #endregion


            #region Set laser tail position and dusts

            if (_charge < 100) return;
            Vector2 start = player.Center;
            Vector2 unit = (player.Center - mousePos);
            unit.Normalize();
            unit *= -1;
            for (_moveDist = 45f; _moveDist <= 1600; _moveDist += 5)      //this 1600 is the dsitance of the beam
            {
                start = player.Center + unit * _moveDist;
                if (!Collision.CanHit(player.Center, 1, 1, start, 1, 1))
                {
                    _moveDist -= 5f;
                    break;
                }
                if (Projectile.soundDelay <= 0)//this is the proper sound delay for this type of weapon
                {
                    SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 15);    //this is the sound when the weapon is used   cheange 15 for diferent sound
                    Projectile.soundDelay = 40;    //this is the proper sound delay for this type of weapon
                }
            }
            _targetPos = player.Center + unit * _moveDist;

            //dust
            for (int i = 0; i < 15; ++i)
            {
                float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
                float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
                Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
            }

            #endregion
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}