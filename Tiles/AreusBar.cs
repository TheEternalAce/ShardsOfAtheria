using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using Terraria.ObjectData;

namespace ShardsOfAtheria.Tiles
{
    public class AreusBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Areus Bar");
            AddMapEntry(new Color(100, 150, 200), name);

            DustType = DustID.Electric;
            SoundType = SoundID.Tink;
            SoundStyle = 1;
        }

        public override bool Drop(int i, int j)
        {
            Tile t = Main.tile[i, j];
            int style = t.frameX / 18;
            if (style == 0) // It can be useful to share a single tile with multiple styles. This code will let you ItemDrop the appropriate bar if you had multiple.
            {
                Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<AreusBarItem>());
            }
            return base.Drop(i, j);
        }
    }
}
