using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            Main.tileOreFinderPriority[Type] = 260;

            ItemDrop = ModContent.ItemType<AreusShard>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Areus Shard");
            AddMapEntry(new Color(100, 150, 200), name);

            DustType = ModContent.DustType<AreusDust>();
            MinPick = 65;
            HitSound = SoundID.Tink;
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
            player.AddBuff(ModContent.BuffType<ElectricShock>(), 5 * 60);
        }
    }
}
