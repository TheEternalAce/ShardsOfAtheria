using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.NPCProj.Elizabeth;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class BloodWaveFriendly : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<BloodWaveHostile>().Texture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.timeLeft = 120;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Magic;

            DrawOffsetX = -13;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 15 && !Projectile.friendly)
            {
                Projectile.friendly = true;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }

            var numProjs = 5;
            var rotation = MathHelper.ToRadians(15);
            for (int i = 0; i < numProjs; i++)
            {
                var vector = new Vector2(0, -1).RotateRandom(rotation) * 8f * Main.rand.NextFloat(0.7f, 1);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                    vector, ModContent.ProjectileType<BloodDropFriendly>(), Projectile.damage,
                    Projectile.knockBack, Projectile.owner);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawPrimsAfterImage(lightColor);
            return true;
        }
    }
}