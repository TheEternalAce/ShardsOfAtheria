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
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
    public partial class SoA : Mod
    {
        public static int MaxNecronomiconPages = 2;

        public static ModKeybind OverdriveKey;
        public static ModKeybind TomeKey;
        public static ModKeybind EmeraldTeleportKey;
        public static ModKeybind PhaseSwitch;
        public static ModKeybind SoulTeleport;
        public static ModKeybind ArmorSetBonusActive;
        public static ModKeybind ProcessorElement;

        public static ShardsServer ServerConfig;
        public static ShardsClient ClientConfig;
        public static ShardsDownedSystem DownedSystem;
        public static bool AprilFools => DateTime.Now is DateTime { Month: 4 };

        public static bool ElementModEnabled => ModLoader.TryGetMod("BattleNetworkElements", out Mod _);

        public static Mod Instance { get; private set; }

        public const string BlankTexture = "ShardsOfAtheria/Blank";
        public const string PlaceholderTexture = "ShardsOfAtheria/PlaceholderSprite";
        public const string BuffTemplate = "ShardsOfAtheria/Buffs/BuffTemp";
        public const string DebuffTemplate = "ShardsOfAtheria/Buffs/DebuffTemp";
        public const string SwordSlashTexture = "ShardsOfAtheria/Projectiles/Bases/SwordSlash";

        public const string LocalizeCommon = "Mods.ShardsOfAtheria.Common.";

        public static SoundStyle ReactorAlarm;
        public static SoundStyle TheMessiah;
        public static SoundStyle Rekkoha;
        public static SoundStyle Coin;

        public static readonly Color HardlightColor = new(224, 92, 165);

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

        public override void Load()
        {
            OverdriveKey = KeybindLoader.RegisterKeybind(this, "ToggleOverdrive", "F");
            TomeKey = KeybindLoader.RegisterKeybind(this, "KnowledgeBase", "N");
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(this, "EmeraldTeleport", "Z");
            PhaseSwitch = KeybindLoader.RegisterKeybind(this, "PhaseType", "RightAlt");
            SoulTeleport = KeybindLoader.RegisterKeybind(this, "SoulCrystalTeleport", "V");
            ArmorSetBonusActive = KeybindLoader.RegisterKeybind(this, "ArmorSetBonus", "Mouse4");
            ProcessorElement = KeybindLoader.RegisterKeybind(this, "CycleElementAffinity", "C");

            Instance = this;

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

                ReactorAlarm = new SoundStyle("ShardsOfAtheria/Sounds/Item/ReactorMeltdownAlarm")
                {
                    Volume = 0.9f,
                    MaxInstances = 3,
                };
                TheMessiah = new SoundStyle("ShardsOfAtheria/Sounds/Item/TheMessiah");
                Rekkoha = new SoundStyle("ShardsOfAtheria/Sounds/Item/MessiahRekkoha");
                Coin = new SoundStyle("ShardsOfAtheria/Sounds/Item/Coin");
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
                string basePath = "CrossMod/TerraTypes/";
                Dictionary<string, object> addWeapon = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "weapon" },
                    { "callingmod", Instance },
                    { "filename", basePath + "Weapons.csv" }
                };
                terratyping.Call(addWeapon);

                Dictionary<string, object> addProjectile = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "projectile" },
                    { "callingmod", Instance },
                    { "filename", basePath + "Projectiles.csv" }
                };
                terratyping.Call(addProjectile);

                Dictionary<string, object> addAmmo = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "ammo" },
                    { "callingmod", Instance },
                    { "filename", basePath + "Ammo.csv" }
                };
                terratyping.Call(addAmmo);

                Dictionary<string, object> addNPC = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "npc" },
                    { "callingmod", Instance },
                    { "filename", basePath + "NPCs.csv" }
                };
                terratyping.Call(addNPC);

                Dictionary<string, object> addArmor = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "armor" },
                    { "callingmod", Instance },
                    { "filename", basePath + "Armor.csv" }
                };
                terratyping.Call(addArmor);
            }

            if (ModLoader.TryGetMod("BossChecklist", out Mod checklist))
            {
                string despawnPath = "Mods.ShardsOfAtheria.NPCs.";
                string despawnPath2 = ".BossChecklistIntegration.Despawn";
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
                        ["despawnMessage"] = Language.GetText(despawnPath + "NovaStellar" + despawnPath2)
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
                        ["despawnMessage"] = Language.GetText(despawnPath + "Death" + despawnPath2)
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

        public static string ChooseTitleText()
        {
            List<string> title = new();
            for (int i = 0; i < 2; i++)
            {
                title.Add(Language.GetTextValue("Mods.ShardsOfAtheria.Common.TitleText" + i));
            }
            int index = Main.rand.Next(2);

            return title[index];
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
