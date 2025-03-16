using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ThorSpear
{
    public class ElectricJavelin : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<EarthmoverSpear>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(5);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 2;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            Projectile.SetVisualOffsets(72);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
            if (++Projectile.ai[0] >= 32f) Projectile.ApplyGravity();
            if (Projectile.alpha > 0) Projectile.alpha = 0;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);
            if (Projectile.damage < 80) Projectile.damage = 80;
        }
    }
}
