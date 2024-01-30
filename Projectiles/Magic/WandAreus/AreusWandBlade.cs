﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Magic.WandAreus
{
    public class AreusWandBlade : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 150;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 3;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 300);
            base.OnHitNPC(target, hit, damageDone);
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
            return GenericSwing2(progress);
        }

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            return 0f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return SingleEdgeSwordDraw(lightColor);
        }
    }
}