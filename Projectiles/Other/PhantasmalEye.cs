using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class PhantasmalEye : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 10;
            Projectile.DamageType = DamageClass.Summon;
        }

        // Custom AI
        public override void AI()
        {
            float maxDetectRadius = 400f; // The maximum radius at which a projectile can detect a target
            float projSpeed = 10f; // The speed at which the projectile moves towards the target

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // Trying to find NPC closest to the projectile
            NPC closestNPC = Projectile.FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            Projectile.Track(closestNPC, maxDetectRadius, projSpeed);
        }
    }
}
