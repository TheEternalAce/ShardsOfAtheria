using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Items.Accessories.GemCores;
using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Items.Accessories.GemCores.Super;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.NPCs.Boss.Elizabeth;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.NPCs.Town.TheAtherian;
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
        public static ModKeybind ProcessorElement { get; private set; }
        public static ModKeybind ChargeWeapons { get; private set; }

        public static ShardsServer ServerConfig => ModContent.GetInstance<ShardsServer>();
        public static ShardsClient ClientConfig => ModContent.GetInstance<ShardsClient>();
        public static ShardsDownedSystem DownedSystem => ModContent.GetInstance<ShardsDownedSystem>();
        public static bool AprilFools => DateTime.Now is DateTime { Month: 4 };

        public static bool BNEEnabled => ModLoader.TryGetMod(ElementModName, out Mod _);

        public static Mod Instance => ModContent.GetInstance<SoA>();

        public static readonly SoundStyle ReactorAlarm = new(ItemSoundPath + "ReactorMeltdownAlarm");
        public static readonly SoundStyle TheMessiah = new(ItemSoundPath + "TheMessiah");
        public static readonly SoundStyle Rekkoha = new(ItemSoundPath + "MessiahRekkoha");
        public static readonly SoundStyle Coin = new(ItemSoundPath + "Coin");
        public static readonly SoundStyle MagnetChargeUp = new(ItemSoundPath + "MagnetChargeUp") { Volume = 0.75f };
        public static readonly SoundStyle MagnetWeakShot = new(ItemSoundPath + "MagnetWeakShot") { Volume = 0.75f };
        public static readonly SoundStyle MagnetShot = new(ItemSoundPath + "MagnetShot") { Volume = 0.75f };
        public static readonly SoundStyle KeyPress = new(ItemSoundPath + "KeyPress");
        public static readonly SoundStyle ZeroCharge = new(ItemSoundPath + "ZeroCharge") { MaxInstances = 1 };
        public static readonly SoundStyle Katana = new(ItemSoundPath + "Katana") { Volume = 0.25f, MaxInstances = 2, PitchVariance = 0.1f };
        public static readonly SoundStyle HeavyCut = new(ItemSoundPath + "HeavyCut") { Volume = 0.15f, MaxInstances = 2, PitchVariance = 0.2f };
        public static readonly SoundStyle Judgement0 = new(ItemSoundPath + "Judgement1") { Volume = 0.15f, MaxInstances = 2, PitchVariance = 0.1f };
        public static readonly SoundStyle Judgement1 = new(ItemSoundPath + "Judgement2_1") { Volume = 0.15f, MaxInstances = 2, PitchVariance = 0.1f };
        public static readonly SoundStyle Judgement2 = new(ItemSoundPath + "Judgement2_2") { Volume = 0.15f, MaxInstances = 2, PitchVariance = 0.1f };
        public static readonly SoundStyle Judgement3 = new(ItemSoundPath + "Judgement2_3") { Volume = 0.15f, MaxInstances = 2, PitchVariance = 0.1f };
        public static readonly SoundStyle SilverRings = new(ItemSoundPath + "SilverRing");
        public static readonly SoundStyle SilverRingsSoft = new(ItemSoundPath + "SilverRingSoft");

        public static readonly Color HardlightColor = new(224, 92, 165);
        public static readonly Color HardlightColorA = HardlightColor.UseA(0);
        public static readonly Vector3 HardlightColorV3 = HardlightColor.ToVector3();
        public static readonly Vector3 HardlightColorV3A = HardlightColorA.ToVector3();

        public static readonly Color HardlightBlueColor = new(189, 209, 242);
        public static readonly Color HardlightBlueColorA = HardlightBlueColor.UseA(0);
        public static readonly Vector3 HardlightBlueColorV3 = HardlightBlueColor.ToVector3();
        public static readonly Vector3 HardlightBlueColorV3A = HardlightBlueColorA.ToVector3();

        public static readonly Color AreusColor = Color.Cyan;
        public static readonly Color AreusColorA0 = AreusColor.UseA(0);
        public static readonly Vector3 AreusColorV3 = AreusColor.ToVector3();
        public static readonly Vector3 AreusColorV3A0 = AreusColorA0.ToVector3();

        public static readonly Color ElectricColor = new(113, 251, 255);
        public static readonly Color ElectricColorA0 = ElectricColor.UseA(0);
        public static readonly Vector3 ElectricColorV3 = ElectricColor.ToVector3();
        public static readonly Vector3 ElectricColorV3A0 = ElectricColorA0.ToVector3();

        public static Texture2D OrbBloom => ModContent.Request<Texture2D>("ShardsOfAtheria/Assets/BlurTrails/OrbBlur").Value;
        public static Texture2D DiamondBloom => ModContent.Request<Texture2D>("ShardsOfAtheria/Assets/BlurTrails/DiamondBlur").Value;
        public static Texture2D LineBloom => ModContent.Request<Texture2D>("ShardsOfAtheria/Assets/BlurTrails/LineTrail").Value;
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
            return Eternity() && Main.masterMode;
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
            OverdriveKey = KeybindLoader.RegisterKeybind(Instance, "ToggleOverdrive", Keys.F);
            TomeKey = KeybindLoader.RegisterKeybind(Instance, "KnowledgeBase", Keys.N);
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(Instance, "EmeraldTeleport", Keys.Z);
            AmethystBombToggle = KeybindLoader.RegisterKeybind(Instance, "AmethystBombToggle", Keys.J);
            PhaseSwitch = KeybindLoader.RegisterKeybind(Instance, "PhaseType", Keys.RightAlt);
            SoulTeleport = KeybindLoader.RegisterKeybind(Instance, "SoulCrystalTeleport", Keys.V);
            ChargeWeapons = KeybindLoader.RegisterKeybind(Instance, "ChargeWeapons", Keys.LeftShift);
            if (BNEEnabled) ProcessorElement = KeybindLoader.RegisterKeybind(Instance, "CycleElementAffinity", Keys.C);

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

        public override void PostSetupContent()
        {
            if (!Main.dedServ && ClientConfig.windowTitle && Main.rand.NextBool(3))
                Main.instance.Window.Title = ChooseTitleText();

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
            if (ModLoader.TryGetMod("RecipeBrowser", out Mod rb))
            {
                var iconTexture = ModContent.Request<Texture2D>(ModContent.GetInstance<Atherian>().HeadTexture);
                rb.Call("AddItemCategory", "Upgrades", "", iconTexture, (Predicate<Item>)((Item item) => HasBlueprint(item)));
                static bool HasBlueprint(Item item)
                {
                    foreach (UpgradeBlueprint blueprint in UpgradeBlueprintLoader.upgrades)
                    {
                        if (blueprint.ResultItem.type == item.type) return true;
                    }
                    return false;
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

            ShardsHelpers.SetGunStats(ModContent.ItemType<AreusAssaultRifle>(), "rifle", 30);
            ShardsHelpers.SetGunStats(ModContent.ItemType<AreusCalibratedShotgun>(), "shotgun", 8);
            ShardsHelpers.SetGunStats(ModContent.ItemType<AreusMagnum>(), "revolver", 1);
            ShardsHelpers.SetGunStats(ModContent.ItemType<AreusPistol>(), "pistol", 8);
            ShardsHelpers.SetGunStats(ModContent.ItemType<HansMachineGun>(), "lmg", 100);
            ShardsHelpers.SetGunStats(ModContent.ItemType<HeroGun>(), "pistol", 12);
            ShardsHelpers.SetGunStats(ModContent.ItemType<HuntingRifle>(), "rifle", 20);
            ShardsHelpers.SetGunStats(ModContent.ItemType<Magnus>(), "revolver", 8);
            ShardsHelpers.SetGunStats(ModContent.ItemType<P90>(), "smg", 50);
            ShardsHelpers.SetGunStats(ModContent.ItemType<PhantomRose>(), "pistol", 12);
            ShardsHelpers.SetGunStats(ModContent.ItemType<Scarlet>(), "sniper", 10);
        }

        public static void TryElementCall(params object[] args)
        {
            if (ModLoader.TryGetMod(ElementModName, out var battleNetworkElements))
            {
                battleNetworkElements.Call(args);
            }
        }

        public static void TryDungeonCall(params object[] args)
        {
            if (ModLoader.TryGetMod("DNT", out var dnt))
            {
                dnt.Call(args);
            }
        }

        public static void TryCalamityCall(params object[] args)
        {
            if (ModLoader.TryGetMod("CalamityMod", out var calamity))
            {
                calamity.Call(args);
            }
        }

        public static void TryRedemptionCall(params object[] args)
        {
            if (ModLoader.TryGetMod("Redemption", out var redemption))
            {
                redemption.Call(args);
            }
        }

        public static void TryReloadableGunsCall(params object[] args)
        {
            if (ModLoader.TryGetMod("ReloadableGunsRevitalized", out var rgr))
            {
                rgr.Call(args);
            }
        }

        public static string ChooseTitleText()
        {
            WeightedRandom<string> titles = new();
            int i = 0;
            while (true)
            {
                string titleText = ShardsHelpers.LocalizeCommon("TitleText" + i);
                if (titleText.Equals("No key found.")) break;
                else { titles.Add(titleText); i++; }
                ;
            }
            return titles;
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
