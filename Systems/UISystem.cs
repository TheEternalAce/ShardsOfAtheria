using Microsoft.Xna.Framework;
using ShardsOfAtheria.UI.LoreTablet;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.Systems
{
    public class UISystem : ModSystem
    {
        internal ScreenState TabletScreenState;
        private UserInterface _tabletScreenState;

        public override void Load()
        {
            TabletScreenState  = new();
            TabletScreenState.Activate();
            _tabletScreenState = new();
            _tabletScreenState.SetState(TabletScreenState);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _tabletScreenState?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: A Description",
                    delegate
                    {
                        _tabletScreenState.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
