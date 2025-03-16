using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.GunRose
{
    public class ScarletPetal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(10);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(9);
            Projectile.AddRedemptionElement(10);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
            float rotation = MathHelper.ToRadians(15);
            Projectile.velocity = Projectile.velocity.RotatedBy(rotation);
            if (Projectile.ai[0] > -1)
            {
                var rose = Main.projectile[(int)Projectile.ai[0]];
                Projectile.Center = rose.Center + Projectile.velocity * 50f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Withering>(), 600);
        }
    }
}
