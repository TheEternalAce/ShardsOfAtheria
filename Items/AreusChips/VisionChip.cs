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
            player.nightVision = true;
            player.dangerSense = true;
            player.detectCreature = true;
            player.buffImmune[BuffID.NightOwl] = true;
            player.buffImmune[BuffID.Hunter] = true;
            player.buffImmune[BuffID.Dangersense] = true;
            player.GetCritChance(DamageClass.Generic) += 0.15f;
        }
    }
}