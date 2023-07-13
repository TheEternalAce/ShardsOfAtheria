using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.GunRose
{
    public class WitheringPetal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 145;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            float rotation = MathHelper.ToRadians(5);
            Projectile.velocity = Projectile.velocity.RotatedBy(rotation);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Withering>(), 600);
        }
    }
}
