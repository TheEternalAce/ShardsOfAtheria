using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Spectrum
{
    public class GerdGun : ChargeGun
    {
        public override string Texture => "ShardsOfAtheria/Items/DedicatedItems/MrGerd26/AlphaSpectrum";
        float AttackSpeed => (Owner.GetTotalAttackSpeed(DamageClass.Magic) - 1) * 0.6f;

        public override int LingerDuration => chargeLevel * (5 - (int)(5 * AttackSpeed)) + 14;
        public override float ChargeLevelTime => chargeLevel == 0 ? 20 - 20 * AttackSpeed : 15 - 15 * AttackSpeed;
        public override int MaxCharge => -1;

        public override float BaseShootSpeed => 16f;

        public override SoundStyle ChargeLevelUpSound => SoundID.Item61;
        public override SoundStyle ShootSound => SoundID.Item12;

        int laserNum = 0;

        int ManaCost => (int)(20 * Owner.manaCost);

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 100;
            Projectile.height = 42;
        }

        public override void AI()
        {
            base.AI();
            if (BeingHeld)
            {
                Owner.manaRegenDelay = 10;
                Owner.manaRegen = 0;
            }
        }

        internal override bool CanCharge()
        {
            if (Owner.statMana < ManaCost)
            {
                if (Owner.manaFlower) Owner.QuickMana();
                return false;
            }
            return base.CanCharge();
        }

        internal override void OnChargeIncrement()
        {
            Owner.statMana -= ManaCost;
            CombatText.NewText(Owner.getRect(), Color.Cyan, chargeLevel + 1);
        }

        internal override bool GetFireStats(float speed, out Vector2 position, out Vector2 velocity, out int type, out int damage, out float knockback, out float recoil)
        {
            position = Projectile.Center + aimNormal * 40;
            if (!Collision.CanHit(position, 0, 0, Owner.Center, 0, 0)) position = Owner.Center;
            velocity = aimNormal * speed;
            type = ModContent.ProjectileType<SpectrumLaser>();
            damage = Projectile.originalDamage;
            knockback = 6f;
            recoil = Main.rand.NextFloat(0.5f, 0.8f);
            recoil -= recoil * AttackSpeed;
            return Projectile.timeLeft % (int)(5 - 5 * AttackSpeed) == 0 && Projectile.timeLeft >= 10;
        }

        internal override void Fire(Vector2 position, Vector2 velocity, int type, int damage, float knockback, float recoil)
        {
            recoilAmount += recoil;
            SoundEngine.PlaySound(ShootSound, Projectile.Center);
            charging = false;

            float numberProjectiles = 3; // 3 shots
            float rotation = MathHelper.ToRadians(10);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation,
                    i / (numberProjectiles - 1)));
                Projectile projectile = Projectile.NewProjectileDirect(
                    Projectile.GetSource_FromThis(), position, perturbedSpeed,
                    ModContent.ProjectileType<SpectrumLaser>(), damage, 6, Projectile.owner);
                if (projectile.ModProjectile is SpectrumLaser laser)
                {
                    laser.laserColor = GetLaserColor();
                }
            }
            if (++laserNum > 5)
            {
                laserNum = 0;
            }
        }

        internal override Vector2 HoldOffset()
        {
            return new(-10f, 0f);
        }

        Color GetLaserColor()
        {
            Color color = laserNum switch
            {
                5 => Color.Purple,
                4 => Color.Blue,
                3 => Color.Green,
                2 => Color.Yellow,
                1 => Color.Orange,
                0 => Color.Red,
                _ => Color.White,
            };
            return color;
        }
    }
}
