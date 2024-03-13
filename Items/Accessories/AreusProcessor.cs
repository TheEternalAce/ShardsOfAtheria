using BattleNetworkElements;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class AreusProcessor : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return SoA.BNEEnabled;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var shards = player.Shards();
            shards.areusProcessor = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer;
            var shards = player.Shards();

            var color = Color.White;
            var element = "";
            switch (shards.processorElement)
            {
                case Element.Fire:
                    color = Color.Firebrick;
                    element = "[i:BattleNetworkElements/FireIcon] Fire";
                    break;
                case Element.Aqua:
                    color = Color.LightSkyBlue;
                    element = "[i:BattleNetworkElements/AquaIcon] Aqua";
                    break;
                case Element.Elec:
                    color = Color.Cyan;
                    element = "[i:BattleNetworkElements/ElecIcon] Elec";
                    break;
                case Element.Wood:
                    color = Color.Green;
                    element = "[i:BattleNetworkElements/WoodIcon] Wood";
                    break;
            }
            var tooltip = new TooltipLine(Mod, "Processor Element", element)
            {
                OverrideColor = color
            };
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), tooltip);
        }
    }
}
