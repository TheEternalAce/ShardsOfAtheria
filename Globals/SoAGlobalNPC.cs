using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.ItemDropRules.Conditions;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Consumable;
using ShardsOfAtheria.Items.GrabBags;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.PetItems;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Items.SinfulSouls.Extras;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok;
using ShardsOfAtheria.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalNPC : GlobalNPC
    {
        #region NPC Elements (for 1.0)
        public static List<int> MetalNPC = new();
        public static List<int> FireNPC = new();
        public static List<int> IceNPC = new();
        public static List<int> ElectricNPC = new();
        #endregion

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            Player player = Main.LocalPlayer;
            switch (type)
            {
                case NPCID.Merchant:
                    if (player.GetModPlayer<SlayerPlayer>().slayerMode)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<RepairKit_Lesser>());
                        nextSlot++;
                    }
                    break;
                case NPCID.TravellingMerchant:
                    // Sells during a Full Moon
                    if (Main.moonPhase == 0)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusShard>());
                        nextSlot++;
                    }
                    // Sells during a New Moon
                    if (Main.moonPhase == 4)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<BionicOreItem>());
                        nextSlot++;
                    }
                    break;
                case NPCID.ArmsDealer:
                    if (NPC.downedBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<AmmoBag>());
                        nextSlot++;
                    }
                    break;
                case NPCID.Wizard:
                    if (Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<SinfulSoul>());
                        shop.item[nextSlot].shopCustomPrice = 250000;
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<SinfulArmament>());
                        shop.item[nextSlot].shopCustomPrice = 250000;
                        nextSlot++;
                    }
                    break;
            }

            if (ModLoader.TryGetMod("AlchemistNPCLite", out Mod alchemistNPCLite))
            {
                if (type == alchemistNPCLite.Find<ModNPC>("Operator").Type)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<SoulOfDaylight>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<SoulOfSpite>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<SoulOfTwilight>());
                    nextSlot++;
                }
            }

            if (ModLoader.TryGetMod("Fargowiltas", out Mod mutantMod))
            {
                if (type == mutantMod.Find<ModNPC>("Mutant").Type && ShardsDownedSystem.downedValkyrie)
                {
                    //shop.item[nextSlot].SetDefaults(ModContent.ItemType<ValkyrieCrest>());
                    nextSlot++;
                }
            }
        }

        public override void OnKill(NPC npc)
        {
            if (Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight)
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfDaylight>());
            }
            if ((Main.eclipse || !Main.dayTime) && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight)
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfTwilight>());
            }
            if (Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneUnderworldHeight)
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfSpite>());
            }
            Player lastPlayerToHitThisNPC = npc.AnyInteractions() ? Main.player[npc.lastInteraction] : null;
            if (lastPlayerToHitThisNPC != null && lastPlayerToHitThisNPC.GetModPlayer<SlayerPlayer>().slayerMode)
            {
                int numPlayers = Main.CurrentFrameFlags.ActivePlayersCount;
                if (npc.type == NPCID.KingSlime)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainKing = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.KingSlimeMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.NinjaHood);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.NinjaShirt);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.NinjaPants);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SlimeGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SlimeHook);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RoyalGel);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.KingSlimePetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<KingSoulCrystal>());
                        //Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SlimedKatana>());

                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Solidifier, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.KingSlimeTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.KingSlimeMasterTrophy);
                }
                if (npc.type == NPCID.EyeofCthulhu)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainEOC = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Binoculars);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EoCShield);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeOfCthulhuPetItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.AviatorSunglasses);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<EyeSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Cataracnia>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrimtaneOre, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrimsonSeeds, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DemoniteOre, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CorruptSeeds, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeofCthulhuTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeofCthulhuMasterTrophy);
                }
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainBOC = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BoneRattle);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainOfConfusion);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainOfCthulhuPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<BrainSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<StrangeTissueSample>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<TomeOfOmniscience>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrimtaneOre, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TissueSample, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainofCthulhuTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainofCthulhuMasterTrophy);
                }
                if (npc.boss && Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainEOW = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        //NormalMode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EaterMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EatersBone);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WormScarf);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<WormBloom>());

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EaterOfWorldsPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<EaterSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<WormTench>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DemoniteOre, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ShadowScale, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EaterofWorldsTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EaterofWorldsMasterTrophy);
                }
                if (npc.type == NPCID.QueenBee)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainBee = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HiveWand);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeHat);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeShirt);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeePants);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HoneyedGoggles);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Nectar);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HoneyComb);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeKeeper);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeesKnees);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HiveBackpack);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Glock80>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<HecateII>());

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenBeePetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<BeeSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BottledHoney, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeWax, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Beenade, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenBeeTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenBeeMasterTrophy);
                }
                if (npc.type == NPCID.SkeletronHead)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainSkull = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BookofSkulls);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronHand);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BoneGlove);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SkullSoulCrystal>());
                        //Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronMasterTrophy);
                }
                if (npc.type == NPCID.Deerclops)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainDeerclops = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DeerclopsMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ChesterPetItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Eyebrella);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DontStarveShaderItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PewMaticHorn);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WeatherPain);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HoundiusShootius);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LucyTheAxe);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BoneHelm);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DeerclopsPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DeerclopsSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<ScreamLantern>());

                    }

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DeerclopsTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DeerclopsMasterTrophy);
                }
                if (npc.type == NPCID.WallofFlesh)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainWall = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FleshMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Pwnhammer);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RangerEmblem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WarriorEmblem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SorcererEmblem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SummonerEmblem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BreakerBlade);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ClockworkAssaultRifle);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LaserRifle);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BadgersHat);

                        // Expert Mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DemonHeart);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WallOfFleshGoatMountItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<WallSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FlailOfFlesh>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WallofFleshTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WallofFleshMasterTrophy);
                }
                if (npc.type == NPCID.QueenSlimeBoss)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainQueen = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrystalNinjaHelmet);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrystalNinjaChestplate);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrystalNinjaLeggings);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GelBalloon);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Smolstar);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeHook);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeMountSaddle);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.VolatileGelatin);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimePetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<QueenSoulCrystal>());
                        //Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<>());
                    }

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeMasterTrophy);
                }
                if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail && npc.boss)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainMechWorm = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DestroyerMask);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MechanicalWagonPiece);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DestroyerPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DestroyerSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Coilgun>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SoulofMight, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DestroyerTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DestroyerMasterTrophy);
                }
                if (npc.boss && Array.IndexOf(new int[] { NPCID.Spazmatism, NPCID.Retinazer }, npc.type) > -1)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainTwins = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TwinMask);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MechanicalWheelPiece);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TwinsPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<TwinsSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DoubleBow>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SoulofSight, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RetinazerTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SpazmatismTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TwinsMasterTrophy);
                }
                if (npc.type == NPCID.SkeletronPrime)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPrime = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPrimeMask);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MechanicalBatteryPiece);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPrimePetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<PrimeSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<HandCanon>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SoulofFright, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPrimeTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPrimeMasterTrophy);
                }
                if (npc.type == NPCID.Plantera)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPlant = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PlanteraMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TempleKey);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Seedling);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GrenadeLauncher);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.VenusMagnum);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.NettleBurst);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LeafBlower);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FlowerPow);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WaspGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Seedler);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PygmyStaff);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ThornHook);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TheAxe);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SporeSac);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PlanteraPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<PlantSoulCrystal>());
                        //Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PlanteraTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PlanteraMasterTrophy);
                }
                if (npc.type == NPCID.Golem)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainGolem = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Picksaw);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Stynger);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SunStone);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeoftheGolem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HeatRay);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.StaffofEarth);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemFist);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PossessedHatchet);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ShinyStone);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<GolemSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeetleHusk, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.StyngerBolt, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemMasterTrophy);
                }
                if (npc.type == NPCID.DukeFishron)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainDuke = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BubbleGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Flairon);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RazorbladeTyphoon);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TempestStaff);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Tsunami);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FishronWings);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DukeFishronMask);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DukeFishronPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DukeSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DukeFishronTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DukeFishronMasterTrophy);
                }
                if (npc.type == NPCID.HallowBoss)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainEmpress = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenMagicItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PiercingStarlight);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RainbowWhip);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenRangedItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RainbowWings);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SparkleGuitar);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RainbowCursor);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EmpressBlade);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EmpressFlightBooster);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<EmpressSoulCrystal>());
                        //Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HallowBossDye, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenMasterTrophy);
                }
                if (npc.type == NPCID.CultistBoss)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainLunatic = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BossMaskCultist);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LunaticCultistPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<LunaticSoulCrystal>());
                        //Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<>());
                    }
                    if (ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        ModContent.GetInstance<ShardsDownedSystem>().slainPillarNebula = true;
                        ModContent.GetInstance<ShardsDownedSystem>().slainPillarSolar = true;
                        ModContent.GetInstance<ShardsDownedSystem>().slainPillarStardust = true;
                        ModContent.GetInstance<ShardsDownedSystem>().slainPillarVortex = true;
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentNebula, 1000);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentSolar, 1000);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentStardust, 1000);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentVortex, 1000);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 1000);
                    }

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.AncientCultistTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LunaticCultistMasterTrophy);
                }
                if (npc.type == NPCID.LunarTowerNebula)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPillarNebula = true;
                    if (!ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentNebula, 1000);
                        for (int i = 0; i < 10; i++)
                        {
                            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 25);
                        }
                    }
                }
                if (npc.type == NPCID.LunarTowerSolar)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPillarSolar = true;
                    if (!ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentSolar, 1000);
                        for (int i = 0; i < 10; i++)
                        {
                            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 25);
                        }
                    }
                }
                if (npc.type == NPCID.LunarTowerStardust)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPillarStardust = true;
                    if (!ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentStardust, 1000);
                        for (int i = 0; i < 10; i++)
                        {
                            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 25);
                        }
                    }
                }
                if (npc.type == NPCID.LunarTowerVortex)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPillarVortex = true;
                    if (!ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentVortex, 1000);
                        for (int i = 0; i < 10; i++)
                        {
                            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 25);
                        }
                    }
                }
                if (npc.type == NPCID.MoonLordCore)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainMoonLord = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BossMaskMoonlord);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PortalGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Meowmere);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Terrarian);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.StarWrath);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SDMG);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LastPrism);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LunarFlareBook);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RainbowCrystalStaff);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MoonlordTurretStaff);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FireworksLauncher);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Celeb2);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MeowmereMinecart);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GravityGlobe);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MoonLordPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<LordSoulCrystal>());
                        //Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LunarOre, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MoonLordTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MoonLordMasterTrophy);
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule notHardmode = new LeadingConditionRule(new Conditions.IsPreHardmode());
            LeadingConditionRule firstTimeKillingPlantera = new LeadingConditionRule(new Conditions.FirstTimeKillingPlantera());
            LeadingConditionRule downedGolem = new LeadingConditionRule(new DownedGolem());
            LeadingConditionRule downedCultist = new LeadingConditionRule(new DownedLunaticCultist());
            LeadingConditionRule downedMoonLord = new LeadingConditionRule(new DownedMoonLord());
            if (npc.type == NPCID.Mothron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenHeroGun>(), 4));
            }
            if (npc.type == NPCID.MartianSaucerCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ReactorMeltdown>(), 4));
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                notHardmode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(notHardmode);
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SinfulSoul>()));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SinfulArmament>()));
            }
            if (npc.type == NPCID.Plantera)
            {
                firstTimeKillingPlantera.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(firstTimeKillingPlantera);
            }
            if (npc.type == NPCID.Golem)
            {
                downedGolem.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(downedGolem);
            }
            if (npc.type == NPCID.CultistBoss)
            {
                downedCultist.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(downedCultist);
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                downedMoonLord.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(downedMoonLord);
            }
        }

        public override bool PreAI(NPC npc)
        {
            if (npc.type == NPCID.KingSlime && ModContent.GetInstance<ShardsDownedSystem>().slainKing)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("King Slime was slain..."), Color.White);
                npc.active = false;
            }
            if (npc.type == NPCID.EyeofCthulhu && ModContent.GetInstance<ShardsDownedSystem>().slainEOC)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Eye of Cthulhu was slain..."), Color.White);
                npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainBOC)
            {
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Brain of Cthulhu was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.Creeper && ModContent.GetInstance<ShardsDownedSystem>().slainBOC)
                    npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainEOW)
            {
                if (npc.type == NPCID.EaterofWorldsHead)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Eater of Worlds was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
                    npc.active = false;
            }
            if (npc.type == NPCID.QueenBee && ModContent.GetInstance<ShardsDownedSystem>().slainBee)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Queen Bee was slain..."), Color.White);
                npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainSkull)
            {
                if (npc.type == NPCID.SkeletronHead || npc.type == NPCID.DungeonGuardian)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Skeletron was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.SkeletronHand)
                    npc.active = false;
            }
            if (npc.type == NPCID.Deerclops && ModContent.GetInstance<ShardsDownedSystem>().slainDeerclops)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Deerclops was slain..."), Color.White);
                npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainWall)
            {
                if (npc.type == NPCID.WallofFlesh)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("the Wall of Flesh was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.WallofFleshEye)
                    npc.active = false;
            }
            if (npc.type == NPCID.QueenSlimeBoss && ModContent.GetInstance<ShardsDownedSystem>().slainQueen)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Queen Slime was slain..."), Color.White);
                npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainMechWorm)
            {
                if (npc.type == NPCID.TheDestroyer)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Destroyer was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail)
                    npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainTwins)
            {
                if (npc.type == NPCID.Spazmatism)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Spazmatism was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.Retinazer)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Retinazer was slain..."), Color.White);
                    npc.active = false;
                }
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainPrime)
            {
                if (npc.type == NPCID.SkeletronPrime)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Skeletron Prime was slain... (Again, how???)"), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeLaser || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice)
                    npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainPlant)
            {
                if (npc.type == NPCID.Plantera)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Plantera was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.PlanterasHook || npc.type == NPCID.PlanterasTentacle)
                    npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainGolem)
            {
                if (npc.type == NPCID.Golem)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Golem was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
                    npc.active = false;
            }
            if (npc.type == NPCID.DukeFishron && ModContent.GetInstance<ShardsDownedSystem>().slainDuke)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Duke Fishron was slain..."), Color.White);
                npc.active = false;
            }
            if (npc.type == NPCID.HallowBoss && ModContent.GetInstance<ShardsDownedSystem>().slainEmpress)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Empress of Light was slain..."), Color.White);
                npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainLunatic)
            {
                if (npc.type == NPCID.CultistBoss)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Lunatic Cultist was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.CultistTablet)
                {
                    npc.active = false;
                }
                if (npc.type == NPCID.CultistArcherBlue)
                {
                    npc.active = false;
                }
                if (npc.type == NPCID.CultistArcherWhite)
                {
                    npc.active = false;
                }
                if (npc.type == NPCID.CultistDevote)
                {
                    npc.active = false;
                }
            }
            if (npc.type == NPCID.LunarTowerNebula && ModContent.GetInstance<ShardsDownedSystem>().slainPillarNebula)
            {
                npc.active = false;
            }
            if (npc.type == NPCID.LunarTowerSolar && ModContent.GetInstance<ShardsDownedSystem>().slainPillarSolar)
            {
                npc.active = false;
            }
            if (npc.type == NPCID.LunarTowerStardust && ModContent.GetInstance<ShardsDownedSystem>().slainPillarStardust)
            {
                npc.active = false;
            }
            if (npc.type == NPCID.LunarTowerVortex && ModContent.GetInstance<ShardsDownedSystem>().slainPillarVortex)
            {
                npc.active = false;
            }
            if (ModContent.GetInstance<ShardsDownedSystem>().slainMoonLord)
            {
                if (npc.type == NPCID.MoonLordCore)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Moon Lord was slain..."), Color.White);
                    npc.active = false;
                }
                if (npc.type == NPCID.MoonLordHand || npc.type == NPCID.MoonLordHead)
                    npc.active = false;
            }
            return base.PreAI(npc);
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (target.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] > 0)
                npc.AddBuff(BuffID.OnFire, 1200);
            if (target.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(ModContent.ItemType<ValkyrieSoulCrystal>()))
            {
                npc.AddBuff(ModContent.BuffType<ElectricShock>(), 300);
            }
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (npc.HasBuff(ModContent.BuffType<Marked>()))
                damage += .1f;
            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<Marked>()))
                drawColor = Color.MediumPurple;
        }
    }
}