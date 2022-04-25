using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.Zenova
{
    public class ZenovaCataracnia : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenova Cataracnia");
        }

        public override void SetDefaults() {
            Projectile.width = 21;
            Projectile.height = 21;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            DrawOffsetX = -27;
            DrawOriginOffsetX = 14;
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
                DrawOffsetX = -27;
                DrawOriginOffsetX = 14;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -14;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 60);
            target.AddBuff(BuffID.Blackout, 3600);
        }
    }
}
