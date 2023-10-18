using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class HardlightBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Projectile.ai[0] == 1)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile blade = Main.projectile[i];
                    if (blade.whoAmI != Projectile.whoAmI &&
                        blade.type == Type &&
                        blade.ai[0] == 1 &&
                        blade.active &&
                        Projectile.active)
                    {
                        if (Projectile.Hitbox.Intersects(blade.Hitbox))
                        {
                            Projectile.Kill();
                            blade.Kill();
                        }
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width,
                    Projectile.height, ModContent.DustType<HardlightDust_Blue>(),
                    Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                dust.noGravity = true;
                var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width,
                    Projectile.height, ModContent.DustType<HardlightDust_Pink>(),
                    Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                dust1.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = new(227, 182, 245, 80);
            Projectile.DrawProjectilePrims(color, ShardsHelpers.DiamondX1);
            lightColor = Color.White;
            return true;
        }
    }
}
