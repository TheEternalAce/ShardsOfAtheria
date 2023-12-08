using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class AmberBanner : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Projectile.GetPlayerOwner();
            if (!CheckActive(player))
            {
                return;
            }
            if (player.Gem().megaGemCore && Projectile.ai[0] == 1)
            {
                Projectile.timeLeft = 2;
                Vector2 vector = new(0, -20);
                Projectile.Center = player.Center + vector;
                Projectile.netUpdate = true;
            }
            AmberAura(500);
        }

        private bool CheckActive(Player player)
        {
            if (player.dead || !player.active || (!player.Gem().megaGemCore && Projectile.ai[0] == 1))
                return false;
            return true;
        }

        private void AmberAura(int radius)
        {
            for (var i = 0; i < 20; i++)
            {
                Vector2 spawnPos = Projectile.Center + Main.rand.NextVector2CircularEdge(radius, radius);
                Vector2 offset = spawnPos - Main.LocalPlayer.Center;
                if (Math.Abs(offset.X) > Main.screenWidth * 0.6f || Math.Abs(offset.Y) > Main.screenHeight * 0.6f) //dont spawn dust if its pointless
                    continue;
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.GemAmber, 0, 0, 100);
                dust.velocity = Projectile.velocity;
                if (Main.rand.NextBool(3))
                {
                    dust.velocity += Vector2.Normalize(Projectile.Center - dust.position) * Main.rand.NextFloat(5f);
                    dust.position += dust.velocity * 5f;
                }
                dust.noGravity = true;
            }
            AmberBuff(radius);
        }

        private void AmberBuff(int maxDistance)
        {
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead)
                {
                    var distToNPC = Vector2.Distance(player.Center, Projectile.Center);
                    if (distToNPC <= maxDistance)
                    {
                        player.AddBuff<AmberBannerBuff>(300);
                    }
                }
            }
        }

        public static int FindOldestBanner(Player player)
        {
            int result = -1;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile banner = Main.projectile[i];
                if (banner.active && banner.type == ModContent.ProjectileType<AmberBanner>() && banner.owner == player.whoAmI)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
    }
}