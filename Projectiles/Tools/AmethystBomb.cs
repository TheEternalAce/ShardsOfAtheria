using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class AmethystBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;

            Projectile.aiStyle = ProjAIStyleID.Explosive;
            AIType = ProjectileID.Grenade;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            for (int i = 0; i < 16; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemAmethyst);
                dust.velocity *= 6;
                dust.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            var player = Projectile.GetPlayerOwner();
            var gem = player.Gem();
            Projectile.ExplodeTiles(Projectile.Center, 6, 6, Main.maxTilesX - 6, 6, Main.maxTilesY - 6, gem.amethystWallBomb);
            Projectile.Kill();
            return false;
        }
    }
}
