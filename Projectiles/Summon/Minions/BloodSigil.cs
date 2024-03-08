using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions
{
    public class BloodSigil : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
        }

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 60;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.netImportant = true;
            Projectile.minionSlots = 1f;
            Projectile.ignoreWater = true;
            Projectile.minion = true;

            DrawOriginOffsetY = -6;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return false;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner))
            {
                Projectile.Kill();
                return;
            }

            GeneralBehavior(owner, out Vector2 idlePosition);
            var vectorToIdlePos = idlePosition - Projectile.Center;
            Projectile.velocity = vectorToIdlePos * 0.08f;

            // Trying to find NPC closest to the projectile
            NPC closestNPC = Projectile.FindClosestNPC(null, 3000);
            if (closestNPC != null)
            {
                if (++Projectile.ai[1] >= 60 + 100 * Main.rand.NextFloat())
                {
                    Vector2 velocity = closestNPC.Center - Projectile.Center;
                    velocity.Normalize();
                    velocity *= 8f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                        velocity, ModContent.ProjectileType<BloodSickleFriendly>(), Projectile.damage,
                        Projectile.knockBack, Projectile.owner);
                    Projectile.ai[1] = 0;
                }
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.HasBuff<BloodySigil>())
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }

        private void GeneralBehavior(Player owner, out Vector2 idlePosition)
        {
            Vector2 vector = new(0, -1);
            int count = owner.ownedProjectileCounts[Type];
            if (count == 0)
            {
                count++;
            }
            idlePosition = owner.Center + vector.RotatedBy(MathHelper.TwoPi / count * Projectile.ai[0]) * 90;
            var distanceToIdlePosition = Projectile.Distance(idlePosition);
            if (distanceToIdlePosition > 2000)
            {
                Projectile.Center = idlePosition;
                Projectile.netUpdate = true;
            }

            // If your minion is flying, you want to do this independently of any conditions
            float overlapVelocity = 0.04f;
            //Fix overlap with other minions
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X)
                    {
                        Projectile.velocity.X -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.X += overlapVelocity;
                    }

                    if (Projectile.position.Y < other.position.Y)
                    {
                        Projectile.velocity.Y -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.Y += overlapVelocity;
                    }
                }
            }
        }
    }
}
