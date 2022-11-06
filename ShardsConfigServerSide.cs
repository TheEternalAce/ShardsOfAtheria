using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria
{
    [BackgroundColor(164, 153, 190)]
    [Label("Server Side")]
    public class ShardsConfigServerSide : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

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

        [Label("[i:279] Return throwing damage")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable make old throwing weapons and certain other weapons deal throwing damage")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        [ReloadRequired]
        public bool throwingDamage;

        [Label("[i:279] Throwing rework")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable to use throwing weapon reworks (Does nothing until Shards of Atheria 1.0 release)")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        [ReloadRequired]
        public bool throwingRework;

        [Label("[i:29] Upgrade items change")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable for to enable changes to Life Crystal, Mana Crystal and Life Fruit\n" +
            "(Use time 15, auto reuse, use turn)")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool upgradeChange;

        [Label("[i:3617] Yamiko Insult")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Disable if you want to keep your feelings intact")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool insult;
    }
}
