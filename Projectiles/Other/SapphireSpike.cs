using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class SapphireSpike : ModProjectile
    {
        int gravityTimer;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 12;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            gravityTimer = 16;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ApplyGravity(ref gravityTimer);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            var vector = -Projectile.velocity;
            vector.Normalize();
            vector *= 3f;
            for (int i = 0; i < 6; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemSapphire,
                    vector.X, vector.Y);
                dust.noGravity = true;
            }
        }
    }
}
