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

        [Header("Dialogue")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool yamikoInsult;

        [Header("Mechanics")]
        [DefaultValue(false)]
        public bool cluelessNPCs;

        [DefaultValue(true)]
        public bool catchableNPC;

        [Header("Misc")]
        [DefaultValue(false)]
        public bool altCaveHarpy;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool overrideTypes;

        [Header("Commands")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool genericCommand;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool resetDowned;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool resetSlayer;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool slayBosses;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool validateSlayer;
    }
}
