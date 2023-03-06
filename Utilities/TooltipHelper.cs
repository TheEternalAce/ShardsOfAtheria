using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public static class TooltipHelper
    {
        internal static readonly string[] VanillaTooltipNames = new string[]
        {
                "ItemName",
                "Favorite",
                "FavoriteDesc",
                "Social",
                "SocialDesc",
                "Damage",
                "CritChance",
                "Speed",
                "Knockback",
                "FishingPower",
                "NeedsBait",
                "BaitPower",
                "Equipable",
                "WandConsumes",
                "Quest",
                "Vanity",
                "Defense",
                "PickPower",
                "AxePower",
                "HammerPower",
                "TileBoost",
                "HealLife",
                "HealMana",
                "UseMana",
                "Placeable",
                "Ammo",
                "Consumable",
                "Material",
                "Tooltip#",
                "EtherianManaWarning",
                "WellFedExpert",
                "BuffTime",
                "OneDropLogo",
                "PrefixDamage",
                "PrefixSpeed",
                "PrefixCritChance",
                "PrefixUseMana",
                "PrefixSize",
                "PrefixShootSpeed",
                "PrefixKnockback",
                "PrefixAccDefense",
                "PrefixAccMaxMana",
                "PrefixAccCritChance",
                "PrefixAccDamage",
                "PrefixAccMoveSpeed",
                "PrefixAccMeleeSpeed",
                "SetBonus",
                "Expert",
                "Master",
                "JourneyResearch",
                "BestiaryNotes",
                "SpecialPrice",
                "Price",
        };

        private static int FindLineIndex(string name)
        {
            if (name.StartsWith("Tooltip"))
            {
                name = "Tooltip#";
            }
            for (int i = 0; i < VanillaTooltipNames.Length; i++)
            {
                if (name == VanillaTooltipNames[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public static void AddTooltip(this List<TooltipLine> tooltips, TooltipLine line)
        {
            tooltips.Insert(Math.Min(tooltips.GetIndex("Tooltip#"), tooltips.Count), line);
        }

        public static int GetIndex(this List<TooltipLine> tooltips, string lineName)
        {
            int myIndex = FindLineIndex(lineName);
            int i = 0;
            for (; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && FindLineIndex(tooltips[i].Name) >= myIndex)
                {
                    if (lineName == "Tooltip#")
                    {
                        for (; i < tooltips.Count; i++)
                        {
                            if (!tooltips[i].Name.StartsWith("Tooltip"))
                            {
                                return i;
                            }
                        }
                    }
                    return i;
                }
            }
            return i;
        }
    }
}
