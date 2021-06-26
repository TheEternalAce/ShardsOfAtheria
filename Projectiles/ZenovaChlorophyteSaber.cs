using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class ZenovaChlorophyteSaber : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenova Chlorophyte Saber");
        }

        public override void SetDefaults() {
            projectile.width = 18;
            projectile.height = 18;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            drawOffsetX = -28;
            drawOriginOffsetX = 14;
        }

        public override void AI()
        {
            projectile.velocity.Y = projectile.velocity.Y + 0.1f; // 0.1f for arrow gravity, 0.4f for knife gravity
            if (projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
            {
                projectile.velocity.Y = 16f;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            if (projectile.spriteDirection == 1)
            {
                drawOffsetX = -28;
                drawOriginOffsetX = 14;
            }
            else
            {
                drawOffsetX = 0;
                drawOriginOffsetX = -14;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 60);
            target.AddBuff(BuffID.Blackout, 3600);
        }
    }
}
