using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public class AmethystDagger : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 46;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
            Item.shoot = ModContent.ProjectileType<AmethystDaggerProjectile>();
        }

        public override bool CanUseItem(Player player)
        {
            return !player.immune && !player.creativeGodMode;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = player.Center + new Vector2(100, 0) * player.direction;
            velocity = player.Center - position;
            velocity.Normalize();
            velocity *= Item.shootSpeed;
        }
    }
}