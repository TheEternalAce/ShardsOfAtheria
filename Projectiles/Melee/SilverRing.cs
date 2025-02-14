using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class SilverRing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddRedemptionElement(1);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = ProjAIStyleID.Explosive;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;

            AIType = ProjectileID.BouncyGrenade;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
