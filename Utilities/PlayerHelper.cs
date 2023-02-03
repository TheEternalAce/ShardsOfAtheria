using ShardsOfAtheria.Players;
using System;
using Terraria;

namespace ShardsOfAtheria.Utilities
{
    public static class PlayerHelper
    {
        public static ShardsPlayer ShardsOfAtheria(this Player player)
        {
            return player.GetModPlayer<ShardsPlayer>();
        }

        public static OverchargePlayer Overcharged(this Player player)
        {
            return player.GetModPlayer<OverchargePlayer>();
        }

        public static SinfulPlayer Sinful(this Player player)
        {
            return player.GetModPlayer<SinfulPlayer>();
        }

        public static SlayerPlayer Slayer(this Player player)
        {
            return player.GetModPlayer<SlayerPlayer>();
        }

        public static float CappedMeleeScale(this Player player)
        {
            var item = player.HeldItem;
            return Math.Clamp(player.GetAdjustedItemScale(item), 0.5f * item.scale, 2f * item.scale);
        }
    }
}
