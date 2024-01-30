using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.Sawstring
{
    public class AreusSaw : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Swawstring>().Texture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
        }

        public override void OnSpawn(IEntitySource source)
        {
            var player = Projectile.GetPlayerOwner();
            if (player.Shards().Overdrive)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                        Vector2.Zero, ModContent.ProjectileType<ElecSaw>(), Projectile.damage / 4,
                        0, Projectile.owner, Projectile.whoAmI, i);
                }
            }
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(25f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }
    }
}
