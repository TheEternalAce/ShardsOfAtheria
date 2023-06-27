using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class RangerChip : ClassChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            damageClass = DamageClass.Ranged;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
        }
    }
}