using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Players;
using System;
using Terraria;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsPlayerHelper
    {
        public static ShardsPlayer Shards(this Player player)
        {
            return player.GetModPlayer<ShardsPlayer>();
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
        public static bool IsSlayer(this Player player)
        {
            return player.Slayer().slayerMode;
        }
        public static bool HasSoulCrystal(this Player player, string crystalName)
        {
            return player.Slayer().HasSoulCrystal(crystalName);
        }

        public static AreusArmorPlayer Areus(this Player player)
        {
            return player.GetModPlayer<AreusArmorPlayer>();
        }
        public static bool HasChipEquipped(this Player player, int chip)
        {
            foreach (string name in player.Areus().chipNames)
            {
                var item = new Item(chip);
                if (item.ModItem != null)
                {
                    if (item.ModItem.Name == name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool HasItemEquipped(this Player player, int item)
        {
            bool equipped = false;
            foreach (Item i in player.armor)
            {
                if (i.type == item)
                {
                    equipped = true;
                    break;
                }
            }
            return equipped;
        }

        public static float CappedMeleeScale(this Player player)
        {
            var item = player.HeldItem;
            return Math.Clamp(player.GetAdjustedItemScale(item), 0.5f * item.scale, 2f * item.scale);
        }
    }
}
