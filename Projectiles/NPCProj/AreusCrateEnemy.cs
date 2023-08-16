using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj
{
    public class AreusCrateEnemy : ModProjectile
    {
        int gravityTimer = 0;

        public override string Texture => "ShardsOfAtheria/Projectiles/Minions/Sentry/AreusCrateProj";

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            gravityTimer++;
            if (gravityTimer >= 16)
            {
                if (++Projectile.velocity.Y > 16)
                {
                    Projectile.velocity.Y = 16;
                }
                gravityTimer = 16;
            }
        }
    }
}
