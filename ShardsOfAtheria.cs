using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.UI;
using ShardsOfAtheria.UI;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ShardsOfAtheria.Items;
using System;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Weapons.Magic;

namespace ShardsOfAtheria
{
    public class ShardsOfAtheria : Mod
    {
        private UserInterface _areusResourceBarUserInterface;
        private UserInterface _overdriveTimeBarUserInterface;

        public static int AreusCurrency;
        public static int DryskalCurrency;
        public static ModKeybind OverdriveKey;
        public static ModKeybind TomeKey;
        public static ModKeybind EmeraldTeleportKey;
        public static ModKeybind ShadowCloak;
        public static ModKeybind ShadowTeleport;
        public static ModKeybind PhaseSwitch;
        public static ModKeybind QuickCharge;

        // A place to store the recipe group so we can easily use it later
        public static RecipeGroup EvilMaterial;
        public static RecipeGroup EvilGun;
        public static RecipeGroup Copper;
        public static RecipeGroup Silver;
        public static RecipeGroup Gold;
        public static RecipeGroup EvilBar;
        public static RecipeGroup Cobalt;
        public static RecipeGroup Mythril;
        public static RecipeGroup Adamantite;
        public static RecipeGroup Soul;

        public override void Unload()
        {
            EvilMaterial = null;
            EvilGun = null;
            Copper = null;
            Silver = null;
            Gold = null;
            EvilBar = null;
            Cobalt = null;
            Mythril = null;
            Adamantite = null;
            Soul = null;
        }

        public override void Load()
        {
            Config.Load();

            AreusCurrency = CustomCurrencyManager.RegisterCurrency(new AreusCurrency(ModContent.ItemType<Items.AreusCoin>(), 999L));
            OverdriveKey = KeybindLoader.RegisterKeybind(this, "Toggle Overdrive", "F");
            TomeKey = KeybindLoader.RegisterKeybind(this, "Cycle Knowledge Base", "N");
            EmeraldTeleportKey = KeybindLoader.RegisterKeybind(this, "Emerald Teleport", "Z");
            ShadowCloak = KeybindLoader.RegisterKeybind(this, "Toggle Shadow Cloak", "`");
            ShadowTeleport = KeybindLoader.RegisterKeybind(this, "Shadow Teleport", "X");
            PhaseSwitch = KeybindLoader.RegisterKeybind(this, "Toggle Phase Type", "RightAlt");
            QuickCharge = KeybindLoader.RegisterKeybind(this, "Quick Charge", "C");

            if (!Main.dedServ)
            {
                /*
                if (Config.MegamergeVisual)
                {
                    AddEquipTexture(new Items.LivingMetalHead(), null, EquipType.Head, "OmegaMetalHead", "ShardsOfAtheria/Items/Accessories/OmegaMetal_Head");
                    AddEquipTexture(new Items.LivingMetalBody(), null, EquipType.Body, "OmegaMetalBody", "ShardsOfAtheria/Items/Accessories/OmegaMetal_Body", "ShardsOfAtheria/Items/Accessories/OmegaMetal_Arms");
                    AddEquipTexture(new Items.LivingMetalLegs(), null, EquipType.Legs, "OmegaMetalLegs", "ShardsOfAtheria/Items/Accessories/OmegaMetal_Legs");

                    AddEquipTexture(new Items.LivingMetalHead(), null, EquipType.Head, "LivingMetalHead", "ShardsOfAtheria/Items/LivingMetal_Head");
                    AddEquipTexture(new Items.LivingMetalBody(), null, EquipType.Body, "LivingMetalBody", "ShardsOfAtheria/Items/LivingMetal_Body", "ShardsOfAtheria/Items/LivingMetal_Arms");
                    AddEquipTexture(new Items.LivingMetalLegs(), null, EquipType.Legs, "LivingMetalLegs", "ShardsOfAtheria/Items/LivingMetal_Legs");
                }
                */
                //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicName"), ModContent.ItemType<MusicBox>(), ModContent.TileType<MusicBoxTile>());

                // Areus Charge Bar
                AreusResourceBar bar = new AreusResourceBar();
                _areusResourceBarUserInterface = new UserInterface();
                _areusResourceBarUserInterface.SetState(bar);

                // Overdrive Time Bar
                OverdriveTimeBar overdriveBar = new OverdriveTimeBar();
                _overdriveTimeBarUserInterface = new UserInterface();
                _overdriveTimeBarUserInterface.SetState(overdriveBar);
            }
        }
        /*
        public override void UpdateUI(GameTime gameTime)
        {
            _areusResourceBarUserInterface?.Update(gameTime);
            _overdriveTimeBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Areus Charge Bar",
                    delegate {
                        _areusResourceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Overdrive Time",
                    delegate {
                        _overdriveTimeBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        */
        public override void AddRecipeGroups()
        {
            EvilMaterial = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Material",
                   ItemID.ShadowScale, ItemID.TissueSample);
            RecipeGroup.RegisterGroup("SM:EvilMaterials", EvilMaterial);

            EvilGun = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Gun",
                   ItemID.Musket, ItemID.TheUndertaker);
            RecipeGroup.RegisterGroup("SM:EvilGuns", EvilGun);

            Copper = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CopperBar)}",
                   ItemID.CopperBar, ItemID.TinBar);
            RecipeGroup.RegisterGroup("SM:CopperBars", Copper);

            Silver = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SilverBar)}",
                   ItemID.SilverBar, ItemID.TungstenBar);
            RecipeGroup.RegisterGroup("SM:SilverBars", Silver);

            Gold = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}",
                   ItemID.GoldBar, ItemID.PlatinumBar);
            RecipeGroup.RegisterGroup("SM:GoldBars", Gold);

            EvilBar = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Bar",
                   ItemID.DemoniteBar, ItemID.CrimtaneBar);
            RecipeGroup.RegisterGroup("SM:EvilBars", EvilBar);

            Cobalt = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 1 Bar",
                   ItemID.CobaltBar, ItemID.PalladiumBar);
            RecipeGroup.RegisterGroup("SM:Tier1Bars", Cobalt);

            Mythril = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 2 Bar",
                   ItemID.MythrilBar, ItemID.OrichalcumBar);
            RecipeGroup.RegisterGroup("SM:Tier2Bars", Mythril);

            Adamantite = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 3 Bar",
                   ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("SM:Tier3Bars", Adamantite);

            Soul = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Soul",
                   ItemID.SoulofFlight, ItemID.SoulofFright, ItemID.SoulofLight, ItemID.SoulofMight, ItemID.SoulofNight, ItemID.SoulofSight, ModContent.ItemType<SoulOfDaylight>(),
                   ModContent.ItemType<SoulOfStarlight>(), ModContent.ItemType<SoulOfSpite>());
            RecipeGroup.RegisterGroup("SM:Souls", Soul);
        }

        public override void PostSetupContent()
        {
            List<int> DeathItemList = new List<int>(){
                ModContent.ItemType<DeathEssence>(),
                ModContent.ItemType<DeathsScythe>()
            };
            List<int> NovaItemList = new List<int>(){
                ModContent.ItemType<GildedValkyrieWings>(),
                ItemID.Feather,
                ItemID.GoldBar,
                ModContent.ItemType<ValkyrieCrown>(),
                ModContent.ItemType<ValkyrieBlade>(),
                ModContent.ItemType<ValkyrieStormLance>()
            };
            /*
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "Death", 0f, (Func<bool>)(() => ModContent.GetInstance<SMWorld>().downedDeath), "Use a [i:" + ModContent.ItemType<AncientCoin>() + "] and prepare for pain");
                bossChecklist.Call("AddToBossSpawnItems", "Shards of Atheria", "Death", ModContent.ItemType<AncientCoin>());
                bossChecklist.Call("AddToBossLoot", "Shards of Atheria", "Death", NovaItemList);

                bossChecklist.Call("AddBossWithInfo", "Nova Skyloft", 3.5f, (Func<bool>)(() => ModContent.GetInstance<SMWorld>().downedValkyrie), "Use a [i:" + ModContent.ItemType<ValkyrieCrest>() + "] anywhere but the Crimson/Corruption during the daytime");
                bossChecklist.Call("AddToBossSpawnItems", "Shards of Atheria", "Nova Skyloft", ModContent.ItemType<ValkyrieCrest>());
                bossChecklist.Call("AddToBossLoot", "Shards of Atheria", "Nova Skyloft", NovaItemList);

                bossChecklist.Call("GetBossInfoDictionary", this, "1.1");
            }*/
        }
    }
}