using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon
{
    public class ZenovaWormsTooth : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenova Worm's Tooth");
        }

        public override void SetDefaults() {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            DrawOffsetX = -30;
            DrawOriginOffsetX = 15;
        }

        public override void AI()
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.1f; // 0.1f for arrow gravity, 0.4f for knife gravity
            if (Projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
            {
                Projectile.velocity.Y = 16f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -30;
                DrawOriginOffsetX = 15;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -15;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 600);
            target.AddBuff(BuffID.Weak, 600);
            target.AddBuff(BuffID.Chilled, 600);
        }
    }
}
