using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria.Config
{
    [BackgroundColor(164, 153, 190)]
    [Label("$Mods.ShardsOfAtheria.Config.ServerLabel")]
    public class ShardsServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.ShardsOfAtheria.Config.ItemHeader")]
        [Label("$Mods.ShardsOfAtheria.Config.MetalBladeUseSound")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("$Mods.ShardsOfAtheria.ConfigDesc.MetalBladeUseSound")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool metalBladeSound;

        [Label("$Mods.ShardsOfAtheria.Config.BossSummon")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("$Mods.ShardsOfAtheria.ConfigDesc.BossSummon")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool nonConsumeBoss;

        [Label("$Mods.ShardsOfAtheria.Config.UpgradeItems")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("$Mods.ShardsOfAtheria.ConfigDesc.UpgradeItems")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool upgradeChange;

        [Header("$Mods.ShardsOfAtheria.Config.DialogueHeader")]
        [Label("$Mods.ShardsOfAtheria.Config.YamikoInsult")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("$Mods.ShardsOfAtheria.ConfigDesc.YamikoInsult")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool yamikoInsult;

        [Header("$Mods.ShardsOfAtheria.Config.MechanicsHeader")]
        [Label("$Mods.ShardsOfAtheria.Config.Clueless")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("$Mods.ShardsOfAtheria.ConfigDesc.Clueless")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        public bool cluelessNPCs;

        [Label("$Mods.ShardsOfAtheria.Config.BetterWeapon")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [DrawTicks]
        [OptionStrings(new string[] { "Off", "No Use Turn", "Mouse Direction" })]
        [DefaultValue("Mouse Direction")]
        [ReloadRequired]
        public string betterWeapon;

        [Header("$Mods.ShardsOfAtheria.Config.MiscHeader")]
        [Label("$Mods.ShardsOfAtheria.Config.AltCaveHarpy")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("$Mods.ShardsOfAtheria.ConfigDesc.AltCaveHarpy")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        public bool altCaveHarpy;
    }
}
