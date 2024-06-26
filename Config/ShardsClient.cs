﻿using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria.Config
{
    [BackgroundColor(164, 153, 190)]
    public class ShardsClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Mechanics")]
        [DefaultValue(false)]
        public bool instantAbsorb;

        [DefaultValue(false)]
        public bool entryView;

        [DefaultValue(1f)]
        [DrawTicks]
        [Increment(0.05f)]
        public float screenShakeIntensity;

        [Header("Sounds")]
        [DefaultValue(true)]
        public bool biometalSound;

        [DefaultValue(true)]
        public bool chargeSound;

        [DefaultValue(true)]
        public bool reactorBeep;

        [Header("UI")]
        [ReloadRequired()]
        public bool upgradeWarning;

        [ReloadRequired()]
        public Vector2 loreTabletOffset;

        [Header("Misc")]
        [DefaultValue(true)]
        public bool dialogue;

        [DefaultValue(15)]
        public int dialogueDuration;

        [DefaultValue(200)]
        [Range(1, 200)]
        //[DrawTicks()]
        [Slider()]
        public int speedLimit;

        [DefaultValue(true)]
        [ReloadRequired()]
        public bool windowTitle;

        [DefaultValue(false)]
        public bool debug;
    }
}
