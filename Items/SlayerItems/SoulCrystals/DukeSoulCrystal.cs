using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class DukeSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Duke Fishron)");
            Tooltip.SetDefault("Increased max flight time\n" +
                "Summon a Sharknado over your head");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");
            var line2 = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");

            tooltips.Add(new TooltipLine(Mod, "SoulTeleport", string.Format("Press {0} to teleport", ShardsOfAtheria.SoulTeleport.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.SoulTeleport.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));
            if (!ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
                line = new TooltipLine(Mod, "SoulCrystal", "Hold left click for 5 seconds to absorb the soul inside, this grants you this boss's powers")
                {
                    OverrideColor = Color.Purple
                };
            else line = new TooltipLine(Mod, "SoulCrystal", "Use to absorb the soul inside, this grants you this boss's powers")
            {
                OverrideColor = Color.Purple
            };
            line2 = new TooltipLine(Mod, "Slayer Item", "Slayer Item")
            {
                OverrideColor = Color.Red
            };
            tooltips.Add(line);
            tooltips.Add(line2);
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().DukeSoul = SoulCrystalStatus.Absorbed;
            }
            return base.UseItem(player);
        }
    }
}
