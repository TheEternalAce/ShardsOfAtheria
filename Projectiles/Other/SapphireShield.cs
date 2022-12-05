using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
	public class SapphireShield : ModProjectile
	{
		public static Asset<Texture2D> glowmask;

		public override void Load()
		{
			glowmask = ModContent.Request<Texture2D>(Texture);
		}

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			Main.projFrames[Type] = 5;
		}

		public override void SetDefaults()
		{
			Projectile.width = 64;
			Projectile.height = 64;
			Projectile.timeLeft = 35;
			Projectile.tileCollide = false;
			Projectile.alpha = 100;
		}

		public override void AI()
		{
			int frameSpeed = 6;
			if (++Projectile.frameCounter >= frameSpeed)
			{
				if (Projectile.frame < Main.projFrames[Type] - 1)
				{
					Projectile.frame++;
				}
			}
			Player owner = Main.player[Projectile.owner];
			Projectile.Center = owner.Center;
			owner.immuneNoBlink = true;

			if (Projectile.ai[0] == 0)
			{
				SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.5f), Projectile.Center);
				//SoundEngine.PlaySound(SoundID.Item37.WithPitchOffset(-0.5f), Projectile.Center);
				//SoundEngine.PlaySound(SoundID.NPCHit4.WithPitchOffset(-0.5f), Projectile.Center);
				Projectile.ai[0] = 1;
			}
		}

		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			Projectile.hide = true;
			overPlayers.Add(index);
		}

		public override void PostDraw(Color lightColor)
		{
			//TODO Generic glowmask draw, maybe generalize method
			Player player = Main.player[Projectile.owner];

			int offsetY = 0;
			int offsetX = 0;
			Texture2D glowmaskTexture = glowmask.Value;
			float originX = (glowmaskTexture.Width - Projectile.width) * 0.5f + Projectile.width * 0.5f;
			ProjectileLoader.DrawOffset(Projectile, ref offsetX, ref offsetY, ref originX);

			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}

			if (Projectile.ownerHitCheck && player.gravDir == -1f)
			{
				if (player.direction == 1)
				{
					spriteEffects = SpriteEffects.FlipHorizontally;
				}
				else if (player.direction == -1)
				{
					spriteEffects = SpriteEffects.None;
				}
			}

			Vector2 drawPos = new Vector2(Projectile.position.X - Main.screenPosition.X + originX + offsetX, Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + Projectile.gfxOffY);
			Rectangle sourceRect = glowmaskTexture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
			Color glowColor = new Color(255, 255, 255, 255) * 0.7f * Projectile.Opacity;
			Vector2 drawOrigin = new Vector2(originX, Projectile.height / 2 + offsetY);
			Main.EntitySpriteDraw(glowmaskTexture, drawPos, sourceRect, glowColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
		}
	}
}