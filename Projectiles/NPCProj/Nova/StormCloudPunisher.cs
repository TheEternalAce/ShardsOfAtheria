using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class StormCloudPunisher : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Blank";

        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.ElectricProj.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 250;
            Projectile.height = 100;

            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Center = player.Center + new Vector2(0, -200);

            if (++Projectile.ai[1] >= 20)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Normalize(player.Center - Projectile.Center) * 4f, ModContent.ProjectileType<LightningBolt>(), 50, 0, Main.myPlayer);
                Projectile.ai[1] = 0;
            }

            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.RainCloud, Scale: 2f);
                dust.noGravity = true;
            }

            if (player.dead)
            {
                Projectile.Kill();
            }
            else
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<StormCloud>()] == 0)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}
