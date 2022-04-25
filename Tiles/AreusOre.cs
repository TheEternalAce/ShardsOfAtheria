using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Tiles
{
    public class AreusOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileOreFinderPriority[Type] = 725;

            ItemDrop = ModContent.ItemType<AreusShard>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Areus Ore");
            AddMapEntry(new Color(100, 150, 200), name);

            DustType = DustID.Electric;
            MinPick = 65;
            SoundType = SoundID.Tink;
            SoundStyle = 1;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0f;
            g = 1f;
            b = 1f;
        }

        public override void FloorVisuals(Player player)
        {
            player.AddBuff(BuffID.Electrified, 5*60);
        }
    }
}
