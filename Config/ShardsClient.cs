using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria.Config
{
    [BackgroundColor(164, 153, 190)]
    public class ShardsClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Mechanics")]
        [DefaultValue(false)]
        public bool instantAbsorb;

        [DefaultValue(false)]
        public bool entryView;

        [Header("Sounds")]
        [DefaultValue(true)]
        public bool biometalSound;

        [DefaultValue(true)]
        public bool reactorBeep;

        [Header("UI")]
        [ReloadRequired()]
        public Vector2 loreTabletOffset;

        [Header("Misc")]
        [DefaultValue(true)]
        public bool sinfulArmamentText;

        [DefaultValue(true)]
        [ReloadRequired()]
        public bool windowTitle;

        [OptionStrings(new[] { "Off", "Console only", "Console and Chat" })]
        [DrawTicks()]
        [DefaultValue("Off")]
        public string debug;
    }
}
