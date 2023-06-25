using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class WarriorChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            damageClass = DamageClass.Melee;
            slotType = SlotHead;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
        }
    }
}