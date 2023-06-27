using Terraria;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class SpeedChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            slotType = SlotLegs;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
            player.moveSpeed += 2f;
        }
    }
}