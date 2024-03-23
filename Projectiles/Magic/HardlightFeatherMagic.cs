using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class HardlightFeatherMagic : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/NPCProj/Nova/FeatherBlade";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Magic;
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
                    if (blade.type == ModContent.ProjectileType<HardlightFeatherMagic>() && blade.whoAmI != Projectile.whoAmI && Projectile.active && blade.active)
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
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBloomTrail(SoA.HardlightColor.UseA(50), SoA.DiamondBloom, MathHelper.PiOver2);
            lightColor = Color.White;
            return true;
        }
    }
}
