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
        public bool metalBladeSound;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool speedCapCrafting;
        [DefaultValue(false)]
        public bool throwingWeapons;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool nonConsumeBoss;

        [Header("Dialogue")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool yamikoInsult;

        [Header("NPC")]
        [DefaultValue(false)]
        public bool cluelessNPCs;

        [DefaultValue(true)]
        public bool catchableNPC;

        [DefaultValue(false)]
        public bool altCaveHarpy;

        [DefaultValue(true)]
        public bool antiGrief;

        [Header("Misc")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool overrideTypes;
    }
}
