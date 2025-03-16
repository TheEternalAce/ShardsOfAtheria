using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged.DeckOfCards
{
    // It appears to be the ace of spades
    public class AceOfSpades : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(3);
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Ranged.TryThrowing();
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnKill(int timeLeft)
        {
            if (!Projectile.GetPlayerOwner().IsLocal()) return;
            var explosion = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                ModContent.ProjectileType<FieryExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            explosion.DamageType = Projectile.DamageType;
        }
    }
}
