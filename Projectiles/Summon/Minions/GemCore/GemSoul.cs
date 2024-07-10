using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.GemCore
{
    public class GemSoul : ModProjectile
    {
        int shootTimer = 0;
        int projectileShootTimer = 300;
        int emoteTimer = 0;
        int sleepyTimer = 0;
        bool sleep = false;
        bool grounded = false;

        readonly string[] randomEmotes = [":3", ":D", "X3", "XD", ":)", ">:3", ">:)", ":o"];

        int blinkTimer = 0;
        int animationState = ANIMATION_IDLE;
        const int ANIMATION_IDLE = 0;
        const int ANIMATION_PLATFORM = 1;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 9;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 40;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;

            //DrawOffsetX = -6;
            //DrawOriginOffsetY = -4;
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
            CombatText.NewText(Projectile.Hitbox, Color.Blue, "<3");
            SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(1.5f), Projectile.Center);
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
            idlePosition.X -= 48f * owner.direction;
            idlePosition.Y -= 16f;

            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            int direction = owner.direction;
            if ((idlePosition.X > owner.Center.X && Projectile.Center.X < owner.Center.X) ||
                (idlePosition.X < owner.Center.X && Projectile.Center.X > owner.Center.X))
            {
                direction *= -1;
            }
            if (Projectile.frame != 4)
            {
                Projectile.spriteDirection = direction;
            }

            int torch = TorchID.Purple;
            if (projectileShootTimer >= 180)
            {
                torch = TorchID.White;
            }
            Lighting.AddLight(Projectile.Center, torch);

            if (owner.rocketTime == 0 && owner.wingTime == 0 && owner.mount.FlyTime == 0 && owner.velocity.Y > 0)
            {
                if (Projectile.ai[0] == 0)
                {
                    var position = owner.Center;
                    position.X += owner.velocity.X * 8f;
                    position.Y += 60;
                    if (Collision.CheckAABBvLineCollision(owner.Center, owner.Size, owner.Center, position))
                    {
                        animationState = ANIMATION_PLATFORM;
                        Projectile.frame = 5;
                        var velocity = position - Projectile.Center;
                        velocity.Normalize();
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<EmeraldShot>(), 0, 0, -1, position.X, position.Y);
                        ShardsHelpers.DustRing(Projectile.Center, 3f, DustID.GemEmerald);
                    }
                    Projectile.ai[0] = 1;
                }
            }
            else
            {
                Projectile.ai[0] = 0;
            }

            if (++emoteTimer >= 900 + Main.rand.Next(300) && !sleep)
            {
                CombatText.NewText(Projectile.Hitbox, Color.Blue, randomEmotes[Main.rand.Next(randomEmotes.Length)]);
                SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(1), Projectile.Center);
                emoteTimer = 0;
            }
        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            if (owner.Gem().sapphireSpiritUpgrade)
            {
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
                else if (sleep)
                {
                    sleep = false;
                    sleepyTimer = 0;
                    Projectile.frame = 0;
                    Projectile.frameCounter = 0;
                }
            }
        }

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            // Default movement parameters (here for attacking)
            float speed = 4;
            float inertia = 80;

            if (foundTarget)
            {
                if (Projectile.damage == 0)
                {
                    return;
                }
                sleep = false;
                sleepyTimer = 0;
                emoteTimer = 0;
                Projectile.velocity = vectorToIdlePosition * 0.055f;
                if (++shootTimer >= 90)
                {
                    Projectile.frame = 4;
                    Projectile.frameCounter = 0;
                    Projectile.damage = 150;
                    Projectile.spriteDirection = targetCenter.X < Projectile.Center.X ? -1 : 1;

                    Vector2 velocity = Vector2.Normalize(targetCenter - Projectile.Center);
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, velocity,
                        ModContent.ProjectileType<RubyShot>(), Projectile.damage, 0, Projectile.owner);
                    shootTimer = 0;
                }
            }
            else
            {
                shootTimer = 0;
            }
            if (Projectile.GetPlayerOwner().Gem().sapphireSpiritUpgrade)
            {
                if (++projectileShootTimer >= 180)
                {
                    if (projectileShootTimer == 180)
                    {
                        for (var i = 0; i < 28; i++)
                        {
                            var vector = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, vector * 3f);
                            d.fadeIn = 1.3f;
                            d.noGravity = true;
                        }
                    }
                    var player = Projectile.GetPlayerOwner();
                    if (!player.immune)
                    {
                        var projectile = player.Center.FindClosestProjectile(350, ValidProjectile);
                        if (projectile != null)
                        {
                            if (projectile.Distance(player.Center) < 350f)
                            {
                                bool hostileProjectileMovingTowardsPlayer = ShardsHelpers.MovingTowardPoint(projectile.position, projectile.velocity, player.Center, 400f);
                                if (hostileProjectileMovingTowardsPlayer)
                                {
                                    sleep = false;
                                    Projectile.frame = 4;
                                    Projectile.frameCounter = 0;
                                    Projectile.spriteDirection = projectile.Center.X > Projectile.Center.X ? 1 : -1;
                                    Vector2 velocity = Vector2.Normalize(projectile.Center - Projectile.Center);
                                    Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, velocity,
                                        ModContent.ProjectileType<DiamondShot>(), Projectile.damage, 0, Projectile.owner, projectile.whoAmI);
                                    projectileShootTimer = 0;
                                }
                            }
                        }
                    }
                }
            }

            if (distanceToIdlePosition > 200f)
            {
                speed = 26f;
                inertia = 40f;
            }
            if (distanceToIdlePosition > 400f)
            {
                Projectile.velocity = vectorToIdlePosition * 0.05f;
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
                Projectile.velocity.X = -0.05f;
                Projectile.velocity.Y = -0.01f;
            }
            else if (++sleepyTimer >= 600)
            {
                sleep = true;
                Projectile.velocity.X = 0;
            }
        }

        private bool ValidProjectile(Projectile projectile)
        {
            if (!projectile.hostile) return false;
            if (!projectile.active) return false;
            if (!SoAGlobalProjectile.ReflectAiList.Contains(projectile.aiStyle)) return false;
            if (Main.projPet[projectile.type]) return false;
            return true;
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
            Projectile.frame = 8;
            Projectile.frameCounter = 0;
            Projectile.rotation = 0;
            emoteTimer = 0;
            shootTimer = 0;
            projectileShootTimer = 0;

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
            if (owner.dead || !owner.active || !owner.Gem().gemSoul || owner.ownedProjectileCounts[Type] > 1)
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

            int frameTime = 5;
            int maxframe = 4;
            if (animationState == ANIMATION_PLATFORM)
            {
                maxframe = 7;
            }
            if (animationState == ANIMATION_IDLE)
            {
                maxframe = 1;
            }
            if (sleepyTimer > 400)
            {
                maxframe = 3;
            }
            if (blinkTimer > 0)
            {
                blinkTimer--;
            }
            switch (Projectile.frame)
            {
                case 0:
                    if (Projectile.frameCounter == 0 && ((Main.rand.NextBool(20) && blinkTimer <= 0) || sleepyTimer > 400))
                    {
                        Projectile.frame += 2;
                        maxframe += 2;
                        blinkTimer = 60;
                    }
                    frameTime = 7;
                    break;
                case 1:
                case 2:
                case 3:
                    frameTime = 7;
                    break;
                case 4:
                    frameTime = 30;
                    break;
                case 5:
                case 6:
                    frameTime = 20;
                    break;
                case 7:
                    frameTime = 20;
                    animationState = ANIMATION_IDLE;
                    break;
            }

            if (++Projectile.frameCounter >= frameTime)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame > maxframe)
                {
                    Projectile.frame = 0;
                    if (sleepyTimer > 400)
                    {
                        Projectile.frame = 2;
                    }
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
            CombatText.NewText(Projectile.Hitbox, Color.Blue, "...");
            SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(0.5f), Projectile.Center);
        }
    }
}
