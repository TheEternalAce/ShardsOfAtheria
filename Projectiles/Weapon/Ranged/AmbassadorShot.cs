using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class AmbassadorShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 15;
            SoAGlobalProjectile.AreusProj.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 78;

            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;

            DrawOffsetX = -28;
            DrawOriginOffsetY = -30;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter == 2)
            {
                if (++Projectile.frame == Main.projFrames[Projectile.type])
                {
                    Projectile.Kill();
                }
                Projectile.frameCounter = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}