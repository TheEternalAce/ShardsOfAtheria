using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.UI;
using SagesMania.UI;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SagesMania.Items;

namespace SagesMania
{
    public class SagesMania : Mod
    {
        private UserInterface _areusResourceBarUserInterface;
        private UserInterface _overdriveTimeBarUserInterface;

        public static int AreusCurrency;
        public static int DryskalCurrency;
        public static ModHotKey OverdriveKey;
        public static ModHotKey TomeKey;
        public static ModHotKey EmeraldTeleportKey;
        public static ModHotKey ShadowCloak;
        public static ModHotKey ShadowTeleport;
        public static ModHotKey Megamerge;
        public static ModHotKey PhaseSwitch;

        public override void Load()
        {
            AreusCurrency = CustomCurrencyManager.RegisterCurrency(new AreusCurrency(ModContent.ItemType<Items.AreusCoin>(), 999L));
            DryskalCurrency = CustomCurrencyManager.RegisterCurrency(new AreusCurrency(ModContent.ItemType<Items.Dryskal>(), 999L));
            OverdriveKey = RegisterHotKey("Toggle Overdrive", "F");
            TomeKey = RegisterHotKey("Cycle Knowledge Base", "N");
            EmeraldTeleportKey = RegisterHotKey("Emerald Teleport", "Z");
            ShadowCloak = RegisterHotKey("Toggle Shadow Cloak", "`");
            ShadowTeleport = RegisterHotKey("Shadow Teleport", "X");
            Megamerge = RegisterHotKey("Megamerge", "LeftAlt");
            PhaseSwitch = RegisterHotKey("Toggle Phase Type", "RightAlt");

            if (!Main.dedServ)
            {
                AddEquipTexture(new Items.Accessories.LivingMetalHead(), null, EquipType.Head, "OmegaMetalHead", "SagesMania/Items/Accessories/OmegaMetal_Head");
                AddEquipTexture(new Items.Accessories.LivingMetalBody(), null, EquipType.Body, "OmegaMetalBody", "SagesMania/Items/Accessories/OmegaMetal_Body", "SagesMania/Items/Accessories/OmegaMetal_Arms");
                AddEquipTexture(new Items.Accessories.LivingMetalLegs(), null, EquipType.Legs, "OmegaMetalLegs", "SagesMania/Items/Accessories/OmegaMetal_Legs");

                AddEquipTexture(new Items.Accessories.LivingMetalHead(), null, EquipType.Head, "LivingMetalHead", "SagesMania/Items/Accessories/LivingMetal_Head");
                AddEquipTexture(new Items.Accessories.LivingMetalBody(), null, EquipType.Body, "LivingMetalBody", "SagesMania/Items/Accessories/LivingMetal_Body", "SagesMania/Items/Accessories/LivingMetal_Arms");
                AddEquipTexture(new Items.Accessories.LivingMetalLegs(), null, EquipType.Legs, "LivingMetalLegs", "SagesMania/Items/Accessories/LivingMetal_Legs");

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
                    "SagesMania: Areus Charge Bar",
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
                    "SagesMania: Overdrive Time",
                    delegate {
                        _overdriveTimeBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup copper = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Copper Bar", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("SM:CopperBars", copper);

            RecipeGroup silver = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("SM:SilverBars", silver);

            RecipeGroup gold = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("SM:GoldBars", gold);

            RecipeGroup evilBar = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("SM:EvilBars", evilBar );

            RecipeGroup cobalt = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 1 Bar", new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup.RegisterGroup("SM:CobaltBars", cobalt);

            RecipeGroup mythril = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 2 Bar", new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup.RegisterGroup("SM:MythrilBars", mythril);

            RecipeGroup adamantite = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 3 Bar", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("SM:AdamantiteBars", adamantite);

            RecipeGroup souls = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Soul", new int[]
            {
                ModContent.ItemType<SoulOfDaylight>(),
                ModContent.ItemType<SoulOfStarlight>(),
                ModContent.ItemType<SoulOfSpite>(),
                ItemID.SoulofFlight,
                ItemID.SoulofFright,
                ItemID.SoulofLight,
                ItemID.SoulofMight,
                ItemID.SoulofNight,
                ItemID.SoulofSight
            });
            RecipeGroup.RegisterGroup("SM:Souls", souls);
        }
    }
}