using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI
{
    class OverdriveEnergyBar : UIState
    {
        UIHoverImage bar;
        UIImage[] ticks = new UIImage[10];
        string path = "ShardsOfAtheria/ShardsUI/OverdriveBar/";

        public override void OnInitialize()
        {
            bar = new(ModContent.Request<Texture2D>(path + "OverdriveEnergyBar"), "");
            bar.SetRectangle(0, 0, 18, 66);

            for (int i = 0; i < ticks.Length; i++)
            {
                float x = 6;
                float startY = 42;
                float y = startY - 4 * i;

                ticks[i] = new(ModContent.Request<Texture2D>(path + "BarTick"));
                ticks[i].SetRectangle(x, y, 8, 4);
            }

            Append(bar);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int x = Main.screenWidth - 320;
            int y = 100;
            bar.SetRectangle(x, y, 18, 66);

            var player = Main.LocalPlayer;
            var shards = player.Shards();
            float ticksToDraw = shards.overdriveTimeCurrent / 30f;
            ticksToDraw = (float)Math.Round(ticksToDraw);

            bar.hoverText = $"{shards.overdriveTimeCurrent} / {shards.overdriveTimeMax2}";
            bar.RemoveAllChildren();
            for (int i = 0; i < ticksToDraw; i++)
            {
                bar.Append(ticks[i]);
            }
        }
    }

    public class OverdriveEnergyBarSystem : ModSystem
    {
        internal OverdriveEnergyBar OverdriveBar;
        private UserInterface barInterface;

        public override void Load()
        {
            OverdriveBar = new OverdriveEnergyBar();
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
                    "ShardsOfAtheria: Overdrive bar",
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
