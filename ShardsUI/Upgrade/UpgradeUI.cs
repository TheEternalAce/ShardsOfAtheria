using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.NPCs.Town.TheAtherian;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI
{
    class UpgradeUI : UIState
    {
        UIPanel backPanel;
        UIPanel upgradePanel;
        VanillaItemSlotWrapper mainSlot;
        VanillaItemSlotWrapper[] materialSlots;
        UIText infoText;
        UIText timerText;
        UIImage slotLock;
        bool slotsCreated = false;
        readonly float halfSlotWidth = 21;
        static int AtherianType => ModContent.NPCType<Atherian>();
        static Atherian FirstActiveAtherian => NPC.AnyNPCs(AtherianType) ? Main.npc[NPC.FindFirstNPC(AtherianType)].ModNPC as Atherian : null;

        public override void OnInitialize()
        {
            backPanel = new();
            backPanel.SetDimensions(878, 524);

            upgradePanel = new();
            upgradePanel.SetDimensions(500, 500);
            backPanel.Append(upgradePanel);

            mainSlot = new VanillaItemSlotWrapper(scale: 0.8f)
            {
                Left = { Pixels = -halfSlotWidth, Percent = 0.5f },
                Top = { Pixels = -halfSlotWidth, Percent = 0.5f },
                ValidItemFunc = ValidateItem
            };
            upgradePanel.Append(mainSlot);

            UIHoverImageButton upgradeButton = new(ModContent.Request<Texture2D>("Terraria/Images/UI/Reforge_0"), "Upgrade");
            upgradeButton.Left.Set(-30, 1f);
            upgradeButton.Top.Set(-30, 1f);
            upgradeButton.SetDimensions(30f, 30f);
            upgradeButton.OnLeftClick += (a, b) => UpgradeItem();
            upgradePanel.Append(upgradeButton);

            UIPanel infoPanel = new();
            infoPanel.Left.Set(-342, 1f);
            infoPanel.SetDimensions(342, 500);
            backPanel.Append(infoPanel);

            infoText = new("Insert an upgradable item into the slot");
            infoText.SetDimensions(308, 386 + (ModContent.GetInstance<ShardsClient>().upgradeWarning ? 0f : 90f));
            infoPanel.Append(infoText);

            timerText = new("");
            timerText.Top.Set(-26f, 1f);
            timerText.SetDimensions(40, 26);
            upgradePanel.Append(timerText);

            if (ModContent.GetInstance<ShardsClient>().upgradeWarning)
            {
                UIText warningText = new("Note: Upgraded items may [c/FF0000:disapear]\n" +
                    "when you close the game, make sure the\n" +
                    "upgrade is complete and received before\n" +
                    "saving and exiting.\n" +
                    "This message can be disabled in configs.");
                warningText.SetDimensions(308, 130);
                warningText.Top.Set(-130, 1f);
                infoPanel.Append(warningText);
            }

            slotLock = new(ModContent.Request<Texture2D>(SoA.AreusLock))
            {
                Left = { Pixels = -16, Percent = 0.5f },
                Top = { Pixels = -16, Percent = 0.5f }
            };
            slotLock.SetDimensions(30, 30);
            Append(backPanel);
        }

        bool ValidateItem(Item item)
        {
            if (!item.IsAir && !item.IsUpgradable()) return false;
            return UpgradeSystem.UpgradeReady(Main.LocalPlayer.name);
        }

        public void InitializeSlotItem()
        {
            if (mainSlot.Item.IsAir && UpgradeSystem.UpgradeInProgress(Main.LocalPlayer.name, out int itemType))
            {
                mainSlot.Item = new(itemType);
            }
        }

        void CreateSlots(int amount)
        {
            if (slotsCreated)
            {
                return;
            }
            materialSlots = new VanillaItemSlotWrapper[amount];
            float rotation = MathHelper.TwoPi / amount;
            Vector2 center = new(238);
            for (int i = 0; i < amount; i++)
            {
                var vector = Vector2.UnitX.RotatedBy(rotation * i) * 150;
                materialSlots[i] = new VanillaItemSlotWrapper(scale: 0.8f)
                {
                    Left = { Pixels = center.X + vector.X - halfSlotWidth },
                    Top = { Pixels = center.X + vector.Y - halfSlotWidth },
                    ValidItemFunc = item => true
                };
                upgradePanel.Append(materialSlots[i]);
            }
            slotsCreated = true;
        }

        void ClearSlots()
        {
            if (materialSlots != null)
            {
                var player = Main.LocalPlayer;
                foreach (var slot in materialSlots)
                {
                    int newItem = Item.NewItem(slot.Item.GetSource_DropAsItem(), player.getRect(), slot.Item);
                    Main.item[newItem].noGrabDelay = 0; // Set the new item to be able to be picked up instantly

                    // Here we need to make sure the item is synced in multiplayer games.
                    if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
                    }
                    upgradePanel.RemoveChild(slot);
                }
                materialSlots = null;
            }
            slotsCreated = false;
        }

        private void ListMaterials()
        {
            Item upgradeItem = mainSlot.Item;
            var blueprint = UpgradeBlueprintLoader.Find(upgradeItem, Main.LocalPlayer);
            if (upgradeItem.type == ItemID.None || blueprint == null)
            {
                infoText.SetText("Insert an upgradable item into the slot.");
                return;
            }
            bool blueprintItemPrereqisiteMet = blueprint.CheckItem == null || blueprint.CheckItem(mainSlot.Item);
            bool blueprintPlayerPrereqisiteMet = blueprint.CheckPlayer == null || blueprint.CheckPlayer(Main.LocalPlayer);
            string text = "";
            if (!blueprint.RequisitesMet(upgradeItem, Main.LocalPlayer))
            {
                if (!blueprintItemPrereqisiteMet) text += blueprint.GetItemFailMessage();
                if (!blueprintItemPrereqisiteMet && !blueprintPlayerPrereqisiteMet) text += "\n";
                if (!blueprintPlayerPrereqisiteMet) text += blueprint.GetPlayerFailMessage();
                infoText.SetText(text);
                return;
            }
            for (int i = 0; i < blueprint.Materials.GetLength(0); i++)
            {
                int materialType = blueprint.Materials[i, 0];
                int materialCount = blueprint.Materials[i, 1];
                Item MaterialItem = new(materialType);
                string colorHex = AnySlotContains(materialType, materialCount) ? "00FF00" : "FFFFFF";
                text += $"[i:{materialType}] [c/{colorHex}:{MaterialItem.Name} x{materialCount}],\n";
            }
            text += $"Upgrade will take [c/FFFF00:{blueprint.TimeToUpgrade / 60} seconds.]\n";
            if (blueprint.ReplaceItem)
            {
                Item resultItem = blueprint.ResultItem;
                text += "Creates " + "[i:" + resultItem.type + "] " + resultItem.Name + ".\n";
            }
            else text += "Upgrading will make the weapon stronger.";
            //if (Main.LocalPlayer.IsSlayer())
            //{
            //    Item resultItem = blueprint.ResultItem;
            //    int cost = resultItem.value * 4;
            //    string colorHex = Main.LocalPlayer.CanAfford(cost) ? "00FF00" : "FF0000";
            //    int[] costInCoins = Utils.CoinsSplit(cost);
            //    text += $"\n[c/{colorHex}:Cost:] [i:PlatinumCoin][c/{colorHex}:{costInCoins[3]}][i:GoldCoin][c/{colorHex}:{costInCoins[2]}][i:SilverCoin][c/{colorHex}:{costInCoins[1]}][i:CopperCoin][c/{colorHex}:{costInCoins[0]}]";
            //}
            infoText.SetText(text);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            backPanel.CenterOnScreen();

            var player = Main.LocalPlayer;
            if (!player.isNearNPC(ModContent.NPCType<Atherian>(), 90))
            {
                ModContent.GetInstance<UpgradeUISystem>().HideUI();
            }

            if (backPanel.ContainsPoint(Main.MouseScreen))
            {
                player.mouseInterface = true;
            }

            timerText.SetText("");
            if (mainSlot.Item.type > ItemID.None)
            {
                if (!UpgradeSystem.UpgradeReady(Main.LocalPlayer.name)) upgradePanel.Append(slotLock);
                else if (upgradePanel.HasChild(slotLock)) upgradePanel.RemoveChild(slotLock);
                if (UpgradeSystem.UpgradeInProgress(Main.LocalPlayer.name, out int _, out int timeLeft) && timeLeft > 0)
                    timerText.SetText(timeLeft / 60 + "s");
                var blueprint = UpgradeBlueprintLoader.Find(mainSlot.Item, player);

                if (blueprint != null && blueprint.RequisitesMet(mainSlot.Item, Main.LocalPlayer))
                {
                    int slotsToMake = blueprint.Materials.GetLength(0);
                    CreateSlots(slotsToMake);
                }
            }
            else
            {
                if (upgradePanel.HasChild(slotLock)) upgradePanel.RemoveChild(slotLock);
                ClearSlots();
            }
            ListMaterials();
        }

        void UpgradeItem()
        {
            var player = Main.LocalPlayer;

            var blueprint = UpgradeBlueprintLoader.Find(mainSlot.Item, player);
            if (UpgradeSystem.UpgradeInProgress(player.name, out int _, out int timeLeft) && timeLeft > 0 || blueprint == null || !blueprint.RequisitesMet(mainSlot.Item, player)) return;

            var requiredMaterials = blueprint.Materials;
            var suppliedMaterials = MaterialsInSlots();

            int matchingMaterials = 0;
            for (int i = 0; i < materialSlots.Length; i++)
            {
                for (int j = 0; j < requiredMaterials.GetLength(0); j++)
                {
                    if (suppliedMaterials[i, 0] == requiredMaterials[j, 0])
                    {
                        if (suppliedMaterials[i, 1] >= requiredMaterials[j, 1])
                        {
                            matchingMaterials++;
                        }
                    }
                }
            }
            if (matchingMaterials == requiredMaterials.GetLength(0))
            {
                if (blueprint.ReplaceItem) mainSlot.Item = blueprint.ResultItem;
                else blueprint.ModifyItem(mainSlot.Item, player);
                ConsumeItems(requiredMaterials);
                UpgradeSystem.StartUpgrade(player.name, mainSlot.Item.type, blueprint.TimeToUpgrade);
                ClearSlots();
            }
        }

        int[,] MaterialsInSlots()
        {
            int[,] result = new int[materialSlots.Length, 2];
            for (int i = 0; i < materialSlots.Length; i++)
            {
                result[i, 0] = materialSlots[i].Item.type;
                result[i, 1] = materialSlots[i].Item.stack;
            }
            return result;
        }

        void ConsumeItems(int[,] items)
        {
            for (int i = 0; i < items.GetLength(0); i++)
            {
                int itemid = items[i, 0];
                int itemStack = items[i, 1];
                var ind = FindSlotWithItem(itemid);
                if (ind != -1)
                {
                    var slot = materialSlots[ind];
                    var item = slot.Item;
                    if (item.stack > itemStack)
                    {
                        item.stack -= itemStack;
                    }
                    else
                    {
                        item.TurnToAir();
                    }
                }
            }
            FirstActiveAtherian.upgrading = true;
            SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound
        }
        int FindSlotWithItem(int item)
        {
            int slotInd = -1;
            if (materialSlots != null)
            {
                for (int i = 0; i < materialSlots.Length; i++)
                {
                    var slot = materialSlots[i];
                    if (slot != null)
                    {
                        if (slot.Item.type == item)
                        {
                            slotInd = i;
                            break;
                        }
                    }
                }
            }
            return slotInd;
        }
        bool AnySlotContains(int item, int stack = 1)
        {
            bool doesContain = false;
            if (materialSlots != null)
            {
                foreach (var slot in materialSlots)
                {
                    if (slot != null)
                    {
                        if (slot.Item.type == item)
                        {
                            if (slot.Item.stack >= stack)
                            {
                                doesContain = true;
                                break;
                            }
                        }
                    }
                }
            }
            return doesContain;
        }
    }

    public class UpgradeUISystem : ModSystem
    {
        internal UpgradeUI _UpgradeUI;
        private UserInterface upgradeInterface;

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            _UpgradeUI = new UpgradeUI();
            _UpgradeUI.Activate();
            upgradeInterface = new UserInterface();
            upgradeInterface.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            upgradeInterface?.Update(gameTime);
        }

        public void ShowUI()
        {
            upgradeInterface.SetState(_UpgradeUI);
            _UpgradeUI.InitializeSlotItem();
        }
        public void HideUI()
        {
            upgradeInterface.SetState(null);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ShardsOfAtheria: Upgrade UI",
                    delegate
                    {
                        upgradeInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
