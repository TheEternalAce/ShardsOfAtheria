using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodDropHostile : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.GolfBallDyedViolet}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.timeLeft = 120;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }

        int gravityTimer = 16;

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
            }

            Projectile.ApplyGravity(ref gravityTimer);

            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Blood, newColor: Color.White);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DeathBleed>(), 600);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}