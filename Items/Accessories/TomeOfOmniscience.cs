using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
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
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 22;
            Item.accessory = true;

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SlayerPlayer>().omnicientTome = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();

            tooltips.Add(new TooltipLine(Mod, "Tooltip", string.Format(Language.GetTextValue("Mods.ShardsOfAtheria.Common.TomeOfOmniscience"),
                    ShardsOfAtheria.TomeKey.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.TomeKey.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));

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

            tooltips.Add(line);
            base.ModifyTooltips(tooltips);
        }
    }
}