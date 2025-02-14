using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.Tools.Misc.Slayer;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.EntropicSelection
{
    internal class NecronomiconSelection : UIState
    {
        private DragablePanel panel;
        private Vector2 panelDimensions = new(280, 100);
        private UIHoverImageButton swordSelect;
        private UIHoverImageButton bowSelect;
        private UIHoverImageButton orbSelect;
        private UIHoverImageButton artifactSelect;
        private UIHoverImageButton daggerSelect;
        private static string path = "ShardsOfAtheria/ShardsUI/EntropicSelection/SelectBack";
        private Asset<Texture2D> toggleBack = ModContent.Request<Texture2D>(path);

        public override void OnInitialize()
        {
            panel = new DragablePanel();
            panel.SetPadding(0);
            panel.SetRectangle(Main.ScreenSize.X / 2, Main.ScreenSize.Y / 2, panelDimensions.X, panelDimensions.Y);

            UIText text = new("Chose a weapon.");
            text.SetRectangle(10, 10, 0, 0);
            panel.Append(text);

            MakeSelectButton(ref swordSelect, "Entropic Saber", "Weapons/Melee/EntropicSaber", 0.2f);
            swordSelect.OnLeftClick += (a, b) => CreateItem<EntropicSaber>();

            MakeSelectButton(ref bowSelect, "Entropic Hunter", "Weapons/Ranged/EntropicHunter", 0.4f);
            bowSelect.OnLeftClick += (a, b) => CreateItem<EntropicHunter>();

            MakeSelectButton(ref orbSelect, "Entropic Catalyst", "Weapons/Magic/EntropicCatalyst", 0.6f);
            orbSelect.OnLeftClick += (a, b) => CreateItem<EntropicCatalyst>();

            MakeSelectButton(ref artifactSelect, "Entropic Artifact", "Weapons/Summon/EntropicArtifact", 0.8f);
            artifactSelect.OnLeftClick += (a, b) => CreateItem<EntropicArtifact>();

            MakeSelectButton(ref daggerSelect, "Entropic Dagger", "Tools/Misc/Slayer/SoulExtractingDagger", 1f);
            daggerSelect.OnLeftClick += (a, b) => CreateItem<SoulExtractingDagger>();

            Append(panel);
        }

        void MakeSelectButton(ref UIHoverImageButton selection, string name, string pathSuffix, float percent)
        {
            string path = "ShardsOfAtheria/Items/" + pathSuffix;
            selection = new(toggleBack, "Create " + name);
            Vector2 dimensions = new(40, 40);
            selection.SetRectangle(Vector2.Zero, dimensions);
            selection.Left.Set(-45f, percent);
            selection.Top.Set(40f, 0f);
            var itemTexture = ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad);
            UIImage itemImage = new(itemTexture);
            float scale;
            if (itemTexture.Width() >= itemTexture.Height()) scale = 30 / (float)itemTexture.Width();
            else scale = 30 / (float)itemTexture.Height();
            float width = itemTexture.Width() * scale;
            float height = itemTexture.Height() * scale;
            itemImage.SetDimensions(width, height);
            itemImage.Left.Set(-width / 2f, 0.5f);
            itemImage.Top.Set(-height / 2f, 0.5f);
            itemImage.ScaleToFit = true;
            selection.Append(itemImage);
            panel.Append(selection);
        }

        private void CreateItem<T>() where T : ModItem
        {
            Player player = Main.LocalPlayer;

            ShardsHelpers.DustRing(player.Center, 10f, DustID.ShadowbeamStaff);

            var newItem = Item.NewItem(player.GetSource_FromThis(), player.Hitbox, ModContent.ItemType<T>());
            // Here we need to make sure the item is synced in multiplayer games.
            if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
            }

            SlayerBookSelectionUI.Instance.Disable();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 position = Main.ScreenSize.ToVector2() / 2;
            position.X -= panelDimensions.X / 2f;
            position.Y -= panelDimensions.Y / 2f;
            panel.SetRectangle(position, panelDimensions);
        }
    }

    public class SlayerBookSelectionUI : ModSystem
    {
        public static SlayerBookSelectionUI Instance => ModContent.GetInstance<SlayerBookSelectionUI>();
        internal NecronomiconSelection selectionsState;
        private UserInterface selectionsUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                selectionsState = new();
                selectionsState.Activate();
                selectionsUI = new();
            }
        }

        public void Enable()
        {
            selectionsUI.SetState(selectionsState);
        }

        public void Disable()
        {
            selectionsUI.SetState(null);
        }

        public bool UIActive => selectionsUI.CurrentState != null;

        public override void UpdateUI(GameTime gameTime)
        {
            if (selectionsUI != null)
                selectionsUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Necronomicon Weapon Creation",
                    delegate
                    {
                        selectionsUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
