using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Spectrum
{
    public class SpectrumTrainPortal : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_729";

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item117, Projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch);
                dust.velocity *= 4f;
            }
        }

        public override void SetDefaults()
        {
            Projectile.width = 72;
            Projectile.height = 72;

            Projectile.aiStyle = 0;
            Projectile.tileCollide = false;
            Projectile.light = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            foreach (var projectile in Main.projectile)
            {
                if (projectile.active && projectile.type == Type - 1 && projectile.owner == Projectile.owner && Projectile.whoAmI != projectile.whoAmI &&
                    projectile.Hitbox.Intersects(Projectile.Hitbox))
                {
                    Projectile.timeLeft = 60;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.Orange.UseA(0);
            return base.PreDraw(ref lightColor);
        }
    }
}
