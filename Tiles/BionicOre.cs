using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SagesMania.Items.Placeable;

namespace SagesMania.Tiles
{
    public class BionicOre : ModTile
    {
        public override void SetDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileValue[Type] = 410;

            drop = ModContent.ItemType<BionicOreItem>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Bionic Ore");
            AddMapEntry(new Color(100, 100, 100), name);

            dustType = DustID.Platinum;
            soundType = SoundID.Tink;
            soundStyle = 1;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}
