using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.IO;

namespace SOTS.FakePlayer
{
	public class SubspaceServant : ModProjectile
	{
		public FakePlayer FakePlayer;
		public Vector2 ItemLocation;
		public float ItemRotation;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subspace Servant");
			Main.projPet[Projectile.type] = true;
			//Main.vanityPet[Projectile.type] = true;
		}
		public sealed override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 42;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.ignoreWater = true;
			Projectile.netImportant = true;
			Projectile.hide = true;
		}
		bool runOnce = true;
        public override bool PreAI()
		{
			if (Main.myPlayer != Projectile.owner)
				Projectile.timeLeft = 20;
			if (runOnce)
			{
				Projectile.ai[1] = 80f;
				runOnce = false;
			}
			if (FakePlayer == null)
				FakePlayer = new FakePlayer(0);
			return base.PreAI();
		}
        public override bool? CanCutTiles() => false;
		public override bool MinionContactDamage() => false;
		public override bool ShouldUpdatePosition() => false;
		Vector2 cursorArea;
		int Direction = 1;
        public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(cursorArea.X);
			writer.Write(cursorArea.Y);
			writer.Write(ItemLocation.X);
			writer.Write(ItemLocation.Y);
			writer.Write(ItemRotation);
			writer.Write(Direction);
			base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
		{
			cursorArea.X = reader.ReadSingle();
			cursorArea.Y = reader.ReadSingle();
			ItemLocation.X = reader.ReadSingle();
			ItemLocation.Y = reader.ReadSingle();
			ItemRotation = reader.ReadSingle();
			Direction = reader.ReadInt32();
			base.ReceiveExtraAI(reader);
        }
		bool isVanity = true;
        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if(!isVanity && SubspacePlayer.ModPlayer(player).servantIsVanity)
            {
				Projectile.Kill();
            }
			if (!SubspacePlayer.ModPlayer(player).servantIsVanity)
            {
				isVanity = false;
            }
			int TrailingType = FakePlayer.TrailingType;
			Vector2 idlePosition = player.Center;
			if (Main.myPlayer == player.whoAmI)
			{
				cursorArea = Main.MouseWorld;
				if (FakePlayer.itemAnimation <= 0 || FakePlayer.heldItem.IsAir)
				{
					Direction = player.direction;
				}
				if(FakePlayer.itemAnimation == FakePlayer.itemAnimationMax && !SubspacePlayer.ModPlayer(player).servantIsVanity && FakePlayer.itemAnimationMax != 0)
                {
					Direction = Math.Sign(cursorArea.X - Projectile.Center.X);
                }
			}
			if (cursorArea != Vector2.Zero)
			{
				if (TrailingType == 0)
				{
					idlePosition.X -= player.direction * 64f;
				}
				if (TrailingType == 1) //magic
				{
					idlePosition.Y -= 48f;
					Vector2 toCursor = cursorArea - player.Center;
					toCursor = toCursor.SafeNormalize(Vector2.Zero) * -128f;
					toCursor.Y *= 0.375f;
					toCursor.Y = -Math.Abs(toCursor.Y);
					idlePosition += toCursor;
				}
				if (TrailingType == 2) //ranged
				{
					idlePosition.Y -= 64f;
					Vector2 toCursor = cursorArea - player.Center;
					toCursor = toCursor.SafeNormalize(Vector2.Zero) * 128f;
					toCursor.Y *= 0.4125f;
					idlePosition += toCursor;
				}
				if (TrailingType == 3) //melee
				{
					//idlePosition.Y += 8f + (float)Math.Sqrt(Item.width * Item.height) * 0.5f;
					Vector2 toCursor = cursorArea - player.Center;
					float lengthToCursor = -32 + toCursor.Length();
					toCursor = toCursor.SafeNormalize(Vector2.Zero) * lengthToCursor;
					idlePosition += toCursor;
				}
				if (TrailingType == 4) //melee, but no melee ?
				{
					idlePosition.Y += 32f;
					Vector2 toCursor = cursorArea - player.Center;
					float lengthToCursor = -32 + toCursor.Length() * 0.66f;
					toCursor.Y *= 0.7f;
					toCursor = toCursor.SafeNormalize(Vector2.Zero) * lengthToCursor;
					idlePosition += toCursor;
				}
				Vector2 toIdle = idlePosition - Projectile.Center;
				float dist = toIdle.Length();
				float speed = 3 + (float)Math.Pow(dist, 1.45) * 0.002f;
				if (dist < speed)
				{
					speed = toIdle.Length();
				}
				Projectile.velocity = toIdle.SafeNormalize(Vector2.Zero) * speed;
				if (Direction == 1)
				{
					if (Projectile.ai[0] < Direction)
						Projectile.ai[0] += 0.1f;
				}
				else
				{
					if (Projectile.ai[0] > Direction)
						Projectile.ai[0] -= 0.1f;
				}
				if (Projectile.ai[1] >= 24)
				{
					Projectile.ai[1] -= 24f;
				}
				Vector2 circular = new Vector2(2f, 0).RotatedBy(MathHelper.ToRadians(15 * Projectile.ai[1]));
				Projectile.ai[1] += 0.75f;
				if (circular.Y > 0)
					circular.Y *= 0.5f;
				Projectile.velocity.Y += circular.Y;

				FakePlayer.itemLocation = ItemLocation;
				FakePlayer.itemRotation = ItemRotation;
				FakePlayer.WingFrame = (int)(Projectile.ai[1] / 4);
				FakePlayer.OldPosition = Projectile.position + new Vector2(-5 * Projectile.ai[0], 2);
				Projectile.position += Projectile.velocity;
				FakePlayer.direction = Direction;
				FakePlayer.Position = Projectile.position;
				FakePlayer.Velocity = Projectile.velocity; //this is only used for wing drawing
				FakePlayer.ItemCheckHack(player);
				Direction = FakePlayer.direction;
				if (Main.myPlayer == player.whoAmI)
				{
					ItemLocation = FakePlayer.itemLocation;
					ItemRotation = FakePlayer.itemRotation;
				}
			}
			if (Main.myPlayer == player.whoAmI) //might be excessive but is the easiest way to sync everything
				Projectile.netUpdate = true;
			Lighting.AddLight(Projectile.Center, new Vector3(75, 30, 75) * 1f / 255f);
		}
        public override bool PreDraw(ref Color lightColor)
		{
			return false;
        }
    }
}