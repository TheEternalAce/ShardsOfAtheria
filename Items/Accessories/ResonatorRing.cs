using BattleNetworkElements;
using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    [JITWhenModsEnabled("BattleNetworkElements")]
    public class ResonatorRing : ModItem
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
            Item.width = 16;
            Item.height = 22;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            float[] playerElements = player.ElementMultipliers();
            string elementInfo = player.name + "'s Elemental Multipliers\n" +
                "[i:BattleNetworkElements/FireIcon] Fire: " + playerElements[Element.Fire] + "x\n" +
                "[i:BattleNetworkElements/AquaIcon] Aqua: " + playerElements[Element.Aqua] + "x\n" +
                "[i:BattleNetworkElements/ElecIcon] Elec: " + playerElements[Element.Elec] + "x\n" +
                "[i:BattleNetworkElements/WoodIcon] Wood: " + playerElements[Element.Wood] + "x";
            tooltips.Insert(ShardsHelpers.GetIndex(tooltips, "OneDropLogo"), new(Mod, "ElementInfo", elementInfo));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().resonator = true;
        }

        public static float ModifyElements(Player player, object source, object victim)
        {
            float multiplier = 1f;
            var shards = player.Shards();
            if (shards.resonator)
            {
                float Base = ElementHelper.MultiplyDamage(source, victim);
                if (Base > 1f)
                {
                    multiplier = 2f;
                }
                else if (Base < 1f)
                {
                    multiplier = 0.5f;
                }
            }
            return multiplier;
        }
    }
}