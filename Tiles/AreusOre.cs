using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SagesMania.Items.Placeable;

namespace SagesMania.Tiles
{
    public class AreusOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;

            drop = ModContent.ItemType<AreusOreItem>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Areus Ore");
            AddMapEntry(new Color(100, 150, 200), name);

            dustType = DustID.Electric;
            minPick = 65;
            soundType = SoundID.Tink;
            soundStyle = 1;
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
