using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Crystal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile refProj = new Projectile();
            refProj.SetDefaults(ProjectileID.HarpyFeather);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode || Main.hardMode)
            {
                target.AddBuff(BuffID.Confused, 60);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkCrystalShard, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default, 0.75f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default, 0.75f);
            }
        }
    }
}