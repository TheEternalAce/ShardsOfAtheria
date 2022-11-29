using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus
{
    public class ElectricOrb : ModProjectile
    {
        Point point;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer && Projectile.ai[0] == 0)
            {
                point = Main.MouseWorld.ToPoint();
                Projectile.ai[0] = 1;
            }
            if (Projectile.getRect().Contains(point))
            {
                Projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            bool flag = Main.rand.NextBool(2);
            for (int i = 0; i < 4; i++)
            {
                Vector2 projPos = Projectile.Center + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                if (flag)
                {
                    projPos = Projectile.Center + new Vector2(1.425f, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                }

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(projPos - Projectile.Center) * 2f, ModContent.ProjectileType<LightningBoltFriendly>(),
                    Projectile.damage, Projectile.knockBack, Main.myPlayer);
            }
        }
    }
}
