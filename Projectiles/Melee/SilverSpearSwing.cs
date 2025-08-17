using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class SilverSpearSwing : CoolSword
    {
        public override string Texture => ModContent.GetInstance<SilverSpearThrust>().Texture;

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
            base.AI();
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

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            return -170f + ((float)Math.Sin(swingProgress * MathHelper.Pi) + 1f) * 80f;
        }

        public override float GetScale(float progress)
        {
            return base.GetScale(progress);
        }

        public override float SwingProgress(float progress)
        {
            if (progress >= 0.5f) return progress;
            return SwingProgressAequus(progress);
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