using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.CardinalSelection
{
    internal class HowSinful : UIState
    {
        private DragablePanel panel;
        private UIHoverImageButtonLocalized envySelect;
        private UIHoverImageButtonLocalized gluttonySelect;
        private UIHoverImageButtonLocalized greedSelect;
        private UIHoverImageButtonLocalized lustSelect;
        private UIHoverImageButtonLocalized prideSelect;
        private UIHoverImageButtonLocalized slothSelect;
        private UIHoverImageButtonLocalized wrathSelect;
        public override void OnInitialize()
        {
            panel = new DragablePanel();
            panel.SetPadding(0);
            panel.SetDimensions(300, 300);

            UIText text = new("What is your Sin?");
            text.SetRectangle(10, 10, 0, 0);
            panel.Append(text);

            MakeSelect(ref envySelect, "Envy");
            envySelect.OnLeftClick += (a, b) => SelectSoul(CardinalSoulID.Envy);

            MakeSelect(ref gluttonySelect, "Gluttony");
            gluttonySelect.OnLeftClick += (a, b) => SelectSoul(CardinalSoulID.Gluttony);

            MakeSelect(ref greedSelect, "Greed");
            greedSelect.OnLeftClick += (a, b) => SelectSoul(CardinalSoulID.Greed);

            MakeSelect(ref lustSelect, "Lust");
            lustSelect.OnLeftClick += (a, b) => SelectSoul(CardinalSoulID.Lust);

            MakeSelect(ref prideSelect, "Pride");
            prideSelect.OnLeftClick += (a, b) => SelectSoul(CardinalSoulID.Pride);

            MakeSelect(ref slothSelect, "Sloth");
            slothSelect.OnLeftClick += (a, b) => SelectSoul(CardinalSoulID.Sloth);

            MakeSelect(ref wrathSelect, "Wrath");
            wrathSelect.OnLeftClick += (a, b) => SelectSoul(CardinalSoulID.Wrath);

            UIGrid grid = [envySelect, gluttonySelect, greedSelect, lustSelect, prideSelect, slothSelect, wrathSelect];
            grid.SetPosition(10, 30);
            grid.Width.Set(0f, 1f);
            grid.Height.Set(0f, 1f);
            panel.Append(grid);

            Append(panel);
        }

        void MakeSelect(ref UIHoverImageButtonLocalized selection, string name)
        {
            string filePathBase = "ShardsOfAtheria/ShardsUI/CardinalSelection/";
            string filePath = filePathBase + name;
            var text = Language.GetText("Mods.ShardsOfAtheria.CardinalSouls." + name).WithFormatArgs("Sin Ability Key");
            selection = new(ModContent.Request<Texture2D>(filePath), text);
            Vector2 dimensions = new(80, 80);
            selection.SetDimensions(dimensions);
        }

        private void SelectSoul(int soul)
        {
            Main.LocalPlayer.CardinalSoul().cardinalSoul = soul;
            SinfulUI.Instance.ToggleSelections();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            panel.SetPosition(Main.screenWidth / 2 - panel.Width.Pixels / 2, Main.screenHeight / 2 - panel.Height.Pixels / 2);
            panel.SetDimensions(265, 250);
        }
    }

    class SinfulUI : ModSystem
    {
        public static SinfulUI Instance;
        internal HowSinful howSinfulState;
        internal MySoul mySinState;
        private UserInterface UI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                howSinfulState = new();
                howSinfulState.Activate();
                mySinState = new();
                mySinState.Activate();
                UI = new();
                Instance = this;
            }
        }

        public void ToggleSelections()
        {
            if (UI.CurrentState == null) UI.SetState(howSinfulState);
            else UI.SetState(null);
        }

        public void ToggleSelected()
        {
            if (UI.CurrentState == null) UI.SetState(mySinState);
            else UI.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (UI != null)
                UI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: What is your Sin?",
                    delegate
                    {
                        UI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
