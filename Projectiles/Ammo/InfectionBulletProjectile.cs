using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;
using Microsoft.Xna.Framework;

namespace SagesMania.Projectiles.Ammo
{
    public class InfectionBulletProjectile : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infection Bullet");
        }

        public override void SetDefaults() {
            projectile.width = 2;
            projectile.height = 20;

            projectile.ranged = true;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(ModContent.BuffType<Infection>(), 10 * 60);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            projectile.spriteDirection = projectile.direction = (projectile.velocity.X > 0).ToDirectionInt();
            // Adding Pi to rotation if facing left corrects the drawing
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
            if (projectile.spriteDirection == 1) // facing right
            {
                drawOffsetX = -19;
                drawOriginOffsetX = 9;
            }
            else
            {
                drawOffsetX = 0;
                drawOriginOffsetX = -9; // Math works out that this is negative of the other value.
            }
        }
    }
}
