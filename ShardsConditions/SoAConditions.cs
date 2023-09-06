using ShardsOfAtheria.NPCs.Town.TheAtherian;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ShardsConditions
{
    public static class SoAConditions
    {
        public static Condition SlayerMode = new("Mods.ShardsOfAtheria.Conditions.SlayerMode",
            () => Main.LocalPlayer.IsSlayer());
        public static Condition NotSlayerMode = new("Mods.ShardsOfAtheria.Conditions.NotSlayer",
            () => !Main.LocalPlayer.IsSlayer());
        public static Condition Upgrade = new("Mods.ShardsOfAtheria.Conditions.Upgrade",
            () => false);
        public static Condition AtherianPresent = new("Mods.ShardsOfAtheria.Conditions.AtherianPresent",
            () => NPC.AnyNPCs(ModContent.NPCType<Atherian>()));
        public static Condition AtherianNotPresent = new("Mods.ShardsOfAtheria.Conditions.NotAtherianPresent",
            () => !NPC.AnyNPCs(ModContent.NPCType<Atherian>()));
        public static Condition ElementModEnabled = new("Mods.ShardsOfAtheria.Conditions.ElementModEnabled",
            () => ModLoader.TryGetMod("BattleNetworkElements", out var _));

        public static Condition DownedNova = new("Mods.ShardsOfAtheria.Conditions.DownedNova",
            () => ShardsDownedSystem.downedValkyrie);
        public static Condition NotDownedNova = new("Mods.ShardsOfAtheria.Conditions.NotDownedNova",
            () => !ShardsDownedSystem.downedValkyrie);
        //public static Condition DownedArmor = new("Mods.ShardsOfAtheria.Conditions.DownedArmor",
        //    () => ShardsDownedSystem.downedArmor);
        //public static Condition NotDownedArmor = new("Mods.ShardsOfAtheria.Conditions.NotDownedArmor",
        //    () => !ShardsDownedSystem.downedArmor);
        //public static Condition DownedPNova = new("Mods.ShardsOfAtheria.Conditions.DownedPNova",
        //    () => ShardsDownedSystem.downedPheonix);
        //public static Condition NotDownedPNova = new("Mods.ShardsOfAtheria.Conditions.NotDownedPNova",
        //    () => !ShardsDownedSystem.downedPheonix);
        //public static Condition DownedAnnah = new("Mods.ShardsOfAtheria.Conditions.DownedAnnah",
        //    () => ShardsDownedSystem.downedHarpyQueen);
        //public static Condition NotDownedAnnah = new("Mods.ShardsOfAtheria.Conditions.NotDownedAnnah",
        //    () => !ShardsDownedSystem.downedHarpyQueen);
        public static Condition DownedDeath = new("Mods.ShardsOfAtheria.Conditions.DownedDeath",
            () => ShardsDownedSystem.downedDeath);
        public static Condition NotDownedDeath = new("Mods.ShardsOfAtheria.Conditions.NotDownedDeath",
            () => !ShardsDownedSystem.downedDeath);
        //public static Condition DownedAndromeda = new("Mods.ShardsOfAtheria.Conditions.DownedAndromeda",
        //    () => ShardsDownedSystem.downedCentipede);
        //public static Condition NotDownedAndromeda = new("Mods.ShardsOfAtheria.Conditions.NotDownedAndromeda",
        //    () => !ShardsDownedSystem.downedCentipede);
        public static Condition DownedGenesis = new("Mods.ShardsOfAtheria.Conditions.DownedGenesis",
            () => ShardsDownedSystem.downedGenesis);
        public static Condition NotDownedGenesis = new("Mods.ShardsOfAtheria.Conditions.NotDownedGenesis",
            () => !ShardsDownedSystem.downedGenesis);
        public static Condition DownedSenterra = new("Mods.ShardsOfAtheria.Conditions.DownedSenterra",
            () => ShardsDownedSystem.downedSenterra);
        public static Condition NotDownedSenterra = new("Mods.ShardsOfAtheria.Conditions.NotDownedSenterra",
            () => !ShardsDownedSystem.downedSenterra);
    }
}
