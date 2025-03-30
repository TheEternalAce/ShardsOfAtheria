using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class StormSlash : BladeAura
    {
        public override Color AuraColor => SoA.HardlightBlueColor;
        public override int OutterDust => DustID.Electric;
        public override int InnerDust => ModContent.DustType<HardlightDust_Blue>();
        public override float ScaleMultiplier => 1f;
        public override float ScaleAdder => 1.5f;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(5);
            base.SetStaticDefaults();
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }
    }
}