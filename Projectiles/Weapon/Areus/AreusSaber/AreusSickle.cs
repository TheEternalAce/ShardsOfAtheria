using Microsoft.Xna.Framework;
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
                targetNPC = FindClosestNPC(maxDetectRadius);
                return;
            }

            Projectile.TrackTarget(targetNPC);
        }

        // Finding the closest NPC to attack within maxDetectDistance range
        // If not found then returns null
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }
    }
}
