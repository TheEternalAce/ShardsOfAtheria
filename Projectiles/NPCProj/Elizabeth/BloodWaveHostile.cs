using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodWaveHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.timeLeft = 60;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;

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

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<DeathBleed>(300);
            Projectile.Kill();
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
                var vector = new Vector2(0, -1).RotateRandom(rotation) * 8f * Main.rand.NextFloat(0.8f, 1);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                    vector, ModContent.ProjectileType<BloodDropHostile>(), Projectile.damage,
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