using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class RubyExplosive : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.LargeRuby;

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 32;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = 0;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                Vector2 vector = Main.rand.NextVector2CircularEdge(1f, 1f);
                vector *= 12f * (1 - Main.rand.NextFloat(0.33f));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vector,
                    ModContent.ProjectileType<RubyShard>(), Projectile.damage, 0f);
            }
        }
    }
}
