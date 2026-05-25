using ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor;
using ShardsOfAtheria.Buffs.PlayerBuff.OnHitBuffs;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings
{
    public class EfficientAmethyst : ModBuff
    {
        public static readonly int[] BlacklistedBuffs = [
            // Gem buffs
            ModContent.BuffType<CunningSapphire>(),
            ModContent.BuffType<DiamondBarrierBuff>(),
            ModContent.BuffType<EfficientAmethyst>(),
            ModContent.BuffType<FleetingEmerald>(),
            ModContent.BuffType<MendingTopaz>(),
            ModContent.BuffType<TenaciousDiamond>(),
            ModContent.BuffType<VengefulRuby>(),

            ModContent.BuffType<PartisanBuff>(),
            ModContent.BuffType<ThousandStrikes>(),

            ModContent.BuffType<ChargedMinions>(),
            ModContent.BuffType<ChargingDrones>(),
            ModContent.BuffType<ShadeState>(),

            ModContent.BuffType<MessiahRekkoha>(),
            ];

        public override void Update(Player player, ref int buffIndex)
        {
            for (int i = 0; i < player.CountBuffs(); i++)
            {
                int buffID = player.buffType[i];
                if (!BlacklistedBuffs.Contains(buffID) && !Main.debuff[buffID] && !BuffID.Sets.TimeLeftDoesNotDecrease[buffID] && !Main.buffNoTimeDisplay[buffID])
                {
                    player.buffTime[i] += 1;
                    if (player.buffTime[i] < 60)
                    {
                        player.buffTime[i] = 60;
                    }
                }
            }
        }
    }
}
