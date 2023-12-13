using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class LifeStealGem : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 360;
            Projectile.extraUpdates = 3;
        }

        public override void AI()
        {
            var player = Main.player[Projectile.owner];
            Projectile.Track(player.Center);
            if (Projectile.Hitbox.Intersects(player.Hitbox))
            {
                Projectile.Kill();
                player.Heal(Projectile.damage);
            }

            int type = DustID.GemRuby;
            if (Main.rand.NextBool())
            {
                type = DustID.GemTopaz;
            }
            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, type);
            d.velocity *= 0;
            d.noGravity = true;
        }
    }
}