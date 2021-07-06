using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SagesMania.Items.DevItems.AceOfSpades2370
{
    [AutoloadEquip(EquipType.Body)]
    public class AcesJacket : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Ace's Jacket");
            Tooltip.SetDefault("'Great for impersonating devs!'\n" +
                "'Pockets included!'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
    }
}
