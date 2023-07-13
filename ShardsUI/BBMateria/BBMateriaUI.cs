using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.BreakerBladeMateria;
using ShardsOfAtheria.Players;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI.BBMateria
{
    class BBMateriaUI : UIState
    {
        UIPanel panel;
        VanillaItemSlotWrapper[] slots;
        bool slotsInit = false;

        public override void OnInitialize()
        {
            panel = new UIPanel();

            slots = new VanillaItemSlotWrapper[2];

            SetSlot(0);
            SetSlot(1);

            Append(panel);
        }

        // Slots are set manually so there wont be errors relating to matching slotTypes
        private void SetSlot(int i)
        {
            slots[i] = new VanillaItemSlotWrapper(scale: 0.8f)
            {
                Left = { Pixels = 0 },
                Top = { Pixels = 52f * i },
                ValidItemFunc = ValidateItem
            };
            panel.Append(slots[i]);
        }

        private bool ValidateItem(Item item)
        {
            if (!item.IsAir)
            {
                if (item.ModItem is not Materia materia)
                {
                    return false;
                }
                var player = Main.LocalPlayer;
                var bladePlayer = player.GetModPlayer<BreakerBladePlayer>();
                if (bladePlayer.materiaSlots.Contains(materia.Name))
                {
                    return false;
                }
            }
            return true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Main.playerInventory)
            {
                ModContent.GetInstance<BBMateriaUISystem>().HideUI();
                return;
            }
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var player = Main.LocalPlayer;
            var bladePlayer = player.GetModPlayer<BreakerBladePlayer>();
            var materiaSlots = bladePlayer.materiaSlots;

            float x = 600;
            float y = 100;
            panel.SetRectangle(x, y, 66, 116);

            for (int i = 0; i < 2; i++)
            {
                var slot = slots[i];
                var modItem = slot.Item.ModItem;
                if (slot.Item.type != ItemID.None || modItem != null)
                {
                    materiaSlots[i] = modItem.Name;
                }
                else
                {
                    materiaSlots[i] = "";
                }
            }
        }

        public void InitializeMateria()
        {
            if (!slotsInit)
            {
                slotsInit = true;
                var player = Main.LocalPlayer;
                var bladePlayer = player.GetModPlayer<BreakerBladePlayer>();
                var materiaSlots = bladePlayer.materiaSlots;

                for (int i = 0; i < 2; i++)
                {
                    var slot = slots[i];
                    var pendingChip = materiaSlots[i];
                    if (SoA.Instance.TryFind(pendingChip, out ModItem item))
                    {
                        slot.Item = new(item.Type);
                    }
                }
            }
        }
    }

    public class BBMateriaUISystem : ModSystem
    {
        internal BBMateriaUI MateriaIU;
        private UserInterface MateriaInterface;

        public override void Load()
        {
            MateriaIU = new();
            MateriaIU.Activate();
            MateriaInterface = new UserInterface();
            MateriaInterface.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            MateriaInterface?.Update(gameTime);
        }

        public void ToggleUI()
        {
            if (MateriaInterface.CurrentState == null)
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }
        public void ShowUI()
        {
            MateriaInterface.SetState(MateriaIU);
            MateriaIU.InitializeMateria();
        }
        public void HideUI()
        {
            MateriaInterface.SetState(null);
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
                        MateriaInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
