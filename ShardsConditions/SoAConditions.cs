using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.NPCs.Town.TheAtherian;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ShardsConditions
{
    public static class SoAConditions
    {
        public const string KeyBase = "Mods.ShardsOfAtheria.Conditions.";

        public static readonly Condition SlayerMode = new(KeyBase + "SlayerMode",
            () => Main.LocalPlayer.IsSlayer());
        public static readonly Condition NotSlayerMode = new(KeyBase + "NotSlayer",
            () => !Main.LocalPlayer.IsSlayer());
        public static readonly Condition Upgrade = new(KeyBase + "Upgrade",
            () => false);
        public static readonly Condition AtherianPresent = new(KeyBase + "AtherianPresent",
            () => NPC.AnyNPCs(ModContent.NPCType<Atherian>()));
        public static readonly Condition AtherianNotPresent = new(KeyBase + "NotAtherianPresent",
            () => !NPC.AnyNPCs(ModContent.NPCType<Atherian>()));
        public static readonly Condition ElementModEnabled = new(KeyBase + "ElementModEnabled",
            () => ModLoader.TryGetMod("BattleNetworkElements", out var _));
        public static readonly Condition SacrificeKatana = new(KeyBase + "SacrificeKatana",
            () => false);
        public static readonly Condition AreusVoidSet = new(KeyBase + "VoidSet",
            () => Main.LocalPlayer.Areus().imperialSet);
        public static readonly Condition HasCoin = new(KeyBase + "HaveCoin",
            () => Main.LocalPlayer.HasItem<AreusGambit>());
        public static readonly Condition HasDie = new(KeyBase + "HaveDie",
            () => Main.LocalPlayer.HasItem<AreusGamble>());
        public static readonly Condition HasMessiah = new(KeyBase + "HaveMessiah",
            () => Main.LocalPlayer.HasItem<TheMessiah>());

        public static readonly Condition DownedNova = new(KeyBase + "DownedNova",
            () => ShardsDownedSystem.downedValkyrie);
        public static readonly Condition NotDownedNova = new(KeyBase + "NotDownedNova",
            () => !ShardsDownedSystem.downedValkyrie);
        //public static readonly Condition DownedArmor = new(KeyBase+"DownedArmor",
        //    () => ShardsDownedSystem.downedArmor);
        //public static readonly Condition NotDownedArmor = new(KeyBase+"NotDownedArmor",
        //    () => !ShardsDownedSystem.downedArmor);
        //public static readonly Condition DownedPNova = new(KeyBase+"DownedPNova",
        //    () => ShardsDownedSystem.downedPheonix);
        //public static readonly Condition NotDownedPNova = new(KeyBase+"NotDownedPNova",
        //    () => !ShardsDownedSystem.downedPheonix);
        //public static readonly Condition DownedAnnah = new(KeyBase+"DownedAnnah",
        //    () => ShardsDownedSystem.downedHarpyQueen);
        //public static readonly Condition NotDownedAnnah = new(KeyBase+"NotDownedAnnah",
        //    () => !ShardsDownedSystem.downedHarpyQueen);
        public static readonly Condition DownedDeath = new(KeyBase + "DownedDeath",
            () => ShardsDownedSystem.downedDeath);
        public static readonly Condition NotDownedDeath = new(KeyBase + "NotDownedDeath",
            () => !ShardsDownedSystem.downedDeath);
        //public static readonly Condition DownedAndromeda = new(KeyBase+"DownedAndromeda",
        //    () => ShardsDownedSystem.downedCentipede);
        //public static readonly Condition NotDownedAndromeda = new(KeyBase+"NotDownedAndromeda",
        //    () => !ShardsDownedSystem.downedCentipede);
        public static readonly Condition DownedGenesis = new(KeyBase + "DownedGenesis",
            () => ShardsDownedSystem.downedGenesis);
        public static readonly Condition NotDownedGenesis = new(KeyBase + "NotDownedGenesis",
            () => !ShardsDownedSystem.downedGenesis);
        public static readonly Condition DownedSenterra = new(KeyBase + "DownedSenterra",
            () => ShardsDownedSystem.downedSenterra);
        public static readonly Condition NotDownedSenterra = new(KeyBase + "NotDownedSenterra",
            () => !ShardsDownedSystem.downedSenterra);
    }
}
