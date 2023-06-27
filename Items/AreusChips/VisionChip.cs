using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class VisionChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            slotType = SlotHead;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
            player.AddBuff(BuffID.Shine, 2);
            player.AddBuff(BuffID.NightOwl, 2);
            player.AddBuff(BuffID.Hunter, 2);
            player.AddBuff(BuffID.Dangersense, 2);
            player.GetCritChance(DamageClass.Generic) += 0.15f;
        }
    }
}