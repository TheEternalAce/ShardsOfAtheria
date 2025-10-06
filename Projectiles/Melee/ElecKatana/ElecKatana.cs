using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.ElecKatana
{
    public class ElecKatana : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddDamageType(5);
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 18;
            Projectile.scale = 2f;
            swordReach = 100;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            hitsLeft = 3;
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            combo = 0;
            //if (shards.itemCombo > 0)
            //{
            //    swingDirection *= -1;
            //}
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
            FireProjectile(progress, ModContent.ProjectileType<ElecShuriken>(), Projectile.damage,
                Projectile.knockBack, 32f, 100f);
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
            return SingleEdgeSwordDraw<AreusKatana>(lightColor);
        }
    }
}