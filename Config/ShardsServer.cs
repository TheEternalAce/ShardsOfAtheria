using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria.Config
{
    [BackgroundColor(164, 153, 190)]
    public class ShardsServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Item")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool metalBladeSound;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool nonConsumeBoss;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool upgradeChange;

        [Header("Dialogue")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool yamikoInsult;

        [Header("Mechanics")]
        [DefaultValue(false)]
        public bool cluelessNPCs;

        [DrawTicks]
        [OptionStrings(new string[] { "Off", "No Use Turn", "Mouse Direction" })]
        [DefaultValue("Mouse Direction")]
        [ReloadRequired]
        public string betterWeapon;

        [Header("Misc")]
        public bool altCaveHarpy;
    }
}
