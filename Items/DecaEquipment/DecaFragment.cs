using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public abstract class DecaFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unite with the other fragments to unlock their power\n" +
                "[c/FF4100:Deca Fragment]");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Red;
            Item.width = 26;
            Item.height = 26;
        }
    }
}