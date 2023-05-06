using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class ElectricOrb : ModProjectile
    {
        Point point;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            Projectile.AddElec();
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
            if (++Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }

        public override void Kill(int timeLeft)
        {
            bool flag = Main.rand.NextBool(2);
            for (int i = 0; i < 4; i++)
            {
                Vector2 vector = Projectile.Center + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                if (flag)
                {
                    vector = Projectile.Center + new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                }
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(vector - Projectile.Center),
                    ModContent.ProjectileType<LightningBoltFriendly>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
