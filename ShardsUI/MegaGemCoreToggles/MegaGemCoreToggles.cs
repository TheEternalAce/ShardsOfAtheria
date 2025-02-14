using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.MegaGemCoreToggles
{
    internal class MGCTogglesState : UIState
    {
        private DragablePanel panel;
        private Rectangle panelRect = new(0, 0, 160, 190);
        private UIHoverImageButton amberToggle;
        private UIHoverImageButton amethystToggle;
        private UIHoverImageButton diamondToggle;
        private UIHoverImageButton emeraldToggle;
        private UIHoverImageButton rubyToggle;
        private UIHoverImageButton sapphireToggle;
        private UIHoverImageButton topazToggle;
        private UIHoverImageButton toggleAll;
        private static string path = "ShardsOfAtheria/ShardsUI/MegaGemCoreToggles/ToggleBack";
        private Asset<Texture2D> toggleBack = ModContent.Request<Texture2D>(path);
        private Asset<Texture2D> toggleBack_Highlight = ModContent.Request<Texture2D>(path + "_Highlight");

        public override void OnInitialize()
        {
            panel = new DragablePanel();
            panel.SetPadding(0);
            panel.SetRectangle(111, 266, 160, 190);

            UIText text = new("Core Cstm.");
            text.SetRectangle(10, 10, 0, 0);
            panel.Append(text);

            MakeToggle(ref amberToggle, "Amber", new(10, 40));
            amberToggle.OnLeftClick += (a, b) => ToggleAmber();

            MakeToggle(ref amethystToggle, "Amethyst", new(60, 40));
            amethystToggle.OnLeftClick += (a, b) => ToggleAmethyst();

            MakeToggle(ref diamondToggle, "Diamond", new(110, 40));
            diamondToggle.OnLeftClick += (a, b) => ToggleDiamond();

            MakeToggle(ref emeraldToggle, "Emerald", new(10, 90));
            emeraldToggle.OnLeftClick += (a, b) => ToggleEmerald();

            MakeToggle(ref rubyToggle, "Ruby", new(60, 90));
            rubyToggle.OnLeftClick += (a, b) => ToggleRuby();

            MakeToggle(ref sapphireToggle, "Sapphire", new(110, 90));
            sapphireToggle.OnLeftClick += (a, b) => ToggleSapphire();

            MakeToggle(ref topazToggle, "Topaz", new(10, 140));
            topazToggle.OnLeftClick += (a, b) => ToggleTopaz();

            MakeAllToggle(ref toggleAll, new(60, 140));
            toggleAll.OnLeftClick += (a, b) => ToggleAll();

            Append(panel);
        }

        void MakeToggle(ref UIHoverImageButton toggle, string name, Vector2 pos)
        {
            string pathBase = "ShardsOfAtheria/Items/Accessories/GemCores/Super/";
            toggle = new(toggleBack_Highlight, "Toggle " + name);
            Vector2 dimensions = new(40, 40);
            toggle.SetRectangle(pos, dimensions);
            string path = pathBase + name + "Core_Super";
            UIImage gemCoreImage = new(ModContent.Request<Texture2D>(path));
            gemCoreImage.SetRectangle(5, 5, 30, 30);
            toggle.Append(gemCoreImage);
            panel.Append(toggle);
        }

        void MakeAllToggle(ref UIHoverImageButton toggle, Vector2 pos)
        {
            string path = "ShardsOfAtheria/Items/Accessories/GemCores/MegaGemCore";
            toggle = new(toggleBack_Highlight, "Toggle All");
            Vector2 dimensions = new(40, 40);
            toggle.SetRectangle(pos, dimensions);
            UIImage gemCoreImage = new(ModContent.Request<Texture2D>(path));
            gemCoreImage.SetRectangle(3, 3, 30, 30);
            toggle.Append(gemCoreImage);
            panel.Append(toggle);
        }

        private void ToggleAll()
        {
            ToggleAmber();
            ToggleAmethyst();
            ToggleDiamond();
            ToggleEmerald();
            ToggleRuby();
            ToggleSapphire();
            ToggleTopaz();
        }

        private void ToggleAmber()
        {
            Toggle(0);
        }

        private void ToggleAmethyst()
        {
            Toggle(1);
        }

        private void ToggleDiamond()
        {
            Toggle(2);
        }

        private void ToggleEmerald()
        {
            Toggle(3);
        }

        private void ToggleRuby()
        {
            Toggle(4);
        }

        private void ToggleSapphire()
        {
            Toggle(5);
        }

        private void ToggleTopaz()
        {
            Toggle(6);
        }

        private void Toggle(int index)
        {
            Player player = Main.LocalPlayer;
            int loadout = player.CurrentLoadoutIndex;
            var gem = player.Gem();
            gem.masterGemCoreToggles[loadout, index] = !gem.masterGemCoreToggles[loadout, index];
        }

        private void UpdateHighlight(UIHoverImageButton toggle, int index)
        {
            Player player = Main.LocalPlayer;
            int loadout = player.CurrentLoadoutIndex;
            var gem = player.Gem();

            if (gem.masterGemCoreToggles[loadout, index])
            {
                toggle.SetImage(toggleBack_Highlight);
            }
            else
            {
                toggle.SetImage(toggleBack);
            }
        }

        private void UpdateHighlightAll(UIHoverImageButton toggle)
        {
            Player player = Main.LocalPlayer;
            int loadout = player.CurrentLoadoutIndex;
            var gem = player.Gem();
            bool allOn = true;

            for (int i = 0; i < gem.masterGemCoreToggles.GetLength(loadout); i++)
            {
                if (!gem.masterGemCoreToggles[loadout, i])
                {
                    allOn = false;
                    break;
                }
            }

            if (allOn)
            {
                toggle.SetImage(toggleBack_Highlight);
            }
            else
            {
                toggle.SetImage(toggleBack);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateHighlight(amberToggle, 0);
            UpdateHighlight(amethystToggle, 1);
            UpdateHighlight(diamondToggle, 2);
            UpdateHighlight(emeraldToggle, 3);
            UpdateHighlight(rubyToggle, 4);
            UpdateHighlight(sapphireToggle, 5);
            UpdateHighlight(topazToggle, 6);
            UpdateHighlightAll(toggleAll);
        }
    }

    class MGCToggleUI : ModSystem
    {
        internal MGCTogglesState mgcVisualsState;
        private UserInterface mgcVisualsStateUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                mgcVisualsState = new();
                mgcVisualsState.Activate();
                mgcVisualsStateUI = new();
            }
        }

        public void ToggleVisualSettings()
        {
            if (mgcVisualsStateUI.CurrentState == null)
            {
                mgcVisualsStateUI.SetState(mgcVisualsState);
            }
            else
            {
                mgcVisualsStateUI.SetState(null);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (ModContent.GetInstance<GemCoreConfigOpener>().CurrentState == 1)
                mgcVisualsStateUI.SetState(mgcVisualsState);
            else mgcVisualsStateUI.SetState(null);

            if (mgcVisualsStateUI != null)
                mgcVisualsStateUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Mega Gem Core Visuals",
                    delegate
                    {
                        mgcVisualsStateUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
