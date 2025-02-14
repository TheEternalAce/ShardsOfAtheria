using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Projectiles.Magic.MagicalGems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.MagicStonesSelection
{
    internal class StonesSelectionState : UIState
    {
        private DragablePanel panel;
        private Rectangle panelRect = new(0, 0, 350, 90);
        private UIHoverImageButton amberToggle;
        private UIHoverImageButton amethystToggle;
        private UIHoverImageButton diamondToggle;
        private UIHoverImageButton emeraldToggle;
        private UIHoverImageButton rubyToggle;
        private UIHoverImageButton sapphireToggle;
        private UIHoverImageButton topazToggle;
        private static readonly string path = "ShardsOfAtheria/ShardsUI/MegaGemCoreToggles/ToggleBack";
        private readonly Asset<Texture2D> toggleBack = ModContent.Request<Texture2D>(path);
        private readonly Asset<Texture2D> toggleBack_Highlight = ModContent.Request<Texture2D>(path + "_Highlight");
        private MagicStones targetItem;

        public override void OnInitialize()
        {
            panel = new DragablePanel();
            panel.SetPadding(0);
            panel.SetRectangle(111, 266, panelRect.Width, panelRect.Height);

            UIText text = new("Magic Stones.");
            text.SetRectangle(10, 10, 0, 0);
            panel.Append(text);

            MakeToggle(ref amberToggle, ItemID.Amber, "Amber\n" +
                "Summons an amber fly on hit.", 0);
            amberToggle.OnLeftClick += (a, b) => ToggleAmber();

            MakeToggle(ref amethystToggle, ItemID.Amethyst, "Amethyst\n" +
                "Bounces off walls", 1);
            amethystToggle.OnLeftClick += (a, b) => ToggleAmethyst();

            MakeToggle(ref diamondToggle, ItemID.Diamond, "Diamond\n" +
                "Pierces once.", 2);
            diamondToggle.OnLeftClick += (a, b) => ToggleDiamond();

            MakeToggle(ref emeraldToggle, ItemID.Emerald, "Emerald\n" +
                "Travels faster.", 3);
            emeraldToggle.OnLeftClick += (a, b) => ToggleEmerald();

            MakeToggle(ref rubyToggle, ItemID.Ruby, "Ruby\n" +
                "Deals more damage.", 4);
            rubyToggle.OnLeftClick += (a, b) => ToggleRuby();

            MakeToggle(ref sapphireToggle, ItemID.Sapphire, "Sapphire\n" +
                "Seaks nearby enemies.", 5);
            sapphireToggle.OnLeftClick += (a, b) => ToggleSapphire();

            MakeToggle(ref topazToggle, ItemID.Topaz, "Topaz\n" +
                "Slightly reduces debuff duration", 6);
            topazToggle.OnLeftClick += (a, b) => ToggleTopaz();

            Append(panel);
        }

        public void SetTargetITem(Item item)
        {
            targetItem = item.ModItem as MagicStones;
        }

        void MakeToggle(ref UIHoverImageButton toggle, int item, string name, int index)
        {
            string path = "Terraria/Images/Item_" + item;
            Vector2 dimensions = new(40, 40);
            var texture = ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad);
            UIImage gem = new(texture);
            float percent = 1f / 7f * index;

            toggle = new(toggleBack_Highlight, name);
            toggle.SetDimensions(dimensions);
            toggle.Left.Set(5, percent);
            toggle.Top.Set(40, 0);

            gem.SetDimensions(texture.Width(), texture.Height());
            gem.Left.Set(-texture.Width() / 2, 0.5f);
            gem.Top.Set(-texture.Height() / 2, 0.5f);

            toggle.Append(gem);
            panel.Append(toggle);
        }

        private void ToggleAmber()
        {
            Toggle(ModContent.ProjectileType<AmberStone>());
        }

        private void ToggleAmethyst()
        {
            Toggle(ModContent.ProjectileType<AmethystStone>());
        }

        private void ToggleDiamond()
        {
            Toggle(ModContent.ProjectileType<DiamondStone>());
        }

        private void ToggleEmerald()
        {
            Toggle(ModContent.ProjectileType<EmeraldStone>());
        }

        private void ToggleRuby()
        {
            Toggle(ModContent.ProjectileType<RubyStone>());
        }

        private void ToggleSapphire()
        {
            Toggle(ModContent.ProjectileType<SapphireStone>());
        }

        private void ToggleTopaz()
        {
            Toggle(ModContent.ProjectileType<TopazStone>());
        }

        private void Toggle(int gemType)
        {
            int[] gems = targetItem.selectedGems;
            if (gems.Contains(gemType))
            {
                for (int i = 0; i < gems.Length; i++)
                {
                    if (gems[i] == gemType)
                    {
                        gems[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < gems.Length; i++)
                {
                    if (gems[i] == 0)
                    {
                        gems[i] = gemType;
                        break;
                    }
                }
            }
        }

        private void UpdateHighlight(UIHoverImageButton toggle, int gemType)
        {
            int[] gems = targetItem.selectedGems;
            if (gems.Contains(gemType))
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
            UpdateHighlight(amberToggle, ModContent.ProjectileType<AmberStone>());
            UpdateHighlight(amethystToggle, ModContent.ProjectileType<AmethystStone>());
            UpdateHighlight(diamondToggle, ModContent.ProjectileType<DiamondStone>());
            UpdateHighlight(emeraldToggle, ModContent.ProjectileType<EmeraldStone>());
            UpdateHighlight(rubyToggle, ModContent.ProjectileType<RubyStone>());
            UpdateHighlight(sapphireToggle, ModContent.ProjectileType<SapphireStone>());
            UpdateHighlight(topazToggle, ModContent.ProjectileType<TopazStone>());
        }
    }

    class StonesSelectionUI : ModSystem
    {
        internal StonesSelectionState stonesState;
        private UserInterface stonesSelectionStateUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                stonesState = new();
                stonesState.Activate();
                stonesSelectionStateUI = new();
            }
        }

        public void ToggleVisualSettings(Item item)
        {
            stonesState.SetTargetITem(item);
            if (stonesSelectionStateUI.CurrentState == null)
            {
                stonesSelectionStateUI.SetState(stonesState);
            }
            else
            {
                stonesSelectionStateUI.SetState(null);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (stonesSelectionStateUI != null)
                stonesSelectionStateUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Magic Stones Selection",
                    delegate
                    {
                        stonesSelectionStateUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
