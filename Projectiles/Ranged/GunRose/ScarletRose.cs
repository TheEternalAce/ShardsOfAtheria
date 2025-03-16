using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.GunRose
{
    public class ScarletRose : ModProjectile
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
            Projectile refProj = new();
            refProj.SetDefaults(ProjectileID.FlowerPow);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
        }

        int[] petals = new int[4];
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 4; i++)
            {
                var vector = Projectile.velocity.RotatedBy(MathHelper.PiOver2 * i);
                vector.Normalize();
                petals[i] = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vector, ModContent.ProjectileType<ScarletPetal>(),
                    Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
            }
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(15);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < petals.Length; i++)
            {
                Main.projectile[petals[i]].Kill();
            }
        }
    }
}