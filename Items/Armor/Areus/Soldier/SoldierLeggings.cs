using ShardsOfAtheria.Items.AreusChips;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Soldier
{
    [AutoloadEquip(EquipType.Legs)]
    public class SoldierLeggings : AreusArmorPiece
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;

            Item.defense = 12;

            slotType = AreusArmorChip.SlotLegs;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.08f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.08f;
            ArmorPlayer.areusDamage += 0.05f;
            player.moveSpeed += 0.13f;
            ArmorPlayer.areusLegs = true;
        }
    }
}
