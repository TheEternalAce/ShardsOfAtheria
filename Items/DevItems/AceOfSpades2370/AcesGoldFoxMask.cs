using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ShardsOfAtheria.Items.DevItems.AceOfSpades2370
{
    [AutoloadEquip(EquipType.Head)]
    public class AcesGoldFoxMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Ace's Gold Fox Mask");
            Tooltip.SetDefault("'Great for impersonating devs!'");
            ArmorIDs.Head.Sets.DrawBackHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}
