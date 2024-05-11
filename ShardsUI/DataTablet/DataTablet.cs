using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.DataTablet
{
    internal class ScreenState : UIState
    {
        private UIText text;
        private UIImage screen;
        readonly string DiskPath = "ShardsOfAtheria/ShardsUI/DataTablet/DataDisks/";

        public override void OnInitialize()
        {
            screen = new UIImage(ModContent.Request<Texture2D>("ShardsOfAtheria/ShardsUI/DataTablet/Tablet")); // Tablet image
            screen.SetRectangle(0, 0, 700, 500);

            text = new UIText("Loading...", 1f); // text to read disk contents
            text.SetRectangle(40, 40, 624, 424);
            screen.Append(text);

            UIImageButton closeButton = new(TextureAssets.CraftDownButton);
            closeButton.SetRectangle(13, 13, 16, 16);
            closeButton.OnLeftClick += (a, b) => ModContent.GetInstance<DataTabletUI>().HideTablet();
            screen.Append(closeButton);

            Append(screen);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            screen.CenterOnScreen();
            if (screen.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            ShardsPlayer shardsPlayer = Main.LocalPlayer.Shards();
            int readingDisk = shardsPlayer.readingDisk;

            string key = "Mods.ShardsOfAtheria.DiskFile.DataDisk";
            if (readingDisk < 100)
            {
                key += "0";
            }
            if (readingDisk < 10)
            {
                key += "0";
            }
            key += readingDisk;
            text.SetText(Language.GetTextValue(key));
        }
    }

    class DataTabletUI : ModSystem
    {
        internal ScreenState TabletScreenState;
        private UserInterface tabletInterface;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                TabletScreenState = new();
                tabletInterface = new();
            }
        }

        public void ShowTablet()
        {
            tabletInterface.SetState(TabletScreenState);
        }
        public void HideTablet()
        {
            tabletInterface.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            tabletInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Data Tablet",
                    delegate
                    {
                        tabletInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
