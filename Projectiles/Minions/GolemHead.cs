using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Minions
{
    public class GolemHead : ModProjectile
    {
        public static Asset<Texture2D> glowmask;
		public int fireTimer;

        public override void Load()
        {
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.width = 112;
            Projectile.height = 138;
            Projectile.penetrate = -1;
			Projectile.tileCollide = false;
        }

        public override void AI()
		{
			Player owner = Main.player[Projectile.owner];

			Projectile.Center = owner.Center - new Vector2(0, 90);

			if (!CheckActive(owner))
				Projectile.Kill();

			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);

			if (foundTarget)
				fireTimer++;
			else fireTimer = 0;
			if (fireTimer > 60)
			{
				SoundEngine.PlaySound(SoundID.Item33);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(16, -12), Vector2.Normalize(targetCenter - Projectile.Center) * 16, ModContent.ProjectileType<GolemBeam>(), 90, 1, owner.whoAmI);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center - new Vector2(16, 12), Vector2.Normalize(targetCenter - Projectile.Center) * 16, ModContent.ProjectileType<GolemBeam>(), 90, 1, owner.whoAmI);
				fireTimer = 0;
			}
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner)
		{
			if (owner.dead || !owner.active || !owner.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(ModContent.ItemType<GolemSoulCrystal>()) || owner.statLife > owner.statLifeMax2 / 2)
				return false;
			else Projectile.timeLeft = 2;
			return true;
		}

		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
		{
			// Starting search distance
			distanceFromTarget = 700f;
			targetCenter = Projectile.position;
			foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (owner.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[owner.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, Projectile.Center);

				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 2000f)
				{
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}

			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;

						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			Projectile.friendly = foundTarget;
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
