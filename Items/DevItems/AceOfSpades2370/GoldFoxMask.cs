using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SagesMania.Items.DevItems.AceOfSpades2370
{
    [AutoloadEquip(EquipType.Head)]
    public class GoldFoxMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AceOfSpades' Gold Fox Mask");
            Tooltip.SetDefault("'Great for impersonating devs!'");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
        
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
        }
    }
}
