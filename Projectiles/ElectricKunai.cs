using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SagesMania.Projectiles
{
    public class ElectricKunai : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.scale = .5f;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = false;
            projectile.light = 1;
            projectile.extraUpdates = 1;

            drawOffsetX = -28;
            drawOriginOffsetX = 14;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Electrified, 10*60);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 20f)
            {
                projectile.ai[0] = 15f;
                projectile.velocity.Y = projectile.velocity.Y + 0.1f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }
    }
}
