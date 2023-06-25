using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon.Minion;
using ShardsOfAtheria.NPCs.Boss.Elizabeth;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
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

        public const string BlankTexture = "ShardsOfAtheria/Blank";
        public const string PlaceholderTexture = "ShardsOfAtheria/PlaceholderSprite";
        public const string BuffTemplate = "ShardsOfAtheria/Buffs/BuffTemp";
        public const string DebuffTemplate = "ShardsOfAtheria/Buffs/DebuffTemp";

        public const string LocalizeCommon = "Mods.ShardsOfAtheria.Common.";

        public static SoundStyle ReactorAlarm;
        public static SoundStyle TheMessiah;
        public static SoundStyle Rekkoha;
        public static SoundStyle Coin;

        public override void Load()
        {
            OverdriveKey = KeybindLoader.RegisterKeybind(this, "ToggleOverdrive", "F");
            TomeKey = KeybindLoader.RegisterKeybind(this, "KnowledgeBase", "N");
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(this, "EmeraldTeleport", "Z");
            PhaseSwitch = KeybindLoader.RegisterKeybind(this, "PhaseType", "RightAlt");
            SoulTeleport = KeybindLoader.RegisterKeybind(this, "SoulCrystalTeleport", "V");
            ArmorSetBonusActive = KeybindLoader.RegisterKeybind(this, "ArmorSetBonus", "Mouse4");
            ProcessorElement = KeybindLoader.RegisterKeybind(this, "CycleElementAffinity", "C");

            ServerConfig = ModContent.GetInstance<ShardsServer>();
            ClientConfig = ModContent.GetInstance<ShardsClient>();
            DownedSystem = ModContent.GetInstance<ShardsDownedSystem>();

            if (!Main.dedServ)
            {
                ModLoader.TryGetMod("Wikithis", out Mod wikithis);
                if (wikithis != null)
                {
                    wikithis.Call("AddModURL", this, "terrariamods.wiki.gg$Shards_of_Atheria");

                    // If you want to replace default icon for your mod, then call this. Icon should be 30x30, either way it will be cut.
                    wikithis.Call("AddWikiTexture", this, ModContent.Request<Texture2D>("ShardsOfAtheria/icon_small"));
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

        public override void PostSetupContent()
        {
            if (!Main.dedServ)
            {
                if (SoA.ClientConfig.windowTitle)
                {
                    if (Main.rand.NextBool(3))
                    {
                        Main.instance.Window.Title = ChooseTitleText();
                    }
                }
            }

            // Add Areus weapons to Electric weapons list
            foreach (int item in SoAGlobalItem.AreusWeapon)
            {
                item.AddElecItem();
            }

            if (ModLoader.TryGetMod("BossChecklist", out Mod foundMod1))
            {
                foundMod1.Call(
                    "AddBoss",
                    this,
                    "Nova Stellar",
                    new List<int> { ModContent.NPCType<NovaStellar>() },
                    5.5f,
                    () => ShardsDownedSystem.downedValkyrie,
                    () => true,
                    new List<int> { ModContent.ItemType<ValkyrieStormLance>(), ModContent.ItemType<GildedValkyrieWings>(), ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<DownBow>(),
                        ModContent.ItemType<PlumeCodex>(), ModContent.ItemType<NestlingStaff>(), ModContent.ItemType<ValkyrieCrown>(),
                        ItemID.GoldBar, ItemID.Feather },
                    ModContent.ItemType<ValkyrieCrest>()
                );
                foundMod1.Call(
                    "AddBoss",
                    this,
                    "Elizabeth Norman, Death",
                    new List<int> { ModContent.NPCType<Death>() },
                    5.5f,
                    () => ShardsDownedSystem.downedDeath,
                    () => true,
                    new List<int> { },
                    ModContent.ItemType<AncientMedalion>()
                );
            }
            if (ModLoader.TryGetMod("Fargowiltas", out Mod foundMod2))
            {
                foundMod2.Call("AddSummon", 5.5f, ModContent.ItemType<ValkyrieCrest>(), () => ShardsDownedSystem.downedValkyrie, 50000);
            }

            if (ModLoader.TryGetMod("RORBossHealthbars", out Mod ror2HBS))
            {
                ror2HBS.Call("HPPool", new List<int>()
                {
                    ModContent.NPCType<NovaStellar>()
                });
                ror2HBS.Call("CustomName", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.NPCName.NovaStellar");
                ror2HBS.Call("BossDesc", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.BossDesc.NovaStellar");
            }

            if (ModLoader.TryGetMod("RiskOfTerrain", out Mod rot))
            {
                rot.Call("HPPool", new List<int>()
                {
                    ModContent.NPCType<NovaStellar>()
                });
                rot.Call("CustomName", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.NPCName.NovaStellar");
                rot.Call("BossDesc", ModContent.NPCType<NovaStellar>(), "Mods.ShardsOfAtheria.BossDesc.NovaStellar");
            }
        }

        public string ChooseTitleText()
        {
            List<string> title = new List<string>();
            for (int i = 0; i < 2; i++)
            {
                title.Add(Language.GetTextValue("Mods.ShardsOfAtheria.Common.TitleText" + i));
            }
            int index = Main.rand.Next(2);

            return title[index];
        }

        private static bool ConsoleDebug => ClientConfig.debug == "Console only";
        private static bool ConsoleAndChatDebug => ClientConfig.debug == "Console and Chat";
        internal static void Log(string label, object value, bool ignoreDebugConfig = false)
        {
            Log(label, value, Color.White, ignoreDebugConfig);
        }
        internal static void Log(string label, object value, Color color, bool ignoreDebugConfig = false)
        {
            var debug = "[Shards of Atheria Debug] " + label;
            if (ConsoleDebug || ConsoleAndChatDebug || ignoreDebugConfig)
            {
                Console.WriteLine(debug + value);
                if (value is IList list)
                {
                    Console.WriteLine("--List items--");
                    if (list.Count == 0)
                    {
                        Console.WriteLine("None");
                    }
                    else
                    {
                        foreach (object item in list)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
            }
            if (ConsoleAndChatDebug || ignoreDebugConfig)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(debug + value.ToString()), color);
                if (value is IList list)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("--List items--"), color);
                    if (list.Count == 0)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("None"), color);
                    }
                    else
                    {
                        foreach (object item in list)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(item.ToString()), color);
                        }
                    }
                }
            }
        }
    }
}
