using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.Sawstring
{
    public class ElecSaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            Projectile.AddDamageType(5);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;

            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        private double rotation;
        public override void AI()
        {
            var projectile = Main.projectile[(int)Projectile.ai[0]];

            if (!CheckActive(projectile))
            {
                Projectile.Kill();
            }

            //Idle
            rotation += MathHelper.ToRadians(4);
            Projectile.Center = projectile.Center + Vector2.One.RotatedBy(Projectile.ai[1] / 4f * MathHelper.TwoPi + rotation) * 65;
            Projectile.timeLeft = 60;
            Projectile.rotation += MathHelper.ToRadians(15f);

            if (Projectile.ai[2] == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width,
                        Projectile.height, DustID.Electric);
                }
                Projectile.ai[2] = 1;
            }
        }

        bool CheckActive(Projectile proj)
        {
            if (proj == null || !proj.active)
            {
                return false;
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.ElectricColorA0;
            Projectile.DrawAfterImage(lightColor);
            return true;
        }
    }
}
