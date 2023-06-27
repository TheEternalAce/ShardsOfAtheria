using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class CommanderChip : ClassChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            damageClass = DamageClass.Summon;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
        }
    }
}