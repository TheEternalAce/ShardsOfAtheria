using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using ShardsOfAtheria.Items;
using Terraria.Audio;

namespace ShardsOfAtheria.Projectiles.Weapon
{
    public class MicrobeScraperProj : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.aiStyle = -1;
            Projectile.penetrate = 10;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
            Projectile.timeLeft = 240;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            // Adding Pi to rotation if facing left corrects the drawing
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -19;
                DrawOriginOffsetX = 9;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -9; // Math works out that this is negative of the other value.
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Item.NewItem(target.GetSpawnSource_NPCHurt(), target.getRect(), ModContent.ItemType<UnanalyzedMicrobe>());
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Item.NewItem(target.GetProjectileSource_OnHit(target, ModContent.ProjectileType<MicrobeScraperProj>()), target.getRect(), ModContent.ItemType<UnanalyzedMicrobe>());
        }
    }
}
