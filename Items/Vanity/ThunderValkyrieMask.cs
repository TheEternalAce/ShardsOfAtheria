using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class ThunderValkyrieMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.vanity = true;

            Item.rare = ItemDefaults.RarityBossMasks;
            Item.value = ItemDefaults.ValueBossMasks;
        }
    }
}
