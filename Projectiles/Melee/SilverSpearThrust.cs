using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class SilverSpearThrust : CoolSword
    {
        private Vector2 LockedAngleVector = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(11);
            Projectile.AddRedemptionElement(1);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 220;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            hitsLeft = 5;
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo == 0) swingDirection *= -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            var player = Main.player[Projectile.owner];
            Projectile.aiStyle = -1;

            if (Projectile.numUpdates == -1 && freezeFrame > 0)
            {
                freezeFrame--;
                if (freezeFrame != 1)
                {
                    player.itemAnimation++;
                    player.itemTime++;
                }
            }
            if (player.ownedProjectileCounts[Type] > 1 || player.itemTime < 2 || player.dead)
            {
                Projectile.Kill();
                player.ownedProjectileCounts[Type]--;
            }

            var shards = player.Shards();

            player.heldProj = Projectile.whoAmI;
            Init(player, shards);

            if (SwingSwitchDir) UpdateDirection(player);

            if (!player.frozen && !player.stoned)
            {
                var arm = Main.GetPlayerArmPosition(Projectile);
                float progress = AnimProgress;
                lastAnimProgress = progress;
                if (!forced50 && progress >= 0.5f)
                {
                    progress = 0.5f;
                    forced50 = true;
                }
                InterpolateSword(progress, out var angleVector, out float swingProgress, out float scale, out float outer);
                if (forced50)
                {
                    if (LockedAngleVector == Vector2.Zero) LockedAngleVector = angleVector;
                    else angleVector = LockedAngleVector;
                }
                if (freezeFrame <= 0)
                    AngleVector = angleVector;
                Projectile.position = arm + AngleVector * swordReach;
                Projectile.position.X -= Projectile.width / 2f;
                Projectile.position.Y -= Projectile.height / 2f;
                Projectile.rotation = (arm - Projectile.Center).ToRotation() + rotationOffset;
                if (freezeFrame <= 0)
                    UpdateSwing(progress, swingProgress);
                if (Main.netMode != NetmodeID.Server)
                    SetArmRotation(player, progress, swingProgress);
                Projectile.scale = scale;
                visualOutwards = (int)outer;
            }
            if (Main.player[Projectile.owner].itemAnimation <= 1)
                Main.player[Projectile.owner].Shards().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                if (Projectile.ai[1] == 1) SoundEngine.PlaySound(SoA.SilverRings);
                SoundEngine.PlaySound(HeavySwing, Projectile.Center);
            }
        }

        public override void UpdateSwing(float progress, float interpolatedSwingProgress)
        {
            if (progress == 0.5f && Main.myPlayer == Projectile.owner && Projectile.ai[1] == 1f)
                SilverSpear.ShootRings(Projectile, AngleVector);
        }

        public override Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 1.5f) - MathHelper.PiOver2 * 1.5f) * -swingDirection * 1.1f);
        }

        public override float SwingProgress(float progress)
        {
            return SwingProgressAequus(progress);
        }

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            if (progress > 0.8f)
            {
                float p = 1f - (1f - progress) / 0.2f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            if (progress < 0.8f)
            {
                float p = 1f - (1f - progress) / 0.15f;
                Projectile.alpha = (int)(p * 255);
                return 20f * p;
            }
            return 0f;
        }

        public override float GetScale(float progress)
        {
            return base.GetScale(progress);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.type == NPCID.Werewolf) modifiers.ScalingBonusDamage += 1f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return SingleEdgeSwordDraw(lightColor);
        }
    }
}