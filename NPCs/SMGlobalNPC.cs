using SagesMania.Buffs;
using SagesMania.Items;
using SagesMania.Items.Accessories;
using SagesMania.Items.Placeable;
using SagesMania.Items.SlayerItems;
using SagesMania.Items.Weapons.Magic;
using SagesMania.Items.Weapons.Melee;
using SagesMania.Items.Weapons.Ranged;
using SagesMania.NPCs.NovaSkyloft;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs
{
	public class SMGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool infected;
        public bool zenovaJavelin;

        public override void ResetEffects(NPC npc)
        {
            infected = false;
            zenovaJavelin = false;
        }

        public override void SetDefaults(NPC npc)
        {
            Player player = Main.LocalPlayer;
            if (ModContent.GetInstance<SMWorld>().slayerMode)
                npc.damage = player.statLifeMax2 / 10;
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
			if (type == NPCID.ArmsDealer)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<CO2Cartridge>());
				nextSlot++;
            }
            Player player = Main.LocalPlayer;
            if (type == NPCID.Merchant && player.HasItem(ModContent.ItemType<SolarStorm>()))
            {
                shop.item[nextSlot].SetDefaults(ItemID.Flare);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BlueFlare);
                nextSlot++;
            }
            if (type == NPCID.TravellingMerchant)
            {
                if (Main.rand.NextFloat() < .05f)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusOreItem>());
                    nextSlot++;
                }
                if (Main.rand.NextFloat() < .05f)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<BionicOreItem>());
                    nextSlot++;
                }
            }
        }

        public override void NPCLoot(NPC npc)
        {
			if (npc.type == NPCID.Mothron && Main.rand.NextFloat() < 0.25f)
			{
				Item.NewItem(npc.getRect(), ModContent.ItemType<BrokenHeroGun>());
            }
            if (npc.type == NPCID.MartianSaucerCore && Main.rand.NextFloat() < 0.25f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<ReactorMeltdown>());
            }
            if (Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson && Main.eclipse))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfDaylight>());
            }
            if (!Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson && Main.eclipse))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfStarlight>());
            }
            if (Main.eclipse && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfStarlight>());
            }
            if (Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneUnderworldHeight && !(Main.LocalPlayer.ZoneHoly))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfSpite>());
            }
            if (ModContent.GetInstance<SMWorld>().slayerMode)
            {
                if (npc.type == NPCID.EyeofCthulhu)
                {
                    ModContent.GetInstance<SMWorld>().slainEOC = true;
                    Item.NewItem(npc.getRect(), ItemID.EoCShield);
                    Item.NewItem(npc.getRect(), ItemID.EyeofCthulhuTrophy);
                    Item.NewItem(npc.getRect(), ItemID.EyeMask);
                    if (WorldGen.crimson)
                    {
                        Item.NewItem(npc.getRect(), ItemID.CrimtaneOre, 4000);
                        Item.NewItem(npc.getRect(), ItemID.CrimsonSeeds, 4000);
                    }
                    else
                    {
                        Item.NewItem(npc.getRect(), ItemID.DemoniteOre, 4000);
                        Item.NewItem(npc.getRect(), ItemID.CorruptSeeds, 4000);
                    }
                    Item.NewItem(npc.getRect(), ItemID.Binoculars);
                    Item.NewItem(npc.getRect(), ItemID.AviatorSunglasses);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<EyeOfTheAllSeer>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Cataracnia>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    ModContent.GetInstance<SMWorld>().slainBOC = true;
                    Item.NewItem(npc.getRect(), ItemID.BrainOfConfusion);
                    Item.NewItem(npc.getRect(), ItemID.BrainMask);
                    Item.NewItem(npc.getRect(), ItemID.BrainofCthulhuTrophy);
                    Item.NewItem(npc.getRect(), ItemID.BoneRattle);
                    Item.NewItem(npc.getRect(), ItemID.CrimtaneOre, 4000);
                    Item.NewItem(npc.getRect(), ItemID.TissueSample, 4000);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<TomeOfOmniscience>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<StrangeTissueSample>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.boss && System.Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
                {
                    ModContent.GetInstance<SMWorld>().slainEOW = true;
                    Item.NewItem(npc.getRect(), ItemID.WormScarf);
                    Item.NewItem(npc.getRect(), ItemID.EaterofWorldsTrophy);
                    Item.NewItem(npc.getRect(), ItemID.EaterMask);
                    Item.NewItem(npc.getRect(), ItemID.EatersBone);
                    Item.NewItem(npc.getRect(), ItemID.DemoniteOre, 4000);
                    Item.NewItem(npc.getRect(), ItemID.ShadowScale, 4000);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<WormTench>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<OversizedWormsTooth>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.QueenBee)
                {
                    ModContent.GetInstance<SMWorld>().slainBee = true;
                    Item.NewItem(npc.getRect(), ItemID.HiveBackpack);
                    Item.NewItem(npc.getRect(), ItemID.BeeMask);
                    Item.NewItem(npc.getRect(), ItemID.QueenBeeTrophy);
                    Item.NewItem(npc.getRect(), ItemID.BottledHoney, 4000);
                    Item.NewItem(npc.getRect(), ItemID.BeeWax, 4000);
                    Item.NewItem(npc.getRect(), ItemID.Beenade, 4000);
                    Item.NewItem(npc.getRect(), ItemID.HiveWand);
                    Item.NewItem(npc.getRect(), ItemID.BeeHat);
                    Item.NewItem(npc.getRect(), ItemID.BeeShirt);
                    Item.NewItem(npc.getRect(), ItemID.BeePants);
                    Item.NewItem(npc.getRect(), ItemID.HoneyedGoggles);
                    Item.NewItem(npc.getRect(), ItemID.Nectar);
                    Item.NewItem(npc.getRect(), ItemID.HoneyComb);
                    Item.NewItem(npc.getRect(), ItemID.BeeGun);
                    Item.NewItem(npc.getRect(), ItemID.BeeKeeper);
                    Item.NewItem(npc.getRect(), ItemID.BeesKnees);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<LCAR9>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<HiddenBlade>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<DemonClaw>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<HecateII>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<ShadowBrand>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<MarkOfAnastasia>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<HoneyCrown>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.SkeletronHead)
                {
                    ModContent.GetInstance<SMWorld>().slainSkull = true;
                    Item.NewItem(npc.getRect(), ItemID.BoneGlove);
                    Item.NewItem(npc.getRect(), ItemID.SkeletronTrophy);
                    Item.NewItem(npc.getRect(), ItemID.SkeletronMask);
                    Item.NewItem(npc.getRect(), ItemID.BookofSkulls);
                    Item.NewItem(npc.getRect(), ItemID.SkeletronHand);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<VampiricJaw>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.WallofFlesh)
                {
                    ModContent.GetInstance<SMWorld>().slainWall = true;
                    Item.NewItem(npc.getRect(), ItemID.DemonHeart);
                    Item.NewItem(npc.getRect(), ItemID.WallofFleshTrophy);
                    Item.NewItem(npc.getRect(), ItemID.FleshMask);
                    Item.NewItem(npc.getRect(), ItemID.Pwnhammer);
                    Item.NewItem(npc.getRect(), ItemID.RangerEmblem);
                    Item.NewItem(npc.getRect(), ItemID.WarriorEmblem);
                    Item.NewItem(npc.getRect(), ItemID.SorcererEmblem);
                    Item.NewItem(npc.getRect(), ItemID.SummonerEmblem);
                    Item.NewItem(npc.getRect(), ItemID.BreakerBlade);
                    Item.NewItem(npc.getRect(), ItemID.ClockworkAssaultRifle);
                    Item.NewItem(npc.getRect(), ItemID.LaserRifle);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<FlailOfFlesh>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.boss && System.Array.IndexOf(new int[] { NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail }, npc.type) > -1)
                {
                    ModContent.GetInstance<SMWorld>().slainMechWorm = true;
                    Item.NewItem(npc.getRect(), ItemID.MechanicalWagonPiece);
                    Item.NewItem(npc.getRect(), ItemID.DestroyerTrophy);
                    Item.NewItem(npc.getRect(), ItemID.DestroyerMask);
                    Item.NewItem(npc.getRect(), ItemID.SoulofMight, 4000);
                    Item.NewItem(npc.getRect(), ItemID.HallowedBar, 1000);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Coilgun>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.boss && System.Array.IndexOf(new int[] { NPCID.Spazmatism, NPCID.Retinazer }, npc.type) > -1)
                {
                    ModContent.GetInstance<SMWorld>().slainTwins = true;
                    Item.NewItem(npc.getRect(), ItemID.MechanicalWheelPiece);
                    Item.NewItem(npc.getRect(), ItemID.RetinazerTrophy);
                    Item.NewItem(npc.getRect(), ItemID.SpazmatismTrophy);
                    Item.NewItem(npc.getRect(), ItemID.TwinMask);
                    Item.NewItem(npc.getRect(), ItemID.SoulofSight, 4000);
                    Item.NewItem(npc.getRect(), ItemID.HallowedBar, 2000);
                    //Item.NewItem(npc.getRect(), ModContent.ItemType<>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.SkeletronPrime)
                {
                    ModContent.GetInstance<SMWorld>().slainPrime = true;
                    Item.NewItem(npc.getRect(), ItemID.MechanicalBatteryPiece);
                    Item.NewItem(npc.getRect(), ItemID.SkeletronPrimeTrophy);
                    Item.NewItem(npc.getRect(), ItemID.SkeletronPrimeMask);
                    Item.NewItem(npc.getRect(), ItemID.SoulofFright, 4000);
                    Item.NewItem(npc.getRect(), ItemID.HallowedBar, 1000);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<BlasterCannonBlueprints>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.Plantera)
                {
                    ModContent.GetInstance<SMWorld>().slainPlant = true;
                    Item.NewItem(npc.getRect(), ItemID.SporeSac);
                    Item.NewItem(npc.getRect(), ItemID.PlanteraTrophy);
                    Item.NewItem(npc.getRect(), ItemID.PlanteraMask);
                    Item.NewItem(npc.getRect(), ItemID.TempleKey);
                    Item.NewItem(npc.getRect(), ItemID.Seedling);
                    Item.NewItem(npc.getRect(), ItemID.GrenadeLauncher);
                    Item.NewItem(npc.getRect(), ItemID.VenusMagnum);
                    Item.NewItem(npc.getRect(), ItemID.NettleBurst);
                    Item.NewItem(npc.getRect(), ItemID.LeafBlower);
                    Item.NewItem(npc.getRect(), ItemID.FlowerPow);
                    Item.NewItem(npc.getRect(), ItemID.WaspGun);
                    Item.NewItem(npc.getRect(), ItemID.Seedler);
                    Item.NewItem(npc.getRect(), ItemID.PygmyStaff);
                    Item.NewItem(npc.getRect(), ItemID.ThornHook);
                    Item.NewItem(npc.getRect(), ItemID.TheAxe);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PlantCells>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.Golem)
                {
                    ModContent.GetInstance<SMWorld>().slainGolem = true;
                    Item.NewItem(npc.getRect(), ItemID.ShinyStone);
                    Item.NewItem(npc.getRect(), ItemID.GolemTrophy);
                    Item.NewItem(npc.getRect(), ItemID.GolemMask);
                    Item.NewItem(npc.getRect(), ItemID.BeetleHusk, 4000);
                    Item.NewItem(npc.getRect(), ItemID.Picksaw);
                    Item.NewItem(npc.getRect(), ItemID.Stynger);
                    Item.NewItem(npc.getRect(), ItemID.StyngerBolt, 4000);
                    Item.NewItem(npc.getRect(), ItemID.PossessedHatchet);
                    Item.NewItem(npc.getRect(), ItemID.SunStone);
                    Item.NewItem(npc.getRect(), ItemID.EyeoftheGolem);
                    Item.NewItem(npc.getRect(), ItemID.HeatRay);
                    Item.NewItem(npc.getRect(), ItemID.StaffofEarth);
                    Item.NewItem(npc.getRect(), ItemID.GolemFist);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SolarStorm>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.DukeFishron)
                {
                    ModContent.GetInstance<SMWorld>().slainDuke = true;
                    Item.NewItem(npc.getRect(), ItemID.DukeFishronTrophy);
                    Item.NewItem(npc.getRect(), ItemID.DukeFishronMask);
                    Item.NewItem(npc.getRect(), ItemID.BubbleGun);
                    Item.NewItem(npc.getRect(), ItemID.Flairon);
                    Item.NewItem(npc.getRect(), ItemID.RazorbladeTyphoon);
                    Item.NewItem(npc.getRect(), ItemID.TempestStaff);
                    Item.NewItem(npc.getRect(), ItemID.Tsunami);
                    Item.NewItem(npc.getRect(), ItemID.FishronWings);
                    //Item.NewItem(npc.getRect(), ModContent.ItemType<>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                /*
                if (npc.tupe == NPCID.EmpressofLight)
                {
                    ModContent.GetInstance<SMWorld>().slainEmpress = true;
                    Item.NewItem(npc.getRect(), ItemID.insignia);
                    Item.NewItem(npc.getRect(), ItemID.EoLTrophy);
                    Item.NewItem(npc.getRect(), ItemID.EoLMask);
                    Item.NewItem(npc.getRect(), ItemID.nightglow);
                    Item.NewItem(npc.getRect(), ItemID.Starlight);
                    Item.NewItem(npc.getRect(), ItemID.Kaleidoscope);
                    Item.NewItem(npc.getRect(), ItemID.Eventide);
                    Item.NewItem(npc.getRect(), ItemID.EoLWings);
                    Item.NewItem(npc.getRect(), ItemID.prismaticDye);
                    Item.NewItem(npc.getRect(), ItemID.StellarTune);
                    Item.NewItem(npc.getRect(), ItemID.RainbowCursor);
                    Item.NewItem(npc.getRect(), ItemID.Terraprisma);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PrismaticOverloader>());
                }
                */
                if (npc.type == NPCID.MoonLordCore)
                {
                    ModContent.GetInstance<SMWorld>().slainMoonLord = true;
                    Item.NewItem(npc.getRect(), ItemID.GravityGlobe);
                    Item.NewItem(npc.getRect(), ItemID.MoonLordTrophy);
                    Item.NewItem(npc.getRect(), ItemID.BossMaskMoonlord);
                    Item.NewItem(npc.getRect(), ItemID.PortalGun);
                    Item.NewItem(npc.getRect(), ItemID.LunarOre, 4000);
                    Item.NewItem(npc.getRect(), ItemID.Meowmere);
                    Item.NewItem(npc.getRect(), ItemID.Terrarian);
                    Item.NewItem(npc.getRect(), ItemID.StarWrath);
                    Item.NewItem(npc.getRect(), ItemID.SDMG);
                    Item.NewItem(npc.getRect(), ItemID.LastPrism);
                    Item.NewItem(npc.getRect(), ItemID.LunarFlareBook);
                    Item.NewItem(npc.getRect(), ItemID.RainbowCrystalStaff);
                    Item.NewItem(npc.getRect(), ItemID.MoonlordTurretStaff);
                    Item.NewItem(npc.getRect(), 3546);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<MoonCore>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
            }
            if (npc.type == NPCID.EyeofCthulhu || npc.type == NPCID.BrainofCthulhu || /*npc.type == NPCID.QueenSlime ||*/ npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism || npc.type == NPCID.Plantera || npc.type == NPCID.Golem || /*npc.type == NPCID.EmpressofLight ||*/ npc.type == NPCID.DukeFishron || npc.type == NPCID.MoonLordCore)
                Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), Main.rand.Next(5, 7));
        }

        public override bool PreAI(NPC npc)
        {
            if ((npc.type == ModContent.NPCType<HarpyKnight>() || npc.type == ModContent.NPCType<HarpyKnight>()) && ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                Main.NewText("Nova Skyloft, the Harpy Knight was slain...");
                npc.active = false;
            }
            if (npc.type == NPCID.EyeofCthulhu && ModContent.GetInstance<SMWorld>().slainEOC)
            {
                Main.NewText("The Eye of Cthulhu was slain...");
                npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainBOC)
            {
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    Main.NewText("The Brain of Cthulhu was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.Creeper && ModContent.GetInstance<SMWorld>().slainBOC)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainEOW)
            {
                if (npc.type == NPCID.EaterofWorldsHead)
                {
                    Main.NewText("The Eater of Worlds was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
                    npc.active = false;
            }
            if (npc.type == NPCID.QueenBee && ModContent.GetInstance<SMWorld>().slainBee)
            {
                Main.NewText("The Queen Bee was slain...");
                npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainSkull)
            {
                if (npc.type == NPCID.SkeletronHead || npc.type == NPCID.DungeonGuardian)
                {
                    Main.NewText("Skeletron was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.SkeletronHand)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainWall)
            {
                if (npc.type == NPCID.WallofFlesh || npc.type == NPCID.DungeonGuardian)
                {
                    Main.NewText("Skeletron was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.WallofFleshEye)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainMechWorm)
            {
                if (npc.type == NPCID.TheDestroyer)
                {
                    Main.NewText("The Destroyer was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainTwins)
            {
                if (npc.type == NPCID.Spazmatism)
                {
                    Main.NewText("The Twins were slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.Retinazer)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainPrime)
            {
                if (npc.type == NPCID.SkeletronPrime)
                {
                    Main.NewText("Skeletron Prime was slain... (Again)");
                    npc.active = false;
                }
                if (npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeLaser || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainPlant)
            {
                if (npc.type == NPCID.Plantera)
                {
                    Main.NewText("Plantera was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.PlanterasHook || npc.type == NPCID.PlanterasTentacle)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SMWorld>().slainGolem)
            {
                if (npc.type == NPCID.Golem)
                {
                    Main.NewText("Golem was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
                    npc.active = false;
            }
            if (npc.type == NPCID.DukeFishron && ModContent.GetInstance<SMWorld>().slainDuke)
            {
                Main.NewText("Duke Fishron was slain...");
                npc.active = false;
            }
            /*
            if (ModContent.GetInstance<SMWorld>().slainEmpress && npc.type == EoL)
            {
                Main.NewText("The Empress of Light was slain...");
                npc.active = false;
            }
            */
            if (ModContent.GetInstance<SMWorld>().slainMoonLord)
            {
                if (npc.type == NPCID.MoonLordCore)
                {
                    Main.NewText("The Moon Lord was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.MoonLordHand || npc.type == NPCID.MoonLordHead)
                    npc.active = false;
            }
            return base.PreAI(npc);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (infected)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 10;
            }
            if (zenovaJavelin)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 100;
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        chat = "Huh? Who are you?!";
                        break;
                    case 1:
                        chat = "You better back off maverick!";
                        break;
                    default:
                        chat = "Get away from me, i'm not doing any business with you.";
                        break;
                }
            }
        }

        public override bool PreChatButtonClicked(NPC npc, bool firstButton)
        {
            return !Main.LocalPlayer.HasBuff(ModContent.BuffType<Megamerged>());
        }
    }
}