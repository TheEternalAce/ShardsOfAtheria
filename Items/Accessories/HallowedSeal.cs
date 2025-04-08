using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class HallowedSeal : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.accessory = true;
            Item.defense = 5;

            Item.rare = ItemDefaults.RarityNova;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var shards = player.Shards();
            player.GetDamage(DamageClass.Melee) += 0.05f;
            player.GetDamage(DamageClass.Magic) += 0.05f;
            player.noFallDmg = true;
            shards.hallowedSeal = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RedHusk)
                .AddIngredient(ItemID.IronBar, 15)
                .AddIngredient(ItemID.ManaCrystal, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}