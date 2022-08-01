using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria
{
    [BackgroundColor(164, 153, 190)]
    [Label("Client Side")]
    public class ConfigClientSide : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Mechanics")]
        [Label("Mega Gem Core Gravitation")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles Gravitation buff from Mega Gem Core")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool megaGemCoreGrav;

        [Label("Instant Soul Crystal Absorption")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the wait for absorbing Soul Crystals")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        public bool instantAbsorb;

        [Label("Biometal Equip Sound")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the sound played when Biometal is equipped")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool biometalSound;
    }
}
