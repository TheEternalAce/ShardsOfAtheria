using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Magic.ThorSpear
{
    public class ZEarthJavelin : EarthJavelin
    {
        public override string Texture => ModContent.GetInstance<ZEarthmoverSpear>().Texture;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.extraUpdates += 1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            minCharge = maxCharge;
        }

        public override void AI()
        {
            base.AI();
            if (charge >= minCharge) Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            if (charge < minCharge) return;
            int type = ModContent.ProjectileType<ZEarthmoverBeam>();
            var player = Projectile.GetPlayerOwner();
            if (!Projectile.GetPlayerOwner().IsLocal()) return;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 16f, type, Projectile.damage, Projectile.knockBack);
            SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
        }
    }
}
