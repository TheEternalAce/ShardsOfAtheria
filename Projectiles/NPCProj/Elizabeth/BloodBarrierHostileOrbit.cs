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
    public class BloodBarrierHostileOrbit : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<BloodBarrierHostile>().Texture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;

            DrawOffsetX = -20;
        }

        public override void AI()
        {
            var npc = Main.npc[(int)Projectile.ai[0]];
            Projectile.Center = npc.Center + Projectile.velocity * 50;

            Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(5));
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (npc.CanBeChasedBy())
            {
                Projectile.timeLeft = 2;
            }

            foreach (var projectile in Main.projectile)
            {
                if (projectile.Hitbox.Intersects(Projectile.Hitbox) && projectile.active && Projectile.whoAmI != projectile.whoAmI)
                {
                    if (SoAGlobalProjectile.ReflectAiList.Contains(projectile.aiStyle) &&
                        projectile.velocity != Vector2.Zero &&
                        projectile.friendly)
                    {
                        projectile.Kill();
                        for (var d = 0; d < 28; d++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust dust = Dust.NewDustPerfect(projectile.Center,
                                DustID.Blood, speed * 2.4f);
                            dust.fadeIn = 1.3f;
                            dust.noGravity = true;
                        }
                        SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
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
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}