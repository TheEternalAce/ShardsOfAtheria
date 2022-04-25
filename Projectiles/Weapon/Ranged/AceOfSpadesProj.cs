using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class AceOfSpadesProj : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 30;
            Projectile.height = 40;
            Projectile.scale = .5f;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
    }
}
