using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.DedicatedItems.Webmillio;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.NPCs.Town.TheAtherian;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
        UIHoverImageButton upgradeButton;
        UIPanel infoPanel;
        UIText infoText;
        bool slotsCreated = false;
        static int AtherianType => ModContent.NPCType<Atherian>();
        static Atherian FirstActiveAtherian => NPC.AnyNPCs(AtherianType) ? Main.npc[NPC.FindFirstNPC(AtherianType)].ModNPC as Atherian : null;

        public override void OnInitialize()
        {
            backPanel = new();
            backPanel.SetRectangle(0, 0, 868, 500);

            upgradePanel = new();
            upgradePanel.SetRectangle(0, 0, 500, 500);
            backPanel.Append(upgradePanel);

            mainSlot = new VanillaItemSlotWrapper(scale: 0.8f)
            {
                Left = { Pixels = 239 - 41.6f / 2 },
                Top = { Pixels = 239 - 41.6f / 2 },
                ValidItemFunc = item => item.IsAir || (!item.IsAir && item.IsUpgradable() /*&& (FirstActiveAtherian.UpgradeFinished() || !FirstActiveAtherian.upgrading)*/)
            };
            upgradePanel.Append(mainSlot);

            upgradeButton = new(TextureAssets.Reforge[0], "Upgrade");
            upgradeButton.Left.Set(-30, 1f);
            upgradeButton.Top.Set(-30, 1f);
            upgradeButton.Width.Set(30, 0f);
            upgradeButton.Height.Set(30, 0f);
            upgradeButton.OnLeftClick += (a, b) => UpgradeItem();
            upgradePanel.Append(upgradeButton);

            infoPanel = new();
            infoPanel.SetRectangle(512, 0, 332, 500);
            backPanel.Append(infoPanel);

            infoText = new("Insert an upgradable item into the slot");
            infoText.SetDimensions(308, 476);
            infoPanel.Append(infoText);

            Append(backPanel);
        }

        void CreateSlots(int amount)
        {
            if (slotsCreated)
            {
                return;
            }
            materialSlots = new VanillaItemSlotWrapper[amount];
            float rotation = MathHelper.TwoPi / amount;
            Vector2 center = new(239);
            for (int i = 0; i < amount; i++)
            {
                var vector = new Vector2(1, 0).RotatedBy(rotation * i) * 150;
                materialSlots[i] = new VanillaItemSlotWrapper(scale: 0.8f)
                {
                    Left = { Pixels = center.X + vector.X - (41.6f / 2) },
                    Top = { Pixels = center.X + vector.Y - (41.6f / 2) },
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
            int upgradeItemType = mainSlot.Item.type;
            var materials = GetMaterials(upgradeItemType, out int result);
            if (upgradeItemType == ItemID.None || materials == null)
            {
                infoText.SetText("Insert an upgradable item into the slot.");
                return;
            }
            string text = "";
            for (int i = 0; i < materials.Rank; i++)
            {
                int materialType = materials[i, 0];
                int materialCount = materials[i, 1];
                Item MaterialItem = new(materialType);
                string colorHex = AnySlotContains(materialType, materialCount) ? "00FF00" : "FFFFFF";
                text += $"[i:{materialType}] [c/{colorHex}:{MaterialItem.Name} x{materialCount}],\n";
            }
            if (result > 0)
            {
                Item resultItem = new(result);
                text += "Creates " + "[i:" + result + "] " + resultItem.Name + ".";
            }
            else
            {
                text += "Upgrading will make the weapon stronger.";
            }
            //if (Main.LocalPlayer.IsSlayer())
            //{
            //    Item resultItem = result > 0 ? new(result) : new(upgradeItemType);
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

            if (mainSlot.Item.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (player.Shards().genesisRagnarockUpgrades == 0)
                {
                    CreateSlots(1);
                }
                else
                {
                    CreateSlots(2);
                }
            }
            else if (mainSlot.Item.type == ModContent.ItemType<AreusKatana>())
            {
                CreateSlots(2);
            }
            else if (mainSlot.Item.type == ModContent.ItemType<AreusGambit>())
            {
                CreateSlots(6);
            }
            else if (mainSlot.Item.type == ModContent.ItemType<AreusRailgun>())
            {
                CreateSlots(2);
            }
            else if (mainSlot.Item.type == ModContent.ItemType<War>())
            {
                CreateSlots(1);
            }
            else if (mainSlot.Item.type == ModContent.ItemType<FuckEarlyGameHarpies>())
            {
                CreateSlots(2);
            }
            else
            {
                ClearSlots();
            }
            ListMaterials();
        }

        void UpgradeItem()
        {
            var player = Main.LocalPlayer;
            var shards = player.Shards();
            bool shouldUpgrade = false;
            int resultType = 0;
            int[,] materials = null;

            #region Arsenal upgrades (Genesis and Ragnarok)
            if (mainSlot.Item.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (UpgradeArsenal1())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, 0);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal2())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, 1);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal3())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, 2);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal4())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, 3);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal5())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, 4);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
            }
            #endregion
            #region Areus upgrades
            if (UpgradeRailgun())
            {
                materials = GetMaterials<AreusRailgun>(out resultType);
                shouldUpgrade = true;
            }
            if (UpgradeGambit())
            {
                materials = GetMaterials<AreusGambit>(out resultType);
                shouldUpgrade = true;
            }
            if (UpgradeSawstring())
            {
                materials = GetMaterials<AreusSawstring>(out resultType);
                shouldUpgrade = true;
            }
            if (RepairPartisan())
            {
                materials = GetMaterials<FuckEarlyGameHarpies>(out resultType);
                shouldUpgrade = true;
            }
            #endregion
            if (UpgradeWar())
            {
                materials = GetMaterials<War>(out resultType);
                var war = mainSlot.Item.ModItem as War;
                war.upgraded = true;
            }
            if (shouldUpgrade)
            {
                ConsumeItems(materials);
                if (resultType > 0)
                {
                    mainSlot.Item.SetDefaults(resultType);
                }
            }
        }

        public static int[,] GetMaterials<T>(out int result, int currentLevel = 0) where T : ModItem
        {
            int[,] materials = GetMaterials(ModContent.ItemType<T>(), out result, currentLevel);
            return materials;
        }
        public static int[,] GetMaterials(int baseItemType, out int result, int currentLevel = 0)
        {
            int[,] materials = null;
            result = 0;
            if (baseItemType == ModContent.ItemType<GenesisAndRagnarok>())
            {
                switch (currentLevel)
                {
                    case 0:
                        materials = new[,]
                         {
                            { ModContent.ItemType<MemoryFragment>(), 1 },
                        };
                        break;
                    case 1:
                        materials = new[,]
                         {
                            { ModContent.ItemType<MemoryFragment>(), 1 },
                            { ItemID.ChlorophyteBar, 14 },
                        };
                        break;
                    case 2:
                        materials = new[,]
                         {
                            { ModContent.ItemType<MemoryFragment>(), 1 },
                            { ItemID.BeetleHusk , 16 },
                        };
                        break;
                    case 3:
                        materials = new[,]
                         {
                            { ModContent.ItemType<MemoryFragment>(), 1 },
                            { ItemID.FragmentSolar, 18 },
                        };
                        break;
                    case 4:
                        materials = new[,]
                         {
                            { ModContent.ItemType<MemoryFragment>(), 1 },
                            { ItemID.LunarBar, 20 },
                        };
                        break;
                }
            }
            if (baseItemType == ModContent.ItemType<AreusRailgun>())
            {
                materials = new[,]
                 {
                    { ItemID.Flamethrower, 1 },
                    { ItemID.BeetleHusk, 14 },
                };
                result = ModContent.ItemType<AreusFlameCannon>();
            }
            if (baseItemType == ModContent.ItemType<AreusGambit>())
            {
                materials = new[,]
                 {
                    { ModContent.ItemType<AreusDagger>(), 1 },
                    { ModContent.ItemType<AreusBow>(), 1 },
                    { ModContent.ItemType<AreusKatana>(), 1 },
                    { ModContent.ItemType<AreusMagnum>(), 1 },
                    { ModContent.ItemType<AreusRailgun>(), 1 },
                    { ItemID.BeetleHusk, 15 },
                };
                result = ModContent.ItemType<AreusGauntlet>();
            }
            if (baseItemType == ModContent.ItemType<AreusSawstring>())
            {
                materials = new[,]
                {
                    { ItemID.LunarBar, 14 },
                    { ItemID.FragmentVortex, 10 },
                };
                result = ModContent.ItemType<AreusOrbiter>();
            }
            if (baseItemType == ModContent.ItemType<FuckEarlyGameHarpies>())
            {
                materials = new[,]
                {
                    { ModContent.ItemType<AreusShard>(), 20 },
                    { ItemID.LunarBar, 14 },
                };
                result = ModContent.ItemType<AreusPartisan>();
            }
            if (baseItemType == ModContent.ItemType<War>())
            {
                materials = new[,]
                {
                    { ItemID.HallowedBar, 20 },
                };
            }
            return materials;
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

        #region Arsenal checks
        bool UpgradeArsenal1()
        {
            return ShouldUpgradeArsenal(1);
        }
        bool UpgradeArsenal2()
        {
            if (!AnySlotContains(ItemID.ChlorophyteBar, 14))
            {
                return false;
            }
            return ShouldUpgradeArsenal(2);
        }
        bool UpgradeArsenal3()
        {
            if (!AnySlotContains(ItemID.BeetleHusk, 16))
            {
                return false;
            }
            return ShouldUpgradeArsenal(3);
        }
        bool UpgradeArsenal4()
        {
            if (!AnySlotContains(ItemID.FragmentSolar, 18))
            {
                return false;
            }
            return ShouldUpgradeArsenal(4);
        }
        bool UpgradeArsenal5()
        {
            if (!AnySlotContains(ItemID.LunarBar, 20))
            {
                return false;
            }
            return ShouldUpgradeArsenal(5);
        }
        bool ShouldUpgradeArsenal(int toLevel)
        {
            var item = mainSlot.Item;
            var player = Main.LocalPlayer;
            if (player.Shards().genesisRagnarockUpgrades >= toLevel)
            {
                return false;
            }
            if (!AnySlotContains(ModContent.ItemType<MemoryFragment>()))
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Areus checks
        bool UpgradeRailgun() // To Flame Cannon
        {
            var item = mainSlot.Item;
            if (item.type != ModContent.ItemType<AreusRailgun>())
            {
                return false;
            }
            if (!AnySlotContains(ItemID.Flamethrower))
            {
                return false;
            }
            if (!AnySlotContains(ItemID.BeetleHusk, 14))
            {
                return false;
            }
            return true;
        }
        bool UpgradeGambit()
        {
            var item = mainSlot.Item;
            if (item.type != ModContent.ItemType<AreusGambit>())
            {
                return false;
            }
            if (!AnySlotContains(ModContent.ItemType<AreusDagger>()))
            {
                return false;
            }
            if (!AnySlotContains(ModContent.ItemType<AreusBow>()))
            {
                return false;
            }
            if (!AnySlotContains(ModContent.ItemType<AreusKatana>()))
            {
                return false;
            }
            if (!AnySlotContains(ModContent.ItemType<AreusMagnum>()))
            {
                return false;
            }
            if (!AnySlotContains(ModContent.ItemType<AreusRailgun>()))
            {
                return false;
            }
            if (!AnySlotContains(ItemID.BeetleHusk, 15))
            {
                return false;
            }
            return true;
        }
        bool RepairPartisan()
        {
            var item = mainSlot.Item;
            if (item.type != ModContent.ItemType<FuckEarlyGameHarpies>())
            {
                return false;
            }
            if (!AnySlotContains(ModContent.ItemType<AreusShard>(), 20))
            {
                return false;
            }
            if (!AnySlotContains(ItemID.LunarBar, 14))
            {
                return false;
            }
            return true;
        }
        bool UpgradeSawstring()
        {
            var item = mainSlot.Item;
            if (item.type != ModContent.ItemType<AreusSawstring>())
            {
                return false;
            }
            if (!AnySlotContains(ItemID.LunarBar, 14))
            {
                return false;
            }
            if (!AnySlotContains(ItemID.FragmentVortex, 10))
            {
                return false;
            }
            return true;
        }
        #endregion
        bool UpgradeWar()
        {
            var item = mainSlot.Item;
            var war = mainSlot.Item.ModItem as War;
            if (item.type != ModContent.ItemType<War>())
            {
                return false;
            }
            if (war.upgraded)
            {
                return false;
            }
            if (!AnySlotContains(ItemID.HallowedBar, 20))
            {
                return false;
            }
            return true;
        }
    }

    public class UpgradeUISystem : ModSystem
    {
        internal UpgradeUI _UpgradeUI;
        private UserInterface upgradeInterface;

        public override void Load()
        {
            _UpgradeUI = new UpgradeUI();
            _UpgradeUI.Activate();
            upgradeInterface = new UserInterface();
            upgradeInterface.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            upgradeInterface?.Update(gameTime);
        }

        public void ToggleChips()
        {
            if (upgradeInterface.CurrentState == null)
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
            upgradeInterface.SetState(_UpgradeUI);
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
