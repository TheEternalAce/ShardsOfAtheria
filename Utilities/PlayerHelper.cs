using ShardsOfAtheria.Items.SinfulSouls;
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
        #region sinful players
        public static SinfulPlayer Sinful(this Player player)
        {
            return player.GetModPlayer<SinfulPlayer>();
        }

        public static EnvyPlayer Envy(this Player player)
        {
            return player.GetModPlayer<EnvyPlayer>();
        }

        public static GluttonyPlayer Gluttony(this Player player)
        {
            return player.GetModPlayer<GluttonyPlayer>();
        }

        public static GreedPlayer Greed(this Player player)
        {
            return player.GetModPlayer<GreedPlayer>();
        }

        public static LustPlayer Lust(this Player player)
        {
            return player.GetModPlayer<LustPlayer>();
        }

        public static PridePlayer Pride(this Player player)
        {
            return player.GetModPlayer<PridePlayer>();
        }

        public static SlothPlayer Sloth(this Player player)
        {
            return player.GetModPlayer<SlothPlayer>();
        }

        public static WrathPlayer Wrath(this Player player)
        {
            return player.GetModPlayer<WrathPlayer>();
        }
        #endregion
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
