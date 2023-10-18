using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.BloodDagger
{
    public class HolyBloodHeal : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 360;
            Projectile.extraUpdates = 3;
        }

        int timer = 12;

        public override void AI()
        {
            if (timer > 0)
            {
                timer--;
            }
            else
            {
                var player = Main.player[Projectile.owner];
                Projectile.Track(player.Center, -1);
                var rect = player.getRect();
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Projectile.Kill();
                    player.Heal(Projectile.damage);
                }
            }

            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Blood);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;
        }
    }
}