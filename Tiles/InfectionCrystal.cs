using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Tiles
{
    public class InfectionCrystal : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileOreFinderPriority[Type] = 285;

            ItemDrop = ModContent.ItemType<CrystalInfection>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Infection Crystal");
            AddMapEntry(new Color(255, 140, 0), name);

            DustType = DustID.Honey;
            MinPick = 100;
            SoundType = SoundID.Tink;
            SoundStyle = 1;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = .5f;
            b = 0f;
        }
    }
}
