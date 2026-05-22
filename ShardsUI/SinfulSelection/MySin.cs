using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.SinfulSelection
{
    internal class MySin : UIState
    {
        private UITextPanel<string> text;
        public override void OnInitialize()
        {
            text = new("What is your Sin?");
            text.SetPosition(10, 10);
            text.SetDimensions(300, 300);
            Append(text);
        }


        static readonly string[] sinNames = ["Envy", "Gluttony", "Greed", "Lust", "Pride", "Sloth", "Wrath"];
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            text.SetPosition(Main.screenWidth / 2 - text.Width.Pixels / 2, Main.screenHeight / 2 - text.Height.Pixels / 2);
            text.SetDimensions(text.MinWidth.Pixels, text.MinHeight.Pixels);

            var sinner = Main.LocalPlayer.Sinner();
            if (sinner.sinID > 0)
            {
                string label = Language.GetTextValue("Mods.ShardsOfAtheria.SinfulSouls." + sinNames[sinner.sinID - 1]);
                text.SetText(label);
                int height = label.Split("\n").Length;
                text.Height.Set((text.MinHeight.Pixels - height) * height, 0f);
            }
            else SinfulUI.Instance.ToggleSelected();
        }
    }
}
