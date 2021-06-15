using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SagesMania.Items.Placeable;

namespace SagesMania.Tiles
{
    public class GlitchTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;

            drop = ModContent.ItemType<GlitchItem>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Glitch");
            AddMapEntry(new Color(255, 160, 160), name);

            dustType = DustID.Asphalt;
            minPick = 0;
        }
        public override void FloorVisuals(Player player)
        {
            player.AddBuff(BuffID.Confused, 5*60);
        }
    }
}
