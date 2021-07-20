using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SagesMania.Projectiles
{
    public class FeatherBladeFriendly : ModProjectile
    {
        public override string Texture => "SagesMania/Projectiles/FeatherBlade";
        public override void SetDefaults() {
            projectile.width = 10;
            projectile.height = 10;
            projectile.damage = 37;

            projectile.timeLeft = 5 * 60;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;

            drawOffsetX = -4;
            drawOriginOffsetX = 2;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 60);
        }
    }
}
