using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class ZHansBullet : HansBullet
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(3, 4, 5);
            base.SetStaticDefaults();
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(3);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.Purple;
            Projectile.DrawBloomTrail(lightColor.UseA(50), SoA.LineBloom);
            return false;
        }
    }
}
