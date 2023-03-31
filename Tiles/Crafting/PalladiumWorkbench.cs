using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ShardsOfAtheria.Tiles.Crafting
{
    public class PalladiumWorkbench : ModTile
    {
        public override void SetStaticDefaults()
        {
            AdjTiles = new int[] { TileID.WorkBenches, TileID.HeavyWorkBench, TileID.MythrilAnvil, TileID.Anvils };
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.CoordinateHeights = new[] { 18 };
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Palladium Workbench");
            AddMapEntry(new Color(200, 200, 200), name);
            DustType = DustID.Cobalt;
            TileID.Sets.DisableSmartCursor[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<PalladiumWorkbenchItem>());
        }
    }
}