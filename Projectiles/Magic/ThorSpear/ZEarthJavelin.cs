using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ThorSpear
{
    public class ZEarthJavelin : EarthJavelin
    {
        public override string Texture => ModContent.GetInstance<ZEarthmoverSpear>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(5);
            Projectile.AddRedemptionElement(7);
        }

        public override void OnSpawn(IEntitySource source)
        {
            minChargeRequired = maxCharge;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            float attackSpeed = player.GetTotalAttackSpeed(DamageClass.Magic);

            if ((BeingHeld || Timer < minChargeRequired + (player.Overdrive() ? 40 : 0))) Timer += 0.25f * attackSpeed * (player.Overdrive() ? 1.2f : 1f);

            base.AI();

            if (Timer >= minChargeRequired) Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            if (Timer < minChargeRequired) return;
            int type = ModContent.ProjectileType<ZEarthmoverBeam>();
            var player = Projectile.GetPlayerOwner();
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 16f, type, Projectile.damage, Projectile.knockBack);
            SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
        }
    }
}
