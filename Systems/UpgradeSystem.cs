using System.Collections.Generic;
using Terraria.ModLoader;

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
            if (atherianUpgrades.ContainsKey(playerName) && atherianUpgrades[playerName][1] > 0) return false;
            return true;
        }
        public static bool UpgradeInProgress(string playerName, out int itemType)
        {
            itemType = 0;
            if (!atherianUpgrades.ContainsKey(playerName)) return false;
            itemType = atherianUpgrades[playerName][0];
            if (atherianUpgrades[playerName][1] <= 0) return false;
            return true;
        }
        public static bool UpgradeInProgress(string playerName, out int itemType, out int timeLeft)
        {
            itemType = 0;
            timeLeft = 0;
            if (!atherianUpgrades.ContainsKey(playerName)) return false;
            itemType = atherianUpgrades[playerName][0];
            timeLeft = atherianUpgrades[playerName][1];
            if (timeLeft <= 0) return false;
            return true;
        }

        // Loading isn't working for some reason
        // Since upgrades [usually] take only 20 seconds hopefully that's not an issue
        //public override void LoadWorldData(TagCompound tag)
        //{
        //    if (tag.ContainsKey("atherianUpgradesKeys") && tag.ContainsKey("atherianUpgradesValues"))
        //    {
        //        var keys = tag.GetList<string>("atherianUpgradesKeys");
        //        var values = tag.GetList<int[]>("atherianUpgradesValues");
        //        for (int i = 0; i < keys.Count; i++)
        //        {
        //            atherianUpgrades.Add(keys[i], values[i]);
        //        }
        //    }
        //}
        //public override void SaveWorldData(TagCompound tag)
        //{
        //    tag["atherianUpgradesKeys"] = atherianUpgrades.Keys;
        //    tag["atherianUpgradesValues"] = atherianUpgrades.Values;
        //}

        //public override void PostUpdateWorld()
        //{
        //    foreach (var item in atherianUpgrades)
        //    {
        //        item.Value[1]--;
        //    }
        //}
    }
}
