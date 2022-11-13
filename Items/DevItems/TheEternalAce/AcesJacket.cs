using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DevItems.TheEternalAce
{
    [AutoloadEquip(EquipType.Body)]
    public class AcesJacket : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Eternal Ace's Jacket");
            Tooltip.SetDefault("'Great for impersonating devs!'\n" +
                "'Pockets included!'");

            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}
