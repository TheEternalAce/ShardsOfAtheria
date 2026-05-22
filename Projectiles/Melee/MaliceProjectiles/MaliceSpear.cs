using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.MaliceProjectiles
{
    public class MaliceSpear : ModProjectile
    {
        // Define the range of the Spear Projectile. These are overrideable properties, in case you'll want to make a class inheriting from this one.
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 160f;

        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.Sets.TrueMelee[Type] = true;

            Projectile.AddDamageType(2, 5);
            Projectile.AddElement(1);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(4);
            Projectile.AddRedemptionElement(7);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff<MaliceDebuff>(300);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);
            Projectile.ownerHitCheck = false;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

            player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

            float halfDuration = duration * 0.5f;
            float progress;

            // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1)
            {
                // If sprite is facing left, rotate 45 degrees
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                // If sprite is facing right, rotate 135 degrees
                Projectile.rotation += MathHelper.ToRadians(135f);
            }

            Projectile.netUpdate = true;

            if (Projectile.timeLeft == halfDuration && Main.myPlayer == player.whoAmI)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 4f,
                    ModContent.ProjectileType<MaliceJavelin>(), (int)(Projectile.damage * 0.5f), Projectile.knockBack, player.whoAmI);
                Projectile.ai[1] = 1;
            }

            return false; // Don't execute vanilla AI.
        }
    }
}
