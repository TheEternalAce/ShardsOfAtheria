using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus.AreusSaber
{
    public class AreusSickle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.AreusProj.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 240;

            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
            DrawOriginOffsetX = 0;
        }

        NPC targetNPC = null;

        public override void AI()
        {
            Projectile.rotation += 0.8f;
            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -2; // These values match the values in SetDefaults
                DrawOriginOffsetY = -2;
                DrawOriginOffsetX = 0;
            }
            else
            {
                // Facing left.
                // You can figure these values out if you flip the sprite in your drawing program.
                DrawOffsetX = 0; // 0 since now the top left corner of the hitbox is on the far left pixel.
                DrawOriginOffsetY = -2; // doesn't change
                DrawOriginOffsetX = 0; // Math works out that this is negative of the other value.
            }

            float maxDetectRadius = 1000f; // The maximum radius at which a projectile can detect a target

            // Trying to find NPC closest to the projectile
            if (targetNPC == null)
            {
                targetNPC = Projectile.FindClosestNPC(maxDetectRadius);
                return;
            }

            Projectile.ChaseNPC(targetNPC, maxDetectRadius, 10f);
        }
    }
}
