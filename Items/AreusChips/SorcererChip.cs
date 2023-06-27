using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class SorcererChip : ClassChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            damageClass = DamageClass.Magic;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
        }
    }
}