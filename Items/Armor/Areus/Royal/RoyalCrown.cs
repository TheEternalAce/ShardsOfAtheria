using ShardsOfAtheria.Items.AreusChips;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Royal
{
    [AutoloadEquip(EquipType.Head)]
    public class RoyalCrown : AreusArmorPiece
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.defense = 20;

            slotType = AreusArmorChip.SlotHead;

            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.15f;
            player.GetCritChance(ArmorPlayer.classChip) += 0.15f;
            ArmorPlayer.areusDamage += 0.1f;
            player.manaCost -= 0.15f;
            ArmorPlayer.areusHead = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RoyalJacket>() &&
                legs.type == ModContent.ItemType<RoyalGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            ArmorPlayer.royalSet = true;
            base.UpdateArmorSet(player);
        }
    }
}
