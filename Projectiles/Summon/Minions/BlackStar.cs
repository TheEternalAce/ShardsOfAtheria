using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Summon.Minions
{
    public class BlackStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 40;
            Projectile.height = 38;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            //Projectile.minion = true;
            //Projectile.minionSlots = 0f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;

            DrawOffsetX = -2;
            DrawOriginOffsetY = -2;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            Projectile.netUpdate = true;
            if (!CheckActive(owner))
            {
                Projectile.Kill();
                return;
            }

            Visuals();
            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            if (Projectile.ai[1] == 0)
            {
                foundTargetIdlePos = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 100;
            }
            Vector2 idlePosition = owner.Center + foundTargetIdlePos;

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            // If your minion is flying, you want to do this independently of any conditions
            float overlapVelocity = 0.04f;

            // Fix overlap with other minions
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner &&
                    Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
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

                        if ((closest && inRange || !foundTarget))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
        }

        Vector2 foundTargetIdlePos;
        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            Player player = Main.player[Projectile.owner];
            float speed = 4f;
            float inertia = 60f;

            if (foundTarget)
            {
                if (distanceFromTarget > 40f)
                {
                    speed = 60f;
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;

                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
            }
            else
            {
                if (++Projectile.ai[1] >= 60)
                {
                    Projectile.ai[1] = 0;
                }

                // Minion doesn't have a target: return to player and idle
                if (distanceToIdlePosition > 300f)
                {
                    // Speed up the minion if it's away from the player
                    speed = 12f;
                    inertia = 40f;
                }


                if (distanceToIdlePosition > 4000f)
                {
                    Projectile.Center = player.Center;
                    Projectile.netUpdate = true;
                }
                else if (distanceToIdlePosition > 20f)
                {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }
        }

        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.Areus().royalSet)
            {
                Projectile.timeLeft = 2;
                return false;
            }
            return true;
        }

        private void Visuals()
        {
            Projectile.rotation += MathHelper.ToRadians(15) * Projectile.direction;
            if (Projectile.spriteDirection == -1)
            {
                DrawOffsetX = -2;
            }
            else
            {
                DrawOffsetX = -2;
            }
            Projectile.spriteDirection = Projectile.direction;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Projectile.GetPlayerOwner();
            var areus = player.Areus();
            areus.royalVoid -= 3;
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 16; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.ShadowbeamStaff, speed * 16f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
            var player = Projectile.GetPlayerOwner();
            if (player.IsLocal())
            {
                SoundEngine.PlaySound(SoundID.Shatter, Projectile.Center);
                SoundEngine.PlaySound(SoundID.NPCDeath52, Projectile.Center);
                var position = Main.MouseWorld;
                int npcWhoAmI = Projectile.FindTargetWithLineOfSight(400);
                if (npcWhoAmI != -1)
                {
                    position = Main.npc[npcWhoAmI].Center;
                }
                var vector = position - Projectile.Center;
                vector.Normalize();
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vector, ModContent.ProjectileType<ElectricShadeShot>(),
                    Projectile.damage, Projectile.knockBack, -1, 0, 1);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }

        public override void PostDraw(Color lightColor)
        {
            Projectile.DrawBlurTrail(lightColor, SoA.OrbBlur);
            Projectile.DrawAfterImage(lightColor);
        }
    }
}
