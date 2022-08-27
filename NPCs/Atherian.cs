using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Tools;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs
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
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                              // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                              // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            // Set Atherian's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang
            NPC.Happiness
                //Biomes
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
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
            NPC.damage = 200;
            NPC.defense = 999;
            NPC.lifeMax = 9000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Stylist;
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

                if (NPC.downedBoss2 && !(ModContent.GetInstance<SoADownedSystem>().slainSenterra || ModContent.GetInstance<SoADownedSystem>().slainGenesis))
                    return true;
            }
            return false;
        }

        public override bool PreAI()
        {
            if (ModContent.GetInstance<SoADownedSystem>().slainSenterra)
                NPC.active = false;
            return base.PreAI();
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() { "Jordan", "Damien", "Jason", "Kevin", "Rain", "Sage", "Archimedes" };
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            int dryad = NPC.FindFirstNPC(NPCID.Dryad);
            if (dryad >= 0)
            {
                chat.Add("Show respect to your elders, I may be 240 years old but I still respect " + Main.npc[dryad].GivenName, 1/4);
            }

            if (!Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
            {
                if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
                {
                    if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 0)
                    {
                        if (!Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentI>()))
                            return "What a peculiar weapon. It shows capabilities of transforming into several types of weapon, but it's locked in spear and whip form.\n" +
                            "There's also memories sealed inside the shield. Perhaps something to unlock those memories will make it stronger?";
                        else
                            return "Hmm, it seems that Memory Fragment could be the key to upgrading your weapon. Mind if I take a look?";
                    }
                }
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<AreusWings>()))
                    return "Those wings are great now, but what if they could be better? If you hand them over I can make them better.";
            }

            chat.Add("DOOR STUCK!");
            chat.Add("Hello there.");
            chat.Add("I used to have so many lines of dialogue what happened?");
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
                if (Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
                {
                    Main.npcChatText = "You may have everyone else fooled but I know.. I will not help you slay our guardians..";
                    return;
                }
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<AreusWings>()))
                {
                    SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                    Main.npcChatText = "There, I upgraded those wings.";

                    int areusWings = Main.LocalPlayer.FindItem(ModContent.ItemType<AreusWings>());

                    Main.LocalPlayer.inventory[areusWings].TurnToAir();
                    Main.LocalPlayer.QuickSpawnItem(new EntitySource_Gift(NPC), ModContent.ItemType<ChargedAreusWings>());

                    return;
                }
                if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>() && (Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades >= 0
                    && (Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentI>()) || Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentII>())
                    || Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentIII>()) || Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentIV>())
                    || Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentV>())))
                {
                    if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 0 && Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentI>()))
                    {
                        SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                        Main.npcChatText = "As I expected, the fragments did make them stronger.\n" +
                            "Either way, Genesis is now able to take the form of a double-headed spear.\n" +
                            "There are still memories sealed within, if you find any more of those Memory Fragments then be sure to see me.";

                        (Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades = 1;
                        Main.LocalPlayer.inventory[Main.LocalPlayer.FindItem(ModContent.ItemType<MemoryFragmentI>())].stack--;

                        return;
                    }
                    else if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 1 && Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentII>()))
                    {
                        SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                        Main.npcChatText = "Genesis and Ragnarok are now stronger and Genesis can attach to Ragnarok for swinging.\n" +
                            "There are still seals to break, if you find any more of those Memory Fragments then be sure to see me.";

                        (Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades = 2;
                        Main.LocalPlayer.inventory[Main.LocalPlayer.FindItem(ModContent.ItemType<MemoryFragmentII>())].stack--;

                        return;
                    }
                    else if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 2 && Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentIII>()))
                    {
                        SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                        Main.npcChatText = "Genesis and Ragnarok now stronger and can set enemies on fire.\n" +
                            "There are still seals to break, if you find any more of those Memory Fragments then be sure to see me.";

                        (Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades = 3;
                        Main.LocalPlayer.inventory[Main.LocalPlayer.FindItem(ModContent.ItemType<MemoryFragmentIII>())].stack--;

                        return;
                    }
                    else if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 3 && Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentIV>()))
                    {
                        SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                        Main.npcChatText = "Genesis and Ragnarok now stronger and can Genesis can transform into a sword.\n" +
                            "There are still seals to break, if you find any more of those Memory Fragments then be sure to see me.";

                        (Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades = 4;
                        Main.LocalPlayer.inventory[Main.LocalPlayer.FindItem(ModContent.ItemType<MemoryFragmentIV>())].stack--;

                        return;
                    }
                    else if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 4 && Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentV>()))
                    {
                        SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                        Main.npcChatText = "Genesis and Ragnarok now stronger and their ice capabilities are revealed.\n" +
                            "That's all the memory seals released, I cannot upgrade them further.";

                        (Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades = 5;
                        Main.LocalPlayer.inventory[Main.LocalPlayer.FindItem(ModContent.ItemType<MemoryFragmentV>())].stack--;

                        return;
                    }
                }

                if (!Main.LocalPlayer.HasItem(ModContent.ItemType<GenesisAndRagnarok>()))
                {
                    if (!Main.LocalPlayer.HasItem(ModContent.ItemType<AreusWings>()))
                    {
                        Main.npcChatText = "Sorry pal, you don't have anything I can upgrade";
                        return;
                    }
                }
                else
                {
                    if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
                    {
                        if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 5)
                        {
                            Main.npcChatText = "I've already upgraded them to their full potential";

                            return;
                        }
                        else if (!Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentI>()) && !Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentII>())
                            && !Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentIII>()) && !Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentIV>())
                            && !Main.LocalPlayer.HasItem(ModContent.ItemType<MemoryFragmentV>()))
                        {
                            Main.npcChatText = "I need another Memory Fragment to upgrade the weapons.";
                            return;
                        }
                    }
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
                if (NPC.downedSlimeKing)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.SlimeCrown);
                    nextSlot++;
                }
                if (NPC.downedBoss1)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.SuspiciousLookingEye);
                    nextSlot++;
                }
                if (NPC.downedBoss2)
                {
                    if (WorldGen.crimson)
                        shop.item[nextSlot].SetDefaults(ItemID.BloodySpine);
                    else shop.item[nextSlot].SetDefaults(ItemID.WormFood);
                    nextSlot++;
                }
                if (NPC.downedQueenBee)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Abeemination);
                    nextSlot++;
                }
                if (SoADownedSystem.downedValkyrie)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ValkyrieCrest>());
                    nextSlot++;
                }
                if (NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ClothierVoodooDoll);
                    nextSlot++;
                }
                if (NPC.downedDeerclops)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.DeerThing);
                    nextSlot++;
                }
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.GuideVoodooDoll);
                    nextSlot++;
                }
                if (NPC.downedQueenSlime)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.QueenSlimeCrystal);
                    nextSlot++;
                }
                if (NPC.downedMechBoss1)
                {
                    if (ModLoader.TryGetMod("PrimeRework", out Mod foundMod))
                    {
                        shop.item[nextSlot].SetDefaults(foundMod.Find<ModItem>("WormRemote").Type);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(foundMod.Find<ModItem>("BrainRemote").Type);
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.MechanicalWorm);
                    }
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
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<PottedPlant>());
                    nextSlot++;
                }
                if (NPC.downedGolemBoss)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.LihzahrdPowerCell);
                    nextSlot++;
                }
                if (NPC.downedFishron)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.TruffleWorm);
                    nextSlot++;
                }
                if (NPC.downedEmpressOfLight)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.EmpressButterfly);
                    nextSlot++;
                }
                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.CelestialSigil);
                    nextSlot++;
                }
                if (SoADownedSystem.downedGenesis)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<SpiderClock>());
                    nextSlot++;
                }
                if (SoADownedSystem.downedSenterra)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusSnakeScale>());
                    nextSlot++;
                }
                if (SoADownedSystem.downedDeath)
                {
                    //shop.item[nextSlot].SetDefaults(ModContent.ItemType<AncientMedalion>());
                    nextSlot++;
                }
            }
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