using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.CardinalSelection
{
    internal class MySoul : UIState
    {
        private UITextPanel<string> text;
        public override void OnInitialize()
        {
            text = new("Let's take a look into your soul.");
            text.SetPosition(10, 10);
            text.SetDimensions(300, 300);
            Append(text);
        }

        static readonly string[] soulNames = ["Envy", "Gluttony", "Greed", "Lust", "Pride", "Sloth", "Wrath",
            "Charity", "Chassity", "Diligence", "Humility", "Kindness", "Patience", "Temperance"];

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            text.SetPosition(Main.screenWidth / 2 - text.Width.Pixels / 2, Main.screenHeight / 2 - text.Height.Pixels / 2);
            text.SetDimensions(text.MinWidth.Pixels, text.MinHeight.Pixels);

            var cardinaal = Main.LocalPlayer.CardinalSoul();
            if (cardinaal.SoulActive && !Main.playerInventory)
            {
                int index = cardinaal.cardinalSoul - 1;
                string abilityKey = SoA.SinAbility.GetAssignedKeys().FirstOrDefault();
                string label = Language.GetTextValue("Mods.ShardsOfAtheria.CardinalSouls." + soulNames[index], abilityKey);
                text.SetText(label);
                int height = label.Split("\n").Length;
                text.Height.Set((text.MinHeight.Pixels - height) * height, 0f);
            }
            else SinfulUI.Instance.ToggleSelected();
        }
    }
}
