﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Tiles
{
    public class BionicOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 240;

            RegisterItemDrop(ModContent.ItemType<BionicOreItem>());

            AddMapEntry(new Color(100, 100, 100), Language.GetText("MapObject.BionicOre.MapEntry"));

            DustType = DustID.Platinum;
            HitSound = SoundID.Tink;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}
