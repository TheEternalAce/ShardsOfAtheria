using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings;
using ShardsOfAtheria.Utilities;
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
            if (!CheckActive(player)) return;
            if (Projectile.ai[0] == 1)
            {
                Projectile.timeLeft = 2;
                Vector2 vector = new(0, -20);
                Projectile.Center = player.MountedCenter + vector;
                Projectile.netUpdate = true;
            }
            else RepellBanners(500f);
            Lighting.AddLight(Projectile.Center, TorchID.Yellow);
            Projectile.velocity *= 0.85f;
            AmberBuff(500f + 300f * Projectile.ai[0]);
        }

        private bool CheckActive(Player player)
        {
            if (player.dead || !player.active || (!player.Gem().megaGemCore && Projectile.ai[0] == 1))
                return false;
            return true;
        }

        private void RepellBanners(float maxDistance)
        {
            foreach (Projectile projectile in Main.projectile)
            {
                if (IsNonFollowingBanner(projectile))
                {
                    var distToPlayer = Vector2.Distance(projectile.Center, Projectile.Center);
                    if (distToPlayer <= maxDistance)
                    {
                        var vector = projectile.Center - Projectile.Center;
                        vector.Normalize();
                        Projectile.velocity = vector * -4;
                        projectile.velocity = vector * 4;
                    }
                }
            }
        }

        private void AmberBuff(float maxDistance)
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

        public bool IsNonFollowingBanner(Projectile projectile)
        {
            if (!projectile.active) return false;
            if (projectile.type != Type) return false;
            if (projectile.owner != Projectile.owner) return false;
            if (projectile.ai[0] == 1) return false;
            if (projectile.whoAmI == Projectile.whoAmI) return false;
            return true;
        }

        public static int FindOldestBanner(Player player)
        {
            int timeleft = 3600;
            int result = -1;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile banner = Main.projectile[i];
                if (banner.active && banner.type == ModContent.ProjectileType<AmberBanner>() && banner.owner == player.whoAmI && banner.timeLeft < timeleft)
                {
                    result = i;
                    timeleft = banner.timeLeft;
                }
            }
            return result;
        }

        public static void MakeOldestBannerFollowPlayer(Player player)
        {
            int firstBannerWhoAmI = FindOldestBanner(player);
            if (firstBannerWhoAmI > -1)
            {
                var firstBanner = Main.projectile[firstBannerWhoAmI];
                firstBanner.ai[0] = 1f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var texture = ModContent.Request<Texture2D>(SoA.Circle);
            Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, Color.Orange.UseA(0) * 0.4f, 0f, texture.Size() / 2f, 2.35f + 1.4f * Projectile.ai[0], 0);
            return base.PreDraw(ref lightColor);
        }
    }
}