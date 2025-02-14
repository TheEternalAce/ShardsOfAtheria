using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Star
{
    public class RedStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Type] = true;
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
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
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

        public override bool PreAI()
        {
            Projectile.minionSlots = 1 + Projectile.GetPlayerOwner().Slayer().artifactExtraSummons;
            return base.PreAI();
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

            Projectile.rotation = Projectile.velocity.X * Projectile.direction * 0.005f;
            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center - new Vector2(50f * owner.direction, 25f);

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
                    if (Projectile.position.X < other.position.X) Projectile.velocity.X -= overlapVelocity;
                    else Projectile.velocity.X += overlapVelocity;

                    if (Projectile.position.Y < other.position.Y) Projectile.velocity.Y -= overlapVelocity;
                    else Projectile.velocity.Y += overlapVelocity;
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

        public bool shoot = false;
        int shootTimer = 0;
        public int shootingTimer = 0;
        int pulseCounter;
        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            Player player = Main.player[Projectile.owner];
            float speed = 4f;
            float inertia = 30f;

            if (shoot)
            {
                if (++shootingTimer % 10 == 0 && shootingTimer < 40)
                {
                    var velocity = Main.rand.NextVector2CircularEdge(1, 1);
                    velocity += Projectile.velocity;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, Type + 3, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
                }
                if (shootingTimer == 30) pulseCounter++;
                if (shootingTimer >= 80)
                {
                    shootTimer = 0;
                    shootingTimer = 0;
                    shoot = false;
                }
            }
            else if (foundTarget)
            {
                int extraSummonTime = player.Slayer().artifactExtraSummons * 10;
                if (extraSummonTime > 90) extraSummonTime = 90;
                if (++shootTimer >= 120 + Main.rand.Next(100) - extraSummonTime && distanceFromTarget < 1000f) shoot = true;
                if (pulseCounter >= 10)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath52.WithPitchOffset(0.5f), Projectile.Center);
                    for (var i = 0; i < 28; i++)
                    {
                        Vector2 dustSpeed = Main.rand.NextVector2CircularEdge(1f, 1f);
                        Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemRuby, dustSpeed * 6f);
                        d.noGravity = true;
                    }
                    for (var i = 0; i < 20; i++)
                    {
                        Vector2 offset = Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i);
                        int num1 = (i + 1) / 4;
                        Vector2 dustSpeed = Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i);
                        Dust d = Dust.NewDustPerfect(Projectile.Center - offset * num1, DustID.GemAmethyst, dustSpeed * 4f);
                        d.noGravity = true;
                    }
                    foreach (NPC npc in Main.npc)
                    {
                        bool strike = false;
                        if (npc.CanBeChasedBy())
                        {
                            if (NPC.downedGolemBoss || npc.Distance(Projectile.Center) < 200f) strike = true;
                            if (strike)
                            {
                                NPC.HitInfo info = new()
                                {
                                    Damage = (int)(Projectile.damage * 1.5f * (1 + player.Slayer().artifactExtraSummons)),
                                    DamageType = DamageClass.Summon,
                                    Knockback = 7f,
                                    HitDirection = npc.Center.X >= Projectile.Center.X ? 1 : -1,
                                };
                                npc.StrikeNPC(info);
                                for (var i = 0; i < 28; i++)
                                {
                                    Vector2 dustSpeed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                    Dust d = Dust.NewDustPerfect(npc.Center, DustID.GemRuby, dustSpeed * 2.4f);
                                    d.noGravity = true;
                                }
                                for (var i = 0; i < 20; i++)
                                {
                                    Vector2 offset = Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i);
                                    int num1 = (i + 1) / 4;
                                    Vector2 dustSpeed = Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i);
                                    Dust d = Dust.NewDustPerfect(npc.Center - offset * num1, DustID.GemAmethyst, dustSpeed * 1f);
                                    d.noGravity = true;
                                }
                            }
                        }
                    }
                    pulseCounter = 0;
                }
            }
            else
            {
                pulseCounter = 0;
                shootTimer = 0;
            }
            // Minion doesn't have a target: return to player and idle
            if (distanceToIdlePosition > 300f)
            {
                // Speed up the minion if it's away from the player
                speed = 12f;
                inertia = 10f;
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

        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.HasBuff<EntropicStar>())
            {
                owner.Slayer().artifactExtraSummons = 0;
                return false;
            }
            Projectile.timeLeft = 2;
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Projectile.GetPlayerOwner();
            if (!player.HasBuff<ShadeState>())
            {
                var areus = player.Areus();
                areus.imperialVoid -= 3;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.Center);
            SoundEngine.PlaySound(SoundID.NPCDeath52, Projectile.Center);
            for (var i = 0; i < 16; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemRuby, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmethyst, speed * 4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }

        public override void PostDraw(Color lightColor)
        {
            Projectile.DrawBloomTrail(lightColor.UseA(50), SoA.OrbBloom);
            Projectile.DrawAfterImage(lightColor);
        }
    }
}
