using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable
{
    public class BionicBarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 59;

            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 9999;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.rare = ItemDefaults.RarityDemoniteCrimtane;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;

            Item.createTile = ModContent.TileType<BionicBar>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicOreItem>(), 4)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}