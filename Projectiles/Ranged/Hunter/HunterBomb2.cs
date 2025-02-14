using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.Hunter
{
    public class HunterBomb2 : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(28);
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 3600;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            var devCard = ToggleableTool.GetInstance<DevelopersKeyCard>(player);
            bool cardActive = devCard != null && devCard.Active;
            if (Projectile.scale < 3f) Projectile.scale += 0.1f;
            if (!cardActive && player.ownedProjectileCounts[Type] > 6)
            {
                int i = FindOldestBomb(player);
                Main.projectile[i].Kill();
                player.ownedProjectileCounts[Type]--;
            }
        }

        public void Bomb()
        {
            Projectile.ai[0] = 1;
            Projectile.timeLeft = 3600;
        }

        public static int FindOldestBomb(Player player)
        {
            int timeleft = 3600;
            int result = -1;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile bomb = Main.projectile[i];
                if (bomb.active && bomb.type == ModContent.ProjectileType<HunterBomb2>() && bomb.owner == player.whoAmI && bomb.timeLeft < timeleft)
                {
                    result = i;
                    timeleft = bomb.timeLeft;
                }
            }
            return result;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.EntitySpriteDraw(SoA.OrbBloom, Projectile.Center - Main.screenPosition, null, Color.DarkGray, 0f, SoA.OrbBloom.Size() / 2, Projectile.scale, SpriteEffects.None);
            return false;
        }
    }
}
