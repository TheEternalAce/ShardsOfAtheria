using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar
{
    public class EMAvatar : ModProjectile
    {
        static Asset<Texture2D> ArmTexture;
        static Asset<Texture2D> BladeTexture;
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

        public override void Load()
        {
            if (!Main.dedServ)
            {
                ArmTexture = ModContent.Request<Texture2D>(Texture + "_Arm");
                BladeTexture = ModContent.Request<Texture2D>(Texture + "_ArmBlade");
            }
        }
        public override void Unload()
        {
            ArmTexture = null;
            BladeTexture = null;
        }

        public override void SetStaticDefaults()
        {
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
            Main.projFrames[Type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 28;
            Projectile.height = 78;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 0f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;

            DrawOffsetX = -24;
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
            CombatText.NewText(Projectile.Hitbox, Color.Cyan, "!");
            SoundEngine.PlaySound(SoundID.NPCHit53, Projectile.Center);
            idleFrame = Main.rand.Next(2);
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
            idlePosition.X -= 88f * owner.direction;

            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
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
            var areus = owner.Areus();
            if (owner.HasBuff<ChargingDrones>()) ChargeDrones();
            else chargeTimer = 0;
            if (foundTarget)
            {
                int projectileType = 0;
                float projectileSpeed = 0f;
                int targetDirection = (targetCenter.X > Projectile.Center.X).ToDirectionInt();
                int projectileTimerMax = 60;
                var shootSound = SoundID.Item1;
                int animState = ANIMATION_IDLE;

                if (areus.WarriorSetChip)
                {
                    projectileType = ModContent.ProjectileType<SaberSlash>();
                    projectileSpeed = 22f;
                    projectileTimerMax = 30;
                    shootSound = SoundID.Item71;
                    vectorToIdlePosition = targetCenter + Vector2.UnitX * 100f * -targetDirection - Projectile.Center;
                    animState = ANIMATION_SWING;
                    Projectile.spriteDirection = targetDirection;
                }
                if (areus.RangerSetChip)
                {
                    projectileType = ModContent.ProjectileType<BusterShot>();
                    projectileSpeed = 10;
                    shootSound = SoA.MagnetShot;
                    animState = ANIMATION_SHOOT;
                }
                if (areus.MageSetChip)
                {
                    projectileType = ModContent.ProjectileType<EMRitual>();
                    projectileSpeed = 0;
                    projectileTimerMax = 900;
                    shootSound = SoundID.Item8;
                    animState = ANIMATION_CAST;
                }
                if (areus.CommanderSetChip)
                {
                    SetAnimation(ANIMATION_POINT, 2);
                    Projectile.spriteDirection = targetDirection;
                }
                if (areus.NinjaSetChip)
                {
                    projectileType = ModContent.ProjectileType<PsychicBlade>();
                    projectileSpeed = 12f;
                    shootSound = SoundID.Item1;
                    animState = ANIMATION_SWING;
                    Projectile.spriteDirection = targetDirection;
                }

                if (projectileType > 0)
                {
                    if (++projectileTimer >= projectileTimerMax)
                    {
                        Vector2 velocity = Vector2.Normalize(targetCenter - Projectile.Center) * projectileSpeed;
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velocity, projectileType, Projectile.damage, 0, Projectile.owner);
                        SoundEngine.PlaySound(shootSound, Projectile.Center);

                        SetAnimation(animState, projectileTimerMax * 2 / 3);
                        Projectile.spriteDirection = targetDirection;
                        projectileTimer = 0;
                    }
                }
            }
            else projectileTimer = 0;

            int type = ModContent.ProjectileType<AreusDrone>();
            if (owner.ownedProjectileCounts[type] == 0 && areus.CommanderSetChip)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, type, Projectile.damage, 0);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, type, Projectile.damage, 0);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, type, Projectile.damage, 0);
                owner.ownedProjectileCounts[type] = 3;
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
                Vector2 target = Main.MouseWorld;
                var npc = ShardsHelpers.FindClosestNPC(Projectile.Center, null);
                if (npc != null) target = npc.Center;
                Vector2 velocity = Vector2.Normalize(target - Projectile.Center);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velocity, projectileType, Projectile.damage, 0);

                //Projectile.frame = 4;
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

        const int ANIMATION_IDLE = 0;
        const int ANIMATION_POINT = 2;
        const int ANIMATION_SHOOT = 3;
        const int ANIMATION_SWING = 4;
        const int ANIMATION_CAST = 5;
        int idleFrame = 0;
        int animationState = ANIMATION_IDLE;
        bool animationIdle = true;
        int nonIdleFrameTime;
        int armFrame = -1;
        private void Visuals()
        {
            if (animationIdle) Projectile.frame = idleFrame;
            else Projectile.frame = animationState;
            if (armFrame > -1)
            {
                if (++Projectile.frameCounter > 4)
                {
                    Projectile.frameCounter = 0;
                    if (++armFrame > 4) armFrame = -1;
                }
            }
            if (nonIdleFrameTime > 0) nonIdleFrameTime--;
            else if (nonIdleFrameTime == 0) { animationIdle = true; animationState = ANIMATION_IDLE; }
            if (animationState != ANIMATION_SWING && animationState != ANIMATION_SHOOT)
            {
                var owner = Projectile.GetPlayerOwner();
                int direction = owner.direction;
                Projectile.spriteDirection = direction;
            }
        }
        private void SetAnimation(int animationState, int animationTime = 60)
        {
            this.animationState = animationState;
            nonIdleFrameTime = animationTime;
            animationIdle = false;
            if (animationState == ANIMATION_SWING) armFrame = 0;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (armFrame > -1)
            {
                var owner = Projectile.GetPlayerOwner();
                var areus = owner.Areus();
                Vector2 position = Projectile.Center - Main.screenPosition;
                Vector2 offset = new(12 * Projectile.spriteDirection, -5);
                Rectangle frame = ArmTexture.Frame(1, 5, 0, armFrame);
                SpriteEffects effects = SpriteEffects.None;
                if (Projectile.spriteDirection == -1) effects = SpriteEffects.FlipHorizontally;
                if (areus.WarriorSetChip) Main.EntitySpriteDraw(BladeTexture.Value, position + offset, frame, lightColor, 0f, new Vector2(64, 50), 1f, effects);
                else Main.EntitySpriteDraw(ArmTexture.Value, position + offset, frame, lightColor, 0f, new Vector2(64, 50), 1f, effects);
            }
            return base.PreDraw(ref lightColor);
        }

        public override void OnKill(int timeLeft)
        {
            if (!Projectile.GetPlayerOwner().IsLocal()) return;
            CombatText.NewText(Projectile.Hitbox, Color.Blue, "...");
            SoundEngine.PlaySound(SoundID.Item53.WithPitchOffset(0.5f), Projectile.Center);
        }
    }
}
