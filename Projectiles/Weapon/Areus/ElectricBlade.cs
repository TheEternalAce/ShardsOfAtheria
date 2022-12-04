using ShardsOfAtheria.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus
{
    public class ElectricBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.ElectricProj.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.scale = 1.5f;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.arrow = false;
            Projectile.light = 1;
        }

        public override void AI()
        {
            Projectile.rotation += 0.4f * Projectile.direction;
            if (Main.rand.NextBool(20))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            }
        }
    }
}
