using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions
{
    public class GolemBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(4);
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EyeBeam);
            Projectile.friendly = true;
            Projectile.hostile = false;
            AIType = ProjectileID.EyeBeam;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}