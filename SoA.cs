using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Items.Accessories.GemCores;
using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Items.Accessories.GemCores.Super;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.NPCs.Boss.Elizabeth;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria
{
    public partial class SoA : Mod
    {
        public static int MaxNecronomiconPages = 2;

        public static ModKeybind OverdriveKey { get; private set; }
        public static ModKeybind TomeKey { get; private set; }
        public static ModKeybind EmeraldTeleportKey { get; private set; }
        public static ModKeybind AmethystBombToggle { get; private set; }
        public static ModKeybind PhaseSwitch { get; private set; }
        public static ModKeybind SoulTeleport { get; private set; }
        public static ModKeybind ArmorSetBonusActive { get; private set; }
        public static ModKeybind ProcessorElement { get; private set; }
        public static ModKeybind MasterCoreToggles { get; private set; }

        public static ShardsServer ServerConfig { get; private set; }
        public static ShardsClient ClientConfig { get; private set; }
        public static ShardsDownedSystem DownedSystem { get; private set; }
        public static bool AprilFools => DateTime.Now is DateTime { Month: 4 };

        public static bool ElementModEnabled => ModLoader.TryGetMod(ElementModName, out Mod _);

        public static Mod Instance { get; private set; }

        public static readonly SoundStyle ReactorAlarm = new(ItemSoundPath + "ReactorMeltdownAlarm");
        public static readonly SoundStyle TheMessiah = new(ItemSoundPath + "TheMessiah");
        public static readonly SoundStyle Rekkoha = new(ItemSoundPath + "MessiahRekkoha");
        public static readonly SoundStyle Coin = new(ItemSoundPath + "Coin");
        public static readonly SoundStyle KatanaScream = new(ItemSoundPath + "KatanaScream");
        public static readonly SoundStyle MagnetChargeUp = new SoundStyle(ItemSoundPath + "MagnetChargeUp").WithVolumeScale(0.75f);
        public static readonly SoundStyle MagnetWeakShot = new SoundStyle(ItemSoundPath + "MagnetWeakShot").WithVolumeScale(0.75f);
        public static readonly SoundStyle MagnetShot = new SoundStyle(ItemSoundPath + "MagnetShot").WithVolumeScale(0.75f);

        public static readonly Color HardlightColor = new(224, 92, 165);
        public static readonly Color HardlightColorA = HardlightColor.UseA(0);
        public static readonly Vector3 HardlightColorV3 = HardlightColor.ToVector3();
        public static readonly Vector3 HardlightColorV3A = HardlightColorA.ToVector3();

        public static readonly Color AreusColor = Color.Cyan;
        public static readonly Color AreusColorA = AreusColor.UseA(0);
        public static readonly Vector3 AreusColorV3 = AreusColor.ToVector3();
        public static readonly Vector3 AreusColorV3A = AreusColorA.ToVector3();

        public static readonly Color ElectricColor = new(113, 251, 255);
        public static readonly Color ElectricColorA = ElectricColor.UseA(0);
        public static readonly Vector3 ElectricColorV3 = ElectricColor.ToVector3();
        public static readonly Vector3 ElectricColorV3A = ElectricColorA.ToVector3();

        public static bool Eternity()
        {
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod souls))
            {
                return (bool)souls.Call("EternityMode");
            }
            return false;
        }
        public static bool Massochist()
        {
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod souls))
            {
                return (bool)souls.Call("MasochistMode");
            }
            return false;
        }
        //Get stick bugged lmao
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡛⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠀⠀⠈⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⣋⣥⣴⣾⢏⣼⣿⣷⣄⠈⠛⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⣩⣤⣶⣿⣿⣿⣿⡏⣼⣿⣿⡿⢋⣤⣦⡀⠈⠙⠛⠛⠛⠟⠛⠛⠛⠛⢛⣙⣛⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢋⣴⣿⣿⣿⣿⣿⣿⣿⡟⣸⣿⡿⢋⣴⣿⣿⣿⡇⠀⣶⣶⣶⣶⣶⣶⣶⣤⣤⡍⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⣱⣿⣿⣿⣿⣿⣿⣿⣿⣿⣱⣿⠟⣡⣾⣿⣿⣿⡿⢠⡆⣽⣿⣿⣿⣿⣿⣿⣿⣿⣧⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢋⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⢣⣿⣿⣸⣿⣿⣿⣿⣿⢡⡿⣱⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⠿⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣱⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⣾⣿⡇⣿⣿⣿⣿⣿⡟⠼⣣⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣶⣶⣶⣶⣤⣤⣤⣀⣈⣉⣉⡉⠉⠛⠛⠛⠰⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⡟⣾⣿⣿⢹⣿⣿⣿⣿⣿⡇⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⣶⣶⣶⣦⣤⣤⣄⣈⣉⣉⣁⣉⠉⠋⠘⠻⠿⠿⠿⠿⢡⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⣶⣶⣤⣤⣬⣍⣉⣉⣉⡛⠛⠛⠛⠿⠿⠿⠸⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⣶⣶⣤⣤⣤⣄⣉⣉⣉⣉⠛⠛⠛⠿⠿⠿⠿⠿
        public override void Load()
        {
            Instance = this;

            OverdriveKey = KeybindLoader.RegisterKeybind(Instance, "ToggleOverdrive", "F");
            TomeKey = KeybindLoader.RegisterKeybind(Instance, "KnowledgeBase", "N");
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(Instance, "EmeraldTeleport", "Z");
            AmethystBombToggle = KeybindLoader.RegisterKeybind(Instance, "AmethystBombToggle", "J");
            PhaseSwitch = KeybindLoader.RegisterKeybind(Instance, "PhaseType", "RightAlt");
            SoulTeleport = KeybindLoader.RegisterKeybind(Instance, "SoulCrystalTeleport", "V");
            ArmorSetBonusActive = KeybindLoader.RegisterKeybind(Instance, "ArmorSetBonus", "Mouse4");
            ProcessorElement = KeybindLoader.RegisterKeybind(Instance, "CycleElementAffinity", "C");
            MasterCoreToggles = KeybindLoader.RegisterKeybind(Instance, "MasterCoreToggles", "Semicolon");

            ServerConfig = ModContent.GetInstance<ShardsServer>();
            ClientConfig = ModContent.GetInstance<ShardsClient>();
            DownedSystem = ModContent.GetInstance<ShardsDownedSystem>();

            if (!Main.dedServ)
            {
                ModLoader.TryGetMod("Wikithis", out Mod wikithis);
                if (wikithis != null)
                {
                    wikithis.Call("AddModURL", Instance, "https://terrariamods.wiki.gg/wiki/Shards_of_Atheria/{}");

                    // If you want to replace default icon for your mod, then call this. Icon should be 30x30, either way it will be cut.
                    wikithis.Call("AddWikiTexture", Instance, ModContent.Request<Texture2D>("ShardsOfAtheria/icon_small"));
                }
            }
        }

        public override void Unload()
        {
            Instance = null;
            ServerConfig = null;
            ClientConfig = null;
            DownedSystem = null;
        }

        public override void PostSetupContent()
        {
            if (!Main.dedServ)
            {
                if (ClientConfig.windowTitle)
                {
                    if (Main.rand.NextBool(3))
                    {
                        Main.instance.Window.Title = ChooseTitleText();
                    }
                }
            }

            if (ModLoader.TryGetMod("TerraTyping", out Mod terratyping))
            {
                Dictionary<string, object> addWeapon = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "weapon" },
                    { "callingmod", Instance },
                    { "filename", TerraTypingFolder.FormatWith("Weapons") }
                };
                terratyping.Call(addWeapon);

                Dictionary<string, object> addProjectile = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "projectile" },
                    { "callingmod", Instance },
                    { "filename", TerraTypingFolder.FormatWith("Projectiles") }
                };
                terratyping.Call(addProjectile);

                Dictionary<string, object> addAmmo = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "ammo" },
                    { "callingmod", Instance },
                    { "filename", TerraTypingFolder.FormatWith("Ammo") }
                };
                terratyping.Call(addAmmo);

                Dictionary<string, object> addNPC = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "npc" },
                    { "callingmod", Instance },
                    { "filename", TerraTypingFolder.FormatWith("NPCs") }
                };
                terratyping.Call(addNPC);

                Dictionary<string, object> addArmor = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "armor" },
                    { "callingmod", Instance },
                    { "filename", TerraTypingFolder.FormatWith("Armor") }
                };
                terratyping.Call(addArmor);

                Dictionary<string, object> addItem = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "specialitem" },
                    { "callingmod", Instance },
                    { "filename", TerraTypingFolder.FormatWith("Items") }
                };
                terratyping.Call(addItem);

                if (ServerConfig.overrideTypes)
                {
                    Dictionary<string, object> overrideNPC = new()
                    {
                        { "call", "OverrideTypes" },
                        { "typestoadd", "npc" },
                        { "callingmod", Instance },
                        { "filename", TerraTypingFolder.FormatWith("NPCOverride") },
                        { "modtarget", "Terraria" }
                    };
                    terratyping.Call(overrideNPC);

                    Dictionary<string, object> overrideProjectile = new()
                    {
                        { "call", "OverrideTypes" },
                        { "typestoadd", "projectile" },
                        { "callingmod", Instance },
                        { "filename", TerraTypingFolder.FormatWith("ProjectileOverride") },
                        { "modtarget", "Terraria" }
                    };
                    terratyping.Call(overrideProjectile);
                }
            }

            if (ModLoader.TryGetMod("BossChecklist", out Mod checklist))
            {
                string despawnPath = "Mods.ShardsOfAtheria.NPCs.{0}.BossChecklistIntegration.Despawn";
                checklist.Call(
                    "LogBoss",
                    Instance,
                    nameof(NovaStellar),
                    5.5f,
                    () => ShardsDownedSystem.downedValkyrie,
                    ModContent.NPCType<NovaStellar>(),
                    new Dictionary<string, object>()
                    {
                        ["spawnItems"] = ModContent.ItemType<ValkyrieCrest>(),
                        ["despawnMessage"] = Language.GetText(despawnPath.FormatWith("NovaStellar"))
                    }
                );
                checklist.Call(
                    "LogBoss",
                    Instance,
                    nameof(Death),
                    19f,
                    () => ShardsDownedSystem.downedDeath,
                    ModContent.NPCType<Death>(),
                    new Dictionary<string, object>()
                    {
                        ["spawnItems"] = ModContent.ItemType<AncientMedalion>(),
                        ["despawnMessage"] = Language.GetText(despawnPath.FormatWith("Death"))
                    }
                );
            }

            if (ModLoader.TryGetMod("ShoeSlot", out Mod shoeSlot))
            {
                shoeSlot.Call(ModContent.ItemType<EmeraldCore>());
                shoeSlot.Call(ModContent.ItemType<EmeraldCore_Greater>());
                shoeSlot.Call(ModContent.ItemType<EmeraldCore_Super>());
                shoeSlot.Call(ModContent.ItemType<MegaGemCore>());
            }

            if (ModLoader.TryGetMod("ShieldSlot", out Mod shieldSlot))
            {
                shieldSlot.Call(ModContent.ItemType<DiamondCore>(),
                    ModContent.ItemType<DiamondCore_Greater>(),
                    ModContent.ItemType<DiamondCore_Super>(),
                    ModContent.ItemType<MegaGemCore>());
            }

            if (ModLoader.TryGetMod("Fargowiltas", out Mod fargos))
            {
                fargos.Call("AddSummon", 5.5f, ModContent.ItemType<ValkyrieCrest>(), () => ShardsDownedSystem.downedValkyrie, 50000);
                fargos.Call("AddSummon", 19f, ModContent.ItemType<AncientMedalion>(), () => ShardsDownedSystem.downedDeath, 500000);
            }
            if (ModLoader.TryGetMod("RORBossHealthbars", out Mod ror2Bars))
            {
                ror2Bars.Call("HPPool", new List<int>()
                {
                    ModContent.NPCType<NovaStellar>(),
                    ModContent.NPCType<Death>(),
                });
                ror2Bars.Call("CustomName", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.NPCs.NovaStellar.DisplayName");
                ror2Bars.Call("BossDesc", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.NPCs.NovaStellar.BossDesc");
                ror2Bars.Call("CustomName", ModContent.NPCType<Death>(), "Mods.ShardsOfAtheria.NPCs.Death.DisplayName");
                ror2Bars.Call("BossDesc", ModContent.NPCType<Death>(), "Mods.ShardsOfAtheria.NPCs.Death.BossDesc");
            }
            if (ModLoader.TryGetMod("RiskOfTerrain", out Mod rot))
            {
                rot.Call("HPPool", new List<int>()
                {
                    ModContent.NPCType<NovaStellar>(),
                    ModContent.NPCType<Death>(),
                });
                rot.Call("CustomName", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.NPCs.NovaStellar.DisplayName");
                rot.Call("BossDesc", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.NPCs.NovaStellar.BossDesc");
                rot.Call("CustomName", ModContent.NPCType<Death>(), "Mods.ShardsOfAtheria.NPCs.Death.DisplayName");
                rot.Call("BossDesc", ModContent.NPCType<Death>(), "Mods.ShardsOfAtheria.NPCs.Death.BossDesc");
            }
        }

        public static void TryElementCall(params object[] args)
        {
            if (ElementModEnabled)
            {
                var em = ModLoader.GetMod(ElementModName);
                em.Call(args);
            }
        }

        public static void TryRedemptionCall(params object[] args)
        {
            if (ModLoader.TryGetMod("Redemption", out var redemption))
            {
                redemption.Call(args);
            }
        }

        public static string ChooseTitleText()
        {
            WeightedRandom<string> title = new();
            bool add = true;
            int i = 0;
            while (add)
            {
                string path = LocalizeCommon + "TitleText" + i;
                if (Language.Exists(path))
                {
                    title.Add(Language.GetTextValue(path));
                    i++;
                }
                else
                {
                    add = false;
                }
            }
            return title;
        }

        public static Dictionary<string, List<string>> GetContentArrayFile(string name)
        {
            using var stream = Instance.GetFileStream($"Content/{name}.json", newFileStream: true);
            using var streamReader = new StreamReader(stream);
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(streamReader.ReadToEnd());
        }

        private static bool ConsoleDebug => ClientConfig.debug;
        internal static void LogInfo(object value, string label = "", bool ignoreDebugConfig = false)
        {
            if (ConsoleDebug || ignoreDebugConfig)
            {
                Instance.Logger.Info(label + value);
                if (value is IList list)
                {
                    Instance.Logger.Info("--List items--");
                    if (list.Count == 0)
                    {
                        Instance.Logger.Info("None");
                    }
                    else
                    {
                        foreach (object item in list)
                        {
                            Instance.Logger.Info(item);
                        }
                    }
                }
            }
        }
    }
}
