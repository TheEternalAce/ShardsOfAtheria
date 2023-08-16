using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic.ElecGauntlet
{
    public class ElecDagger : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(10); // This sets width and height to the same value (important when projectiles can rotate)
            Projectile.aiStyle = -1; // Use our own AI to customize how it behaves, if you don't want that, keep this at ProjAIStyleID.ShortSword. You would still need to use the code in SetVisualOffsets() though
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 1; // Update 1+extraUpdates times per tick
            Projectile.timeLeft = 360; // This value does not matter since we manually kill it earlier, it just has to be higher than the duration we use in AI
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() +
                (Projectile.spriteDirection == 1 ? MathHelper.ToRadians(45f) : MathHelper.ToRadians(135f));
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -12;
                DrawOriginOffsetX = 17;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -17;
            }
        }
    }
}
