using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class LordSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Taking over 100 damage summons a True Eye of Cthulhu\n" +
                "You can have up to 2 of these\n" +
                "Another True EoC stays over you and attacks at your cursor";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
