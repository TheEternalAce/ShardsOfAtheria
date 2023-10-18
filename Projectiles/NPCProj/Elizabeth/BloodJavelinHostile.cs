using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.NPCs.Boss.Elizabeth;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodJavelinHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.timeLeft = 60 * 5;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;

            DrawOffsetX = -8;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
            }
            Projectile.ai[0]++;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (Projectile.ai[0] >= 60 && Projectile.ai[0] < 200)
            {
                if (!Projectile.friendly)
                {
                    Projectile.friendly = true;
                }
                Player player = Projectile.FindClosestPlayer(-1);
                if (player != null)
                {
                    float speed = 8f;
                    Projectile.Track(player.Center, 350, speed, 18);
                }
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.type != ModContent.NPCType<Death>())
            {
                return false;
            }
            return base.CanHitNPC(target);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<DeathBleed>(300);
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
            Projectile.DrawPrimsAfterImage(lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}