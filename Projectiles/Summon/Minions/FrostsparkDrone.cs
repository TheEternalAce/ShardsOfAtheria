using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Projectiles.Summon.Active;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions
{
    public class FrostsparkDrone : ModProjectile
    {
        public bool ElectricMode => Projectile.ai[0] == 1f;
        int ShootTimer
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
            // This is necessary for right-click targeting
            //ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 16;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;

            DrawOffsetX = -9;
            DrawOriginOffsetY = -9;
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

        public override bool PreAI()
        {
            Projectile.minionSlots = 1f + Projectile.GetPlayerOwner().Shards().frostsparkDronesTier;
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

            Visuals();
            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

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
                        float distance = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > distance;
                        bool inRange = distance < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        bool closeThroughWall = distance < 200f;

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
            float speed = 4f;
            float inertia = 60f;

            if (foundTarget)
            {
                Projectile.spriteDirection = targetCenter.X > Projectile.Center.X ? 1 : -1;
                speed = 8f;
                inertia = 80f;
                float shootSpeed = 2f;
                int projType = ModContent.ProjectileType<FrostsparkBeam>();
                if (Projectile.ai[1] == 0)
                {
                    foundTargetIdlePos = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 150;
                    Projectile.ai[1] = 1;
                }
                Vector2 idlePos = targetCenter + foundTargetIdlePos;
                vectorToIdlePosition = idlePos - Projectile.Center;

                if (++ShootTimer >= 120 + Main.rand.Next(60))
                {
                    Vector2 vel = Vector2.Normalize(targetCenter - Projectile.Center) * shootSpeed;
                    int damage = (int)(Projectile.originalDamage * (1 + player.Shards().frostsparkDronesTier));
                    var projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, vel, projType, damage, 0, Projectile.owner, 0f, 1f);
                    projectile.tileCollide = false;
                    Projectile.ai[1] = 0;
                    ShootTimer = 0;
                }
            }
            else
            {
                if (Projectile.ai[1] == 0) foundTargetIdlePos = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 100;
                Vector2 idlePos = player.Center + foundTargetIdlePos;
                vectorToIdlePosition = idlePos - Projectile.Center;
                if (++Projectile.ai[1] >= 60) Projectile.ai[1] = 0;
            }

            if (distanceToIdlePosition > 400f)
            {
                speed = 26f;
                inertia = 40f;
            }

            if (distanceToIdlePosition > 4000f)
            {
                Projectile.Center = player.Center;
                Projectile.netUpdate = true;
            }
            else if (distanceToIdlePosition > 20f)
            {
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

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.HasBuff<FrostsparkDroneBuff>() || owner.ownedProjectileCounts[Type] < 2)
            {
                owner.Shards().frostsparkDronesTier = 0;
                return false;
            }
            else Projectile.timeLeft = 2;
            return true;
        }

        private void Visuals()
        {
            // So it will lean slightly towards the direction it's moving
            Projectile.rotation = Projectile.velocity.X * 0.05f;

            if (Projectile.spriteDirection == -1) DrawOffsetX = -2;
            else DrawOffsetX = -9;
            Projectile.spriteDirection = Projectile.direction;

            if (ElectricMode) Projectile.frame = 1;
            else Projectile.frame = 0;
        }
    }
}
