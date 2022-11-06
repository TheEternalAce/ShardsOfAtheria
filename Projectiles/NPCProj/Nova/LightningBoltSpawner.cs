using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class LightningBoltSpawner : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Blank";

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
        }

        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, 700, DustID.Electric);
            dust.noGravity = true;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, 1) * 50, ModContent.ProjectileType<LightningBolt>(), 16, Projectile.knockBack, Main.myPlayer);
        }
    }
}