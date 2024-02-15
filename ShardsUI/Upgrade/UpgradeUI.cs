using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.ShardsUI
{
    class UpgradeUI : UIState
    {
        UIPanel panel;
        VanillaItemSlotWrapper mainSlot;
        VanillaItemSlotWrapper[] materialSlots;
        UIHoverImageButton upgradeButton;
        bool slotsCreated = false;
        static int AtherianType => ModContent.NPCType<Atherian>();
        static Atherian FirstActiveAtherian => NPC.AnyNPCs(AtherianType) ? Main.npc[NPC.FindFirstNPC(AtherianType)].ModNPC as Atherian : null;

        public override void OnInitialize()
        {
            panel = new();
            panel.SetRectangle(0, 0, 500, 500);

            mainSlot = new VanillaItemSlotWrapper(scale: 0.8f)
            {
                Left = { Pixels = 239 - 41.6f / 2 },
                Top = { Pixels = 239 - 41.6f / 2 },
                ValidItemFunc = item => item.IsAir || (!item.IsAir && item.IsUpgradable() /*&& (FirstActiveAtherian.UpgradeFinished() || !FirstActiveAtherian.upgrading)*/)
            };
            panel.Append(mainSlot);

            Asset<Texture2D> buttonReforgeTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/Reforge_0");
            upgradeButton = new(buttonReforgeTexture, "Upgrade");
            upgradeButton.SetRectangle(446, 446, 30f, 30f);
            upgradeButton.OnLeftClick += new MouseEvent(UpgradeItem);
            panel.Append(upgradeButton);

            Append(panel);
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
                panel.Append(materialSlots[i]);
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
                    panel.RemoveChild(slot);
                }
                materialSlots = null;
            }
            slotsCreated = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int x = Main.screenWidth / 2 - 250;
            int y = Main.screenHeight / 2 - 250;
            panel.SetRectangle(x, y, 500, 500);

            var player = Main.LocalPlayer;
            if (!player.isNearNPC(ModContent.NPCType<Atherian>(), 90))
            {
                ModContent.GetInstance<UpgradeUISystem>().HideUI();
            }

            if (ContainsPoint(Main.MouseScreen))
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
            else
            {
                ClearSlots();
            }
        }

        void UpgradeItem(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer;
            var shards = player.Shards();

            #region Arsenal upgrades (Genesis and Ragnarok)
            if (mainSlot.Item.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (UpgradeArsenal1())
                {
                    int[,] materials = new[,]
                    {
                        { ModContent.ItemType<MemoryFragment>(), 1 }
                    };
                    ConsumeItems(materials);
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal2())
                {
                    int[,] materials = new[,]
                    {
                        { ModContent.ItemType<MemoryFragment>(), 1 },
                        { ItemID.ChlorophyteBar, 14 }
                    };
                    ConsumeItems(materials);
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal3())
                {
                    int[,] materials = new[,]
                    {
                        { ModContent.ItemType<MemoryFragment>(), 1 },
                        { ItemID.BeetleHusk, 16 }
                    };
                    ConsumeItems(materials);
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal4())
                {
                    int[,] materials = new[,]
                    {
                        { ModContent.ItemType<MemoryFragment>(), 1 },
                        { ItemID.FragmentSolar, 18 }
                    };
                    ConsumeItems(materials);
                    shards.genesisRagnarockUpgrades++;
                }
                else if (UpgradeArsenal5())
                {
                    int[,] materials = new[,]
                    {
                        { ModContent.ItemType<MemoryFragment>(), 1 },
                        { ItemID.LunarBar, 20 }
                    };
                    ConsumeItems(materials);
                    shards.genesisRagnarockUpgrades++;
                }
            }
            #endregion
            #region Areus upgrades
            if (UpgradeKatana())
            {
                int[,] materials = new[,]
                {
                    { ItemID.BeetleHusk, 20 },
                    { ItemID.SoulofFright, 14 },
                };
                ConsumeItems(materials);
                mainSlot.Item = new(ModContent.ItemType<TheMourningStar>());
            }
            if (UpgradeRailgun())
            {
                int[,] materials = new[,]
                {
                    { ItemID.Flamethrower, 1 },
                    { ItemID.BeetleHusk, 14 },
                };
                ConsumeItems(materials);
                mainSlot.Item = new(ModContent.ItemType<AreusFlameCannon>());
            }
            if (UpgradeGambit())
            {
                int[,] materials = new[,]
                {
                    { ModContent.ItemType<AreusDagger>(), 1 },
                    { ModContent.ItemType<AreusBow>(), 1 },
                    { ModContent.ItemType<AreusKatana>(), 1 },
                    { ModContent.ItemType<AreusMagnum>(), 1 },
                    { ModContent.ItemType<AreusRailgun>(), 1 },
                    { ItemID.BeetleHusk, 15 },
                };
                ConsumeItems(materials);
                mainSlot.Item = new(ModContent.ItemType<AreusGauntlet>());
            }
            if (UpgradeSawstring())
            {
                int[,] materials = new[,]
                {
                    { ModContent.ItemType<AreusSawstring>(), 1 },
                    { ItemID.LunarBar, 14 },
                    { ItemID.FragmentVortex, 10 },
                };
                ConsumeItems(materials);
                mainSlot.Item = new(ModContent.ItemType<AreusOrbiter>());
            }
            if (RepairPartisan())
            {
                int[,] materials = new[,]
                {
                    { ModContent.ItemType<FuckEarlyGameHarpies>(), 1 },
                    { ModContent.ItemType<AreusShard>(), 20 },
                    { ItemID.LunarBar, 14 },
                };
                ConsumeItems(materials);
                mainSlot.Item = new(ModContent.ItemType<AreusPartisan>());
            }
            #endregion
            if (UpgradeWar())
            {
                int[,] materials = new[,]
                {
                    { ItemID.HallowedBar, 20 }
                };
                ConsumeItems(materials);
                var war = mainSlot.Item.ModItem as War;
                war.upgraded = true;
            }
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
        bool UpgradeKatana()
        {
            var item = mainSlot.Item;
            if (item.type != ModContent.ItemType<AreusKatana>())
            {
                return false;
            }
            if (!AnySlotContains(ItemID.BeetleHusk, 20))
            {
                return false;
            }
            if (!AnySlotContains(ItemID.SoulofFright, 14))
            {
                return false;
            }
            return true;
        }
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
