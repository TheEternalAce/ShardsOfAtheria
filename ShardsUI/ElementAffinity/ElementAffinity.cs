using BattleNetworkElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.ElementAffinity
{
    internal class ElementAffinity : UIState
    {
        private bool display = false;

        private UIImage frame;
        private UIImage currentAffinity;
        private UIImage nextAffinity;
        private string bnePath = "BattleNetworkElements/ElementUI/Icons/";

        public override void OnInitialize()
        {
            frame = new(ModContent.Request<Texture2D>("ShardsofAtheria/ShardsUI/ElementAffinity/AffinityFrame"));
            frame.SetRectangle(0, 0, 78, 42);

            currentAffinity = new(ModContent.Request<Texture2D>(bnePath + "FireIcon"));
            currentAffinity.SetRectangle(6, 6, 30, 30);
            frame.Append(currentAffinity);

            nextAffinity = new(ModContent.Request<Texture2D>(bnePath + "AquaIcon"));
            nextAffinity.SetRectangle(42, 6, 30, 30);
            frame.Append(nextAffinity);

            Append(frame);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ShardsPlayer shardsPlayer = Main.LocalPlayer.Shards();

            display = false;
            if (shardsPlayer.areusProcessor)
            {
                display = true;
            }
            if (display)
            {
                base.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!display)
            {
                return;
            }
            var player = Main.LocalPlayer;
            var shards = player.Shards();

            string currentElement = "";
            string nextElement = "";
            switch (shards.processorElement)
            {
                case Element.Fire:
                    currentElement = "FireIcon";
                    nextElement = "AquaIcon";
                    break;
                case Element.Aqua:
                    currentElement = "AquaIcon";
                    nextElement = "ElecIcon";
                    break;
                case Element.Elec:
                    currentElement = "ElecIcon";
                    nextElement = "WoodIcon";
                    break;
                case Element.Wood:
                    currentElement = "WoodIcon";
                    nextElement = "FireIcon";
                    break;
            }
            currentAffinity.SetImage(ModContent.Request<Texture2D>(bnePath + currentElement));
            nextAffinity.SetImage(ModContent.Request<Texture2D>(bnePath + nextElement));

            var position = new Vector2(Main.screenWidth, Main.screenHeight);
            position.X -= frame.Width.Pixels + 300;
            position.Y -= frame.Height.Pixels + 40;
            frame.SetRectangle(position.X, position.Y, 78, 42);
        }
    }

    class AffinityUI : ModSystem
    {
        internal ElementAffinity affinity;
        private UserInterface affinityUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                affinity = new();
                affinityUI = new();
                affinityUI.SetState(affinity);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            affinityUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Affinity UI",
                    delegate
                    {
                        affinityUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
