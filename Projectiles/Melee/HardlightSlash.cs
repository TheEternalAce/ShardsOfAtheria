using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class HardlightSlash : BladeAura
    {
        public override Color AuraColor => SoA.HardlightColor;
        public override int OutterDust => ModContent.DustType<HardlightDust_Pink>();
        public override int InnerDust => ModContent.DustType<HardlightDust_Blue>();
        public override float ScaleMultiplier => 0.6f;
        public override float ScaleAdder => 1f;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Projectile.AddDamageType(11);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }
    }
}