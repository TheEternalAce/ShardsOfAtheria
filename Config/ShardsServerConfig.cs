using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria.Config
{
    [BackgroundColor(164, 153, 190)]
    [Label("Server Side")]
    public class ShardsServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("[i:1] Items")]
        [Label("[i:890] Metal Blade use sound")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable for custom use sound, disable for vanilla use sound")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool metalBladeSound;

        [Label("[i:43] Non-consumable boss summons")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable for to make Shards of Atheria boss summons non-consumable")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool nonConsumeBoss;

        [Label("[i:29] Upgrade items change")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable for to enable changes to Life Crystal, Mana Crystal and Life Fruit\n" +
            "(Use time 15, auto reuse, use turn)")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool upgradeChange;

        [Header("[i:3617] Dialogue")]
        [Label("[i:3617] Yamiko Insult")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Disable if you want to keep your feelings intact")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool insult;

        [Header("[i:4818] Mechanics")]
        [Label("[i:893] Clueless Atherian")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable to allow the atherian to upgrade items even while in Slayer mode")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        public bool cluelessNPCs;

        [Label("[i:1] Directional Weapons*")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [DrawTicks]
        [OptionStrings(new string[] { "Off", "No Use Turn", "Mouse direction" })]
        [DefaultValue("Mouse direction")]
        [ReloadRequired]
        public string betterWeapon;

        [Label("[i:46] Aggression")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Damage, speed and aggro increases as you damage enemies")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool aggression;

        [Label("[i:3998] Aggression Defense Reduction")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Defense decreases by amount as you damage enemies\n" +
            "Set to 0 to disable")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(1)]
        public int aggressionDR;

        [Label("[i:893] Experimental Features")]
        [Tooltip("Enables experimental/work in progress features")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool experimental;
    }
}
