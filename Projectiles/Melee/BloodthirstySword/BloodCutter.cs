using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.BloodthirstySword
{
    public class BloodCutter : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 70;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (target.life <= 0)
            {
                var shards = player.Shards();
                shards.mourningStarKills++;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            var rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.DrawProjectilePrims(Color.DarkRed, ShardsHelpers.DiamondX1, rotation);
            Projectile.DrawPrimsAfterImage(Color.White);
            return base.PreDraw(ref lightColor);
        }
    }
}
