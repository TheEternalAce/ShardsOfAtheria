using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class BloodShuriken : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 1200;
            Projectile.penetrate = 10;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(15f);

            var player = Projectile.GetPlayerOwner();
            if (++Projectile.ai[0] >= 60 && Projectile.Distance(player.Center) > 60)
            {
                var vector = player.Center - Projectile.Center;
                float speed = Projectile.velocity.Length();
                ShardsHelpers.AdjustMagnitude(ref vector, speed);
                Projectile.velocity = (4 * Projectile.velocity + vector) / 2f;
                ShardsHelpers.AdjustMagnitude(ref Projectile.velocity, speed);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawAfterImage(lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}
