using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.ElecKatana
{
    public class ElecKunaiHoming : ElecKunai
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/Weapon/Melee/ElecKatana/ElecKunai";

        NPC target;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

            if (target == null)
            {
                target = Projectile.FindClosestNPC(-1);
                return;
            }
            Projectile.Track(target, 1000);

            if (!target.active || target.life <= 0)
            {
                Projectile.Kill();
            }

            if (Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, 0, 0, 200, Scale: 1f);
                dust.noGravity = true;
            }
        }
    }
}
