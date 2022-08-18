using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria
{
    [BackgroundColor(164, 153, 190)]
    [Label("Client Side")]
    public class ConfigServerSide : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Metal Blade use sound")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable for custom use sound, disable for basic use sound")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool metalBladeSound;
    }
}
