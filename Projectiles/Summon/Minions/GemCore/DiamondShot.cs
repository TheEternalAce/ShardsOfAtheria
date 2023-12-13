using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.GemCore
{
    public class DiamondShot : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 20;
            Projectile.alpha = 255;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.arrow = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item72);
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8;
            if (++Projectile.ai[1] >= 5)
            {
                int type = DustID.GemDiamond;
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, type);
                d.velocity *= 0;
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }

            var projectile = Main.projectile[(int)Projectile.ai[0]];
            Projectile.Track(projectile.Center, inertia: 1f);
            if (Projectile.Hitbox.Intersects(projectile.Hitbox))
            {
                if (projectile.active && projectile.hostile)
                {
                    projectile.Kill();
                }
            }
            if (!projectile.active)
            {
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 28; i++)
            {
                var speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }
    }
}
