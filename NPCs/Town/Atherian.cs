using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.ItemDropRules.Conditions;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.DataDisks;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Areus.AreusSword;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static ShardsOfAtheria.Utilities.ShardsHelpers;

namespace ShardsOfAtheria.NPCs.Town
{
    // [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class Atherian : ModNPC
    {
        public Item[] inventory = new Item[50];
        public Item[] initialInventory = new Item[] {
            ModContent.GetInstance<AreusKey>().Item,
            ModContent.GetInstance<AreusDataDisk>().Item,
            //ModContent.GetInstance<AreusProcessor>().Item,
            //ModContent.GetInstance<RingOfResonance>().Item,
            //ModContent.GetInstance<BlankAreusChip>().Item,
            ContentSamples.ItemsByType[ItemID.GoldCrown],
        };

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 26;
            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 5;
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    ModContent.BuffType<ElectricShock>(),
                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPCElements.Electric.Add(Type);

            // Set Atherian's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang
            NPC.Happiness
                //Biomes
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Hate)
                //NPCs
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;

            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 60;
            NPC.defense = 10;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Stylist;
            NPC.SetCustomElementMultipliers(0.5, 1.0, 0.4, 2.0);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.ShardsOfAtheria.NPCBestiary.Atherian"))
            });
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num = NPC.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                    continue;
                ShardsDownedSystem shardsDowned = ModContent.GetInstance<ShardsDownedSystem>();
                if (NPC.downedBoss2 && (!(shardsDowned.slainSenterra || shardsDowned.slainGenesis || shardsDowned.slainValkyrie) ||
                    ModContent.GetInstance<ShardsServerConfig>().cluelessNPCs) && !shardsDowned.slainAtherian)
                    return true;
            }
            return false;
        }

        public override bool PreAI()
        {
            if (!ModContent.GetInstance<ShardsServerConfig>().cluelessNPCs)
            {
                ShardsDownedSystem downedSystem = ModContent.GetInstance<ShardsDownedSystem>();
                bool oneDead = downedSystem.slainSenterra || downedSystem.slainGenesis || downedSystem.slainValkyrie;
                bool allDead = downedSystem.slainSenterra && downedSystem.slainGenesis && downedSystem.slainValkyrie;
                if (oneDead)
                {
                    NetworkText text = NetworkText.FromLiteral("");
                    if (allDead)
                    {
                        text = NetworkText.FromKey("Mods.ShardsOfAtheria.Dialogue.Atherian.AllDeath", NPC.GivenName);
                    }
                    else if (downedSystem.slainSenterra)
                    {
                        text = NetworkText.FromKey("Mods.ShardsOfAtheria.Dialogue.Atherian.SenterraDeath", NPC.GivenName);
                    }
                    else if (downedSystem.slainGenesis)
                    {
                        text = NetworkText.FromKey("Mods.ShardsOfAtheria.Dialogue.Atherian.GenesisDeath", NPC.GivenName);
                    }
                    else if (downedSystem.slainValkyrie)
                    {
                        text = NetworkText.FromKey("Mods.ShardsOfAtheria.Dialogue.Atherian.NovaDeath", NPC.GivenName);
                    }
                    NPC.active = false;
                    ChatHelper.BroadcastChatMessage(text, Color.Red);
                    return false;
                }
            }
            if (Main.time == 4 * 3600 + 30 * 60)
            {
                if (Main.rand.NextBool(5))
                {
                    NPC.active = false;
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.ShardsOfAtheria.Dialogue.Atherian.TemporaryLeave",
                        NPC.GivenName), Color.Red);
                    return false;
                }
            }
            return base.PreAI();
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() { "Jordan", "Damien", "Jason", "Kevin", "Rain", "Sage", "Archimedes" };
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();
            ShardsPlayer shardsPlayer = Main.LocalPlayer.ShardsOfAtheria();

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<GenesisAndRagnarok>()))
            {
                int upgrades = shardsPlayer.genesisRagnarockUpgrades;
                if (upgrades == 0)
                {
                    return Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.BaseGenesisAndRagnarok");
                }
            }

            chat.Add(Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.PleaseINeedMoreLinesForThisMan"));
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Upgrade";
            if (Main.LocalPlayer.Slayer().slayerMode)
            {
                button2 += " (18 gold, 78 silver)";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
            {
                Player player = Main.LocalPlayer;
                SlayerPlayer slayer = player.Slayer();
                ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
                int upgrades = shardsPlayer.genesisRagnarockUpgrades;
                if (slayer.slayerMode)
                {
                    int slayerUpgradeCost = 187800;
                    if (!player.CanBuyItem(slayerUpgradeCost))
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.NoUpgradeMoney");
                        return;
                    }
                }

                if (player.HasItem(ModContent.ItemType<GenesisAndRagnarok>()) && upgrades < 5)
                {
                    int result = ModContent.ItemType<GenesisAndRagnarok>();
                    switch (upgrades)
                    {
                        case 0:
                            UpgrageMaterial[] materials = {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1)
                            };
                            UpgradeItem(player, result, materials);
                            break;
                        case 1:
                            UpgrageMaterial[] materials1 = {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.ChlorophyteBar], 14)
                            };
                            UpgradeItem(player, result, materials1);
                            break;
                        case 2:
                            UpgrageMaterial[] materials2 = {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.BeetleHusk], 16)
                            };
                            UpgradeItem(player, result, materials2);
                            break;
                        case 3:
                            UpgrageMaterial[] materials3 = {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.FragmentSolar], 18)
                            };
                            UpgradeItem(player, result, materials3);
                            break;
                        case 4:
                            UpgrageMaterial[] materials4 = {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.LunarBar], 20)
                            };
                            UpgradeItem(player, result, materials4);
                            break;
                    }
                }
                else if (player.HasItem(ModContent.ItemType<AreusDagger>()))
                {
                    UpgrageMaterial[] materials = {
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusDagger>()], 0),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusSword>()], 1),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.LunarBar], 14)
                    };
                    UpgradeItem(player, ModContent.ItemType<AreusSaber>(), materials);
                }
                else if (player.HasItem(ModContent.ItemType<AreusKatana>()))
                {
                    UpgrageMaterial[] materials = {
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusKatana>()], 1),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.BeetleHusk], 20),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.SoulofFright], 14)
                    };
                    UpgradeItem(player, ModContent.ItemType<TheMourningStar>(), materials);
                }
                else if (player.HasItem(ModContent.ItemType<War>()))
                {
                    UpgrageMaterial[] materials = {
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<War>()], 0),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.HallowedBar], 20)
                    };
                    UpgradeItem(player, ModContent.ItemType<War>(), materials);
                }
                else
                {
                    Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.NoUpgradableItem");
                    return;
                }
            }
        }

        /// <summary>
        /// Insert useful summary here
        /// </summary>
        /// <param name="player"> Player with the item to be upgraded</param>
        /// <param name="result"> Item to upgrade into</param>
        /// <param name="materials"> an array of UpgradeMaterials</param>
        public void UpgradeItem(Player player, int result, params UpgrageMaterial[] materials)
        {
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();

            Main.npcChatCornerItem = result;
            if (CanUpgradeItem(player, materials))
            {
                if (player.Slayer().slayerMode)
                {
                    player.BuyItem(187800);
                }
                for (int i = 0; i < materials.Length; i++)
                {
                    player.inventory[player.FindItem(materials[i].item.type)].stack -= materials[i].requiredStack;
                }
                if (materials[0].item.ModItem is GenesisAndRagnarok)
                {
                    shardsPlayer.genesisRagnarockUpgrades++;
                }
                if (materials[0].item.ModItem is War)
                {
                    War war = player.inventory[player.FindItem(materials[0].item.type)].ModItem as War;
                    war.upgraded = true;
                }
                SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound
                if (result > 0)
                {
                    if (result == ModContent.ItemType<GenesisAndRagnarok>())
                    {
                        string key = "";
                        switch (shardsPlayer.genesisRagnarockUpgrades)
                        {
                            case 1:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok1";
                                break;
                            case 2:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok2";
                                break;
                            case 3:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok3";
                                break;
                            case 4:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok4";
                                break;
                            case 5:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok5";
                                break;
                        }
                        Main.npcChatText = Language.GetTextValue(key);
                    }
                    else if (result == ModContent.ItemType<War>())
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeAreusWeapon");
                    }
                    else
                    {
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.getRect(), result);
                        if (result == ModContent.ItemType<AreusSaber>() || result == ModContent.ItemType<TheMourningStar>())
                        {
                            Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeAreusWeapon");
                        }
                    }
                }
            }
            else
            {
                string insufficient = "I need the following items:\n";
                for (int i = 0; i < materials.Length; i++)
                {
                    Item item = null;
                    if (player.HasItem(materials[i].item.type))
                    {
                        item = player.inventory[player.FindItem(materials[i].item.type)];
                    }
                    insufficient += Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.NotEnoughMaterial", i, materials[i].item.Name, materials[i].item.type,
                        materials[i].requiredStack, item == null ? 0 : item.stack) + "\n";
                    Main.npcChatText = insufficient;
                }
            }
        }

        public bool CanUpgradeItem(Player player, UpgrageMaterial[] upgrageMaterials)
        {
            int requiredItems = upgrageMaterials.Length;
            int playerItems = 0;
            for (int i = 0; i < requiredItems; i++)
            {
                if (player.HasItem(upgrageMaterials[i].item.type))
                {
                    Item item = player.inventory[player.FindItem(upgrageMaterials[i].item.type)];
                    if (item.stack >= upgrageMaterials[i].requiredStack)
                    {
                        playerItems++;
                    }
                }
            }
            return playerItems >= requiredItems;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusShard>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusDataDisk>());
            shop.item[nextSlot].shopCustomPrice = 15000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<RushDrive>());
            shop.item[nextSlot].shopCustomPrice = 150000;
            nextSlot++;
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusKey>());
                shop.item[nextSlot].shopCustomPrice = 50000;
                nextSlot++;
            }
            for (int i = 0; i < shop.item.Length; i++)
            {
                Item item = shop.item[i];
                if (item.type == ItemID.TeleportationPylonHallow)
                {
                    Player player = Main.LocalPlayer;
                    ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
                    if (!shardsPlayer.crestRecieved)
                    {
                        Item.NewItem(NPC.GetSource_FromThis("Gift"), NPC.getRect(), ModContent.ItemType<ValkyrieCrest>());
                        ChatHelper.SendChatMessageToClient(NetworkText.FromKey("", player.name), Color.Cyan, player.whoAmI);
                        shardsPlayer.crestRecieved = true;
                    }
                }
            }
        }

        public override void OnKill()
        {
            Player lastPlayerToHitThisNPC = NPC.AnyInteractions() ? Main.player[NPC.lastInteraction] : null;
            if (lastPlayerToHitThisNPC != null)
            {
                if (lastPlayerToHitThisNPC.Slayer().slayerMode)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainAtherian = true;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule isSlayer = new(new IsSlayerMode());

            isSlayer.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieCrest>()));
            npcLoot.Add(isSlayer);
        }

        // Make this Town NPC teleport to the King and/or Queen statue when triggered.
        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = NPC.downedMoonlord ? 200 : 60;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<ElectricBlade>();
            attackDelay = 20;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}