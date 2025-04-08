using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable
{
    public class BurriedPartizanItem : ModItem
    {
        public override void SetStaticDefaults()
        {
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

            Item.createTile = ModContent.TileType<BurriedPartizan>();
            //Item.placeStyle = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FuckEarlyGameHarpies>())
                .AddIngredient(ItemID.StoneBlock, 3)
                .AddTile(TileID.HeavyWorkBench)
                .AddCondition(Condition.InGraveyard)
                .Register();
        }
    }
}