using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Systems
{
    public class UpgradeSystem : ModSystem
    {
        public static readonly Dictionary<string, int[]> atherianUpgrades = [];

        public static void StartUpgrade(string playerName, int itemType, int time = 1200)
        {
            atherianUpgrades[playerName] = [itemType, time];
        }
        public static bool UpgradeReady(string playerName)
        {
            if (atherianUpgrades.TryGetValue(playerName, out int[] value) && value[1] > 0) return false;
            return true;
        }
        public static bool UpgradeInProgress(string playerName, out int itemType)
        {
            itemType = 0;
            if (!atherianUpgrades.TryGetValue(playerName, out int[] value)) return false;
            itemType = value[0];
            if (value[1] <= 0) return false;
            return true;
        }
        public static bool UpgradeInProgress(string playerName, out int itemType, out int timeLeft)
        {
            itemType = 0;
            timeLeft = 0;
            if (!atherianUpgrades.TryGetValue(playerName, out int[] value)) return false;
            itemType = value[0];
            timeLeft = value[1];
            if (timeLeft <= 0) return false;
            return true;
        }

        // Loading isn't working for some reason
        // Since upgrades [usually] take only 20 seconds hopefully that's not an issue
        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.TryGet("atherianUpgradesKeys", out List<string> keys) && tag.TryGet("atherianUpgradesValues", out List<int[]> values))
            {
                for (int i = 0; i < keys.Count; i++)
                {
                    atherianUpgrades.TryAdd(keys[i], values[i]);
                }
            }
        }
        public override void SaveWorldData(TagCompound tag)
        {
            tag["atherianUpgradesKeys"] = atherianUpgrades.Keys.ToList();
            tag["atherianUpgradesValues"] = atherianUpgrades.Values.ToList();
        }

        public override void PostUpdateWorld()
        {
            foreach (var item in atherianUpgrades)
            {
                item.Value[1]--;
                if (item.Value[1] == 1)
                {
                    Item item1 = new Item(item.Value[0]);
                    Main.NewText($"[i:{item.Value[0]}] {item1.Name} is ready!");
                }
            }
        }
    }
}
