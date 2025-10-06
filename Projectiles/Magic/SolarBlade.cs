using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class SolarBlade : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(10);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Magic;

            Projectile.width = Projectile.height = 30;
            swordReach = 230;
            rotationOffset = -MathHelper.PiOver4 * 3f;
        }

        public override bool? CanDamage()
        {
            return AnimProgress > 0.4f && AnimProgress < 0.8f && freezeFrame <= 0 ? null : false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Main.dayTime)
            {
                modifiers.ScalingBonusDamage += 0.5f;
            }
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            combo = 1;
            swingDirection *= -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            base.AI();
            if (Main.player[Projectile.owner].itemAnimation <= 1)
            {
                Main.player[Projectile.owner].Shards().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            }
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-1f), Projectile.Center);
            }
        }

        public override Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 0.7f) - MathHelper.PiOver2 * 1.1f) * -swingDirection * 1.1f);
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

        public override bool PreDraw(ref Color lightColor)
        {
            base.PreDraw(ref lightColor);
            return GenericSwordDraw(Color.Green.UseA(0));
        }
    }
}