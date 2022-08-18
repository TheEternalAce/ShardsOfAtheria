using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class AmbassadorShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 15;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;

            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
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
    }
}