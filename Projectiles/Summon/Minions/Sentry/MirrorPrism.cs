using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Sentry
{
    public class MirrorPrism : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.SentryShot[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 34;
            Projectile.aiStyle = -1;

            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
            }
        }

        public override void AI()
        {
            var owner = Projectile.GetPlayerOwner();
            if (++Projectile.ai[0] >= 30) Projectile.velocity *= 0.8f;

            RepellPrisms(50);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Shooting(foundTarget, targetCenter, distanceFromTarget);
        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 400f;
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
            if (foundTarget && distanceFromTarget < 1000)
            {
                float shootTime = 60f;
                if (++Projectile.ai[0] >= shootTime + Main.rand.Next(45))
                {
                    Projectile.ai[0] = 0f;
                    int type = ModContent.ProjectileType<TinyLaser>();
                    float speed = 16f;
                    var vectorToTarget = targetCenter - Projectile.Center;
                    vectorToTarget.Normalize();
                    int damage = Projectile.damage / 2;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vectorToTarget * speed, type, damage, Projectile.knockBack, -1, 0f, Projectile.whoAmI);
                }
            }
        }

        private void RepellPrisms(float maxDistance)
        {
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.active && projectile.whoAmI != Projectile.whoAmI && projectile.type == Type)
                {
                    var distToPlayer = Vector2.Distance(projectile.Center, Projectile.Center);
                    if (distToPlayer <= maxDistance)
                    {
                        var vector = projectile.Center - Projectile.Center;
                        vector.Normalize();
                        Projectile.velocity = vector * -4;
                        projectile.velocity = vector * 4;
                    }
                }
            }
        }

        public static int FindNewestProjectile(int owner, int mirrorIndex)
        {
            int result = -1;
            int timeLeft = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.type == ModContent.ProjectileType<MirrorPrism>() && projectile.owner == owner && projectile.ai[1] == mirrorIndex)
                {
                    if (projectile.timeLeft > timeLeft)
                    {
                        result = projectile.whoAmI;
                        timeLeft = projectile.timeLeft;
                    }
                }
            }
            return result;
        }

        public static int CountPrismsPerMirror(int owner, int mirrorIndex)
        {
            int result = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.type == ModContent.ProjectileType<MirrorPrism>() && projectile.owner == owner && projectile.ai[1] == mirrorIndex &&
                    Math.Abs(projectile.velocity.X) < 1 && Math.Abs(projectile.velocity.Y) < 1)
                {
                    result++;
                }
            }
            return result;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
