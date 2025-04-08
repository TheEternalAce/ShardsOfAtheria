﻿using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Common.Items
{
    public static class ItemDefaults
    {
        public const int RarityDataDisc = ItemRarityID.Green;
        public const int RaritySinful = RarityEarlyHardmode;
        public const int RaritySlayer = RarityMoonLord;

        public const int RarityShimmerPermanentUpgrade = ItemRarityID.LightPurple;
        public const int RarityPreBoss = ItemRarityID.White;
        public const int RarityBanner = ItemRarityID.Blue;
        public const int RarityBossMasks = ItemRarityID.Blue;
        public const int RarityDemoniteCrimtane = ItemRarityID.Blue;
        public const int RarityNova = ItemRarityID.Green;
        public const int RarityDungeon = ItemRarityID.Green;
        public const int RarityQueenBee = ItemRarityID.Orange;
        public const int RarityJungle = ItemRarityID.Orange;
        public const int RarityMolten = ItemRarityID.Orange;
        public const int RarityPet = ItemRarityID.Orange;
        public const int RarityWallofFlesh = ItemRarityID.LightRed;
        public const int RarityEarlyHardmode = ItemRarityID.LightRed;
        public const int RarityPreMechs = ItemRarityID.LightRed;
        public const int RarityCobaltMythrilAdamantite = ItemRarityID.LightRed;
        public const int RarityMechs = ItemRarityID.Pink;
        public const int RarityPlantera = ItemRarityID.Lime;
        public const int RarityHardmodeDungeon = ItemRarityID.Yellow;
        public const int RarityTemple = ItemRarityID.Yellow;
        public const int RarityMartians = ItemRarityID.Yellow;
        public const int RarityDukeFishron = ItemRarityID.Yellow;
        public const int RarityLunaticCultist = ItemRarityID.Cyan;
        public const int RarityDevSet = ItemRarityID.Cyan;
        public const int RarityLunarPillars = ItemRarityID.Red;
        public const int RarityMoonLord = ItemRarityID.Red;
        public const int RarityDeath = ItemRarityID.Red;

        /// <summary>
        /// 2 silver (sell)
        /// 10 silver (buy)
        /// </summary>
        public static int ValueBuffPotion => Item.sellPrice(silver: 2);
        /// <summary>
        /// 2 gold (sell)
        /// 10 gold (buy)
        /// </summary>
        public static int ValueRelicTrophy => Item.sellPrice(gold: 2);
        /// <summary>
        /// 75 silver (sell)
        /// 3 gold 75 silver (buy)
        /// </summary>
        public static int ValueBossMasks => Item.sellPrice(silver: 75);
        /// <summary>
        /// 50 silver (sell)
        /// 2 gold 50 silver (buy)
        /// </summary>
        public static int ValueEyeOfCthulhu => Item.sellPrice(silver: 50);
        /// <summary>
        /// 50 silver (sell)
        /// </summary>
        public static int ValueBloodMoon => Item.sellPrice(silver: 50);
        /// <summary>
        /// 1 gold 75 silver (sell)
        /// 8 gold 75 silver (buy)
        /// </summary>
        public static int ValueDungeon => Item.sellPrice(gold: 1, silver: 75);
        /// <summary>
        /// 3 gold (sell)
        /// 15 gold (buy)
        /// </summary>
        public static int ValueEarlyHardmode => Item.sellPrice(gold: 3);
        /// <summary>
        /// 6 gold (sell)
        /// 30 gold (buy)
        /// </summary>
        public static int ValueHardmodeDungeon => Item.sellPrice(gold: 6);
        /// <summary>
        /// 10 gold (sell)
        /// 50 gold (buy)
        /// </summary>
        public static int ValueLunarPillars => Item.sellPrice(gold: 10);
    }
}
