using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class CoilgunProj : ChargeGun
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Ranged/Coilgun";
        public override string FlashPath => "ShardsOfAtheria/Projectiles/Ranged/CoilgunProj_Flash";

        public override int LingerDuration => Math.Max(15, 60 - totalChargeTime / (chargeLevel + 1));
        public override float ChargeLevelTime => 60 * (chargeLevel * 0.1f + 1);

        public override float BaseShootSpeed => 12f;

        public override SoundStyle ShootSound => chargeLevel > 1 ? SoA.MagnetShot : SoA.MagnetWeakShot;

        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 20;
            base.SetDefaults();
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            SoundEngine.PlaySound(SoA.MagnetChargeUp);
        }

        internal override bool GetFireStats(float speed, out Vector2 position, out Vector2 velocity, out int type, out int damage, out float knockback, out float recoil)
        {
            if (chargeLevel >= 1) speed += 4;
            if (chargeLevel > 1) speed *= 1.5f;
            bool shoot = base.GetFireStats(speed, out position, out velocity, out type, out damage, out knockback, out recoil);
            recoil = 0;
            if (chargeLevel > 0) recoil += 2f;
            if (chargeLevel == 3) recoil += 3f;
            return shoot;
        }

        internal override void Fire(Vector2 position, Vector2 velocity, int type, int damage, float knockback, float recoil)
        {
            SoundEngine.StopTrackedSounds();
            base.Fire(position, velocity, type, damage, knockback, recoil);
        }

        internal override Vector2 HoldOffset()
        {
            return new(-25, 0);
        }
    }
}
