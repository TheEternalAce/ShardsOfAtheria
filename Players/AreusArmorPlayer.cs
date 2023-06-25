using ShardsOfAtheria.ShardsUI;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class AreusArmorPlayer : ModPlayer
    {
        public int[] chips = new int[3];

        public bool areusArmorPiece;
        public DamageClass classChip;

        public override void SaveData(TagCompound tag)
        {
            tag[nameof(chips)] = chips;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey(nameof(chips)))
            {
                chips = tag.GetIntArray(nameof(chips));
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
