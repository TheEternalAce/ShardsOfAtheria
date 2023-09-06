using ShardsOfAtheria.Items.AreusChips;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Armor.Areus.Soldier
{
    [AutoloadEquip(EquipType.Head)]
    public class SoldierMask : AreusArmorPiece
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.defense = 6;

            slotType = AreusArmorChip.SlotHead;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.GetDamage(ArmorPlayer.classChip) += 0.05f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SoldierBreastplate>() &&
                legs.type == ModContent.ItemType<SoldierLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            ArmorPlayer.soldierSet = true;
            base.UpdateArmorSet(player);
        }
    }
}
