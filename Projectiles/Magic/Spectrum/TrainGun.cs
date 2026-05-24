using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Spectrum
{
    public class TrainGun : ChargeGun
    {
        public override string Texture => "ShardsOfAtheria/Items/DedicatedItems/MrGerd26/TrainSpectrum";

        public override int LingerDuration => 10;
        public override float ChargeLevelTime => chargeLevel == 0 ? 20 - 20 * AttackSpeed : 15 - 15 * AttackSpeed;
        public override int MaxCharge => -1;
        public override float BaseShootSpeed => 16f;

        public override SoundStyle ChargeLevelUpSound => SoundID.Item61;
        public override SoundStyle ShootSound => SoundID.Item34;

        float AttackSpeed => (Owner.GetTotalAttackSpeed(DamageClass.Magic) - 1) * 0.6f;

        int ManaCost => (int)(20 * Owner.manaCost);

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 42;
            base.SetDefaults();
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
            velocity = aimNormal * speed;
            type = ModContent.ProjectileType<SpectrumTrain>();
            damage = Projectile.originalDamage;
            knockback = 6f;
            recoil = Main.rand.NextFloat(0.5f, 0.8f);
            recoil -= recoil * AttackSpeed;
            return charging;
        }

        internal override void Fire(Vector2 position, Vector2 velocity, int type, int damage, float knockback, float recoil) //method to fire regular projectile
        {
            //increase recoil value, make gun appear like it's actually firing with some force
            recoilAmount += recoil;
            SoundEngine.PlaySound(ShootSound, Projectile.Center);
            charging = false;

            var projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), position, velocity, type, damage, 6, Projectile.owner);
            if (projectile.ModProjectile is SpectrumTrain train)
            {
                train.segments = chargeLevel;
            }
        }

        internal override Vector2 HoldOffset()
        {
            return new(-10f, 0f);
        }
    }
}
