using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.Monologue
{
    class MonologueUI : UIState
    {
        const int HEIGHT = 40;
        UIPanel panel;
        UIText uiText;
        public string dialogue;
        public int timer { get; private set; }

        public override void OnInitialize()
        {
            base.OnInitialize();
            panel = new UIPanel();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (uiText != null || panel.HasChild(uiText))
            {
                panel.RemoveAllChildren();
                uiText = null;
                if (timer == 0)
                {
                    timer = dialogue.Length * SoA.ClientConfig.dialogueDuration;
                }
            }

            int width = dialogue.Length * 9 + 20;
            int x = Main.screenWidth / 2 - width / 2;
            int y = Main.screenHeight - 400;

            uiText = new(dialogue);
            uiText.SetRectangle(0, 0, width, HEIGHT);
            panel.SetRectangle(x, y, width, HEIGHT);
            panel.Append(uiText);
            Append(panel);
            if (!Main.gameInactive)
            {
                timer--;
            }
            if (timer <= 0)
            {
                timer = 0;
                MonologueUISystem.Instance.HideMonologue();
            }
        }
    }

    public class MonologueUISystem : ModSystem
    {
        public static MonologueUISystem Instance { get; private set; }
        internal MonologueUI MonologueUI;
        private UserInterface monologueInterface;

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            MonologueUI = new();
            MonologueUI.Activate();
            monologueInterface = new UserInterface();
            monologueInterface.SetState(null);
            Instance = this;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            monologueInterface?.Update(gameTime);
        }

        public void ShowMonologue(string text)
        {
            if (SoA.ClientConfig.dialogue)
            {
                MonologueUI.dialogue = text;
                monologueInterface.SetState(MonologueUI);
            }
        }
        public void HideMonologue()
        {
            monologueInterface.SetState(null);
            MonologueUI.RemoveAllChildren();
        }

        public int GetTimer()
        {
            if (monologueInterface.CurrentState == MonologueUI)
            {
                return MonologueUI.timer;
            }
            return -1;
        }
        public bool ActiveBox()
        {
            return GetTimer() > 0;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Displays monologue for certain NPCs",
                    delegate
                    {
                        monologueInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
