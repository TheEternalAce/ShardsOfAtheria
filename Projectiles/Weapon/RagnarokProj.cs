using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon
{
    public class RagnarokProj : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Melee/Ragnarok";

        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 70;

            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
            Projectile.light = .4f;
        }
    }
}
