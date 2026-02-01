using Humanizer;
using Terraria.Localization;

namespace ShardsOfAtheria.Utilities
{
    public partial class ShardsHelpers
    {
        public static string Localize(string content, params object[] args)
        {
            string key = "Mods.ShardsOfAtheria." + content;
            if (Language.Exists(key))
            {
                if (args.Length > 0) return Language.GetText(key).Value.FormatWith(args);
                else return Language.GetText(key).Value;
            }
            Language.GetOrRegister(key);
            return "No key found.";
        }
        public static string LocalizeCommon(string content, params object[] args)
        {
            return Localize("Common." + content, args);
        }
        public static string LocalizeNecronomicon(string content, params object[] args)
        {
            return Localize("Necronomicon." + content, args);
        }
        public static string LocalizeCondition(string content, params object[] args)
        {
            return Localize("Conditions." + content, args);
        }

        public static NetworkText NetworkTextKey(string content)
        {
            return NetworkText.FromKey("Mods.ShardsOfAtheria." + content);
        }
        public static NetworkText NetworkTextKeyCommon(string content)
        {
            return NetworkTextKey("Common." + content);
        }

        public static LocalizedText LocalizedText(string content)
        {
            return Language.GetText("Mods.ShardsOfAtheria." + content);
        }
    }
}
