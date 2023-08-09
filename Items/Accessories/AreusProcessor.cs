using BattleNetworkElements;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class AreusProcessor : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return SoA.ElementModEnabled;
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

            Item.rare = ItemRarityID.Cyan;
            Item.value = 80000;
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

            var key = SoA.OverdriveKey.GetAssignedKeys().Count > 0 ?
                SoA.OverdriveKey.GetAssignedKeys()[0] : "[Unbound Hotkey]";
            var tooltip = new TooltipLine(Mod, "Processor Element", $"Press {key} to switch element");
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), tooltip);

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
            tooltip = new TooltipLine(Mod, "Processor Element", element)
            {
                OverrideColor = color
            };
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), tooltip);
        }
    }
}
