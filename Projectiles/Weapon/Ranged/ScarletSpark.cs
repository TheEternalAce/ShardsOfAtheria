using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.AreusSaber
{
    public class ScarletSpark : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MartianTurretBolt;

        public override void SetStaticDefaults()
        {
            Projectile.AddElec();
            Main.projFrames[Type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 240;

            DrawOffsetX = -22;
            DrawOriginOffsetY = -2;
            DrawOriginOffsetX = 10;
        }

        NPC targetNPC = null;

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0);
            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -22; // These values match the values in SetDefaults
                DrawOriginOffsetY = -2;
                DrawOriginOffsetX = 10;
            }
            else
            {
                // Facing left.
                // You can figure these values out if you flip the sprite in your drawing program.
                DrawOffsetX = -2; // 0 since now the top left corner of the hitbox is on the far left pixel.
                DrawOriginOffsetY = -2; // doesn't change
                DrawOriginOffsetX = -10; // Math works out that this is negative of the other value.
            }

            float maxDetectRadius = 1000f; // The maximum radius at which a projectile can detect a target

            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.frame = 0;
                }
            }

            // Trying to find NPC closest to the projectile
            if (targetNPC == null || !targetNPC.active)
            {
                targetNPC = Projectile.FindClosestNPC(maxDetectRadius);
                return;
            }

            Projectile.Track(targetNPC, maxDetectRadius, 32f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}