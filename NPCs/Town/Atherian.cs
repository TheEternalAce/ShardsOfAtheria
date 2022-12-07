using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.ItemDropRules.Conditions;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using ShardsOfAtheria.Utilities;
using ShopQuotesMod;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.Town
{
    // [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class Atherian : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
            // DisplayName.SetDefault("Example Person");
            Main.npcFrameCount[Type] = 26;
            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 5;
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            // Influences how the NPC looks in the Bestiary
            //NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            //{
            //    Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            //    Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
            //                  // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
            //                  // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            //};

            //NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    ModContent.BuffType<ElectricShock>(),

                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            SoAGlobalNPC.ElectricNPC.Add(Type);

            // Set Atherian's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang
            NPC.Happiness
                //Biomes
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Hate)
                //NPCs
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like);

            ModContent.GetInstance<QuoteDatabase>()
                .AddNPC(Type, Mod, "Mods.ShardsOfAtheria.ShopQuote.")
                .UseColor(Color.Cyan);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;

            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 200;
            NPC.defense = 999;
            NPC.lifeMax = 9000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Stylist;
            NPC.SetElementEffectivenessMultipliers(0.5, 1.0, 0.4, 2.0);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("Placeholder text.")
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

                if (NPC.downedBoss2 && !(ModContent.GetInstance<ShardsDownedSystem>().slainSenterra || ModContent.GetInstance<ShardsDownedSystem>().slainGenesis))
                    return true;
            }
            return false;
        }

        public override bool PreAI()
        {
            if (ModContent.GetInstance<ShardsDownedSystem>().slainSenterra || ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie)
            {
                NPC.active = false;
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(NPC.GivenName + " has left."), Color.Red);
                return false;
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

            if (!Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode || ModContent.GetInstance<ShardsConfigServerSide>().cluelessNPCs)
            {
                if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
                {
                    if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 0)
                    {
                        return Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.BaseGenesisAndRagnarok");
                    }
                }
            }

            chat.Add(Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.CSGOReference"));
            chat.Add(Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.KenobiReference"));
            chat.Add(Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.PleaseINeedMoreLinesForThisMan"));
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Upgrade";
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
                if (player.GetModPlayer<SlayerPlayer>().slayerMode && !ModContent.GetInstance<ShardsConfigServerSide>().cluelessNPCs)
                {
                    Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.RefuseUpgrade");
                }
                else if (player.HasItem(ModContent.ItemType<GenesisAndRagnarok>()) && (player.inventory[player.FindItem(ModContent.ItemType<GenesisAndRagnarok>())].ModItem as GenesisAndRagnarok).upgrades < 5)
                {
                    GenesisAndRagnarok GenesisRagnarok = player.inventory[player.FindItem(ModContent.ItemType<GenesisAndRagnarok>())].ModItem as GenesisAndRagnarok;
                    if (GenesisRagnarok.upgrades == 0)
                    {
                        ShardsHelpers.UpgrageMaterial[] materials = {
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1)
                        };
                        NPC.UpgradeItem(player, ItemID.None, materials);
                    }
                    else if (GenesisRagnarok.upgrades == 1)
                    {
                        ShardsHelpers.UpgrageMaterial[] materials = {
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ItemID.ChlorophyteBar], 14)
                        };
                        NPC.UpgradeItem(player, ItemID.None, materials);
                    }
                    else if (GenesisRagnarok.upgrades == 2)
                    {
                        ShardsHelpers.UpgrageMaterial[] materials = {
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ItemID.BeetleHusk], 16)
                        };
                        NPC.UpgradeItem(player, ItemID.None, materials);
                    }
                    else if (GenesisRagnarok.upgrades == 3)
                    {
                        ShardsHelpers.UpgrageMaterial[] materials = {
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ItemID.FragmentSolar], 18)
                        };
                        NPC.UpgradeItem(player, ItemID.None, materials);
                    }
                    else if (GenesisRagnarok.upgrades == 4)
                    {
                        ShardsHelpers.UpgrageMaterial[] materials = {
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()], 1),
                            new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ItemID.LunarBar], 20)
                        };
                        NPC.UpgradeItem(player, ItemID.None, materials);
                    }
                }
                else if (player.HasItem(ModContent.ItemType<AreusDagger>()))
                {
                    ShardsHelpers.UpgrageMaterial[] materials = {
                        new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusDagger>()], 0),
                        new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusSword>()], 1),
                        new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ItemID.LunarBar], 14)
                    };
                    NPC.UpgradeItem(player, ModContent.ItemType<AreusSaber>(), materials);
                }
                else if (player.HasItem(ModContent.ItemType<AreusKatana>()))
                {
                    ShardsHelpers.UpgrageMaterial[] materials = {
                        new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusKatana>()], 1),
                        new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ItemID.BeetleHusk], 20),
                        new ShardsHelpers.UpgrageMaterial(ContentSamples.ItemsByType[ItemID.SoulofFright], 14)
                    };
                    NPC.UpgradeItem(player, ModContent.ItemType<TheMourningStar>(), materials);
                }
                else
                {
                    Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.NoUpgradableItem");
                    return;
                }
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusKey>());
                shop.item[nextSlot].shopCustomPrice = 50000;
                nextSlot++;
            }
            if (!Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ValkyrieCrest>());
                nextSlot++;
                if (NPC.downedSlimeKing)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.SlimeCrown);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedBoss1)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.SuspiciousLookingEye);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedBoss2)
                {
                    if (WorldGen.crimson)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.BloodySpine);
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.WormFood);
                    }
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedQueenBee)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Abeemination);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ClothierVoodooDoll);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedDeerclops)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.DeerThing);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.GuideVoodooDoll);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedQueenSlime)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.QueenSlimeCrystal);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedMechBoss1)
                {
                    if (ModLoader.TryGetMod("PrimeRework", out Mod foundMod))
                    {
                        shop.item[nextSlot].SetDefaults(foundMod.Find<ModItem>("WormRemote").Type);
                        shop.item[nextSlot].shopCustomPrice = 50000;
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(foundMod.Find<ModItem>("BrainRemote").Type);
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.MechanicalWorm);
                    }
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedMechBoss2)
                {
                    if (ModLoader.TryGetMod("PrimeRework", out Mod foundMod))
                    {
                        shop.item[nextSlot].SetDefaults(foundMod.Find<ModItem>("EyeRemote").Type);
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.MechanicalEye);
                    }
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedMechBoss3)
                {
                    if (ModLoader.TryGetMod("PrimeRework", out Mod foundMod))
                    {
                        shop.item[nextSlot].SetDefaults(foundMod.Find<ModItem>("SkullRemote").Type);
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.MechanicalSkull);
                    }
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<PottedPlant>());
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedGolemBoss)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.LihzahrdPowerCell);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedFishron)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.TruffleWorm);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedEmpressOfLight)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.EmpressButterfly);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.CelestialSigil);
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (ShardsDownedSystem.downedGenesis)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<SpiderClock>());
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (ShardsDownedSystem.downedSenterra)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusSnakeScale>());
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
                }
                if (ShardsDownedSystem.downedDeath)
                {
                    //shop.item[nextSlot].SetDefaults(ModContent.ItemType<AncientMedalion>());
                    shop.item[nextSlot].shopCustomPrice = 50000;
                    nextSlot++;
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