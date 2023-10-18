using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee.Sawstring;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.PlanetaryYoyo
{
    public class AreusOrbitingSaw : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Swawstring>().Texture;

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
        }

        public override void OnSpawn(IEntitySource source)
        {
            var player = Projectile.GetPlayerOwner();
            foreach (var projectile in Main.projectile)
            {
                if (projectile.owner == Projectile.owner &&
                    projectile.type == Type &&
                    projectile.active)
                {

                }
            }
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(25f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }
    }
}
