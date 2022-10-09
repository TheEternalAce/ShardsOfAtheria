using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class DestroyerSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Summon a Destroyer's Probe that fires at your cursor besides you\n" +
                "Taking over 100 damage spawns another temporary Destroyer's Probe that will fire at nearby enemies\n" +
                "You can have up to five of these \"attack probes\"";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (The Destroyer)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
