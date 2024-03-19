using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.AreusVoid
{
    public class AreusVoidUI : UIState
    {
        UIHoverImage orb;
        const string ORB_PATH = "ShardsOfAtheria/ShardsUI/AreusVoid/AreusVoidOrb";
        private Color gradientA;
        private Color gradientB;

        public override void OnInitialize()
        {
            orb = new(ModContent.Request<Texture2D>(ORB_PATH), "");
            orb.SetRectangle(0, 0, 66, 18);
            Append(orb);

            gradientA = new Color(0, 0, 0);
            gradientB = new Color(255, 255, 255);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var areusPlayer = Main.LocalPlayer.Areus();
            // Calculate quotient
            float quotient = (float)areusPlayer.imperialVoid / AreusArmorPlayer.VOID_MAX; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = orb.GetInnerDimensions().ToRectangle();
            hitbox.X += 6;
            hitbox.Width -= 12;
            hitbox.Y += 6;
            hitbox.Height -= 12;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int bottom = hitbox.Bottom;
            int top = hitbox.Top;
            int steps = (int)((bottom - top) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                float percent = (float)i / steps; // Alternate Gradient Approach
                //float percent = (float)i / (bottom - top);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value,
                    new Rectangle(hitbox.X, hitbox.Bottom - i, hitbox.Width, 1),
                    Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var player = Main.LocalPlayer;
            var areusPlayer = player.Areus();
            orb.hoverText = $"Imperial Void: {areusPlayer.imperialVoid} / {AreusArmorPlayer.VOID_MAX}";

            float x = Main.screenWidth - 480;
            float y = 10;
            orb.SetRectangle(x, y, 64, 64);

            if (!areusPlayer.imperialSet)
            {
                ModContent.GetInstance<AreusVoidSystem>().HideBar();
            }
        }
    }

    public class AreusVoidSystem : ModSystem
    {
        internal AreusVoidUI VoidOrb;
        private UserInterface barInterface;

        public override void Load()
        {
            VoidOrb = new AreusVoidUI();
            VoidOrb.Activate();
            barInterface = new UserInterface();
            barInterface.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            barInterface?.Update(gameTime);
        }

        public void ShowBar()
        {
            barInterface.SetState(VoidOrb);
        }
        public void HideBar()
        {
            barInterface.SetState(null);
        }
        public bool BarShowing => barInterface.CurrentState != null;

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Areus Void bar",
                    delegate
                    {
                        barInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
