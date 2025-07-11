using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Vanity
{
    [AutoloadEquip(EquipType.Body)]
    public class ZetaminShirt : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 16;
            Item.vanity = true;

            Item.rare = ItemDefaults.RarityBossMasks;
            Item.value = ItemDefaults.ValueBossMasks;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddTile(TileID.Loom)
                .Register();
        }
    }
}
