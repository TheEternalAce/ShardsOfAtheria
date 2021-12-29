using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Tiles
{
    public class ExampleBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;

            ItemDrop = ModContent.ItemType<ExampleBlockItem>();

            DustType = DustID.Asphalt;
            MinPick = 0;
        }
    }
}
