using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.NPCProj;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class FoodChunk : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ChickenNugget;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Food Chunk");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Item refItem = new();
            refItem.SetDefaults(ItemID.ChickenNugget);

            Projectile.width = refItem.width;
            Projectile.height = refItem.height;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!CheckActive(player))
            {
                return;
            }

            Projectile.velocity = Vector2.Normalize(Main.player[Projectile.owner].Center - Projectile.Center) * 10f;
            Projectile.rotation += 0.4f * (float)Projectile.direction;

            if (Projectile.Hitbox.Intersects(player.getRect()))
            {
                Projectile.Kill();
                player.statLife += Projectile.ai[0] == 1f ? (int)(player.statLifeMax2 * .2f) : 25;
                player.HealEffect(Projectile.ai[0] == 1f ? (int)(player.statLifeMax2 * .2f) : 25);
            }
        }

        private bool CheckActive(Player player)
        {
            if (player.dead || !player.active)
            {
                return false;
            }
            else Projectile.timeLeft = 2;
            return true;
        }
    }
}