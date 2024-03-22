using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Sentry
{
    public class AreusMirrorSentry : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionSacrificable[Type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 40;
            Projectile.tileCollide = false;
            Projectile.sentry = true;
            Projectile.minionSlots = 1;
            Projectile.timeLeft = Projectile.SentryLifeTime;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Shooting(foundTarget, targetCenter, distanceFromTarget);
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
                if (between < 2500f)
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

                        if ((closest && inRange || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
        }

        private void Shooting(bool foundTarget, Vector2 targetCenter, float distanceFromTarget)
        {
            var player = Projectile.GetPlayerOwner();
            if (foundTarget && distanceFromTarget < 1000)
            {
                float shootTime = 60f;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<MirrorPrism>()] <= 6)
                {
                    shootTime *= 0.5f;
                }
                if (++Projectile.ai[0] >= shootTime + Main.rand.Next(90))
                {
                    Projectile.ai[0] = 0f;
                    int type = ModContent.ProjectileType<MirrorPrism>();
                    float speed = 16f * (1f - Main.rand.NextFloat(0.33f));
                    var vectorToTarget = targetCenter - Projectile.Center;
                    vectorToTarget = vectorToTarget.RotatedByRandom(MathHelper.PiOver4);
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<MirrorPrism>()] >= 6)
                    {
                        int prismWhoAmI = MirrorPrism.FindNewestProjectile(Projectile.owner);
                        if (prismWhoAmI > -1)
                        {
                            var prism = Main.projectile[prismWhoAmI];
                            vectorToTarget = prism.Center - Projectile.Center;
                            speed = 8f;
                            type = ModContent.ProjectileType<BounceLaser>();
                        }
                    }
                    vectorToTarget.Normalize();
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vectorToTarget * speed, type, Projectile.damage, Projectile.knockBack);
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
                dust.velocity = Main.rand.NextVector2Circular(8f, 8f);
            }
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        }
    }
}