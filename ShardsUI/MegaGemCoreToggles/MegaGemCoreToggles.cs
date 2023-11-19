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
        private UIHoverImageButton emeraldToggle;
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

            MakeToggle(ref amethystToggle, "Amethyst", 0, new(10, 40));
            amethystToggle.OnLeftClick += new(ToggleAmethyst);

            MakeToggle(ref diamondToggle, "Diamond", 1, new(60, 40));
            diamondToggle.OnLeftClick += new(ToggleDiamond);

            MakeToggle(ref emeraldToggle, "Emerald", 2, new(110, 40));
            emeraldToggle.OnLeftClick += new(ToggleEmerald);

            MakeToggle(ref rubyToggle, "Ruby", 3, new(10, 90));
            rubyToggle.OnLeftClick += new(ToggleRuby);

            MakeToggle(ref sapphireToggle, "Sapphire", 4, new(60, 90));
            sapphireToggle.OnLeftClick += new(ToggleSapphire);

            MakeToggle(ref topazToggle, "Topaz", 5, new(110, 90));
            topazToggle.OnLeftClick += new(ToggleTopaz);

            MakeGravityToggle(ref gravToggle, new(10, 140));
            gravToggle.OnLeftClick += new(ToggleGrav);

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

        private void ToggleEmerald(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(emeraldToggle, 2);
        }

        private void ToggleRuby(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(rubyToggle, 3);
        }

        private void ToggleSapphire(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(sapphireToggle, 4);
        }

        private void ToggleTopaz(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle(topazToggle, 5);
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
            Toggle(gravToggle, 6);
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
            ModContent.GetInstance<MGCToggleUI>().ToggleVisualSettings();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var shards = Main.LocalPlayer.Shards();
            var toggles = shards.megaGemCoreToggles;
            if (!toggles[0])
            {
                amethystToggle.SetImage(toggleBack);
            }
            if (!toggles[1])
            {
                diamondToggle.SetImage(toggleBack);
            }
            if (!toggles[2])
            {
                emeraldToggle.SetImage(toggleBack);
            }
            if (!toggles[3])
            {
                rubyToggle.SetImage(toggleBack);
            }
            if (!toggles[4])
            {
                sapphireToggle.SetImage(toggleBack);
            }
            if (!toggles[5])
            {
                topazToggle.SetImage(toggleBack);
            }
            if (!toggles[6])
            {
                gravToggle.SetImage(toggleBack);
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
