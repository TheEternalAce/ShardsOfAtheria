using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class EmpressSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Increased max flight time and permanent Shine and Night Owl buffs\n" +
                "Daytime increased damage by 20% and nighttime increases defense by 20\n" +
                "Hitting enemies summons a twilight lance for a second strike";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
