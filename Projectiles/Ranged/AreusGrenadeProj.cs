using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AreusGrenadeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddRedemptionElement(15);
            Main.projFrames[Type] = 2;

            SoAGlobalProjectile.Metalic.Add(Type, 1f);
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 22;

            Projectile.aiStyle = ProjAIStyleID.Explosive;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Ranged.TryThrowing();
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;

            AIType = ProjectileID.Grenade;
        }

        public override void AI()
        {
            if (Projectile.timeLeft == 235)
            {
                SoundEngine.PlaySound(SoundID.Unlock.WithPitchOffset(-1f).WithVolumeScale(0.6f), Projectile.Center);
                Projectile.frame = 1;
                Projectile.friendly = true;
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
                    dust.velocity *= 2f;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (!Projectile.GetPlayerOwner().IsLocal()) return;
            Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                ModContent.ProjectileType<ElectricExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        }
    }
}
