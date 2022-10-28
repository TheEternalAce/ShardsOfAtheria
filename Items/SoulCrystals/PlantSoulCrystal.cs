using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class PlantSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Spawn up to 8 tentacles over the course of 40 seconds\n" +
                "Attacks fire a petal that inflicts venom\n" +
                "Passive 15% increase in movement speed, 10% damage increase and increased life regen";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
