using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable
{
    public class Jade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 15;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 18;
            Item.maxStack = 9999;

            //Item.useStyle = ItemUseStyleID.Swing;
            //Item.useTime = 10;
            //Item.useAnimation = 10;
            //Item.createTile = ModContent.TileType<JadeShard>();
            //Item.consumable = true;
            //Item.useTurn = true;
            //Item.autoReuse = true;

            Item.rare = ItemDefaults.RarityDemoniteCrimtane;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
        }
    }
}