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
            DisplayName.SetDefault("The Eternal Ace's Gold Fox Mask");
            Tooltip.SetDefault("'Great for impersonating devs!'");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
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
