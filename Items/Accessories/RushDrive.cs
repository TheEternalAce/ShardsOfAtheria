using Humanizer;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class RushDrive : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.value = 150000;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string lineText = this.GetLocalization("ChangePhase").Value.FormatWith(SoA.PhaseSwitch.GetAssignedKeys().FirstOrDefault());
            TooltipLine line = new(Mod, "ChangePhaseType", lineText);
            tooltips.AddTooltip(line);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().rushDrive = true;
        }
    }
}