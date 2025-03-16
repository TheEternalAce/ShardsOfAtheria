using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using WebCom.Effects.ScreenShaking;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class Warframe : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(11);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 130;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 4;
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo > 0) swingDirection *= -1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            ScreenShake.ShakeScreen(6, 60);
            var player = Main.player[Projectile.owner];
            var anchorChip = ToggleableTool.GetInstance<AnchorChip>(player);
            if (anchorChip == null || !anchorChip.Active)
            {
                var vector = player.Center - target.Center;
                vector.Normalize();
                player.velocity = vector * Projectile.knockBack;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            base.AI();
            if (Main.player[Projectile.owner].itemAnimation <= 1) Main.player[Projectile.owner].Shards().itemCombo = (ushort)(combo == 0 ? 20 : 0);
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
            return GenericSwordDraw(lightColor);
        }
    }
}