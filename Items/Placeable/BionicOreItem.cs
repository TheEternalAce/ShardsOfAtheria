using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable
{
    public class BionicOreItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemIconPulse[Item.type] = true;

            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.rare = ItemDefaults.RarityDemoniteCrimtane;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;

            Item.createTile = ModContent.TileType<BionicOre>();
        }
    }
}