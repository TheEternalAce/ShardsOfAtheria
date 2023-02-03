using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Buffs.PlayerBuff;
using MMZeroElements;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Bases;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.EnergyScythe
{
    public class EnergyScythe : EpicSwingSword
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 60;
            Projectile.scale = 1.4f;
            hitboxOutwards = 60;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 3;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                target.AddBuff(BuffID.CursedInferno, 10 * 60);
                player.AddBuff(BuffID.Ichor, 10 * 60);
            }
            target.AddBuff(BuffID.OnFire3, 10 * 60);
            player.AddBuff(BuffID.WeaponImbueIchor, 10 * 60);
            base.OnHitNPC(target, damage, knockback, crit);
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
            if (progress == 0.5f && Main.myPlayer == Projectile.owner)
            {
                FireProjectile(ModContent.ProjectileType<EnergyWave>(), (int)(Projectile.damage * 0.75), (int)(Projectile.knockBack * 0.75));
            }
            base.UpdateSwing(progress, interpolatedSwingProgress);
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
                float p = 1f - progress / 0.35f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            return 0f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
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
                Main.instance.LoadItem(ModContent.ItemType<Items.Weapons.Prometheus>());
                texture = TextureAssets.Item[ModContent.ItemType<Items.Weapons.Prometheus>()].Value;
            }
            var origin = new Vector2(0f, texture.Height);

            Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, effects, 0);

            if (AnimProgress > 0.35f && AnimProgress < 0.75f)
            {
                float intensity = (float)Math.Sin((AnimProgress - 0.35f) / 0.4f * MathHelper.Pi);
                Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor.UseA(0) * intensity * 0.5f, Projectile.rotation, origin, Projectile.scale, effects, 0);

                var swish = SwishTexture.Value;
                var swishOrigin = swish.Size() / 2f;
                var swishColor = new Color(100, 120, 140, 80) * intensity * intensity * Projectile.Opacity * 0.5f;
                float r = BaseAngleVector.ToRotation() + ((AnimProgress - 0.45f) / 0.2f * 2f - 1f) * -swingDirection * 0.6f;
                var swishLocation = Main.player[Projectile.owner].Center - Main.screenPosition + r.ToRotationVector2() * (size - 20f) * scale;
                Main.EntitySpriteDraw(swish, swishLocation, null, swishColor.UseA(0), r + MathHelper.PiOver2, swishOrigin, 1f, effects, 0);
            }
            return false;
        }
    }
}