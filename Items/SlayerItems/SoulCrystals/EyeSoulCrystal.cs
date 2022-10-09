using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Tools;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class EyeSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Summon 3 Servants after 5 seconds, these Servants will chase down enemies\n" +
                "Also creates an All Seeing Eye that lights up the cursor and marks enemies, making them take 10% more damage";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Eye Of Cthulhu)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
