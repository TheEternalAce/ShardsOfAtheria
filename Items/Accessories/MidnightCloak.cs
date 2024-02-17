using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class MidnightCloak : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.accessory = true;
            Item.expert = true;

            Item.rare = ItemDefaults.RarityLunarPillars;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().deathCloak = true;
        }
    }
}