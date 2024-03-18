using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ShardsUI
{
    public class GemCoreConfigOpener : BuilderToggle
    {
        public override bool Active() => Main.LocalPlayer.Gem().masterCoreUI;
        public override int NumberOfStates => 2;
        public override string HoverTexture => Texture + "_Hover";

        public override string DisplayValue()
        {
            return "Master Gem Core Config";
        }

        public override Color DisplayColorTexture()
        {
            if (CurrentState == 0)
            {
                return Color.Gray;
            }
            return base.DisplayColorTexture();
        }

        /*
        public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
        {
            Color[] colors = new[] { Color.Red, Color.Blue, Color.Green, Color.Yellow };
            drawParams.Color = colors[CurrentState];
            return true;
        }
        */
    }
}
