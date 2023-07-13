using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class BreakerBladePlayer : ModPlayer
    {
        public string[] materiaSlots = ShardsHelpers.SetEmptyStringArray(2);

        public override void SaveData(TagCompound tag)
        {
            tag.Add(nameof(materiaSlots), materiaSlots.ToList());
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey(nameof(materiaSlots)))
            {
                materiaSlots = tag.GetList<string>(nameof(materiaSlots)).ToArray();
            }
        }
    }
}
