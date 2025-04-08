using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.AreusChips;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Imperial
{
    [AutoloadEquip(EquipType.Body)]
    public class ImperialGarments : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 26;

            slotType = AreusArmorChip.SlotChest;

            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.1f;
            ArmorPlayer.areusDamage += 0.08f;
            player.statManaMax2 += 120;
            ArmorPlayer.areusBody = true;
        }
    }
}
