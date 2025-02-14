using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.ShardsUI.AreusVoid;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Imperial
{
    [AutoloadEquip(EquipType.Head)]
    public class ImperialHood : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 14;
            lesserNonSetDamage = 0.15f;

            slotType = AreusArmorChip.SlotHead;

            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.1f;
            ArmorPlayer.areusDamage += 0.08f;
            player.manaCost -= 0.12f;
            ArmorPlayer.areusHead = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ImperialGarments>() &&
                legs.type == ModContent.ItemType<ImperialGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
            ArmorPlayer.imperialSet = true;
            ModContent.GetInstance<AreusVoidSystem>().ShowBar();
            player.GetDamage(DamageClass.Generic) += ArmorPlayer.imperialVoid / 100f;
            if (ArmorPlayer.CommanderSetChip)
            {
                player.maxMinions += 1;
            }
        }
    }
}
