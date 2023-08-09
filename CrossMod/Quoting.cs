using ShardsOfAtheria.NPCs.Town.TheArchivist;
using ShardsOfAtheria.NPCs.Town.TheAtherian;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.CrossMod
{
    internal class ShopQuotesMod : ModSupport<ShopQuotesMod>
    {
        public const string Key = "Mods.ShardsOfAtheria.ShopQuote.";

        public override void SetStaticDefaults()
        {
            Instance.Call("SetDefaultKey", Mod, Key);

            ModContent.GetInstance<Atherian>().SetupShopQuotes(Instance);
            ModContent.GetInstance<Archivist>().SetupShopQuotes(Instance);
        }

        public static string GetKey(string key)
        {
            return Key + key;
        }

        public static string GetTextValue(string key)
        {
            return Language.GetTextValue(GetKey(key));
        }

        public static LocalizedText GetText(string key)
        {
            return Language.GetText(GetKey(key));
        }
    }
}