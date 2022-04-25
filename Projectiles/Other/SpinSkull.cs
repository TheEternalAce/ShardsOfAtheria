using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.NPCProj;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class SpinSkull : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spin Skull");
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 102;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.rotation -= .4f;
            Projectile.Center = owner.Center;
        }
    }
}