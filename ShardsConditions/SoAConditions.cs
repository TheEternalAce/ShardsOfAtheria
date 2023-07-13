using ShardsOfAtheria.Utilities;
using Terraria;

namespace ShardsOfAtheria.ShardsConditions
{
    public static class SoAConditions
    {
        public static Condition SlayerMode = new("Mods.ShardsOfAtheria.Conditions.SlayerMode",
            () => Main.LocalPlayer.IsSlayer());
        public static Condition NotSlayerMode = new("Mods.ShardsOfAtheria.Conditions.NotSlayer",
            () => !Main.LocalPlayer.IsSlayer());
        public static Condition Upgrade = new("Mods.ShardsOfAtheria.Conditions.Upgrade", () => false);
    }
}
