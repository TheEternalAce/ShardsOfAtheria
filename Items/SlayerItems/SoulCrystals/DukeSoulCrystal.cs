using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
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

            base.SetStaticDefaults();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SoulTeleport", string.Format("Press {0} to teleport", ShardsOfAtheria.SoulTeleport.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.SoulTeleport.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));

            base.ModifyTooltips(tooltips);
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().DukeSoul = true;
            }
            return base.UseItem(player);
        }
    }
}
