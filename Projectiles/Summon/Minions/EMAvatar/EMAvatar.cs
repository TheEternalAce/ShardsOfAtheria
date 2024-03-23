using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar
{
    public class EMAvatar : ModProjectile
    {
        int projectileTimer = 0;
        int chargeTimer = 0;

        //int emoteTimer = 0;
        //readonly string[] randomEmotes = [":3", ":D", "X3", "XD", ":)", ">:3", ">:)", ":o"];

        //int blinkTimer = 0;
        //int animationState = ANIMATION_IDLE;
        //const int ANIMATION_IDLE = 0;
        //const int ANIMATION_SWING = 1;
        //const int ANIMATION_SHOOT = 2;
        //const int ANIMATION_CAST = 3;
        //const int ANIMATION_COMMAND = 4;

        public override void SetStaticDefaults()
        {
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 38;
            Projectile.height = 70;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 0f;
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

        //public override void OnSpawn(IEntitySource source)
        //{
        //    CombatText.NewText(Projectile.Hitbox, Color.Blue, "<3");
        //    SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(1.5f), Projectile.Center);
        //}

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
            idlePosition.X -= 88f * owner.direction;

            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            int direction = owner.direction;
            if (idlePosition.X > owner.Center.X && Projectile.Center.X < owner.Center.X ||
                idlePosition.X < owner.Center.X && Projectile.Center.X > owner.Center.X)
            {
                direction *= -1;
            }
            if (Projectile.frame != 4)
            {
                Projectile.spriteDirection = direction;
            }

            int torch = TorchID.Green;
            Lighting.AddLight(Projectile.Center, torch);
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

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            var owner = Projectile.GetPlayerOwner();
            if (owner.HasBuff<ChargingDrones>()) ChargeDrones();
            else chargeTimer = 0;
            if (foundTarget)
            {
                int projectileType = 0;
                float projectileSpeed = 0f;
                var areus = owner.Areus();
                int targetDirection = (targetCenter.X < Projectile.Center.X).ToDirectionInt();
                int projectileTimerMax = 60;
                var shootSound = SoundID.Item1;

                if (areus.WarriorSet)
                {
                    projectileType = ModContent.ProjectileType<SaberSlash>();
                    projectileSpeed = 22f;
                    projectileTimerMax = 30;
                    shootSound = SoundID.Item71;
                    vectorToIdlePosition = targetCenter + Vector2.UnitX * 100f * targetDirection - Projectile.Center;
                }
                if (areus.RangerSet)
                {
                    projectileType = ModContent.ProjectileType<BusterShot>();
                    projectileSpeed = 10;
                    shootSound = SoA.MagnetShot;
                }
                if (areus.MageSet)
                {
                    projectileType = ModContent.ProjectileType<EMRitual>();
                    projectileSpeed = 0;
                    projectileTimerMax = 900;
                    shootSound = SoundID.Item8;
                }

                if (++projectileTimer >= projectileTimerMax)
                {
                    if (projectileType > 0)
                    {
                        Vector2 velocity = Vector2.Normalize(targetCenter - Projectile.Center) * projectileSpeed;
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velocity, projectileType, Projectile.damage, 0, Projectile.owner);
                        SoundEngine.PlaySound(shootSound, Projectile.Center);

                        //Projectile.frame = 4;
                        //Projectile.frameCounter = 0;
                        Projectile.spriteDirection = targetDirection;
                    }
                    projectileTimer = 0;
                }
            }
            else projectileTimer = 0;

            int type = ModContent.ProjectileType<AreusDrone>();
            if (owner.ownedProjectileCounts[type] == 0 && owner.Areus().CommanderSet)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, type, Projectile.damage, 0);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, type, Projectile.damage, 0);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, type, Projectile.damage, 0);
            }

            Projectile.velocity = vectorToIdlePosition * 0.055f;
        }

        private void ChargeDrones()
        {
            projectileTimer = 0;
            if (++chargeTimer >= 120)
            {
                int projectileType = ModContent.ProjectileType<ChargeLaser>();
                int targetDirection = (Main.mouseX < Projectile.Center.X).ToDirectionInt();
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - Projectile.Center);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velocity, projectileType, Projectile.damage, 0);

                //Projectile.frame = 4;
                //Projectile.frameCounter = 0;
                Projectile.spriteDirection = targetDirection;
                chargeTimer = 0;
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.Areus().royalSet)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }

        private void Visuals()
        {
            //int frameTime = 5;
            //int maxframe = 4;
            //if (animationState == ANIMATION_PLATFORM)
            //{
            //    maxframe = 7;
            //}
            //if (animationState == ANIMATION_IDLE)
            //{
            //    maxframe = 1;
            //}
            //switch (Projectile.frame)
            //{
            //    case 0:
            //        frameTime = 7;
            //        break;
            //    case 1:
            //    case 2:
            //    case 3:
            //        frameTime = 7;
            //        break;
            //    case 4:
            //        frameTime = 30;
            //        break;
            //    case 5:
            //    case 6:
            //        frameTime = 20;
            //        break;
            //    case 7:
            //        frameTime = 20;
            //        animationState = ANIMATION_IDLE;
            //        break;
            //}

            //if (++Projectile.frameCounter >= frameTime)
            //{
            //    Projectile.frameCounter = 0;
            //    Projectile.frame++;

            //    if (Projectile.frame > maxframe)
            //    {
            //        Projectile.frame = 0;
            //    }
            //}
        }

        //public override void OnKill(int timeLeft)
        //{
        //    CombatText.NewText(Projectile.Hitbox, Color.Blue, "...");
        //    SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(0.5f), Projectile.Center);
        //}
    }
}
