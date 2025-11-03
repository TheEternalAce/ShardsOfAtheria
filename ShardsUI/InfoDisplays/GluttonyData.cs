using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ShardsUI.InfoDisplays
{
    public class GluttonyData : InfoDisplay
    {
        public override string Texture => SoA.SinDataTexture;

        public override bool Active() => Main.LocalPlayer.Sinner().sinID == SinnerPlayer.GLUTTONY;

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            var player = Main.LocalPlayer;
            var gluttony = player.Sinner();
            return "" + gluttony.hunger;
        }
    }
}
