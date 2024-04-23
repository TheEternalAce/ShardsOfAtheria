using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
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

        public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams)
        {
            Color[] colors = [Color.Gray, Color.White];
            drawParams.Color = colors[CurrentState];
            return true;
        }
    }
}
