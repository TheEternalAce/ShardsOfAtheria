using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.EntropyCutter
{
    public class EntropySickle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Type] = true;
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = false;
        }

        NPC targetNPC = null;

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(20 * Projectile.direction);

            float maxDetectRadius = 300f; // The maximum radius at which a projectile can detect a target

            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.frame = 0;
                }
            }

            // Trying to find NPC closest to the projectile
            if (targetNPC == null || !targetNPC.active)
            {
                targetNPC = Projectile.FindClosestNPC(maxDetectRadius);
                return;
            }

            Projectile.Track(targetNPC, maxDetectRadius, 32f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var color = new Color(90, 10, 120);
            lightColor = Color.White;
            Projectile.DrawProjectilePrims(color, ShardsHelpers.OrbX1);
            return base.PreDraw(ref lightColor);
        }
    }
}