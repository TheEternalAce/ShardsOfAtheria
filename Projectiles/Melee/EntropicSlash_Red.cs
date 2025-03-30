using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class EntropicSlash_Red : BladeAura
    {
        public override Color AuraColor => Color.Red;
        public override int OutterDust => DustID.Water;
        public override int InnerDust => DustID.GemRuby;
        public override float ScaleMultiplier => 1f;
        public override float ScaleAdder => 1f;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Projectile.AddDamageType(2, 11);
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(3);
        }
    }

    public class EntropicSlash_Blue : EntropicSlash_Red
    {
        public override Color AuraColor => Color.Blue;
        public override int InnerDust => DustID.GemSapphire;
    }
}