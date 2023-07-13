using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.EventHorizon
{
    public class BlackHole : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAqua();
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 2 * 60;
        }

        float rotation = 0;
        public override void AI()
        {
            int rate = 15;
            if (++Projectile.ai[0] % rate == 0 && Projectile.timeLeft > rate * 2)
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 vel = Projectile.Center + Vector2.One.RotatedBy(MathHelper.ToRadians(45 * i) + rotation) * 500f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), vel,
                        Vector2.Normalize(Projectile.Center - vel) * 16, ModContent.ProjectileType<BlackHoleBolt>(),
                        Projectile.damage, Projectile.knockBack, Projectile.owner, 2);
                }
                rotation += MathHelper.ToRadians(10);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
