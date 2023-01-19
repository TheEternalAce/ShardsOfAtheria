using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class CorruptRose : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile refProj = new Projectile();
            refProj.SetDefaults(ProjectileID.FlowerPow);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 60)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.One.RotatedBy(MathHelper.ToRadians(90)) * 16, ModContent.ProjectileType<CorruptPetal>(), Projectile.damage, Projectile.knockBack, Main.player[Projectile.owner].whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.One.RotatedBy(MathHelper.ToRadians(180)) * 16, ModContent.ProjectileType<CorruptPetal>(), Projectile.damage, Projectile.knockBack, Main.player[Projectile.owner].whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.One.RotatedBy(MathHelper.ToRadians(270)) * 16, ModContent.ProjectileType<CorruptPetal>(), Projectile.damage, Projectile.knockBack, Main.player[Projectile.owner].whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.One.RotatedBy(MathHelper.ToRadians(360)) * 16, ModContent.ProjectileType<CorruptPetal>(), Projectile.damage, Projectile.knockBack, Main.player[Projectile.owner].whoAmI);
                Projectile.ai[0] = 0;
            }
        }
    }

    public class CorruptPetal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.FlowerPowPetal);
            AIType = ProjectileID.FlowerPowPetal;
            Projectile.penetrate = 5;
        }
    }
}
