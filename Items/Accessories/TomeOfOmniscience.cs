using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class TomeOfOmniscience : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 22;
            Item.accessory = true;
            Item.master = true;

            Item.rare = ItemRarityID.Master;
            Item.value = Item.sellPrice(0, 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SlayerPlayer>().omnicientTome = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
            var tooltip = string.Format(Language.GetTextValue("Mods.ShardsOfAtheria.Common.TomeOfOmniscience"),
                    SoA.TomeKey.GetAssignedKeys().Count > 0 ? SoA.TomeKey.GetAssignedKeys()[0] : "[Unbounded Hotkey]");
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new(Mod, "Tooltip", tooltip));

            var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");

            if (slayer.TomeKnowledge == 0)
                line = new TooltipLine(Mod, "CurrentKnowledgeBase", Language.GetTextValue("Mods.ShardsOfAtheria.Common.Combat"))
                {
                    OverrideColor = Color.Red
                };
            if (slayer.TomeKnowledge == 1)
                line = new TooltipLine(Mod, "CurrentKnowledgeBase", Language.GetTextValue("Mods.ShardsOfAtheria.Common.Conservation"))
                {
                    OverrideColor = Color.Green
                };
            if (slayer.TomeKnowledge == 2)
                line = new TooltipLine(Mod, "CurrentKnowledgeBase", Language.GetTextValue("Mods.ShardsOfAtheria.Common.Exploration"))
                {
                    OverrideColor = Color.Blue
                };

            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            base.ModifyTooltips(tooltips);
        }
    }
}