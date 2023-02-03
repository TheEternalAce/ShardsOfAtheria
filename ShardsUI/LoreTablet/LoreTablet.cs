using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.DataDisks;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
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

        // Stores the offset from the top left of the UIPanel while dragging
        private Vector2 offset;
        // A flag that checks if the panel is currently being dragged
        private bool dragging;

        public override void MouseDown(UIMouseEvent evt)
        {
            // When you override UIElement methods, don't forget call the base method
            // This helps to keep the basic behavior of the UIElement
            base.MouseDown(evt);
            // When the mouse button is down, then we start dragging
            DragStart(evt);
        }

        public override void MouseUp(UIMouseEvent evt)
        {
            base.MouseUp(evt);
            // When the mouse button is up, then we stop dragging
            DragEnd(evt);
        }

        private void DragStart(UIMouseEvent evt)
        {
            // The offset variable helps to remember the position of the panel relative to the mouse position
            // So no matter where you start dragging the panel, it will move smoothly
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt)
        {
            Vector2 endMousePosition = evt.MousePosition;
            dragging = false;

            Left.Set(endMousePosition.X - offset.X, 0f);
            Top.Set(endMousePosition.Y - offset.Y, 0f);

            Recalculate();
        }

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
            ShardsPlayer shardsPlayer = Main.LocalPlayer.ShardsOfAtheria();

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
            if (display && area.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }

            // Here we check if the DragableUIPanel is outside the Parent UIElement rectangle
            // (In our example, the parent would be ExampleCoinsUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
            // By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution
            var parentSpace = GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                // Recalculate forces the UI system to do the positioning math again.
                Recalculate();
            }

            ShardsPlayer shardsPlayer = Main.LocalPlayer.ShardsOfAtheria();

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
        }
    }
}
