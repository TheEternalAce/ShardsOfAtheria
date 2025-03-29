using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    [AutoloadEquip(EquipType.Back)]
    public class PlaguePack : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.accessory = true;

            Item.rare = ItemRarityID.Quest;
            Item.value = 100000;
        }
    }
}