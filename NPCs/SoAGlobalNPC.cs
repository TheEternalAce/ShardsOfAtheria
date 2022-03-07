using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
	public class SoAGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
            Player player = Main.LocalPlayer;
            if (ModContent.GetInstance<SoAWorld>().slayerMode)
                npc.damage = player.statLifeMax2 / 10;
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
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

        public override void OnKill(NPC npc)
        {
            if (Main.rand.Next(4) == 0)
                Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<UnanalyzedMicrobe>(), Main.rand.Next(1, 20));
            if (npc.HasBuff(ModContent.BuffType<BasicBacterialInfectionI>()) || npc.HasBuff(ModContent.BuffType<BasicBacterialInfectionII>()) || npc.HasBuff(ModContent.BuffType<BasicBacterialInfectionIII>()))
                Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<Bacteria>(), Main.rand.Next(1, 20));
            if (npc.HasBuff(ModContent.BuffType<BasicViralInfectionI>()) || npc.HasBuff(ModContent.BuffType<BasicViralInfectionII>()) || npc.HasBuff(ModContent.BuffType<BasicViralInfectionIII>()))
                Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<Virus>(), Main.rand.Next(1, 20));
            if (ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                if (npc.type == NPCID.EyeofCthulhu)
                {
                    ModContent.GetInstance<SoAWorld>().slainEOC = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.EoCShield);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.EyeofCthulhuTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.EyeMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.CrimtaneOre, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.CrimsonSeeds, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.DemoniteOre, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.CorruptSeeds, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Binoculars);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.AviatorSunglasses);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<EyeOfTheAllSeer>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<Cataracnia>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4924, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4798); //Suspicious Grinning Eye
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    ModContent.GetInstance<SoAWorld>().slainBOC = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BrainOfConfusion, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BrainMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BrainofCthulhuTrophy);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BoneRattle);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.CrimtaneOre, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.TissueSample, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<TomeOfOmniscience>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<StrangeTissueSample>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4926, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4800); //Brain in a Jar
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.boss && System.Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
                {
                    ModContent.GetInstance<SoAWorld>().slainEOW = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.WormScarf);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.EaterofWorldsTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.EaterMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.EatersBone);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.DemoniteOre, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.ShadowScale, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<WormTench>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<OversizedWormsTooth>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4925, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4799); //Writhing Remains
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.QueenBee)
                {
                    ModContent.GetInstance<SoAWorld>().slainBee = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.HiveBackpack);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeeMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.QueenBeeTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BottledHoney, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeeWax, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Beenade, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.HiveWand);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeeHat);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeeShirt);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeePants);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.HoneyedGoggles);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Nectar);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.HoneyComb);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeeGun);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeeKeeper);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeesKnees);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<Glock80>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<HiddenWristBlade>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<DemonClaw>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<HecateII>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<ShadowBrand>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<MarkOfAnastasia>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<HoneyCrown>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4928, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4802); //Sparkling Honey
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.SkeletronHead)
                {
                    ModContent.GetInstance<SoAWorld>().slainSkull = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BoneGlove);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SkeletronTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SkeletronMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BookofSkulls);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SkeletronHand);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<VampiricJaw>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4927, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4801); //Possessed Skull
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.WallofFlesh)
                {
                    ModContent.GetInstance<SoAWorld>().slainWall = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.DemonHeart);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.WallofFleshTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.FleshMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Pwnhammer);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.RangerEmblem);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.WarriorEmblem);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SorcererEmblem);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SummonerEmblem);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BreakerBlade);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.ClockworkAssaultRifle);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.LaserRifle);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<FlailOfFlesh>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4930, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4795); //Goat Skull
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.boss && System.Array.IndexOf(new int[] { NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail }, npc.type) > -1)
                {
                    ModContent.GetInstance<SoAWorld>().slainMechWorm = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.MechanicalWagonPiece);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.DestroyerTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.DestroyerMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SoulofMight, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<Coilgun>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4932, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4803); //Deactivated Probe
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.boss && System.Array.IndexOf(new int[] { NPCID.Spazmatism, NPCID.Retinazer }, npc.type) > -1)
                {
                    ModContent.GetInstance<SoAWorld>().slainTwins = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.MechanicalWheelPiece);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.RetinazerTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SpazmatismTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.TwinMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SoulofSight, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<DoubleBow>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4931, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4804); //Pair of Eyeballs
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.SkeletronPrime)
                {
                    ModContent.GetInstance<SoAWorld>().slainPrime = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.MechanicalBatteryPiece);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SkeletronPrimeTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SkeletronPrimeMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SoulofFright, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<BlasterCannonBlueprints>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4933, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4805); //Robotic Skull
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.Plantera)
                {
                    ModContent.GetInstance<SoAWorld>().slainPlant = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SporeSac);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.PlanteraTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.PlanteraMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.TempleKey);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Seedling);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.GrenadeLauncher);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.VenusMagnum);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.NettleBurst);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.LeafBlower);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.FlowerPow);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.WaspGun);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Seedler);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.PygmyStaff);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.ThornHook);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.TheAxe);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PlantCells>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4934, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4806); //Plantera Seedling
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.Golem)
                {
                    ModContent.GetInstance<SoAWorld>().slainGolem = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.ShinyStone);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.GolemTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.GolemMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BeetleHusk, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Picksaw);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Stynger);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.StyngerBolt, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.PossessedHatchet);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SunStone);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.EyeoftheGolem);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.HeatRay);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.StaffofEarth);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.GolemFist);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<SolarStorm>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4935, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4807); //Guardian Golem
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.DukeFishron)
                {
                    ModContent.GetInstance<SoAWorld>().slainDuke = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.DukeFishronTrophy, 999);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.DukeFishronMask);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BubbleGun);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Flairon);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.RazorbladeTyphoon);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.TempestStaff);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Tsunami);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.FishronWings);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<HolyMackerel>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4936, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4808); //Pork of the Sea
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == 636)
                {
                    ModContent.GetInstance<SoAWorld>().slainEmpress = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4989); //Soaring Insignia
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4783, 999); //Trophy
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4784); //Mask
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4952); //Nightglow
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4923); //Starlight
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4914); //Kaleidoscope
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4953); //Eventide
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4823); //Empress Wings
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4778, 999); //Prismatic Dye
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4715); //Stellar Tune
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 5075); //Rainbow Cursor
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 5005); //Terraprisma
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4949, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4811); //Jewel of Light
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PrismaticOverloader>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
                if (npc.type == NPCID.MoonLordCore)
                {
                    ModContent.GetInstance<SoAWorld>().slainMoonLord = true;
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.GravityGlobe);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.MoonLordTrophy);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.BossMaskMoonlord);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.PortalGun);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.LunarOre, 4000);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Meowmere);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.Terrarian);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.StarWrath);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.SDMG);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.LastPrism);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.LunarFlareBook);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.RainbowCrystalStaff);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ItemID.MoonlordTurretStaff);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 3546);
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<MoonCore>());
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4938, 999); //Relic
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), 4810); //Piece of Moon Squid
                    Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
                }
            }
            if (Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson))
            {
                Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfDaylight>());
            }
            if (!Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson))
            {
                Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfStarlight>());
            }
            if (Main.eclipse && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson))
            {
                Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfStarlight>());
            }
            if (Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneUnderworldHeight && !(Main.LocalPlayer.ZoneHallow))
            {
                Item.NewItem(npc.GetItemSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfSpite>());
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Mothron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenHeroGun>(), 4));
            }
            if (npc.type == NPCID.MartianSaucerCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ReactorMeltdown>(), 4));
            }
            if (npc.type == NPCID.EyeofCthulhu || npc.type == NPCID.BrainofCthulhu || npc.type == 657 || npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism
                || npc.type == NPCID.Plantera || npc.type == NPCID.Golem || npc.type == 636 || npc.type == NPCID.DukeFishron || npc.type == NPCID.MoonLordCore)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PhaseOreItem>(), 1, 5, 7));
        }

        public override bool PreAI(NPC npc)
        {
            if (npc.type == NPCID.EyeofCthulhu && ModContent.GetInstance<SoAWorld>().slainEOC)
            {
                Main.NewText("The Eye of Cthulhu was slain...");
                npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainBOC)
            {
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    Main.NewText("The Brain of Cthulhu was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.Creeper && ModContent.GetInstance<SoAWorld>().slainBOC)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainEOW)
            {
                if (npc.type == NPCID.EaterofWorldsHead)
                {
                    Main.NewText("The Eater of Worlds was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
                    npc.active = false;
            }
            if (npc.type == NPCID.QueenBee && ModContent.GetInstance<SoAWorld>().slainBee)
            {
                Main.NewText("The Queen Bee was slain...");
                npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainSkull)
            {
                if (npc.type == NPCID.SkeletronHead || npc.type == NPCID.DungeonGuardian)
                {
                    Main.NewText("Skeletron was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.SkeletronHand)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainWall)
            {
                if (npc.type == NPCID.WallofFlesh)
                {
                    Main.NewText("the Wall of Flesh was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.WallofFleshEye)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainMechWorm)
            {
                if (npc.type == NPCID.TheDestroyer)
                {
                    Main.NewText("The Destroyer was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainTwins)
            {
                if (npc.type == NPCID.Spazmatism)
                {
                    Main.NewText("The Twins were slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.Retinazer)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainPrime)
            {
                if (npc.type == NPCID.SkeletronPrime)
                {
                    Main.NewText("Skeletron Prime was slain... (Again, how???)");
                    npc.active = false;
                }
                if (npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeLaser || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainPlant)
            {
                if (npc.type == NPCID.Plantera)
                {
                    Main.NewText("Plantera was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.PlanterasHook || npc.type == NPCID.PlanterasTentacle)
                    npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainGolem)
            {
                if (npc.type == NPCID.Golem)
                {
                    Main.NewText("Golem was slain...");
                    npc.active = false;
                }
                if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
                    npc.active = false;
            }
            if (npc.type == NPCID.DukeFishron && ModContent.GetInstance<SoAWorld>().slainDuke)
            {
                Main.NewText("Duke Fishron was slain...");
                npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainEmpress && npc.type == 636)
            {
                Main.NewText("The Empress of Light was slain...");
                npc.active = false;
            }
            if (ModContent.GetInstance<SoAWorld>().slainMoonLord)
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
                        chat = "Get away from me, I'm not doing any business with you.";
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