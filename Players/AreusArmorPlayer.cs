using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class AreusArmorPlayer : ModPlayer
    {
        public string[] chipNames = ShardsHelpers.SetEmptyStringArray(3);

        public bool areusArmorPiece;
        public DamageClass classChip;

        public override void SaveData(TagCompound tag)
        {
            tag.Add(nameof(chipNames), chipNames.ToList());
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey(nameof(chipNames)))
            {
                chipNames = tag.GetList<string>(nameof(chipNames)).ToArray();
            }
        }

        public override void ResetEffects()
        {
            areusArmorPiece = false;
            classChip = DamageClass.Generic;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (areusArmorPiece)
            {
                if (Main.playerInventory)
                {
                    ModContent.GetInstance<ChipsUISystem>().ShowChips();
                }
            }
        }

        public override void UpdateEquips()
        {
            base.UpdateEquips();
        }
    }
}
