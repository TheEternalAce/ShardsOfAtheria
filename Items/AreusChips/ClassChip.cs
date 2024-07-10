using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public abstract class ClassChip : AreusArmorChip
    {
        public DamageClass DamageClass { get; internal set; } = DamageClass.Default;

        public override ModItem Clone(Item newEntity)
        {
            var clone = (ClassChip)base.Clone(newEntity);
            clone.DamageClass = DamageClass;
            return clone;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.maxStack = 1;
            slotType = SlotHead;
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

        public override void UpdateChip(Player player)
        {
            var armorPlayer = player.Areus();
            armorPlayer.classChip = DamageClass;
        }
    }
}