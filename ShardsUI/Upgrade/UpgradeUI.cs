using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Items.DedicatedItems.Webmillio;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon;
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
                UIText warningText = new("Note: Upgrades will [c/FF0000:NOT] save on world\n" +
                    "exit, make sure the upgrade is complete\n" +
                    "and received before saving and exiting.");
                warningText.SetDimensions(308, 78);
                warningText.Top.Set(-78, 1f);
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
            int upgradeItemType = mainSlot.Item.type;
            var materials = GetMaterials(upgradeItemType, out int result, out int upgradeTime);
            if (upgradeItemType == ItemID.None || materials == null)
            {
                infoText.SetText("Insert an upgradable item into the slot.");
                return;
            }
            string text = "";
            for (int i = 0; i < materials.GetLength(0); i++)
            {
                int materialType = materials[i, 0];
                int materialCount = materials[i, 1];
                Item MaterialItem = new(materialType);
                string colorHex = AnySlotContains(materialType, materialCount) ? "00FF00" : "FFFFFF";
                text += $"[i:{materialType}] [c/{colorHex}:{MaterialItem.Name} x{materialCount}],\n";
            }
            text += $"Upgrade will take [c/FFFF00:{upgradeTime / 60} seconds.]\n";
            if (result > 0)
            {
                Item resultItem = new(result);
                text += "Creates " + "[i:" + result + "] " + resultItem.Name + ".\n";
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

            timerText.SetText("");
            if (mainSlot.Item.type > ItemID.None)
            {
                if (!UpgradeSystem.UpgradeReady(Main.LocalPlayer.name)) upgradePanel.Append(slotLock);
                else if (upgradePanel.HasChild(slotLock)) upgradePanel.RemoveChild(slotLock);
                if (UpgradeSystem.UpgradeInProgress(Main.LocalPlayer.name, out int _, out int timeLeft) && timeLeft > 0)
                    timerText.SetText(timeLeft / 60 + "s");
                int[,] materials = GetMaterials(mainSlot.Item.type, out int _, out int _);
                if (materials != null)
                {
                    int slotsToMake = materials.GetLength(0);
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
            var shards = player.Shards();
            bool shouldUpgrade = false;
            int resultType = 0;
            int upgradeTime = 1200;
            int[,] materials = null;

            #region Arsenal upgrades (Genesis and Ragnarok)
            if (mainSlot.Item.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (UpgradeArsenal1())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, out upgradeTime, 0);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal2())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, out upgradeTime, 1);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal3())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, out upgradeTime, 2);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal4())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, out upgradeTime, 3);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal5())
                {
                    materials = GetMaterials<GenesisAndRagnarok>(out resultType, out upgradeTime, 4);
                    shouldUpgrade = true;
                    shards.genesisRagnarockUpgrades++;
                }
            }
            #endregion
            #region Areus upgrades
            if (UpgradeRailgun())
            {
                materials = GetMaterials<AreusRailgun>(out resultType, out upgradeTime);
                shouldUpgrade = true;
            }
            if (UpgradeGambit())
            {
                materials = GetMaterials<AreusGambit>(out resultType, out upgradeTime);
                shouldUpgrade = true;
            }
            if (UpgradeSawstring())
            {
                materials = GetMaterials<AreusSawstring>(out resultType, out upgradeTime);
                shouldUpgrade = true;
            }
            if (RepairPartisan())
            {
                materials = GetMaterials<FuckEarlyGameHarpies>(out resultType, out upgradeTime);
                shouldUpgrade = true;
            }
            if (RepairMirror())
            {
                materials = GetMaterials<BrokenAreusMirror>(out resultType, out upgradeTime);
                shouldUpgrade = true;
            }
            #endregion
            if (UpgradeWar())
            {
                materials = GetMaterials<War>(out resultType, out upgradeTime);
                var war = mainSlot.Item.ModItem as War;
                war.upgraded = true;
                UpgradeSystem.StartUpgrade(Main.LocalPlayer.name, resultType, upgradeTime);
            }
            if (shouldUpgrade)
            {
                ConsumeItems(materials);
                if (resultType > 0)
                {
                    mainSlot.Item.SetDefaults(resultType);
                }
                UpgradeSystem.StartUpgrade(Main.LocalPlayer.name, resultType, upgradeTime);
            }
        }

        public static int[,] GetMaterials<T>(out int result, out int timeToUpgrade, int currentLevel = 0) where T : ModItem
        {
            int[,] materials = GetMaterials(ModContent.ItemType<T>(), out result, out timeToUpgrade, currentLevel);
            return materials;
        }
        public static int[,] GetMaterials(int baseItemType, out int result, out int timeToUpgrade, int currentLevel = 0)
        {
            timeToUpgrade = 1200;
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
            if (baseItemType == ModContent.ItemType<BrokenAreusMirror>())
            {
                materials = new[,]
                {
                    { ModContent.ItemType<AreusShard>(), 10 },
                    { ModContent.ItemType<Jade>(), 3 },
                    { ItemID.CrystalShard, 15 },
                };
                result = ModContent.ItemType<AreusMirror>();
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
        bool RepairMirror()
        {
            var item = mainSlot.Item;
            if (!AnySlotContains(ModContent.ItemType<AreusShard>(), 10))
            {
                return false;
            }
            if (!AnySlotContains(ModContent.ItemType<Jade>(), 3))
            {
                return false;
            }
            if (item.type != ModContent.ItemType<BrokenAreusMirror>())
            {
                return false;
            }
            if (!AnySlotContains(ItemID.CrystalShard, 15))
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
