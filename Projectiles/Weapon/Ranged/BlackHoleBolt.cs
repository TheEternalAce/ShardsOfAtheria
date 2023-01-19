using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class BlackHoleBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.extraUpdates = 3;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BlackHole>(), 180, 0, Main.player[Projectile.owner].whoAmI);
        }
    }
}
