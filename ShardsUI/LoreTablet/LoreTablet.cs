using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.DataDisks;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.UI.LoreTablet
{
    internal class ScreenState : UIState
    {
        private bool display = false;

        private UIElement area;
        private UIText text;
        private UIImage screen;

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-350, 0.5f); // Place the tablet at the center of the screen.
            area.Top.Set(-250, 0.5f);
            area.Width.Set(700, 0f); // We will be placing the following 2 UIElements within this 700x500 area.
            area.Height.Set(500, 0f);

            screen = new UIImage(ModContent.Request<Texture2D>("ShardsOfAtheria/ShardsUI/LoreTablet/Tablet")); // Tablet image
            screen.Left.Set(0, 0f);
            screen.Top.Set(0, 0f);
            screen.Width.Set(700, 0f);
            screen.Height.Set(500, 0f);

            text = new UIText("Loading", 1f); // text to read disk contents
            text.Left.Set(40, 0f);
            text.Top.Set(40, 0f);
            text.Width.Set(624, 0f);
            text.Height.Set(424, 0f);

            area.Append(screen);
            area.Append(text);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SoAPlayer shardsPlayer = Main.LocalPlayer.GetModPlayer<SoAPlayer>();

            if (Main.LocalPlayer.HeldItem.ModItem is DataDisk disk)
            {
                if (disk.diskType != shardsPlayer.readingDisk)
                {
                    display = false;
                    disk.readingDisk = false;
                    return;
                }
                disk.readingDisk = true;
            }
            else
            {
                display = false;
                return;
            }
            display = true;
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SoAPlayer shardsPlayer = Main.LocalPlayer.GetModPlayer<SoAPlayer>();

            switch (shardsPlayer.readingDisk)
            {
                default:
                    text.SetText(Language.GetTextValue("Mods.ShardsOfAtheria.DiskFile.DiskError"));
                    break;
                case 1:
                    text.SetText(Language.GetTextValue("Mods.ShardsOfAtheria.DiskFile.DataDisk1"));
                    break;
                case 2:
                    text.SetText(Language.GetTextValue("Mods.ShardsOfAtheria.DiskFile.DataDisk2"));
                    break;
                case 3:
                    text.SetText(Language.GetTextValue("Mods.ShardsOfAtheria.DiskFile.DataDisk3"));
                    break;
                case 4:
                    text.SetText(Language.GetTextValue("Mods.ShardsOfAtheria.DiskFile.DataDisk4"));
                    break;
                case 5:
                    text.SetText(Language.GetTextValue("Mods.ShardsOfAtheria.DiskFile.DataDisk5"));
                    break;
            }
            if (display && area.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}
