using Terraria;
using Terraria.ID;

namespace ShardsOfAtheria.Items
{
    public static class ItemDefaults
    {
        public const int RarityAreus = ItemRarityID.Cyan;
        public const int RarityHardlight = ItemRarityID.Green;
        public const int RarityDataDisc = ItemRarityID.Green;
        public const int RaritySinful = ItemRarityID.Orange;
        public const int RaritySlayer = ItemRarityID.Yellow;

        public const int RarityShimmerPermanentUpgrade = ItemRarityID.LightPurple;
        public const int RarityPreBoss = ItemRarityID.White;
        public const int RarityBanner = ItemRarityID.Blue;
        public const int RarityBossMasks = ItemRarityID.Blue;
        public const int RarityDemoniteCrimtane = ItemRarityID.Blue;
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
        public const int RarityLunarPillars = ItemRarityID.Red;
        public const int RarityMoonLord = ItemRarityID.Red;

        /// <summary>
        /// 2 silver
        /// </summary>
        public static int ValueBuffPotion => Item.sellPrice(silver: 2);
        /// <summary>
        /// 1 gold
        /// </summary>
        public static int ValueRelicTrophy => Item.sellPrice(gold: 2);
        /// <summary>
        /// 50 silver
        /// </summary>
        public static int ValueEyeOfCthulhu => Item.sellPrice(silver: 50);
        /// <summary>
        /// 50 silver
        /// </summary>
        public static int ValueBloodMoon => Item.sellPrice(silver: 50);
        /// <summary>
        /// 1 gold 75 silver
        /// </summary>
        public static int ValueDungeon => Item.sellPrice(gold: 1, silver: 75);
        /// <summary>
        /// 3 gold
        /// </summary>
        public static int ValueEarlyHardmode => Item.sellPrice(gold: 3);
        /// <summary>
        /// 6 gold
        /// </summary>
        public static int ValueHardmodeDungeon => Item.sellPrice(gold: 6);
        /// <summary>
        /// 10 gold
        /// </summary>
        public static int ValueLunarPillars => Item.sellPrice(gold: 10);
    }
}
