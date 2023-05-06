using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.MegaGemCoreToggles
{
    internal class MGCTogglesState : UIState
    {
        private DragablePanel panel;
        private Rectangle panelRect = new(0, 0, 160, 190);
        private UIHoverImageButton amethystToggle;
        private UIHoverImageButton diamondToggle;
        private UIHoverImageButton rubyToggle;
        private UIHoverImageButton sapphireToggle;
        private UIHoverImageButton topazToggle;
        private UIHoverImageButton gravToggle;
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
            UIHoverImageButton closeButton = new UIHoverImageButton(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52")); // Localized text for "Close"
            closeButton.SetRectangle(160 - 32f, 10, 22f, 22f);
            closeButton.OnLeftClick += new MouseEvent(CloseButtonClicked);
            panel.Append(closeButton);

            MakeToggle(ref amethystToggle, "Amethyst", new(10, 40));
            amethystToggle.OnLeftClick += new(ToggleAmethyst);

            MakeToggle(ref diamondToggle, "Diamond", new(60, 40));
            diamondToggle.OnLeftClick += new(ToggleDiamond);

            MakeToggle(ref rubyToggle, "Ruby", new(110, 40));
            rubyToggle.OnLeftClick += new(ToggleRuby);

            MakeToggle(ref sapphireToggle, "Sapphire", new(10, 90));
            sapphireToggle.OnLeftClick += new(ToggleSapphire);

            MakeToggle(ref topazToggle, "Topaz", new(60, 90));
            topazToggle.OnLeftClick += new(ToggleTopaz);

            MakeGravityToggle(ref gravToggle, new(110, 90));
            gravToggle.OnLeftClick += new(ToggleGrav);

            MakeAllToggle(ref toggleAll, new(10, 140));
            toggleAll.OnLeftClick += new(ToggleAll);

            Append(panel);
        }

        void MakeToggle(ref UIHoverImageButton toggle, string name, Vector2 pos, bool mega = false)
        {
            string pathBase = "ShardsOfAtheria/Items/Accessories/GemCores/";
            toggle = new(toggleBack_Highlight, "Toggle " + name);
            Vector2 dimensions = new(40, 40);
            toggle.SetRectangle(pos, dimensions);
            string path = pathBase + name + "Core";
            if (!mega)
            {
                path += "_Super";
            }
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

        void MakeGravityToggle(ref UIHoverImageButton toggle, Vector2 pos)
        {
            string path = "Terraria/Images/Item_" + ItemID.GravitationPotion;
            toggle = new(toggleBack_Highlight, "Toggle Gravitation");
            Vector2 dimensions = new(40, 40);
            toggle.SetRectangle(pos, dimensions);
            UIImage potionImage = new(ModContent.Request<Texture2D>(path));
            potionImage.SetRectangle(10, 5, 20, 30);
            toggle.Append(potionImage);
            panel.Append(toggle);
        }

        private void ToggleAmethyst(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(amethystToggle, 0);
        }

        private void ToggleDiamond(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(diamondToggle, 1);
        }

        private void ToggleRuby(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(rubyToggle, 2);
        }

        private void ToggleSapphire(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(sapphireToggle, 3);
        }

        private void ToggleTopaz(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(topazToggle, 4);
        }

        private void ToggleAll(UIMouseEvent evt, UIElement listeningElement)
        {
            ToggleAmethyst(evt, listeningElement);
            ToggleDiamond(evt, listeningElement);
            ToggleRuby(evt, listeningElement);
            ToggleSapphire(evt, listeningElement);
            ToggleTopaz(evt, listeningElement);
            ToggleGrav(evt, listeningElement);
        }

        private void ToggleGrav(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(gravToggle, 5);
        }

        private void Toggle(UIHoverImageButton toggle, int index)
        {
            Player player = Main.LocalPlayer;
            ShardsPlayer shards = player.Shards();

            if (shards.megaGemCoreToggles[index])
            {
                toggle.SetImage(toggleBack);
                shards.megaGemCoreToggles[index] = false;
            }
            else
            {
                toggle.SetImage(toggleBack_Highlight);
                shards.megaGemCoreToggles[index] = true;
            }
        }

        void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            ModContent.GetInstance<MGCToggleUI>().ToggleToggles();
        }
    }

    class MGCToggleUI : ModSystem
    {
        internal MGCTogglesState togglesState;
        private UserInterface togglesStateUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                togglesState = new();
                togglesState.Activate();
                togglesStateUI = new();
            }
        }

        public void ToggleToggles()
        {
            if (togglesStateUI.CurrentState == null)
            {
                togglesStateUI.SetState(togglesState);
            }
            else
            {
                togglesStateUI.SetState(null);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (togglesStateUI != null)
                togglesStateUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Mega Gem Core Toggles",
                    delegate
                    {
                        togglesStateUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
