using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Magic.ThorSpear
{
    public class ZEarthmoverBeam : EarthmoverBeam
    {
        public override void StaticProperties()
        {
            Projectile.AddDamageType(3, 5);
            Projectile.AddElement(0);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(5);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            hitscanColor = Color.Firebrick;
        }

        public override void AI()
        {
            base.AI();
            if (Projectile.timeLeft > StopTime && Main.rand.NextBool(3))
            {
                var dust = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2CircularEdge(3f, 3f) * (1 - Main.rand.NextFloat(0.33f)));
                dust.noGravity = true;
            }
        }
    }
}
