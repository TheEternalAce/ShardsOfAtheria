using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class AreusArmorChip : ModItem
    {
        public const int SlotAny = -1;
        public const int SlotHead = 0;
        public const int SlotChest = 1;
        public const int SlotLegs = 2;

        public int slotType { get; internal set; } = SlotAny;

        public override string Texture => "ShardsOfAtheria/Items/AreusChips/AreusArmorChip";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.rare = ItemDefaults.RarityAreus;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string chipType = "";
            switch (slotType)
            {
                case SlotAny:
                    chipType = "Any slot";
                    break;
                case SlotHead:
                    chipType = "Head slot";
                    break;
                case SlotChest:
                    chipType = "Chest slot";
                    break;
                case SlotLegs:
                    chipType = "Leg slot";
                    break;
            }
            TooltipLine line = new(Mod, "ChipType", chipType);
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
        }

        public virtual void ChipEffect(Player player)
        {
        }
    }
}