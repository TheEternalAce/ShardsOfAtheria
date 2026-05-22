using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Melee.AreusGlaive
{
    public class AreusGlaive_Swing : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddDamageType(11);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.scale = 1.2f;
            Projectile.width = Projectile.height = 30;
            swordReach = 158;
            rotationOffset = -MathHelper.PiOver4 * 3f;
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

        public override float SwingProgress(float progress)
        {
            return SwingProgressAequus(progress);
        }

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            return -170f + ((float)Math.Sin(swingProgress * MathHelper.Pi) + 1f) * 80f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            base.PreDraw(ref lightColor);
            return SingleEdgeSwordDraw(lightColor);
        }
    }
}