using ShardsOfAtheria.Dusts;
using MMZeroElements;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus
{
    public class AreusGrenadeProj : ModProjectile
    {
        int armTimer = 0;

        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 22;

            Projectile.aiStyle = ProjAIStyleID.Explosive;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.timeLeft = 240;

            AIType = ProjectileID.Grenade;
        }

        public override void AI()
        {
            if (++armTimer == 10)
            {
                SoundEngine.PlaySound(SoundID.Unlock.WithPitchOffset(-1f).WithVolumeScale(0.6f));
                Projectile.frame = 1;
                Projectile.friendly = true;
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust_Standard>());
                    dust.velocity *= 2f;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.Explode(Projectile.Center);
            if (Projectile.GetGlobalProjectile<OverchargedProjectile>().overcharged)
            {
                Projectile.CallStorm(3);
            }
        }
    }
}
