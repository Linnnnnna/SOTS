using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Items.Banners;
using SOTS.Items.Pyramid;
using SOTS.NPCs.Constructs;
using SOTS.Projectiles.Pyramid;
using SOTS.Void;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace SOTS.NPCs.Boss.Lux
{
	public class FakeLux : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Illusion");
		}
		public override void SetDefaults()
		{
            npc.aiStyle = 0; 
            npc.lifeMax = 10000;   
            npc.damage = 80; 
            npc.defense = 10;  
            npc.knockBackResist = 0f;
            npc.width = 70;
            npc.height = 70;
			Main.npcFrameCount[npc.type] = 1;  
            npc.value = 0;
            npc.npcSlots = 1f;
			npc.dontCountMe = true;
			npc.HitSound = null;
			npc.DeathSound = null;
			npc.lavaImmune = true;
			npc.netAlways = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Frostburn] = true;
			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.dontTakeDamage = true;
		}
		RingManager ring;
        public override bool PreNPCLoot()
        {
            return  false;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = 20000;
			npc.damage = 140;
		}
		bool runOnce = true;
		float rotationCounter = 0;
		public override bool PreAI()
		{
			if (runOnce)
				ring = new RingManager(MathHelper.ToRadians(npc.ai[1]), 0.6f, 3, 72);
			WingStuff();
			runOnce = false;
			Player player = Main.player[npc.target];
			Vector2 rotateCenter = player.Center;
			int parentID = (int)npc.ai[0];
			if (parentID >= 0)
			{
				NPC npc2 = Main.npc[parentID];
				if (npc2.active && npc2.type == NPCType<Lux>())
				{
					rotateCenter = npc2.Center;
				}
				else
				{
					for (int i = 0; i < 50; i++)
					{
						Dust dust = Dust.NewDustDirect(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.RainbowMk2);
						dust.color = VoidPlayer.pastelAttempt(Main.rand.NextFloat(6.28f), illusionColor());
						dust.noGravity = true;
						dust.fadeIn = 0.1f;
						dust.scale *= 2.2f;
						dust.velocity *= 4f;
					}
					npc.active = false;
				}
			}
			rotationCounter += 0.5f;
			Vector2 rotatePos = new Vector2(640, 0).RotatedBy(MathHelper.ToRadians(rotationCounter + npc.ai[1]));
			Vector2 toPos = rotatePos + rotateCenter;
			Vector2 goToPos = toPos - npc.Center;
			float speed = 12;
			float length = goToPos.Length();
			if (speed > length)
			{
				speed = length;
			}
			goToPos = goToPos.SafeNormalize(Vector2.Zero);
			npc.velocity = goToPos * speed;
			npc.rotation = npc.velocity.X * 0.05f;
			return base.PreAI();
		}
        public override void PostAI()
		{
			ring.CalculationStuff(npc.Center + npc.velocity);
		}
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return npc.ai[3] > 90;
        }
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return false;
		}
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[npc.type];
			Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
			ChaosSpirit.DrawWings(wingHeight, npc.ai[2], npc.rotation, npc.Center, illusionColor());
			DrawRings(spriteBatch, false);
			for (int k = 0; k < 7; k++)
			{
				Color color = illusionColor();
				Vector2 circular = new Vector2(4, 0).RotatedBy(MathHelper.ToRadians(k * 60 + Main.GameUpdateCount));
				if (k != 0)
				{
					color = VoidPlayer.pastelAttempt(MathHelper.ToRadians(k * 60), color);
				}
				else
					circular *= 0f;
				color.A = 0;
				Main.spriteBatch.Draw(texture, npc.Center + circular - Main.screenPosition, null, npc.GetAlpha(color), 0f, drawOrigin, npc.scale * 1.1f, SpriteEffects.None, 0f);
			}
			DrawRings(spriteBatch, true);
		}
        float wingSpeedMult = 1;
		float wingHeight = 0;
		public void WingStuff()
		{
			npc.ai[3]++;
			npc.ai[2] += 7.5f * wingSpeedMult;
			float dipAndRise = (float)Math.Sin(MathHelper.ToRadians(npc.ai[2] + npc.ai[1]));
			//dipAndRise *= (float)Math.sqrt(dipAndRise);
			wingHeight = 19 + dipAndRise * 27;
		}
		public void DrawRings(SpriteBatch spriteBatch, bool front)
		{
			ring.Draw(spriteBatch, illusionColor(), 3, 1, 1, 1, npc.rotation, front);
		}
		public Color illusionColor()
        {
			if(npc.ai[1] == 120)
				return new Color(80, 240, 80);
			if (npc.ai[1] == 240)
				return new Color(60, 140, 200);
			return new Color(200, 100, 100);
        }
	}
}