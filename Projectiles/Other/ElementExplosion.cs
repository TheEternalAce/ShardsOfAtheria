using BattleNetworkElements.Elements;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class ElementExplosion : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.penetrate = 7;
            Projectile.timeLeft = 10;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                ScreenShake.ShakeScreen(6, 60);
                SoundEngine.PlaySound(SoundID.Item14);
                Projectile.ai[0] = 1;
            }

            if (SoA.ElementModEnabled)
            {
                ElementalParticles();
            }
            Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Smoke, Scale: 1.5f);
            dust2.velocity *= 2f;
        }

        [JITWhenModsEnabled("BattleNetworkElements")]
        private void ElementalParticles()
        {
            BNGlobalProjectile elementExplosion = Projectile.GetGlobalProjectile<BNGlobalProjectile>();
            if (Main.rand.NextBool(3))
            {
                if (elementExplosion.isFire)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch, Scale: 1.3f);
                    dust.velocity *= 4f;
                }
                if (elementExplosion.isAqua)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Ice);
                    dust.velocity *= 4f;
                }
                if (elementExplosion.isElec)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric);
                    dust.velocity *= 4f;
                }
            }
        }
    }
}