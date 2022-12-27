﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals.Elements;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Bases;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Throwing
{
    public class HardlightKnife_Swing : EpicSwingSword
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.MetalProj.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 20;
            Projectile.DamageType = DamageClass.Throwing;
            hitboxOutwards = 25;
            rotationOffset = -MathHelper.PiOver4 * 3f;
        }

        protected override void Initialize(Player player, SoAPlayer shards)
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
                Main.player[Projectile.owner].ShardsOfAtheria().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            }
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-1f), Projectile.Center);
            }
        }

        public override void UpdateSwing(float progress, float interpolatedSwingProgress)
        {
            if (AnimProgress == 0.5f && Main.myPlayer == Projectile.owner)
            {
                Vector2 velocity = AngleVector * Projectile.velocity.Length() * 16;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<HardlightKnifeProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }

        public override float SwingProgress(float progress)
        {
            return GenericSwing2(progress);
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
                float p = 1f - (progress) / 0.35f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            return 0f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (AnimProgress > 0.5f)
            {
                return false;
            }

            var texture = TextureAssets.Projectile[Type].Value;
            var center = Main.player[Projectile.owner].Center;
            var handPosition = Main.GetPlayerArmPosition(Projectile) + AngleVector * visualOutwards;
            var drawColor = Projectile.GetAlpha(lightColor) * Projectile.Opacity;
            var drawCoords = handPosition - Main.screenPosition;
            float size = texture.Size().Length();
            var effects = SpriteEffects.None;
            bool flip = Main.player[Projectile.owner].direction == 1 ? combo > 0 : combo == 0;
            if (flip)
            {
                texture = TextureAssets.Projectile[ModContent.ProjectileType<HardlightKnife_Swing>()].Value;
            }
            var origin = new Vector2(0f, texture.Height);

            Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, effects, 0);
            return false;
        }
    }
}