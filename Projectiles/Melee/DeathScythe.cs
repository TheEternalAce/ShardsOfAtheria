using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class DeathScythe : CoolSword
    {
        float charge = 0f;

        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.Eraser.Add(Type);
            Projectile.AddDamageType(6);
            Projectile.AddElement(1, 3);
            Projectile.AddRedemptionElement(1, 12);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 50;
            swordReach = 250;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            hitsLeft = -1;
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            //if (shards.itemCombo > 0) swingDirection *= -1;
            shards.itemCombo = 1;
            swingDirection = -player.direction;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            base.AI();
            if (Owner.itemAnimation <= 1)
                Owner.Shards().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            if (BeingHeld && !Owner.controlUseTile && charge < 0.8f)
            {
                Projectile.timeLeft = 3600;
                if (AnimProgress < 0.35f)
                {
                    Owner.itemAnimation--;
                    Owner.itemTime--;
                }
                else if (charge < 0.8f)
                {
                    float chargeIncrement = 0.0036f * (Owner.GetWeaponAttackSpeed(Owner.HeldItem));
                    charge += chargeIncrement;
                    scale += chargeIncrement;
                }
                AngleVector = GetOffsetVector(0);
                Projectile.velocity = Owner.Center.DirectionTo(Main.MouseWorld);
                freezeFrame = 3;
                int direction = Main.MouseWorld.X >= Owner.Center.X ? 1 : -1;
                Owner.direction = direction;
                swingDirection = Projectile.direction = -direction;
            }
            else if (freezeFrame > 0 && AnimProgress >= 0.35f)
            {
                Owner.itemAnimation = Owner.itemAnimationMax;
                Owner.itemTime = Owner.itemTimeMax;
            }
            else if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item71.WithPitchOffset(-1f), Projectile.Center);
            }
        }

        public override Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 1.8f) - MathHelper.PiOver2 * 1.5f) * -swingDirection * 1.1f);
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
            if (progress < 0.35f)
            {
                float p = 1f - progress / 0.35f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            return 0f;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            var center = Owner.Center;
            return ShardsHelpers.DeathrayHitbox(center, center + AngleVector * (swordReach * Projectile.scale), targetHitbox, swordSize * Projectile.scale * scale);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.life < target.lifeMax / 50) modifiers.ScalingBonusDamage += 0.8f;
            modifiers.ScalingBonusDamage += charge * 2.5f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff<DeathBleed>(1200);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return SingleEdgeSwordDraw(lightColor);
        }
    }
}