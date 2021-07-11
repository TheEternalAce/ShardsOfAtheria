using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
    public class RushDrive : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gives the user a ''phase 2'' when below 50% life\n" +
                "Press 'Toggle Phase Type' to chose between two phase types:\n" +
                "Offensive: Sacrifice half of total defense for doubled damage and 20% increased crit chance\n" +
                "Defensive: Sacrifice half of total damage for doubled defense and 20% reduced damage\n" +
                "Always get 20% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SMPlayer>().rushDrive = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Special Item", "[c/FF6400:Special Item]"));
        }
    }
}