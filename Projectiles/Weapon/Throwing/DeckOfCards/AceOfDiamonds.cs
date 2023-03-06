using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Throwing.DeckOfCards
{
    public class AceOfDiamonds : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 5;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            ProjectileElements.Fire.Remove(Type);
            ProjectileElements.Ice.Remove(Type);
            ProjectileElements.Electric.Remove(Type);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}
