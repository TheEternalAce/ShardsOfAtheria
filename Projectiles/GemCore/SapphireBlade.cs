using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.GemCore
{
    public class SapphireBlade : ModProjectile
    {
        private float rotation;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = -1;
            Projectile.timeLeft = 10;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            if (!CheckActive(player))
            {
                Projectile.Kill();
                return;
            }
            float toRotate = rotation + MathHelper.Pi * Projectile.ai[0];
            Projectile.Center = player.Center + Vector2.One.RotatedBy(toRotate) * 45f;
            rotation -= MathHelper.ToRadians(2.5f);
            Projectile.rotation = (Projectile.Center - player.Center).ToRotation() + MathHelper.PiOver2;
        }

        bool CheckActive(Player player)
        {
            if (player.dead || !player.active || !player.Gem().sapphireCore || !player.HasBuff<CunningSapphire>())
                return false;
            Projectile.timeLeft = 2;
            return true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }
    }
}