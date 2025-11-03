using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Players;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public partial class ShardsHelpers
    {
        public static bool TryShards(this Player player, out ShardsPlayer shards)
        {
            return player.TryGetModPlayer(out shards);
        }
        public static ShardsPlayer Shards(this Player player)
        {
            return player.GetModPlayer<ShardsPlayer>();
        }

        public static SinnerPlayer Sinner(this Player player)
        {
            return player.GetModPlayer<SinnerPlayer>();
        }

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

        public static bool Overdrive(this Player player, int minOverdrive = 0, int maxOverdrive = 300)
        {
            var shards = player.Shards();
            return shards.Overdrive && shards.overdriveTimeCurrent >= minOverdrive && shards.overdriveTimeCurrent <= maxOverdrive;
        }

        public static void RestoreMana(this Player player, int amount)
        {
            player.ManaEffect(amount);
            player.statMana += amount;
        }

        public static void TryClearBuff(this Player player, int buffID)
        {
            if (player.HasBuff(buffID)) player.ClearBuff(buffID);
        }
        public static void TryClearBuff<T>(this Player player) where T : ModBuff
        {
            if (player.HasBuff<T>()) player.ClearBuff<T>();
        }

        public static int ApplyAttackSpeed(this Player player, int baseTime, DamageClass damage, int minTime = 0, float capAttackSpeedAt = 1f - float.Epsilon)
        {
            float attackSpeed = Math.Min(player.GetTotalAttackSpeed(damage) - 1f, capAttackSpeedAt);
            int time = baseTime - (int)(baseTime * attackSpeed);
            if (time < minTime) time = minTime;
            return time;
        }

        public static bool HasChipEquipped(this Player player, int chip)
        {
            foreach (string name in player.Areus().chipNames)
            {
                var item = new Item(chip);
                if (item.ModItem != null && item.ModItem.Name == name)
                    return true;
            }
            return false;
        }

        public static bool HasItemEquipped<T>(this Player player, out ModItem modItem, bool allowVanity = false) where T : ModItem
        {
            modItem = null;
            for (int i = 0; i < player.armor.Length; i++)
            {
                Item item = player.armor[i];
                if (item.type == ModContent.ItemType<T>())
                {
                    modItem = item.ModItem as T;
                    return true;
                }
                if (i > 7 + player.extraAccessorySlots && !allowVanity) return false;
            }
            return false;
        }

        public static bool HasItemEquipped<T>(this Player player, bool allowVanity = false) where T : ModItem
        {
            for (int i = 0; i < player.armor.Length; i++)
            {
                Item item = player.armor[i];
                if (item.type == ModContent.ItemType<T>()) return true;
                if (i > 7 + player.extraAccessorySlots && !allowVanity) return false;
            }
            return false;
        }

        public static bool InCombat(this Player player)
        {
            return player.Shards().InCombat;
        }

        public static bool ArmorSetCooldown(this Player player)
        {
            return player.HasBuff<SetBonusCooldown>();
        }

        public static bool HasItem<T>(this Player player) where T : ModItem
        {
            return player.HasItem(ModContent.ItemType<T>());
        }

        public static bool HasItem<T>(this Player player, Item[] collection) where T : ModItem
        {
            return player.HasItem(ModContent.ItemType<T>(), collection);
        }

        public static int FindItem<T>(this Player player) where T : ModItem
        {
            return player.FindItem(ModContent.ItemType<T>());
        }

        public static int FindItem<T>(this Player player, Item[] collection) where T : ModItem
        {
            return player.FindItem(ModContent.ItemType<T>(), collection);
        }

        public static float CappedMeleeScale(this Player player)
        {
            var item = player.HeldItem;
            return Math.Clamp(player.GetAdjustedItemScale(item), 0.5f * item.scale, 2f * item.scale);
        }

        /// <summary>
        /// Index 0: Fire <br/>
        /// Index 1: Aqua <br/>
        /// Index 2: Elec <br/>
        /// Index 3: Wood
        /// </summary>
        /// <param name="player"></param>
        /// <param name="element"></param>
        /// <param name="mod"></param>
        public static void ModifyElementMultiplier(this Player player, int element, float mod)
        {
            SoA.TryElementCall("modifyMultipliers", player, element, mod);
        }
    }
}
