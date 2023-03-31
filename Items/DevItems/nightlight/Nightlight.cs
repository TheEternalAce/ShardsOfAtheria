using ShardsOfAtheria.Tiles.DevFurniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DevItems.nightlight
{
    public class Nightlight : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            ModLoader.TryGetMod("excels", out Mod excels);
            return excels == null;
        }

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.rare = ItemRarityID.Cyan;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<NightlightLamp>();
        }
    }
}
