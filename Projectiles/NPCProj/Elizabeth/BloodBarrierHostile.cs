using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodBarrierHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.timeLeft = 120;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;

            DrawOffsetX = -20;
        }

        public override void AI()
        {
            var npc = Main.npc[(int)Projectile.ai[0]];
            Projectile.Center = npc.Center + Projectile.velocity * 50;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            foreach (var projectile in Main.projectile)
            {
                if (projectile.Hitbox.Intersects(Projectile.Hitbox))
                {
                    if (SoAGlobalProjectile.ReflectAiList.Contains(projectile.aiStyle) &&
                        projectile.velocity != Vector2.Zero &&
                        projectile.friendly &&
                        !projectile.hostile &&
                        projectile.active)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, Projectile.Center);
                        SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.Center);
                        projectile.hostile = true;
                        projectile.velocity *= -1.5f;
                        projectile.damage = (int)(Projectile.damage * 0.25f);
                    }
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<DeathBleed>(300);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}