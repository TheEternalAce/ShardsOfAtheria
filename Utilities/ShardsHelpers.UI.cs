using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;

namespace ShardsOfAtheria.Utilities
{
    partial class ShardsHelpers
    {
        public static void SetRectangle(this UIElement uiElement, float left, float top, float width, float height)
        {
            uiElement.Left.Set(left, 0f);
            uiElement.Top.Set(top, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }

        public static void SetRectangle(this UIElement uiElement, Rectangle rect)
        {
            uiElement.Left.Set(rect.X, 0f);
            uiElement.Top.Set(rect.Y, 0f);
            uiElement.Width.Set(rect.Width, 0f);
            uiElement.Height.Set(rect.Height, 0f);
        }

        public static void SetRectangle(this UIElement uiElement, Vector2 pos, Vector2 dimensions)
        {
            uiElement.Left.Set(pos.X, 0f);
            uiElement.Top.Set(pos.Y, 0f);
            uiElement.Width.Set(dimensions.X, 0f);
            uiElement.Height.Set(dimensions.Y, 0f);
        }

        public static void SetDimensions(this UIElement uiElement, float width, float height)
        {
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }
        public static void SetDimensions(this UIElement uiElement, Vector2 dimensions)
        {
            uiElement.SetDimensions(dimensions.X, dimensions.Y);
        }

        public static void SetPosition(this UIElement uiElement, float x, float y)
        {
            uiElement.Left.Set(x, 0f);
            uiElement.Top.Set(y, 0f);
        }
        public static void SetPosition(this UIElement uiElement, Vector2 position)
        {
            uiElement.SetPosition(position.X, position.Y);
        }
        public static void CenterOnScreen(this UIElement uiElement)
        {
            uiElement.SetPosition(Main.screenWidth / 2 - uiElement.Width.Pixels / 2, Main.screenHeight / 2 - uiElement.Height.Pixels / 2);
        }
    }
}
