using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class YamikoSlash : BladeAura
    {
        public override Color AuraColor => Color.Firebrick;
        public override int OutterDust => DustID.Torch;
        public override int InnerDust => DustID.Torch;
        public override float ScaleMultiplier => 0.6f;
        public override float ScaleAdder => 1f;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(BuffID.OnFire, 600);
        }
    }
}