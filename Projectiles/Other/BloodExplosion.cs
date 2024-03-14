using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class BloodExplosion : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DD2BetsyFlameBreath;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Main.projFrames[ProjectileID.LunarFlare];
            ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(12);
            Projectile.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.light = 1f;
        }

        public override void OnSpawn(IEntitySource source)
        {
            ScreenShake.ShakeScreen(6, 60);
            SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Blood, Scale: 1.3f);
                dust.velocity *= 4f;
            }
        }

        public override void AI()
        {
            if (++Projectile.frameCounter > 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame--;
                    Projectile.Kill();
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.DarkRed;
            return base.PreDraw(ref lightColor);
        }
    }
}