using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee.Sawstring;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusOrbit
{
    public class AreusOrbitingSaw : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Swawstring>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 180;
            Projectile.penetrate = 15;
        }

        private double orbit;
        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(15f);
            var player = Projectile.GetPlayerOwner();
            var projectile = ShardsHelpers.FindClosestProjectile(Projectile.Center, 200f, ModContent.ProjectileType<OrbiterMagnet>(), player.whoAmI);
            if (projectile == null)
            {
                return;
            }
            orbit += MathHelper.ToRadians(10);
            Projectile.Center = projectile.Center + Vector2.One.RotatedBy(orbit) * 80;
            Projectile.timeLeft = 180;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }
    }
}
