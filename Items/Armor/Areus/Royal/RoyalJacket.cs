using ShardsOfAtheria.Items.AreusChips;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Royal
{
    [AutoloadEquip(EquipType.Body)]
    public class RoyalJacket : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;

            Item.defense = 28;

            slotType = AreusArmorChip.SlotChest;

            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.15f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.15f;
            ArmorPlayer.areusDamage += 0.1f;
            player.statManaMax2 += 120;
            ArmorPlayer.areusBody = true;
        }
    }
}
