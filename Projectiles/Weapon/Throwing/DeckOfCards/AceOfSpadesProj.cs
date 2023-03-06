using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Throwing.DeckOfCards
{
    public class AceOfSpadesProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Fire.Add(Type);
            ProjectileElements.Ice.Remove(Type);
            ProjectileElements.Electric.Remove(Type);
            ProjectileElements.Metal.Remove(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            ProjectileElements.Ice.Remove(Type);
            ProjectileElements.Electric.Remove(Type);
            ProjectileElements.Metal.Remove(Type);
        }

        public override void Kill(int timeLeft)
        {
            Projectile.Explode(Projectile.Center);
            base.Kill(timeLeft);
        }
    }
}
