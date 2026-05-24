using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Ranged.FireCannon
{
    public class FlameCannon : ChargeGun
    {
        public override int LingerDuration => 10;
        public override float ChargeLevelTime => chargeLevel == 0 ? 20 : 60;
        public override SoundStyle ChargeLevelUpSound => SoundID.DD2_BetsyFireballImpact;

        public override float BaseShootSpeed => 20f;

        public override SoundStyle ShootSound => SoundID.Item38;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 56;
            base.SetDefaults();
        }

        internal override bool GetFireStats(float speed, out Vector2 position, out Vector2 velocity, out int type, out int damage, out float knockback, out float recoil)
        {
            bool shoot = base.GetFireStats(speed, out position, out velocity, out type, out damage, out knockback, out recoil);
            type = ModContent.ProjectileType<FireCannon_Fire1>();
            if (chargeLevel >= 2)
            {
                recoil += 2f;
                type = ModContent.ProjectileType<FireCannon_Fire2>();
            }
            if (chargeLevel == 3)
            {
                recoil += 3;
                damage *= 2;
                velocity *= 0.5f;
                type = ModContent.ProjectileType<FireCannon_Fire3>();
            }
            return shoot && chargeLevel > 0;
        }

        override internal void Fire(Vector2 position, Vector2 velocity, int type, int damage, float knockback, float recoil) //method to fire regular projectile
        {
            recoilAmount = recoil;
            SoundEngine.PlaySound(ShootSound, Projectile.Center);
            charging = false;
            var source = Projectile.GetSource_FromThis();
            if (chargeLevel == 3)
            {
                ScreenShake.ShakeScreen(12, 60);
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Owner.whoAmI);
                return;
            }
            float numberProjectiles = 2; // 2 shots

            float piOver10 = (float)Math.PI / 10f;
            float offsetX = Main.mouseX + Main.screenPosition.X - position.X;
            float offsetY = Main.mouseY + Main.screenPosition.Y - position.Y;
            Vector2 baseShootOffset = new(offsetX, offsetY);
            baseShootOffset.Normalize();
            baseShootOffset *= 50f;
            bool flag3 = Collision.CanHit(position, 0, 0, position + baseShootOffset, 0, 0);
            for (int i = 0; i < numberProjectiles; i++)
            {
                float num120 = i - (numberProjectiles - 1f) / 2f;
                Vector2 shootOffset = baseShootOffset.RotatedBy(piOver10 * num120);
                if (!flag3)
                {
                    shootOffset -= baseShootOffset;
                }
                Projectile.NewProjectile(source, position + shootOffset, velocity, type, damage, knockback, Owner.whoAmI);
            }
        }

        override internal void UpdateVisual()
        {
            Projectile.frame = chargeLevel;
        }

        internal override Vector2 HoldOffset()
        {
            return new(-25, 0);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void SendExtraAI(BinaryWriter writer) //important because mouse cursor logic is really unstable in multiplayer if done wrong
        {
            writer.WriteVector2(aimNormal);
        }
    }
}
