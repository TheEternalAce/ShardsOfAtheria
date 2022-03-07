using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Projectiles.Ammo
{
    public class BacteriaNeedleProj : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 6;
            Projectile.height = 6;

            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            DrawOriginOffsetX = -12;
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
                DrawOriginOffsetX = -12;
            }
            else
            {
                DrawOriginOffsetX = 12; // Math works out that this is negative of the other value.
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var severityChooser = new WeightedRandom<int>();
            severityChooser.Add(ModContent.BuffType<BasicBacterialInfectionI>());
            severityChooser.Add(ModContent.BuffType<BasicBacterialInfectionII>());
            severityChooser.Add(ModContent.BuffType<BasicBacterialInfectionIII>());
            int severityChoice = severityChooser;

            if (!target.HasBuff(ModContent.BuffType<BasicBacterialInfectionI>()) && !target.HasBuff(ModContent.BuffType<BasicBacterialInfectionII>())
                && !target.HasBuff(ModContent.BuffType<BasicBacterialInfectionIII>()))
                target.AddBuff(severityChoice, 10 * 60);
        }
    }
}
