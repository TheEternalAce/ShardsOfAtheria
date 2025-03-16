using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged.GunRose
{
    public class WitheringSeed : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(6, 7);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(9);
            Projectile.AddRedemptionElement(10);
            Projectile.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 120;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnKill(int timeLeft)
        {
            if (!Projectile.GetPlayerOwner().IsLocal()) return;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                Vector2.Zero, ModContent.ProjectileType<WitheringRose>(), 180, 0, Projectile.owner);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Withering>(), 600);
        }
    }
}
