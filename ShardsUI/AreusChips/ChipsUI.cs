using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI
{
    class ChipsUI : UIState
    {
        UIPanel panel;
        VanillaItemSlotWrapper[] slots;
        bool cardsInit = false;
        int[] slotTypes = new[] { AreusArmorChip.SlotHead, AreusArmorChip.SlotChest, AreusArmorChip.SlotLegs };

        public override void OnInitialize()
        {
            panel = new UIPanel();

            slots = new VanillaItemSlotWrapper[3];

            SetSlot(0);
            SetSlot(1);
            SetSlot(2);

            Append(panel);
        }

        // Slots are set manually so there wont be errors relating to matching slotTypes
        private void SetSlot(int i)
        {
            slots[i] = new VanillaItemSlotWrapper(scale: 0.8f)
            {
                Left = { Pixels = 0 },
                Top = { Pixels = 52f * i },
                ValidItemFunc = item => ValidateItem(item, i)
            };
            panel.Append(slots[i]);
        }

        private bool ValidateItem(Item item, int ind)
        {
            if (!item.IsAir)
            {
                if (item.ModItem is not AreusArmorChip chip)
                {
                    return false;
                }
                else if (chip.Type == ModContent.ItemType<AreusArmorChip>())
                {
                    return false;
                }
                else if (chip.slotType != AreusArmorChip.SlotAny)
                {
                    if (chip.slotType != slotTypes[ind])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void SetRectangle(UIElement uiElement, float left, float top, float width, float height)
        {
            uiElement.Left.Set(left, 0f);
            uiElement.Top.Set(top, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var areus = Main.LocalPlayer.Areus();
            if (!Main.playerInventory || !areus.areusArmorPiece)
            {
                ModContent.GetInstance<ChipsUISystem>().HideChips();
                return;
            }

            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int x = Main.screenWidth - 400;
            int y = 250;
            panel.SetRectangle(x, y, 74, 168);

            var player = Main.LocalPlayer;
            var armorPlayer = player.Areus();
            var chips = armorPlayer.chipNames;

            for (int i = 0; i < 3; i++)
            {
                var slot = slots[i];
                var modItem = slot.Item.ModItem;
                if (slot.Item.type != ItemID.None && modItem != null)
                {
                    chips[i] = modItem.Name;
                }
                else
                {
                    chips[i] = "";
                }
            }
        }

        public void InitializeChips()
        {
            if (!cardsInit)
            {
                cardsInit = true;
                var player = Main.LocalPlayer;
                var armorPlayer = player.Areus();
                var chips = armorPlayer.chipNames;

                for (int i = 0; i < 3; i++)
                {
                    var slot = slots[i];
                    var pendingChip = chips[i];
                    if (SoA.Instance.TryFind<ModItem>(pendingChip, out var item))
                    {
                        slot.Item = new(item.Type);
                    }
                }
            }
        }
    }

    public class ChipsUISystem : ModSystem
    {
        internal ChipsUI ChipsIU;
        private UserInterface chipsInterface;

        public override void Load()
        {
            ChipsIU = new ChipsUI();
            ChipsIU.Activate();
            chipsInterface = new UserInterface();
            chipsInterface.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            chipsInterface?.Update(gameTime);
        }

        public void ToggleChips()
        {
            if (chipsInterface.CurrentState == null)
            {
                ShowChips();
            }
            else
            {
                HideChips();
            }
        }
        public void ShowChips()
        {
            chipsInterface.SetState(ChipsIU);
            ChipsIU.InitializeChips();
        }
        public void HideChips()
        {
            chipsInterface.SetState(null);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Holds inserted Areus Armor Chips",
                    delegate
                    {
                        chipsInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
