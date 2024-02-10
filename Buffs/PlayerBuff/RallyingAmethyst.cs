using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class RallyingAmethyst : ModBuff
    {
        public override void SetStaticDefaults()
        {
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.InCombat())
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }

            if (player.HasBuff<SwarmingAmber>())
            {
                player.maxMinions += 2;
            }
            if (player.HasBuff<TenaciousDiamond>())
            {
                player.statDefense += 15;
            }
            if (player.HasBuff<FleetingEmerald>())
            {
                player.moveSpeed += 0.2f;
            }
            if (player.HasBuff<VengefulRuby>())
            {
                player.GetDamage(DamageClass.Generic) += 0.25f;
            }
        }
    }

    public class RallyingAmethystPlayer : ModPlayer
    {
        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff<MendingTopaz>() && Player.HasBuff<RallyingAmethyst>())
            {
                Player.lifeRegen += 20;
            }
        }
    }
}
