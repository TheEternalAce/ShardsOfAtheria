using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.Hunter
{
    public class HunterBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(28);
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 360;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 15) Projectile.velocity *= 0.99f;
            Projectile.rotation += Vector2.Normalize(Projectile.velocity).Length() * 0.05f;
            foreach (var proj in Main.projectile)
            {
                if (proj.friendly &&
                    proj.whoAmI != Projectile.whoAmI &&
                    proj.owner == Projectile.owner &&
                    (proj.aiStyle == 1 || proj.aiStyle == 0) &&
                    proj.damage > 0 &&
                    proj.active &&
                    proj.arrow)
                {
                    if (proj.Distance(Projectile.Center) <= 15)
                    {
                        proj.Kill();
                        Projectile.Kill();
                        break;
                    }
                }
            }
            var devCard = ToggleableTool.GetInstance<DevelopersKeyCard>(player);
            bool cardActive = devCard != null && devCard.Active;
            if (!cardActive && player.ownedProjectileCounts[Type] > 6)
            {
                int i = FindOldestBomb(player);
                Main.projectile[i].Kill();
                player.ownedProjectileCounts[Type]--;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                    ModContent.ProjectileType<HunterSingularity>(), Projectile.damage / 2, 0f);
        }

        public static int FindOldestBomb(Player player)
        {
            int timeleft = 3600;
            int result = -1;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile bomb = Main.projectile[i];
                if (bomb.active && bomb.type == ModContent.ProjectileType<HunterBomb>() && bomb.owner == player.whoAmI &&
                    bomb.ai[0] == 0 && bomb.timeLeft < timeleft)
                {
                    result = i;
                    timeleft = bomb.timeLeft;
                }
            }
            return result;
        }
    }
}
