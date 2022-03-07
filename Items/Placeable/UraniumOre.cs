using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable
{
    public class UraniumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'You feel a little sick'\n" +
                "'Uranium isn't green, people'");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.createTile = ModContent.TileType<UraniumOreTile>();
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void UpdateInventory(Player player)
        {
            player.AddBuff(ModContent.BuffType<MildRadiationPoisoning>(), 600);
        }
    }
}