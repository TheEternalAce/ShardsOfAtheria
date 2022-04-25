using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj
{
    public class ElectricTrail : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Trail");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric,
                       Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }
    }
}