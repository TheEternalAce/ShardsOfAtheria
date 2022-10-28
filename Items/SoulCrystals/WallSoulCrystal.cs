using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class WallSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Summon 5 friendly The Hungry over the course of 5 seconds";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
