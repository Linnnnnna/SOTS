using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Dusts;
using SOTS.Items.Fragments;
using SOTS.Items.Otherworld.FromChests;
using SOTS.Items.Otherworld.Furniture;
using SOTS.Void;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static SOTS.SOTS;

namespace SOTS.Items.Otherworld.EpicWings
{
	[AutoloadEquip(EquipType.Wings)]
	public class TestWings : ModItem
	{ 
		public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("Machina Booster");
			Tooltip.SetDefault("temp");
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (string key in SOTS.MachinaBoosterHotKey.GetAssignedKeys()) //gets the key configured to this hotkey
			{
				foreach (TooltipLine line in tooltips) //goes through each tooltip line
				{
					if (line.Mod == "Terraria" && line.Name == "Tooltip0") //checks the name of the tootip line
					{
						line.Text = "Allows flight and slow fall\nIncreases void gain by 1\nPress the " + "'" + key + "' key to gain fast, multidirectional flight at the cost of 5 void\nIncreases void drain by 3 while active";
						return;
					}
				}
			}
			foreach (TooltipLine line in tooltips) //goes through each tooltip line
			{
				if (line.Mod == "Terraria" && line.Name == "Tooltip0")
				{
					string key = "Unbound";
					line.Text = "Allows flight and slow fall\nIncreases void gain by 1\nPress the " + "'" + key + "' key to gain fast, multidirectional flight at the cost of 5 void\nIncreases void drain by 3 while active";
				}
			}
			base.ModifyTooltips(tooltips);
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D texture = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/TestWings").Value;
			Main.spriteBatch.Draw(texture, new Vector2(position.X, position.Y), null, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
			Texture2D texture2 = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/TestWingsEffect").Value;
			Color color = new Color(110, 110, 110, 0);
			for (int k = 0; k < 4; k++)
			{
				float x = Main.rand.Next(-10, 11) * 0.03f;
				float y = Main.rand.Next(-10, 11) * 0.03f;
				Main.spriteBatch.Draw(texture2, new Vector2(position.X + x, position.Y + y), null, color * (1f - (Item.alpha / 255f)), 0f, origin, scale, SpriteEffects.None, 0f);
			}
			texture = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/TestWingsBorder").Value;
			Main.spriteBatch.Draw(texture, new Vector2(position.X, position.Y), null, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
			return false;
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			Vector2 drawOrigin = new Vector2(Terraria.GameContent.TextureAssets.Item[Item.type].Value.Width * 0.5f, Item.height * 0.5f);
			Texture2D texture = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/TestWings").Value;
			Main.spriteBatch.Draw(texture, new Vector2((float)(Item.Center.X - (int)Main.screenPosition.X), (float)(Item.Center.Y - (int)Main.screenPosition.Y) + 2), null, lightColor * (1f - (Item.alpha / 255f)), rotation, drawOrigin, scale, SpriteEffects.None, 0f);
			Texture2D texture2 = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/TestWingsEffect").Value;
			Color color = new Color(110, 110, 110, 0);
			for (int k = 0; k < 4; k++)
			{
				float x = Main.rand.Next(-10, 11) * 0.03f;
				float y = Main.rand.Next(-10, 11) * 0.03f;
				Main.spriteBatch.Draw(texture2, new Vector2((float)(Item.Center.X - (int)Main.screenPosition.X) + x, (float)(Item.Center.Y - (int)Main.screenPosition.Y) + y + 2), null, color * (1f - (Item.alpha / 255f)), rotation, drawOrigin, scale, SpriteEffects.None, 0f);
			}
			texture = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/TestWingsBorder").Value;
			Main.spriteBatch.Draw(texture, new Vector2((float)(Item.Center.X - (int)Main.screenPosition.X), (float)(Item.Center.Y - (int)Main.screenPosition.Y) + 2), null, lightColor * (1f - (Item.alpha / 255f)), rotation, drawOrigin, scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void SetDefaults()
		{
			Item.width = 52;
			Item.height = 28;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Red;
			Item.expert = true;
			Item.accessory = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<TwilightGyroscope>(), 1).AddIngredient(ModContent.ItemType<DissolvingAether>(), 1).AddIngredient(ModContent.ItemType<StarlightAlloy>(), 20).AddIngredient(ItemID.SoulofFlight, 20).AddTile(TileID.MythrilAnvil).Register();
			CreateRecipe(1).AddIngredient(ModContent.ItemType<TwilightGyroscope>(), 1).AddIngredient(ModContent.ItemType<DissolvingAether>(), 1).AddIngredient(ModContent.ItemType<StarlightAlloy>(), 20).AddIngredient(ItemID.SoulofFlight, 20).AddTile(ModContent.TileType<HardlightFabricatorTile>()).Register();
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			TestWingsPlayer testWingsPlayer = (TestWingsPlayer)player.GetModPlayer(Mod, "TestWingsPlayer");
			testWingsPlayer.canCreativeFlight = true;
			player.wingTimeMax = 150;
			player.noFallDmg = true;
			VoidPlayer voidPlayer = VoidPlayer.ModPlayer(player);
			voidPlayer.bonusVoidGain += 1f;
		}
		public override bool WingUpdate(Player player, bool inUse)
		{
			TestWingsPlayer testWingsPlayer = (TestWingsPlayer)player.GetModPlayer(Mod, "TestWingsPlayer");
			if(testWingsPlayer.creativeFlight)
			{
				player.wingFrame = 2;
			}
			else if ((player.controlJump && player.velocity.Y != 0f) || player.velocity.Y != 0f)
			{
				player.wingFrame = 1;
			}
			else
			{
				player.wingFrame = 0;
			}
			return true;
		}
		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			float num1 = 0.1f;
			float num2 = 0.5f;
			float num3 = 1.5f;
			float num4 = 0.5f;
			float num5 = 0.1f;
			ascentWhenFalling = num2;
			ascentWhenRising = num5;
			maxCanAscendMultiplier = num4;
			maxAscentMultiplier = num3;
			constantAscend = num1;
		}
		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 7f;
			acceleration *= 1.5f;
		}
	}
	public class TestWingsPlayer : ModPlayer
	{
		public void SendPacket()
		{
			var packet = Mod.GetPacket();
			packet.Write((byte)SOTSMessageType.SyncCreativeFlight);
			packet.Write((byte)Player.whoAmI);
			packet.Write(creativeFlight);
			packet.Send();
			netUpdate = false;
		}
		public const float voidDrain = 3f;
		public bool canCreativeFlight = false;
		public bool creativeFlight = false;
		public float wingSpeed = 7f;
		public float wingSpeedMax = 7f;
		public int epicWingType = 0;
		public bool gyro = false;
		public bool netUpdate = false;
		public enum EpicWingType : int
		{
			Default = 0,
			Blocky = 1,
		}
		public void DustExplosion()
		{
			for (int i = 0; i < 360; i += 4)
			{
				if (epicWingType == 0)
				{
					int index = Dust.NewDust(Player.Center + new Vector2(-4, -4), 0, 0, ModContent.DustType<CopyDust>(), 0, 0, 0, Color.White);
					Dust dust = Main.dust[index];
					dust.noGravity = true;
					dust.fadeIn = 0.1f;
					dust.velocity *= 0.8f;
					dust.scale += 1.5f;
					dust.velocity += new Vector2(12, 0).RotatedBy(MathHelper.ToRadians(i));
					dust.shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
				}
				else
				{
					int index = Dust.NewDust(Player.Center + new Vector2(-5, -5), 0, 0, ModContent.DustType<CopyDust>());
					Dust dust = Main.dust[index];
					dust.noGravity = true;
					dust.fadeIn = 0.5f;
					dust.velocity *= 0.8f;
					dust.scale = 1.5f;
					dust.velocity += new Vector2(12, 0).RotatedBy(MathHelper.ToRadians(i));
					dust.shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
				}
			}
			Terraria.Audio.SoundEngine.PlaySound(12, Player.Center);
		}
		public void flightStart()
		{
			if (SOTSPlayer.ModPlayer(Player).CreativeFlightButtonPressed)
			{
				if (!creativeFlight)
				{
					VoidPlayer voidPlayer = VoidPlayer.ModPlayer(Player);
					voidPlayer.voidMeter -= 5 * voidPlayer.voidCost;
					VoidPlayer.VoidEffect(Player, (int)(-5 * voidPlayer.voidCost), false, false);
				}
				creativeFlight = !creativeFlight; 
				if(Main.myPlayer == Player.whoAmI && Main.netMode != NetmodeID.SinglePlayer)
					SendPacket();
			}
		}
		bool movingHori = false;
		bool movingVert = false;
		public override void PostUpdate()
		{
			if (creativeFlight)
				if (Player.bodyFrame.Y == Player.bodyFrame.Height * 5 && !TilesBelow())
					Player.bodyFrame.Y = Player.bodyFrame.Height * 6;
		}
		public bool TilesBelow()
		{
			int i = (int)Player.Center.X / 16;
			int i2 = (int)(Player.Center.X + 10) / 16;
			int i3 = (int)(Player.Center.X - 10) / 16;
			int j = (int)(Player.position.Y + Player.height + 1) / 16;
			int j2 = (int)(Player.position.Y + Player.height + 15) / 16;
			Tile tile = Framing.GetTileSafely(i, j);
			Tile tile2 = Framing.GetTileSafely(i, j2);
			Tile tile3 = Framing.GetTileSafely(i2, j);
			Tile tile4 = Framing.GetTileSafely(i2, j2);
			Tile tile5 = Framing.GetTileSafely(i3, j);
			Tile tile6 = Framing.GetTileSafely(i3, j2);
			return (tile2 == tile && tile.HasTile && !tile.IsActuated && (Main.tileSolid[tile.TileType] || Main.tileTable[tile.TileType])) || (tile3 == tile4 && tile4.HasTile && !tile4.IsActuated && (Main.tileSolid[tile4.TileType] || Main.tileTable[tile4.TileType])) || (tile5 == tile6 && tile6.HasTile && !tile6.IsActuated && (Main.tileSolid[tile6.TileType] || Main.tileTable[tile6.TileType]));
		}
		int dustIter = 0;
		int[] dustID = new int[180];
		int[] dustOwnerID = new int[180];
		Vector2[] dustPos = new Vector2[180];
		public void HaloDust()
		{
			for (int i = 0; i < 3; i++)
			{
				dustIter++;
				Vector2 center = new Vector2(Player.Center.X - 12 * Player.direction, Player.position.Y - 4);
				Vector2 circularLocation = new Vector2(10, 0).RotatedBy(MathHelper.ToRadians(randCounter * 6 + i * 120));
				Vector2 circularLocation2 = new Vector2(circularLocation.X / 2, circularLocation.Y).RotatedBy(MathHelper.ToRadians(45 * Player.direction));
				Vector2 loc = center + circularLocation2;
				int index = Dust.NewDust(loc + new Vector2(-4, -4), 0, 0, ModContent.DustType<CopyDust2>(), 0, 0, 0, new Color(255, 255, 255));
				Dust dust = Main.dust[index];
				dust.noGravity = true;
				dust.velocity = Player.velocity;
				dust.scale = 0.65f;
				dustPos[dustIter % 180] = dust.position - Player.Center;
				dustID[dustIter % 180] = index;
				dustOwnerID[dustIter % 180] = Player.whoAmI;
				dust.shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
				dust.alpha = (int)(255 - (255 * Player.stealth));
			}
			for (int i = 0; i < (dustIter > 180 ? 180 : dustIter); i++)
			{
				if (dustID[i] != -1 && dustOwnerID[i] != -1)
				{
					Player owner = Main.player[dustOwnerID[i]];
					Dust dust = Main.dust[dustID[i]];
					if (dust.type == ModContent.DustType<CopyDust2>() && dust.active && dustPos[i] != new Vector2(-1, -1))
					{
						dust.position = dustPos[i] + owner.Center;
						if (dustPos[i].X > 0 && owner.direction == 1)
						{
							dust.active = false;
							dustID[i] = -1;
							dustOwnerID[i] = -1;
							dustPos[i] = new Vector2(-1, -1);
						}
						if (dustPos[i].X < 0 && owner.direction == -1)
						{
							dust.active = false;
							dustID[i] = -1;
							dustOwnerID[i] = -1;
							dustPos[i] = new Vector2(-1, -1);
						}
					}
					else
					{
						dustID[i] = -1;
						dustOwnerID[i] = -1;
						dustPos[i] = new Vector2(-1, -1);
					}
				}
			}
		}
		public void flight()
		{
			VoidPlayer voidPlayer = VoidPlayer.ModPlayer(Player);
			voidPlayer.flatVoidRegen -= voidDrain;
			HaloDust();
			Player.gravity = 0f;
			Player.noFallDmg = true;
			Player.maxFallSpeed *= 4f;
			Player.wingTime = -1;

			if (Player.controlDown && !(Player.controlUp || Player.controlJump) && !TilesBelow())
			{
				movingVert = true;
				float toBeAdded = wingSpeed / 3;
				if (Player.velocity.Y + toBeAdded < wingSpeed * 1.5f)
				{
					Player.velocity.Y += toBeAdded;
				}
				else if (Player.velocity.Y < wingSpeed * 1.5f)
				{
					Player.velocity.Y = wingSpeed * 1.5f;
				}
			}
			if ((Player.controlUp || Player.controlJump) && !Player.controlDown)
			{
				movingVert = true;
				float toBeAdded = -wingSpeed / 3;
				if (Player.velocity.Y + toBeAdded > -wingSpeed * 1.5f)
				{
					Player.velocity.Y += toBeAdded;
				}
				else if (Player.velocity.Y > -wingSpeed * 1.5f)
				{
					Player.velocity.Y = -wingSpeed * 1.5f;
				}
			}
			if (Player.controlLeft && Player.dashDelay >= 0 && !Player.controlRight)
			{
				movingHori = true;
				float toBeAdded = -wingSpeed / 3;
				if (Player.velocity.X + toBeAdded > -wingSpeed * 1.5f)
				{
					Player.velocity.X += toBeAdded;
				}
				else if (Player.velocity.X > -wingSpeed * 1.5f)
				{
					Player.velocity.X = -wingSpeed * 1.5f;
				}
			}
			if (Player.controlRight && Player.dashDelay >= 0 && !Player.controlLeft)
			{
				movingHori = true;
				float toBeAdded = wingSpeed / 3;
				if (Player.velocity.X + toBeAdded < wingSpeed * 1.5f)
				{
					Player.velocity.X += toBeAdded;
				}
				else if (Player.velocity.X < wingSpeed * 1.5f)
				{
					Player.velocity.X = wingSpeed * 1.5f;
				}
			}
			if (!movingVert)
			{
				Player.velocity.Y *= 0.8f;
			}
			if (!movingHori && Player.dashDelay >= 0)
			{
				Player.velocity.X *= 0.8f;
			}
			if (TilesBelow())
			{
				if (!Player.controlDown && !Player.controlUp && !Player.controlLeft && !Player.controlRight && !Player.controlJump && Player.velocity.Y > -2)
					creativeFlight = false;
				if (Player.controlDown)
					Player.gravity = Player.defaultGravity + 0.2f;
			}
			else
			{
				if (Player.velocity.Y == 0f)
					Player.velocity.Y = -0.04f;
			}
			movingHori = false;
			movingVert = false;
		}
		int randCounter = 0;
		float boost = 0f;
		bool runOnce = true;
		public override void ResetEffects()
		{
			if (gyro && !TilesBelow())
			{
				if (Player.controlDown && !creativeFlight)
				{
					if (boost < 300)
					{
						boost++;
						boost *= 1.025f;
					}
					else
					{
						boost = 300;
					}
					if (Player.velocity.Y != 0 && boost > 40)
					{
						Dust dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Electric, 0, -1);
						dust.velocity *= 0.2f;
						dust.noGravity = true;
					}
				}
				else
				{
					boost = 0f;
				}
				Player.gravity += 0.0025f * boost;
				Player.maxFallSpeed += 0.075f * boost;
			}
			else
			{
				boost = 0f;
			}
			gyro = false;
			randCounter++;
			if (Player.gravDir != 1)
				canCreativeFlight = false;
			if (Player.mount.Active || !canCreativeFlight)
			{
				canCreativeFlight = false;
				wingSpeed = wingSpeedMax;
				creativeFlight = false;
				return;
			}
			if (canCreativeFlight)
				flightStart();
			if (creativeFlight)
			{
				if (runOnce)
				{
					if (Player.velocity.Y == 0)
						Player.velocity.Y -= 12f;
					DustExplosion();
					runOnce = false;
				}
				flight();
			}
			else
			{
				if (!runOnce)
				{
					DustExplosion();
					runOnce = true;
				}
			}
			canCreativeFlight = false;
			wingSpeed = wingSpeedMax;
		}
		public static Color changeColorBasedOnStealth(Color color, Player drawPlayer)
        {
			var shadow = drawPlayer.shadow;
			if (drawPlayer.inventory[drawPlayer.selectedItem].type == ItemID.PsychoKnife)
			{
				var num23 = drawPlayer.stealth;
				if (num23 < 0.03)
					num23 = 0.03f;
				var num24 = (float)((1.0 + num23 * 10.0) / 11.0);
				if (num23 < 0.0)
					num23 = 0.0f;
				if (num23 >= 1.0 - shadow && shadow > 0.0)
					num23 = shadow * 0.5f;
				color = new Color((int)(byte)(color.R * num23),
					(byte)(color.G * num23),
					(byte)(color.B * num24),
					(byte)(color.A * num23));
			}
			else if (drawPlayer.shroomiteStealth)
			{
				var num23 = drawPlayer.stealth;
				if (num23 < 0.03)
					num23 = 0.03f;
				var num24 = (float)((1.0 + num23 * 10.0) / 11.0);
				if (num23 < 0.0)
					num23 = 0.0f;
				if (num23 >= 1.0 - shadow && shadow > 0.0)
					num23 = shadow * 0.5f;
				color = new Color((byte)(color.R * (double)num23),
					(byte)(color.G * num23),
					(byte)(color.B * num24),
					(byte)(color.A * num23));
			}
			else if (drawPlayer.setVortex)
			{
				var num23 = drawPlayer.stealth;
				if (num23 < 0.03)
					num23 = 0.03f;
				if (num23 < 0.0)
					num23 = 0.0f;
				if (num23 >= 1.0 - shadow && shadow > 0.0)
					num23 = shadow * 0.5f;
				Color secondColor = new Color(Vector4.Lerp(Vector4.One, new Vector4(0.0f, 0.12f, 0.16f, 0.0f), 1f - num23));
				color = color.MultiplyRGBA(secondColor);
			}
			return color;
		}
		public static readonly PlayerLayer TestWingsVisual = new PlayerLayer("SOTS", "TestWingsVisual", PlayerLayer.Wings, delegate (PlayerDrawInfo drawInfo) {

			// We don't want the glowmask to draw if the player is cloaked or dead
			// drawInfo.shadow != 0f || 
			if (drawInfo.drawPlayer.dead || drawInfo.drawPlayer.mount.Active)
			{
				return;
			}

			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("SOTS");
			TestWingsPlayer testWingsPlayer = (TestWingsPlayer)drawPlayer.GetModPlayer(mod, "TestWingsPlayer");
			if (drawPlayer.wings != mod.GetEquipSlot("TestWings", EquipType.Wings))
			{
				return;
			}

			//Texture2D texture = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/TestWings_Wings_Glow").Value;
			Texture2D smallPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart1").Value;
			Texture2D bigPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart2").Value;
			Texture2D bigPieceAlt = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart2").Value;
			Texture2D mediumPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart3").Value;
			Texture2D mediumPieceAlt = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart3").Value;
			Texture2D boosterPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingBooster1").Value;
			Texture2D bonusPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart4EffectFill").Value;
			Texture2D bonusPiece2 = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart4EffectOutline").Value;
			Texture2D bonusPiece3 = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingBooster2Effect").Value;
			Texture2D boosterPieceGlow = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingBooster2Glow").Value;
			Texture2D wingPieceGlow = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart4Glow").Value;
			int type = testWingsPlayer.epicWingType;
			switch (type)
			{
				case (int)EpicWingType.Blocky:
					break;
				case (int)EpicWingType.Default:
					smallPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart1").Value;
					bigPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart4Base").Value;
					bigPieceAlt = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart4Base2").Value;
					mediumPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart5").Value;
					mediumPieceAlt = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingPart5_2").Value;
					boosterPiece = Mod.Assets.Request<Texture2D>("Items/Otherworld/EpicWings/WingBooster2").Value;
					break;
			}

			float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
			//drawX -= 9 * drawPlayer.direction;
			float drawY = (int)drawInfo.position.Y + drawPlayer.height / 2;
			//drawY += 2 * drawPlayer.gravDir;


			Vector2 position = new Vector2(drawX, drawY) - Main.screenPosition;
			float alpha = 1 - drawInfo.shadow;
			float dustAlpha = 1 - drawInfo.shadow;
			dustAlpha *= drawPlayer.stealth;
			alpha *= (255 - drawPlayer.immuneAlpha) / 255f;
			Color color = Color.White.MultiplyRGBA(Lighting.GetColor((int)drawX/16, (int)drawY /16)); //apply lighting to wings
			color = changeColorBasedOnStealth(color, drawPlayer);
			//Rectangle frame = new Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4);
			float rotation = drawPlayer.bodyRotation;
			SpriteEffects spriteEffects = drawInfo.spriteEffects;

			int mode = drawPlayer.wingFrame;
			List<DrawData> drawData = new List<DrawData>();
			List<DrawData> drawData2 = new List<DrawData>();
			List<DrawData> drawData3 = new List<DrawData>();
			List<DrawData> drawData4 = new List<DrawData>();
			int wingNum = 0;
			for (int j = 0; j < 2; j++)
			{
				int direction = (j * 2) - 1;
				direction *= -drawPlayer.direction;
				Vector2 currentPos = position;
				currentPos.X -= direction + drawPlayer.direction;
				currentPos.Y -= 4 * drawPlayer.gravDir;
				if (mode == 0)
				{
					currentPos.X -= direction * 4;
				}
				for (int i = 0; i < 10; i++)
				{
					float scale = 7.5f - (i == 0 ? 0 : j);
					scale /= 7.5f;
					if (type == (int)EpicWingType.Default)
					{
						if (mode == 0 && i != 0)
							scale *= 0.85f;
					}
					float rotationI = MathHelper.ToRadians((i - 2) * -(7.5f + (j == 1 ? 0.5f : 0)) * direction * drawPlayer.gravDir);
					if(mode == 0)
					{
						rotationI = MathHelper.ToRadians((64 + (j == 1 ? 4f : 0)) * direction * drawPlayer.gravDir);
					}
					Vector2 fromPlayer = new Vector2(-6 * direction * scale, 0).RotatedBy(rotation - rotationI);
					if (mode == 0)
					{
						fromPlayer = new Vector2((-1f -0.75f * scale) * direction , 0).RotatedBy(rotation - rotationI);
					}
					currentPos += fromPlayer;
					Texture2D currentTexture = smallPiece;

					if (i == 0)
					{
						currentTexture = j % 2 == 0 ? mediumPiece : mediumPieceAlt;
						if(j == 1)
							currentPos.X += 2 * direction;
						if(mode != 0)
							currentPos.X -= 4 * direction;
					}
					else if (i == 1)
					{
						if (j == 1 && mode == 0)
							currentPos.X -= direction * 2;
						if (mode == 0)
							currentPos.X -= 1 * direction;
						if (mode != 0)
							currentPos.X += 3 * direction;
					}
					else if (i % 3 == 0)
						currentTexture = j % 2 == 0 ? bigPiece : bigPieceAlt;

					Vector2 origin = new Vector2(currentTexture.Width / 2, currentTexture.Height / 2);
					if (currentTexture == bigPiece || currentTexture == bigPieceAlt)
					{
						origin = new Vector2(currentTexture.Width / 2, 8);
						if (drawPlayer.gravDir == -1)
						{
							origin = new Vector2(currentTexture.Width / 2, currentTexture.Height - 8);
						}
					}
					if (type == (int)EpicWingType.Blocky && i % 3 == 0 && mode == 0 && i != 0)
					{
						rotationI += MathHelper.ToRadians(90 * direction * drawPlayer.gravDir);
					}
					if (type == (int)EpicWingType.Default && i % 3 == 0 && mode == 0 && i != 0)
					{
						rotationI += MathHelper.ToRadians(26 * direction * drawPlayer.gravDir);
					}
					bool booster = i != 0 && i % 3 == 0 && drawPlayer.wingFrame == 2;
					if(booster)
					{
						wingNum++;
						currentTexture = boosterPiece;
						Vector2 rotationOrigin = new Vector2(-2.75f * direction, 6f) - drawPlayer.velocity;
						Vector2 currentPos2 = currentPos + Main.screenPosition;
						float overrideRotation = rotationOrigin.ToRotation();
						Vector2 dustVelo = new Vector2(7.2f, 0).RotatedBy(overrideRotation);
						if (type == (int)EpicWingType.Default)
						{
							Color color2 = new Color(110, 110, 110, 0);
							color2 = changeColorBasedOnStealth(color2, drawPlayer);
							for (int k = 0; k < 6; k++)
							{
								float x = Main.rand.Next(-10, 11) * 0.1f;
								float y = Main.rand.Next(-10, 11) * 0.1f;
								DrawData data2 = (new DrawData(bonusPiece3, currentPos + new Vector2(x, y), null, color2 * alpha * alpha, rotation + overrideRotation - MathHelper.ToRadians(90), origin, scale, spriteEffects, 0));
								data2.shader = drawInfo.wingShader;
								drawData3.Add(data2);
							}
							if (drawInfo.shadow == 0f)
							{
								Color colorDust = changeColorBasedOnStealth(Color.White, drawPlayer);
								int index = Dust.NewDust(currentPos2 + dustVelo * 1.5f * scale + new Vector2(-4, -4), 0, 0, Mod.Find<ModDust>("CopyDust" + (j == 0 ? "3" : "")).Type, 0, 0, 0, colorDust);
								Dust dust = Main.dust[index];
								dust.noGravity = true;
								dust.fadeIn = 0.1f;
								dust.velocity = dustVelo;
								dust.scale += 0.3f;
								dust.scale *= scale;
								dust.shader = GameShaders.Armor.GetSecondaryShader(drawPlayer.cWings, drawPlayer);
								dust.alpha = (int)(0.7f * (int)(255 - colorDust.A));
								Main.playerDrawDust.Add(index);
							}
						}
						else
						{
							if (drawInfo.shadow == 0f)
							{
								int index = Dust.NewDust(currentPos2 + dustVelo * 1.5f + new Vector2(-5, -5), 0, 0, Mod.Find<ModDust>("CubeDust").Type);
								Dust dust = Main.dust[index];
								dust.noGravity = true;
								dust.fadeIn = 0.5f;
								dust.velocity *= 0.8f;
								dust.velocity += dustVelo;
								dust.scale *= scale;
								dust.shader = GameShaders.Armor.GetSecondaryShader(drawPlayer.cWings, drawPlayer);
								dust.alpha = (int)(0.7f * (255 - (255 * dustAlpha)));
								Main.playerDrawDust.Add(index);
							}
						}
						DrawData data = (new DrawData(currentTexture, currentPos, null, color * alpha, rotation + overrideRotation - MathHelper.ToRadians(90), origin, scale, spriteEffects, 0));
						data.shader = drawInfo.wingShader;
						drawData.Add(data);
						data = (new DrawData(boosterPieceGlow, currentPos, null, changeColorBasedOnStealth(Color.White, drawPlayer) * alpha, rotation + overrideRotation - MathHelper.ToRadians(90), origin, scale, spriteEffects, 0));
						data.shader = drawInfo.wingShader;
						drawData4.Add(data);
						Color color3 = new Color(60, 70, 80, 0) * 0.4f;
						for (int i2 = 0; i2 < 360; i2 += 30)
						{
							Vector2 addition = new Vector2(-Main.rand.Next(15) * 0.1f + 1.75f, 0).RotatedBy(MathHelper.ToRadians(i2));
							data = (new DrawData(boosterPieceGlow, currentPos + addition, null, changeColorBasedOnStealth(color3, drawPlayer) * alpha, rotation + overrideRotation - MathHelper.ToRadians(90), origin, scale, spriteEffects, 0));
							data.shader = drawInfo.wingShader;
							drawData4.Add(data);
						}
					}
					else
					{
						Vector2 vector2_1 = Vector2.Zero;
						if (i == 0)
						{
							vector2_1 = fromPlayer;
							rotationI = 0f;
						}
						DrawData data = (new DrawData(currentTexture, currentPos - vector2_1, null, color * alpha, rotation - rotationI, origin, scale, spriteEffects, 0));
						data.shader = drawInfo.wingShader;
						drawData.Add(data);
						if (type == (int)EpicWingType.Default && (i % 3 == 0 && i != 0))
                        {
							data = (new DrawData(wingPieceGlow, currentPos - vector2_1, null, changeColorBasedOnStealth(Color.White, drawPlayer) * alpha, rotation - rotationI, origin, scale, spriteEffects, 0));
							data.shader = drawInfo.wingShader;
							drawData4.Add(data);
                        }							
						if (type == (int)EpicWingType.Default && mode == 1 && (i % 3 == 0 && i != 0))
						{
							if (drawPlayer.controlJump)
							{
								Color color3 = new Color(60, 70, 80, 0) * 0.4f;
								for (int i2 = 0; i2 < 360; i2 += 30)
								{
									Vector2 addition = new Vector2(-Main.rand.Next(15) * 0.1f + 1.75f, 0).RotatedBy(MathHelper.ToRadians(i2));
									data = (new DrawData(wingPieceGlow, currentPos + addition - vector2_1, null, changeColorBasedOnStealth(color3, drawPlayer) * alpha, rotation - rotationI, origin, scale, spriteEffects, 0));
									data.shader = drawInfo.wingShader;
									drawData4.Add(data);
								}
							}
							Color color2 = new Color(110, 110, 110, 0);
							color2 = changeColorBasedOnStealth(color2, drawPlayer);
							if (!drawPlayer.controlJump)
								color2 *= 0.5f;
							int amt = drawPlayer.controlJump ? 6 : 2;
							for (int k = 0; k < amt; k++)
							{
								float x = Main.rand.Next(-10, 11) * 0.1f;
								float y = Main.rand.Next(-10, 11) * 0.1f;
								if (k == 0)
								{
									DrawData data3 = (new DrawData(bonusPiece, currentPos, null, color2 * alpha, rotation - rotationI, origin, scale, spriteEffects, 0));
									data3.shader = drawInfo.wingShader;
									drawData2.Add(data3);
								}
								if(k < 4)
								{
									x = 0;
									y = 0;
								}
								Vector2 tilt2 = new Vector2(0, 4.5f).RotatedBy(MathHelper.ToRadians(testWingsPlayer.randCounter * 20));
								Vector2 tilt3 = new Vector2(tilt2.X, 0).RotatedBy(rotation - rotationI);
								Vector2 tilt = new Vector2(0, 1).RotatedBy(rotation - rotationI) * drawPlayer.gravDir;
								Vector2 currentPos2 = currentPos - vector2_1 + Main.screenPosition;
								if (type == (int)EpicWingType.Default && Main.rand.NextBool(18) && drawPlayer.controlJump)
								{
									if (drawInfo.shadow == 0f)
									{
										Color colorDust = changeColorBasedOnStealth(Color.White, drawPlayer);
										int index = Dust.NewDust(currentPos2 + tilt3 * scale + tilt2 * scale + (tilt * Main.rand.Next(16, 34) * scale) + new Vector2(-4, -4), 0, 0, Mod.Find<ModDust>("CopyDust" + (j == 0 ? "3" : "")).Type, 0, 0, 0, colorDust);
										Dust dust = Main.dust[index];
										dust.noGravity = true;
										dust.fadeIn = 0.6f;
										dust.velocity *= 0;
										dust.scale *= 0.45f;
										dust.scale *= scale;
										dust.shader = GameShaders.Armor.GetSecondaryShader(drawPlayer.cWings, drawPlayer);
										dust.alpha = (int)(0.7f * (int)(255 - colorDust.A));
										Main.playerDrawDust.Add(index);
									}
								}
								DrawData data2 = (new DrawData(bonusPiece2, currentPos + new Vector2(x, y), null, color2 * alpha * alpha, rotation - rotationI, origin, scale, spriteEffects, 0));
								data2.shader = drawInfo.wingShader;
								drawData2.Add(data2);
							}
						}
					}
				}
			}


			for (int i = 0; i < drawData2.Count; i++)
				Main.playerDrawData.Add(drawData2[i]);
			for (int i = 0; i < drawData.Count; i++)
				if ((i % 10) % 3 != 0 && mode != 0)
					Main.playerDrawData.Add(drawData[i]);
			for (int i = 0; i < drawData3.Count; i++)
				Main.playerDrawData.Add(drawData3[i]);
			for (int i = 0; i < drawData.Count; i++) //add the more important layers on so they don't have weired overlap
				if ((i % 10) % 3 == 0 || mode == 0)
					Main.playerDrawData.Add(drawData[i]);
			for (int i = 0; i < drawData4.Count; i++)
				Main.playerDrawData.Add(drawData4[i]);

			// Here we generate visual dust while our glowmask is visible
			if (Main.rand.NextBool(10))
			{
				/*
				int index = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 135);
				Dust dust = Main.dust[index];
				dust.noGravity = true;
				dust.noLight = true;
				dust.fadeIn = Main.rand.NextFloat(0.5f, 0.8f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(drawInfo.wingShader, drawPlayer);
				Main.playerDrawDust.Add(index);
				*/
			}
		});
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			int wingLayer = layers.FindIndex(l => l == PlayerLayer.Wings);

			if (wingLayer > -1)
			{
				layers.Insert(wingLayer + 1, TestWingsVisual);
			}
		}
	}
}