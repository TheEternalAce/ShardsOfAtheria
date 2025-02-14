﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar
{
    public class AreusDrone : ModProjectile
    {
        int shootTimer;

        public override void SetStaticDefaults()
        {
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 32;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.minion = true;
            Projectile.minionSlots = 0f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
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

            Projectile.netUpdate = true;
            if (!CheckActive(owner)) return;
            Projectile.timeLeft = 2;

            Projectile.rotation = Projectile.velocity.X * 0.05f;
            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath44, Projectile.Center);
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            if (Projectile.ai[0] == 0 && !owner.HasBuff<ChargingDrones>())
            {
                foundTargetIdlePos = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 25;
            }
            if (++Projectile.ai[0] >= 60)
            {
                Projectile.ai[0] = 0;
            }
            if (Projectile.localAI[0] == 0)
            {
                var target = ShardsHelpers.FindClosestProjectile(Projectile.Center, 3000, ModContent.ProjectileType<EMAvatar>(), Projectile.owner);
                if (target != null) Projectile.localAI[0] = target.whoAmI;
            }
            var avatar = Main.projectile[(int)Projectile.localAI[0]];
            Vector2 idlePosition = avatar.Center + foundTargetIdlePos;

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

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
            distanceFromTarget = 200f;
            targetCenter = Projectile.position;
            foundTarget = false;

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float distance = Vector2.Distance(npc.Center, owner.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > distance;
                        bool inRange = distance < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        bool closeThroughWall = distance < 100f;

                        if ((closest && inRange || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = distance;
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
            // Default movement parameters (here for attacking)
            float speed = 4;
            float inertia = 80;

            if (foundTarget && !player.HasBuff<ChargingDrones>())
            {
                speed = 8f;
                inertia = 80f;

                if (++shootTimer >= 90 + Main.rand.Next(120))
                {
                    SoundEngine.PlaySound(SoA.MagnetShot, Projectile.Center);
                    Vector2 velocity = Vector2.Normalize(targetCenter - Projectile.Center) * 8f;
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, velocity,
                        ModContent.ProjectileType<AreusTagLaser>(), Projectile.damage, 0, Projectile.owner);

                    shootTimer = 0;
                }
            }
            else
            {
                shootTimer = 0;
            }

            // Minion doesn't have a target: return to player and idle
            if (distanceToIdlePosition > 600f)
            {
                // Speed up the minion if it's away from the player
                speed = 12f;
                inertia = 60f;
            }

            if (distanceToIdlePosition > 200f)
            {
                speed = 32f;
                inertia = 20f;
            }

            if (distanceToIdlePosition > 4000f)
            {
                Projectile.Center = player.Center;
                Projectile.netUpdate = true;
            }
            else if (distanceToIdlePosition > 20f && !player.HasBuff<ChargingDrones>())
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

            if (player.HasBuff<ChargingDrones>())
            {
                Projectile.velocity = vectorToIdlePosition * 0.05f;
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private static bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.Areus().royalSet || !owner.Areus().CommanderSetChip)
                return false;
            return true;
        }
    }
}
