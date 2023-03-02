using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Minions
{
    public class SapphireSpirit : ModProjectile
    {
        int shootTimer = 0;
        int idleTimer = 0;
        bool sleep = false;
        bool grounded = false;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = false; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
            ProjectileElements.Metal.Add(Type);
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 22;
            Projectile.height = 38;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 0f;
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
            if (owner.ShardsOfAtheria().superSapphireCore)
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
                Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
            }
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.X += 48f * owner.direction; // Go up 48 coordinates (three tiles from the center of the player)
            idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

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
            else
            {
                sleep = false;
                idleTimer = 0;
                Projectile.frame = 0;
                Projectile.frameCounter = 0;
            }
        }

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            // Default movement parameters (here for attacking)
            float speed = 16f;
            float inertia = 20f;

            if (foundTarget)
            {
                Projectile.hostile = false;
                speed = 4f;
                inertia = 80f;
                Vector2 newIdlePosition = targetCenter - Projectile.Center;
                newIdlePosition.Y -= 124f;

                newIdlePosition.Normalize();
                newIdlePosition *= speed;

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + newIdlePosition) / inertia;

                if (Projectile.damage == 0)
                {
                    return;
                }
                if (++shootTimer >= 120)
                {
                    Projectile.frame = 2;
                    Projectile.frameCounter = 0;
                    Projectile.damage = 50;
                    Projectile.spriteDirection = targetCenter.X < Projectile.Center.X ? -1 : 1;
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
                // Minion doesn't have a target: return to player and idle
                if (distanceToIdlePosition > 600f)
                {
                    // Speed up the minion if it's away from the player
                    speed = 12f;
                    inertia = 60f;
                }
                else
                {
                    // Slow down the minion if closer to the player
                    speed = 4f;
                    inertia = 80f;
                }

                sleep = false;
                if (distanceToIdlePosition > 20f)
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
                else if (++idleTimer >= 600)
                {
                    sleep = true;
                    Projectile.velocity.X = 0;
                }
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
                idleTimer = 0;
                grounded = false;
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.ShardsOfAtheria().sapphireSpirit)
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

            if (idleTimer >= 400)
            {
                Projectile.frame = 1;
                return;
            }

            // This is a simple "loop through all frames from top to bottom" animation
            int frameTime = 5;
            if (Projectile.frame == 0)
            {
                frameTime = 120;
            }
            if (Projectile.frame == 1)
            {
                frameTime = 60;
            }
            if (Projectile.frame == 2)
            {
                frameTime = 20;
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
                SoundEngine.PlaySound(SoundID.Item53);
                grounded = true;
            }
            return false;
        }
    }
}
