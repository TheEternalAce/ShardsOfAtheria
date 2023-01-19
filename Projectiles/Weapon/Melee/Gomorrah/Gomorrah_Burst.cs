using MMZeroElements;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.Gomorrah
{
    public class Gomorrah_Burst : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Blank";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.timeLeft = 10;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                ScreenShake.ShakeScreen(6, 60);
                SoundEngine.PlaySound(SoundID.Item14);
                Projectile.ai[0] = 1;
            }
            for (int i = 0; i < 6; i++)
            {
                Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone, 0f, 0f, 100, default, 2f)];
                obj4.noGravity = true;
                obj4.velocity *= 2f;
                obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                obj4.fadeIn = 1.5f;
            }
            for (int i = 0; i < 6; i++)
            {
                Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BoneTorch, 0f, 0f, 100, default, 2f)];
                obj4.noGravity = true;
                obj4.velocity *= 2f;
                obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                obj4.fadeIn = 1.5f;
            }

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (Projectile.Hitbox.Intersects(proj.getRect()))
                {
                    if (proj.type == ModContent.ProjectileType<Gomorrah_Javelin>() && proj.owner == Projectile.owner)
                    {
                        proj.Kill();
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 600);
        }
    }
}
