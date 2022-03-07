using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Tiles
{
    public class UraniumOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileOreFinderPriority[Type] = 725;

            ItemDrop = ModContent.ItemType<UraniumOre>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Uranium Ore");
            AddMapEntry(new Color(150, 150, 200), name);

            DustType = DustID.Platinum;
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
            player.AddBuff(ModContent.BuffType<MildRadiationPoisoning>(), 300);
        }
    }
}
