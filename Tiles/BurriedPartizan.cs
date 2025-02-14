using ShardsOfAtheria.Items.Weapons.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ShardsOfAtheria.Tiles
{
    public class BurriedPartizan : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
            TileObjectData.newTile.DrawFlipHorizontal = false;
            TileObjectData.addTile(Type);

            // Etc
            AddMapEntry(SoA.AreusColor, CreateMapEntryName());
            RegisterItemDrop(ModContent.ItemType<FuckEarlyGameHarpies>(), 0);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 64, ItemID.StoneBlock, 3);
        }
    }
}
