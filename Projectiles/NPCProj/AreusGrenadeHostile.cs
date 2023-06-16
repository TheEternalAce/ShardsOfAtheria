using BattleNetworkElements.Utilities;
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
        int armTimer = 0;

        public override string Texture => "ShardsOfAtheria/Projectiles/Weapon/Ranged/AreusGrenadeProj";

        public override void SetStaticDefaults()
        {
            Projectile.AddElec();
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 22;

            Projectile.aiStyle = ProjAIStyleID.Explosive;
            Projectile.penetrate = 7;

            AIType = ProjectileID.Grenade;
        }

        public override void AI()
        {
            if (++armTimer == 5)
            {
                SoundEngine.PlaySound(SoundID.Unlock.WithPitchOffset(-1f).WithVolumeScale(0.6f));
                Projectile.frame = 1;
                Projectile.hostile = true;
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
                    dust.velocity *= 2f;
                }
            }
            if (armTimer == 240 && Projectile.alpha == 0)
            {
                Projectile.Explode();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.alpha == 0)
            {
                Projectile.Explode();
            }
        }
    }
}
