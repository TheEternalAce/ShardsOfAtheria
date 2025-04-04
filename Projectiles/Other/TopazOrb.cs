using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class TopazOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
        }

        int gravityDelay = 6;
        public override void AI()
        {
            if (Projectile.timeLeft <= 255) Projectile.alpha++;

            Vector2 vector2 = Vector2.Zero;
            var player = Projectile.FindClosestPlayer(150f);
            if (player != null)
            {
                if (!player.dead && player.active) vector2 = player.Center;
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Projectile.Kill();
                    player.Heal(20);
                }
            }

            if (vector2 == Vector2.Zero)
            {
                Projectile.ApplyGravity(ref gravityDelay, 1f, 5f);
                if (Math.Abs(Projectile.velocity.X) > 0f) Projectile.velocity.X *= 0.8f;
            }
            else Projectile.Track(vector2);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            var player = Projectile.FindClosestPlayer(150f);
            if (player != null)
            {
                bool flag = Projectile.Distance(player.Center) < 50;
                fallThrough = flag;
                return flag;
            }
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            var player = Projectile.GetPlayerOwner();
            var gem = player.Gem();
            if (gem.megaGemCore)
            {
                for (var i = 0; i < 30; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemTopaz, speed * 6f);
                    d.fadeIn = 1.3f;
                    d.noGravity = true;
                }
                foreach (Player player1 in Main.player)
                {
                    if (player1.team == player.team && player1.whoAmI != player.whoAmI && player1.Distance(Projectile.Center) < 1000)
                        player1.Heal(20 + player1.statLifeMax2 / 20);
                }
            }
        }
    }
}