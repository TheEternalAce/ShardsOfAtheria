using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI
{
    public class AreusEnergyUI : UIState
    {
        UIHoverImage bar;
        const string BAR_PATH = "ShardsOfAtheria/ShardsUI/AreusEnergy/AreusEnergyBar";
        private Color gradientA;
        private Color gradientB;

        public override void OnInitialize()
        {
            bar = new(ModContent.Request<Texture2D>(BAR_PATH), "");
            bar.SetRectangle(0, 0, 66, 18);
            Append(bar);

            gradientA = new Color(4, 33, 88);
            gradientB = new Color(187, 249, 238);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var areusPlayer = Main.LocalPlayer.Areus();
            // Calculate quotient
            float quotient = (float)areusPlayer.areusEnergy / AreusArmorPlayer.AREUS_ENERGY_MAX; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = bar.GetInnerDimensions().ToRectangle();
            hitbox.X += 6;
            hitbox.Width -= 12;
            hitbox.Y += 6;
            hitbox.Height -= 12;

            string path = "ShardsOfAtheria/ShardsUI/AreusEnergy/AreusEnergyBar_Back";
            spriteBatch.Draw(ModContent.Request<Texture2D>(path).Value,
                hitbox,
                Color.White);

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int right = hitbox.Right;
            int left = hitbox.Left;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                float percent = (float)i / steps; // Alternate Gradient Approach
                //float percent = (float)i / (bottom - top);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value,
                    new Rectangle(hitbox.X + i, hitbox.Top, 1, hitbox.Height),
                    Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var player = Main.LocalPlayer;
            var areusPlayer = player.Areus();
            bar.hoverText = $"Areus Energy: {areusPlayer.areusEnergy} / {AreusArmorPlayer.AREUS_ENERGY_MAX}";

            var barDimensions = bar.GetDimensions();
            float x = Main.screenWidth / 2 - barDimensions.Width / 2f;
            float y = Main.screenHeight / 2 + 90;
            bar.SetRectangle(x, y, 66, 18);

            if (!areusPlayer.guardSet)
            {
                ModContent.GetInstance<AreusEnergySystem>().HideBar();
            }
        }
    }

    public class AreusEnergySystem : ModSystem
    {
        internal AreusEnergyUI OverdriveBar;
        private UserInterface barInterface;

        public override void Load()
        {
            OverdriveBar = new AreusEnergyUI();
            OverdriveBar.Activate();
            barInterface = new UserInterface();
            barInterface.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            barInterface?.Update(gameTime);
        }

        public void ShowBar()
        {
            barInterface.SetState(OverdriveBar);
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
                    "ShardsOfAtheria: Areus Energy bar",
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
