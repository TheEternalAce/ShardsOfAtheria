using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class VendettaTendril : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        override public void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ShadowFlame);

            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 60;
            Projectile.usesLocalNPCImmunity = false;
            Projectile.localNPCHitCooldown = 0;
        }

        public override void AI()
        {
            Vector2 center2 = Projectile.Center;
            Projectile.scale = 1f - Projectile.localAI[0];
            Projectile.width = (int)(20f * Projectile.scale);
            Projectile.height = Projectile.width;
            Projectile.position.X = center2.X - Projectile.width / 2;
            Projectile.position.Y = center2.Y - Projectile.height / 2;
            if (Projectile.localAI[0] < 0.1)
            {
                Projectile.localAI[0] += 0.01f;
            }
            else
            {
                Projectile.localAI[0] += 0.025f;
            }
            if (Projectile.localAI[0] >= 0.95f)
            {
                Projectile.Kill();
            }
            Projectile.velocity.X += Projectile.ai[0] * 1.5f;
            Projectile.velocity.Y += Projectile.ai[1] * 1.5f;
            if (Projectile.velocity.Length() > 16f)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= 16f;
            }
            Projectile.ai[0] *= 1.05f;
            Projectile.ai[1] *= 1.05f;
            if (Projectile.scale < 1f)
            {
                for (int num827 = 0; num827 < Projectile.scale * 10f; num827++)
                {
                    int num828 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                        Projectile.height, DustID.Shadowflame, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1.1f);
                    Main.dust[num828].position = (Main.dust[num828].position + Projectile.Center) / 2f;
                    Main.dust[num828].noGravity = true;
                    Dust dust70 = Main.dust[num828];
                    Dust dust195 = dust70;
                    dust195.velocity *= 0.1f;
                    dust70 = Main.dust[num828];
                    dust195 = dust70;
                    dust195.velocity -= Projectile.velocity * (1.3f - Projectile.scale);
                    Main.dust[num828].fadeIn = 100 + Projectile.owner;
                    dust70 = Main.dust[num828];
                    dust195 = dust70;
                    dust195.scale += Projectile.scale * 0.75f;
                }
            }
        }
    }
}
