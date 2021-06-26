using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.UI;

namespace SagesMania
{
    public class SagesMania : Mod
    {
        public static int AreusCurrency;
        public static int DryskalCurrency;
        public static ModHotKey OverdriveKey;
        public static ModHotKey TomeKey;
        public static ModHotKey EmeraldTeleportKey;

        public override void Load()
        {
            AreusCurrency = CustomCurrencyManager.RegisterCurrency(new AreusCurrency(ModContent.ItemType<Items.AreusCoin>(), 999L));
            DryskalCurrency = CustomCurrencyManager.RegisterCurrency(new AreusCurrency(ModContent.ItemType<Items.Dryskal>(), 999L));
            OverdriveKey = RegisterHotKey("Toggle Overdrive", "F");
            TomeKey = RegisterHotKey("Cycle Knowledge Base", "N");
            EmeraldTeleportKey = RegisterHotKey("Emerald Teleport", "Z");
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup copper = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Copper Bar", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("SM:CopperBars", copper);

            RecipeGroup gold = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("SM:GoldBars", gold);

            RecipeGroup silver = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("SM:SilverBars", silver);

            RecipeGroup evilBar = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("SM:EvilBars", evilBar );

            RecipeGroup adamantite = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 3 Bar", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("SM:AdamantiteBars", adamantite);

            RecipeGroup souls = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Soul", new int[]
            {
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