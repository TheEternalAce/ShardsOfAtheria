﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.GemCore
{
    public class SapphireSpirit : ModProjectile
    {
        int shootTimer = 0;
        int emoteTimer = 0;
        int sleepyTimer = 0;
        bool sleep = false;
        bool grounded = false;

        readonly string[] randomEmotes = [":3", ":D", "X3", "XD", ":)", ">:3", ">:)", ":o"];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 22;
            Projectile.height = 38;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;

            DrawOffsetX = -6;
            DrawOriginOffsetY = -4;
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

        public override void OnSpawn(IEntitySource source)
        {
            CombatText.NewText(Projectile.Hitbox, Color.Blue, "XD");
            SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(2.5f), Projectile.Center);
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
            bool foundTarget = false;
            float distanceFromTarget = 0;
            Vector2 targetCenter = Vector2.Zero;
            if (owner.Gem().sapphireSpiritUpgrade)
            {
                SearchForTargets(owner, out foundTarget, out distanceFromTarget, out targetCenter);
            }
            if (sleep && !foundTarget)
            {
                DoSleep();
            }
            else
            {
                Projectile.tileCollide = false;
                grounded = false;
                Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
            }
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.X += 48f * owner.direction;
            idlePosition.Y -= 48f;

            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
            idlePosition.X += minionPositionOffsetX;

            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            if (++emoteTimer >= 900 + Main.rand.Next(300) && !sleep)
            {
                CombatText.NewText(Projectile.Hitbox, Color.Blue, randomEmotes[Main.rand.Next(randomEmotes.Length)]);
                SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(2), Projectile.Center);
                emoteTimer = 0;
            }

            float overlapVelocity = 0.04f;
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
                        float between = Vector2.Distance(npc.Center, owner.Center);
                        bool closest = Vector2.Distance(owner.Center, targetCenter) > between;
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
            else
            {
                sleep = false;
                sleepyTimer = 0;
                Projectile.frame = 0;
                Projectile.frameCounter = 0;
            }
        }

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            // Default movement parameters (here for attacking)
            float speed = 10;
            float inertia = 80;

            if (foundTarget)
            {
                sleepyTimer = 0;
                speed = 14f;
                inertia = 80f;

                var idlePosition = targetCenter;
                idlePosition.Y -= 124f;
                vectorToIdlePosition = idlePosition - Projectile.Center;
                distanceToIdlePosition = vectorToIdlePosition.LengthSquared();

                if (Projectile.damage == 0)
                {
                    return;
                }
                if (++shootTimer >= 90)
                {
                    Projectile.frame = 2;
                    Projectile.frameCounter = 0;
                    Projectile.damage = 150;
                    Projectile.spriteDirection = targetCenter.X < Projectile.Center.X ? -1 : 1;
                    SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
                    float numberProjectiles = 3; // 3 shots
                    float rotation = MathHelper.ToRadians(5);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 vel = Vector2.Normalize(targetCenter - Projectile.Center);
                        Vector2 perturbedSpeed = vel.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 10f; // Watch out for dividing by 0 if there is only 1 projectile.
                        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, perturbedSpeed,
                            ModContent.ProjectileType<SapphireBolt>(), Projectile.damage, 0, Projectile.owner);
                    }
                    shootTimer = 0;
                }
            }
            else
            {
                shootTimer = 0;
            }

            if (distanceToIdlePosition > 200f)
            {
                speed = 20f;
                inertia = 40f;
            }
            if (sleepyTimer >= 400)
            {
                speed = 2;
            }


            if (distanceToIdlePosition > 20f)
            {
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                if (sleepyTimer > 0)
                {
                    sleepyTimer--;
                }
            }
            else if (Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
            }
            else if (++sleepyTimer >= 600)
            {
                sleep = true;
                Projectile.velocity.X = 0;
            }
        }

        void DoSleep()
        {
            Player owner = Main.player[Projectile.owner];

            Projectile.tileCollide = true;
            if (++Projectile.velocity.Y >= 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if (Projectile.velocity.X != 0)
            {
                Projectile.velocity.X = 0;
            }
            Projectile.frame = 4;
            Projectile.frameCounter = 0;
            Projectile.rotation = 0;

            if (Vector2.Distance(owner.Center, Projectile.Center) >= 200)
            {
                sleep = false;
                sleepyTimer = 0;
                grounded = false;
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.Gem().sapphireSpirit || owner.ownedProjectileCounts[ModContent.ProjectileType<GemSoul>()] > 0)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }

        private void Visuals()
        {
            if (sleep)
            {
                return;
            }

            // So it will lean slightly towards the direction it's moving
            Projectile.rotation = Projectile.velocity.X * 0.05f;

            if (sleepyTimer >= 400)
            {
                Projectile.frame = 1;
                return;
            }

            // This is a simple "loop through all frames from top to bottom" animation
            int frameTime = 5;
            switch (Projectile.frame)
            {
                case 0:
                    frameTime = 120;
                    break;
                case 1:
                    frameTime = 60;
                    break;
                case 2:
                    frameTime = 20;
                    break;
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= frameTime)
            {
                Projectile.spriteDirection = 1;
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!grounded)
            {
                SoundEngine.PlaySound(SoundID.Item53, Projectile.Center);
                grounded = true;
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            if (!Projectile.GetPlayerOwner().IsLocal()) return;
            CombatText.NewText(Projectile.Hitbox, Color.Blue, ":(");
            SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(1.5f), Projectile.Center);
        }
    }
}
