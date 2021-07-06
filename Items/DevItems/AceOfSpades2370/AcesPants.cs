using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SagesMania.Items.DevItems.AceOfSpades2370
{
    [AutoloadEquip(EquipType.Legs)]
    public class AcesPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Ace's pants");
            Tooltip.SetDefault("'Great for impersonating devs!'\n" +
                "'Pockets not included'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }

        public override bool DrawLegs()
        {
            return false;
        }
    }
}
