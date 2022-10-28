using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class EaterSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Grants one revive and shoots a vile shot when using a weapon\n" +
                "Revive has a 5 minute cooldown";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
