using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Magic.ThorSpear;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.BalanceSwords
{
    public class ShadowInLight : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 50;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 5;
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo > 0)
            {
                swingDirection *= -1;
            }
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
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 1.5f) - MathHelper.PiOver2 * 1.5f) * -swingDirection * 1.1f);
        }

        public override void UpdateSwing(float progress, float interpolatedSwingProgress)
        {
            if (progress > 0.85f)
            {
                Projectile.Opacity = 1f - (progress - 0.85f) / 0.15f;
            }

            Projectile.oldPos[0] = AngleVector * 60f * Projectile.scale;
            Projectile.oldRot[0] = Projectile.oldPos[0].ToRotation() + MathHelper.PiOver4;

            // Manually updating oldPos and oldRot 
            for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
            {
                Projectile.oldPos[i] = Projectile.oldPos[i - 1];
                Projectile.oldRot[i] = Projectile.oldRot[i - 1];
            }

            if (progress == 0.5f && Main.myPlayer == Projectile.owner) FireProjectile(progress, ModContent.ProjectileType<ShadowWave>(), (int)(Projectile.damage * 0.8f), 4f, 48f, 0, false);
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
            DrawSwish();
            return SingleEdgeSwordDraw<DarknessWithinLight>(lightColor);
        }
    }
}