using ShardsOfAtheria.Items.AreusChips;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Guard
{
    [AutoloadEquip(EquipType.Legs)]
    public class GuardLeggings : AreusArmorPiece
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;

            Item.defense = 6;

            slotType = AreusArmorChip.SlotLegs;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.05f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.05f;
            ArmorPlayer.areusDamage += 0.03f;
            ArmorPlayer.areusLegs = true;
        }
    }
}
