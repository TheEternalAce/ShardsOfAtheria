using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class HardlightBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElec();
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
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile blade = Main.projectile[i];
                if (blade.type == Type &&
                    blade.whoAmI != Projectile.whoAmI &&
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

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = new(227, 182, 245, 80);
            Projectile.DrawProjectilePrims(color, ShardsProjectileHelper.DiamondX1);
            lightColor = Color.White;
            return true;
        }
    }
}
