using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ShardsOfAtheria.Items.DevItems.AceOfSpades2370
{
    [AutoloadEquip(EquipType.Legs)]
    public class AcesPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Eternal Ace's pants");
            Tooltip.SetDefault("'Great for impersonating devs!'\n" +
                "'Pockets not included'");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}
