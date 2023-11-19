using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj
{
    public class AreusGrenadeHostile : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/Ranged/AreusGrenadeProj";

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 22;

            Projectile.aiStyle = ProjAIStyleID.Explosive;
            Projectile.timeLeft = 240;

            AIType = ProjectileID.Grenade;
        }

        public override void AI()
        {
            if (Projectile.timeLeft == 235)
            {
                SoundEngine.PlaySound(SoundID.Unlock.WithPitchOffset(-1f).WithVolumeScale(0.6f), Projectile.position);
                Projectile.frame = 1;
                Projectile.hostile = true;
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
                    dust.velocity *= 2f;
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Explode(Projectile.Center, Projectile.damage, true);
        }
    }
}
