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
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Slayer().omnicientTome = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SlayerPlayer slayer = Main.LocalPlayer.Slayer();
            TooltipLine line = new(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");
            if (slayer.TomeKnowledge == 0)
                line = new(Mod, "CurrentKnowledgeBase", Language.GetTextValue(SoA.LocalizeCommon + "Combat"))
                {
                    OverrideColor = Color.Red
                };
            if (slayer.TomeKnowledge == 1)
                line = new(Mod, "CurrentKnowledgeBase", Language.GetTextValue(SoA.LocalizeCommon + "Conservation"))
                {
                    OverrideColor = Color.Green
                };
            if (slayer.TomeKnowledge == 2)
                line = new(Mod, "CurrentKnowledgeBase", Language.GetTextValue(SoA.LocalizeCommon + "Exploration"))
                {
                    OverrideColor = Color.Blue
                };

            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            base.ModifyTooltips(tooltips);
        }
    }
}