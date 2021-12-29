using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Tiles
{
    public class PhaseOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileOreFinderPriority[Type] = 285;

            ItemDrop = ModContent.ItemType<PhaseOreItem>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Phase Ore");
            AddMapEntry(new Color(255, 0, 255), name);

            DustType = DustID.GemAmethyst;
            MinPick = 50;
            SoundType = SoundID.Tink;
            SoundStyle = 1;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 0f;
            b = 1f;
        }
    }
}
