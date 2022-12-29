using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria.Config
{
    [BackgroundColor(164, 153, 190)]
    [Label("Client Side")]
    public class ShardsClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("[i:4818] Mechanics")]
        [Label("[i:583] Instant Soul Crystal Absorption")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the wait for absorbing Soul Crystals")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        public bool instantAbsorb;

        [Label("[i:149] View All entries")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the ability to view all Necronomicon entries\nregardless of whether or not the Soul Crystal is absorbed or not")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        public bool entryView;

        [Header("[i:890] Sounds")]
        [Label("[i:890] Biometal Equip Sound")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the sound played when Biometal is equipped")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool biometalSound;
        [Label("[i:890] Reactor Meltdown Sound")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the sound played when Reactor Meltdown is spinning")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool reactorBeep;

        [Header("[i:3617] UI")]
        [Label("[i:3617] Tablet Position")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Move the lore tablet")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [ReloadRequired()]
        public Vector2 loreTabletOffset;

        [Header("[i:1344] Misc")]
        [Label("[i:3617] Sinful Armament dialogue")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the dialogue when using Sinful Armament")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool sinfulArmamentText;
        [Label("[i:3617] Window Title")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles changing the window title")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired()]
        public bool windowTitle;
    }
}
