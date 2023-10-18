using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.ElecKatana
{
    public class ElecShuriken : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(30f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                var vector = Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i));
                vector *= 16f;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vector,
                    ModContent.ProjectileType<ElecKunaiHoming>(), Projectile.damage, Projectile.knockBack,
                    Projectile.owner);
            }
        }
    }
}
