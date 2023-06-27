using Terraria;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class FlightChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            slotType = SlotChest;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
            player.wingTimeMax += 20;
        }
    }
}