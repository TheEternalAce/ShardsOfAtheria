using ShardsOfAtheria.ShardsUI;
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

            Item.rare = ItemDefaults.RarityDungeon;
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

        public override bool CanRightClick()
        {
            var player = Main.LocalPlayer;
            var areus = player.Areus();
            return Type != ModContent.ItemType<AreusArmorChip>() && areus.areusArmorPiece;
        }

        //public override bool ConsumeItem(Player player)
        //{
        //    return Type != ModContent.ItemType<AreusArmorChip>();
        //}

        public override void RightClick(Player player)
        {
            var areusPlayer = player.Areus();
            if (areusPlayer.areusArmorPiece)
            {
                for (int i = 0; i < 3; i++)
                {
                    string chipName = areusPlayer.chipNames[i];
                    if (slotType == i)
                    {
                        if (chipName != "")
                        {
                            var modItem = SoA.Instance.Find<ModItem>(chipName);
                            int inventoryIndex = player.FindItem(Type);
                            player.inventory[inventoryIndex] = new(modItem.Type);
                        }
                        areusPlayer.chipNames[i] = Name;
                        ModContent.GetInstance<ChipsUISystem>().SetSlotItem(i, new(Type));
                        break;
                    }
                    else if (slotType == SlotAny)
                    {
                        int slotIndex = 0;
                        if (areusPlayer.chipNames[0] == "")
                        {
                            slotIndex = 0;
                        }
                        else if (areusPlayer.chipNames[1] == "")
                        {
                            slotIndex = 1;
                        }
                        else if (areusPlayer.chipNames[2] == "")
                        {
                            slotIndex = 2;
                        }
                        else
                        {
                            var modItem = SoA.Instance.Find<ModItem>(chipName);
                            int inventoryIndex = player.FindItem(Type);
                            player.inventory[inventoryIndex] = new(modItem.Type);
                        }
                        areusPlayer.chipNames[slotIndex] = Name;
                        ModContent.GetInstance<ChipsUISystem>().SetSlotItem(slotIndex, new(Type));
                        break;
                    }
                }
            }
        }

        public virtual void ChipEffect(Player player)
        {
        }
    }
}