using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
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
            panel.SetRectangle(panelRect);

            UIText text = new("Core Cstm.");
            text.SetRectangle(10, 10, 0, 0);
            panel.Append(text);

            Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete");
            UIHoverImageButton closeButton = new(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52")); // Localized text for "Close"
            closeButton.SetRectangle(160 - 32f, 10, 22f, 22f);
            closeButton.OnLeftClick += new MouseEvent(CloseButtonClicked);
            panel.Append(closeButton);

            MakeToggle(ref amberToggle, "Amber", 0, new(10, 40));
            amberToggle.OnLeftClick += new(ToggleAmber);

            MakeToggle(ref amethystToggle, "Amethyst", 0, new(60, 40));
            amethystToggle.OnLeftClick += new(ToggleAmethyst);

            MakeToggle(ref diamondToggle, "Diamond", 1, new(110, 40));
            diamondToggle.OnLeftClick += new(ToggleDiamond);

            MakeToggle(ref emeraldToggle, "Emerald", 2, new(10, 90));
            emeraldToggle.OnLeftClick += new(ToggleEmerald);

            MakeToggle(ref rubyToggle, "Ruby", 3, new(60, 90));
            rubyToggle.OnLeftClick += new(ToggleRuby);

            MakeToggle(ref sapphireToggle, "Sapphire", 4, new(110, 90));
            sapphireToggle.OnLeftClick += new(ToggleSapphire);

            MakeToggle(ref topazToggle, "Topaz", 5, new(10, 140));
            topazToggle.OnLeftClick += new(ToggleTopaz);

            MakeAllToggle(ref toggleAll, new(60, 140));
            toggleAll.OnLeftClick += new(ToggleAll);

            Append(panel);
        }

        void MakeToggle(ref UIHoverImageButton toggle, string name, int index, Vector2 pos)
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

        private void ToggleAll(UIMouseEvent evt, UIElement listeningElement)
        {
            ToggleAmber(evt, listeningElement);
            ToggleAmethyst(evt, listeningElement);
            ToggleDiamond(evt, listeningElement);
            ToggleEmerald(evt, listeningElement);
            ToggleRuby(evt, listeningElement);
            ToggleSapphire(evt, listeningElement);
            ToggleTopaz(evt, listeningElement);
        }

        private void ToggleAmber(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(0);
        }

        private void ToggleAmethyst(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(1);
        }

        private void ToggleDiamond(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(2);
        }

        private void ToggleEmerald(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(3);
        }

        private void ToggleRuby(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(4);
        }

        private void ToggleSapphire(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(5);
        }

        private void ToggleTopaz(UIMouseEvent evt, UIElement listeningElement)
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

        void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            ModContent.GetInstance<MGCToggleUI>().ToggleVisualSettings();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ModContent.GetInstance<GemCoreConfigOpener>().CurrentState != 0)
            {
                base.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (ModContent.GetInstance<GemCoreConfigOpener>().CurrentState != 0)
            {
                base.Update(gameTime);
                UpdateHighlight(amberToggle, 0);
                UpdateHighlight(amethystToggle, 1);
                UpdateHighlight(diamondToggle, 2);
                UpdateHighlight(emeraldToggle, 3);
                UpdateHighlight(rubyToggle, 4);
                UpdateHighlight(sapphireToggle, 5);
                UpdateHighlight(topazToggle, 6);
            }
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
                mgcVisualsStateUI.SetState(mgcVisualsState);
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
