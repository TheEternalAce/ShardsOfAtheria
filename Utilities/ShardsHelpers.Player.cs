using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Players;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public partial class ShardsHelpers
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

        public static GemPlayer Gem(this Player player)
        {
            return player.GetModPlayer<GemPlayer>();
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

        public static bool HasItemEquipped<T>(this Player player, out ModItem item) where T : ModItem
        {
            bool equipped = false;
            item = null;
            foreach (Item i in player.armor)
            {
                if (i.type == ModContent.ItemType<T>())
                {
                    item = i.ModItem as T;
                    equipped = true;
                    break;
                }
            }
            return equipped;
        }

        public static bool InCombat(this Player player)
        {
            return player.Shards().InCombat;
        }

        public static bool ArmorSetCooldown(this Player player)
        {
            return player.HasBuff<SetBonusCooldown>();
        }

        public static void AddBuff<T>(this Player player, int time, bool quiet = true) where T : ModBuff
        {
            player.AddBuff(ModContent.BuffType<T>(), time, quiet);
        }
        public static void ClearBuff<T>(this Player player) where T : ModBuff
        {
            player.ClearBuff(ModContent.BuffType<T>());
        }

        public static float CappedMeleeScale(this Player player)
        {
            var item = player.HeldItem;
            return Math.Clamp(player.GetAdjustedItemScale(item), 0.5f * item.scale, 2f * item.scale);
        }
    }
}
