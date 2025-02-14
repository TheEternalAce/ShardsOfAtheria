using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ShardsUI.InfoDisplays
{
    public class GluttonyData : InfoDisplay
    {
        public override string Texture => SoA.SinDataTexture;

        public override bool Active() => Main.LocalPlayer.Gluttony().soulActive;

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            var player = Main.LocalPlayer;
            var gluttony = player.Gluttony();
            return "" + gluttony.hunger;
        }
    }
}
