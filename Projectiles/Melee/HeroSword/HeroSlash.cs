using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.HeroSword
{
    public class HeroSlash : BladeAura
    {
        public override Color AuraColor => new(121, 210, 210, 125);
        public override int OutterDust => DustID.Electric;
        public override int InnerDust => DustID.Torch;
        public override float ScaleMultiplier => 0f;
        public override float ScaleAdder => 2.5f;
        public override float HalfRotations => 2f;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        int hitCooldown = 0;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (hitCooldown == 0)
            {
                Vector2 position = target.Center + Vector2.One.RotatedByRandom(360) * 180;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, Vector2.Normalize(target.Center - position) * 20,
                    ModContent.ProjectileType<HeroBlade>(), 50, 6f, Projectile.owner);
                hitCooldown = 10;
            }
        }
    }
}