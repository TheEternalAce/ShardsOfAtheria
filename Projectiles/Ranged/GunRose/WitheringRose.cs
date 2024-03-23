using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.GunRose
{
    public class WitheringRose : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile refProj = new();
            refProj.SetDefaults(ProjectileID.FlowerPow);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            Projectile.scale = 0;
        }

        public override void AI()
        {
            if (Projectile.scale < 1f)
            {
                Projectile.scale += 0.1f;
            }
            if (Projectile.timeLeft % 60 == 0 && Projectile.timeLeft > 145 && Projectile.timeLeft < 600)
            {
                ShardsHelpers.ProjectileRing(Projectile.GetSource_FromThis(), Projectile.Center,
                    5, 1, 16f, ModContent.ProjectileType<WitheringPetal>(), Projectile.damage,
                    Projectile.knockBack, Projectile.owner);
                SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var texure = ModContent.Request<Texture2D>(Texture).Value;
            Main.spriteBatch.Draw(texure, Projectile.position - Main.screenPosition - texure.Size() / 2 * Projectile.scale,
                null, lightColor, 0f, Vector2.Zero, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
