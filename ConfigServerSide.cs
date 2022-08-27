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
        [Tooltip("Enable for custom use sound, disable for vanilla use sound")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool metalBladeSound;

        [Label("Non-consumable boss summons")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable for to make Shards of Atheria boss summons non-cconsumable")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool nonConsumeBoss;

        [Label("Upgrade items change")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Enable for to enable changes to Life Crystal, Mana Crystal and Life Fruit\n" +
            "(Use time 15, auto reuse, use turn)")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool upgradeChange;
    }
}
