using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Tiles
{
    public class GlitchTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;

            ItemDrop = ModContent.ItemType<GlitchItem>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Glitch");
            AddMapEntry(new Color(255, 160, 160), name);

            DustType = DustID.Asphalt;
            MinPick = 0;
        }
        public override void FloorVisuals(Player player)
        {
            player.AddBuff(BuffID.Confused, 5*60);
        }
    }
}
