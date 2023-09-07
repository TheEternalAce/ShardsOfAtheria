using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Placeable.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ShardsOfAtheria.Tiles.Crafting
{
    public class AreusFabricator : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.addTile(Type);
            RegisterItemDrop(ModContent.ItemType<AreusFabricatorItem>());
            AddMapEntry(new Color(200, 200, 200), CreateMapEntryName());
            DustType = ModContent.DustType<AreusDust>();
            TileID.Sets.DisableSmartCursor[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}