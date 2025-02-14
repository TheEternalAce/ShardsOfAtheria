using ShardsOfAtheria.Items.AreusChips;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Imperial
{
    [AutoloadEquip(EquipType.Legs)]
    public class ImperialGreaves : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 14;

            slotType = AreusArmorChip.SlotLegs;

            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.1f;
            ArmorPlayer.areusDamage += 0.08f;
            player.moveSpeed += 0.16f;
            ArmorPlayer.areusLegs = true;
        }
    }
}
