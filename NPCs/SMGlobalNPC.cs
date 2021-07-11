using SagesMania.Buffs;
using SagesMania.Items;
using SagesMania.Items.Accessories;
using SagesMania.Items.Placeable;
using SagesMania.Items.Weapons;
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

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
			if (type == NPCID.ArmsDealer)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<CO2Cartridge>());
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
                    Item.NewItem(npc.getRect(), ItemID.EoCShield);
                    Item.NewItem(npc.getRect(), ItemID.Binoculars);
                    Item.NewItem(npc.getRect(), ItemID.EyeofCthulhuTrophy);
                    Item.NewItem(npc.getRect(), ItemID.EyeMask);
                    Item.NewItem(npc.getRect(), ItemID.AviatorSunglasses);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<EyeOfTheAllSeer>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Cataracnia>());
                }
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    Item.NewItem(npc.getRect(), ItemID.CrimtaneOre, 4000);
                    Item.NewItem(npc.getRect(), ItemID.TissueSample, 4000);
                    Item.NewItem(npc.getRect(), ItemID.BrainMask);
                    Item.NewItem(npc.getRect(), ItemID.BrainofCthulhuTrophy);
                    Item.NewItem(npc.getRect(), ItemID.BoneRattle);
                    Item.NewItem(npc.getRect(), ItemID.BrainOfConfusion);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<TomeOfOmniscience>());
                }
                if (npc.boss && System.Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
                {
                    Item.NewItem(npc.getRect(), ItemID.DemoniteOre, 4000);
                    Item.NewItem(npc.getRect(), ItemID.ShadowScale, 4000);
                    Item.NewItem(npc.getRect(), ItemID.EaterofWorldsTrophy);
                    Item.NewItem(npc.getRect(), ItemID.EaterMask);
                    Item.NewItem(npc.getRect(), ItemID.EatersBone);
                    Item.NewItem(npc.getRect(), ItemID.WormScarf);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<OversizedWormsTooth>());
                }
                if (npc.type == NPCID.QueenBee)
                {
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
                    Item.NewItem(npc.getRect(), ItemID.HiveBackpack);
                    Item.NewItem(npc.getRect(), ItemID.BeeGun);
                    Item.NewItem(npc.getRect(), ItemID.BeeKeeper);
                    Item.NewItem(npc.getRect(), ItemID.BeesKnees);
                    Item.NewItem(npc.getRect(), ItemID.BeeMask);
                    Item.NewItem(npc.getRect(), ItemID.QueenBeeTrophy);
                    Item.NewItem(npc.getRect(), ItemID.EatersBone);
                    Item.NewItem(npc.getRect(), ItemID.WormScarf);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<LCAR9>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<HiddenBlade>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<DemonClaw>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<HecateII>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<ShadowBrand>());
                    Item.NewItem(npc.getRect(), ModContent.ItemType<MarkOfAnastasia>());
                }
                if (npc.type == NPCID.SkeletronHead)
                {
                    Item.NewItem(npc.getRect(), ItemID.BookofSkulls);
                    Item.NewItem(npc.getRect(), ItemID.SkeletronHand);
                    Item.NewItem(npc.getRect(), ItemID.SkeletronTrophy);
                    Item.NewItem(npc.getRect(), ItemID.SkeletronMask);
                    Item.NewItem(npc.getRect(), ItemID.EatersBone);
                    Item.NewItem(npc.getRect(), ItemID.BoneGlove);
                }
                if (npc.type == NPCID.WallofFlesh)
                {
                    Item.NewItem(npc.getRect(), ItemID.Pwnhammer);
                    Item.NewItem(npc.getRect(), ItemID.RangerEmblem);
                    Item.NewItem(npc.getRect(), ItemID.WarriorEmblem);
                    Item.NewItem(npc.getRect(), ItemID.SorcererEmblem);
                    Item.NewItem(npc.getRect(), ItemID.SummonerEmblem);
                    Item.NewItem(npc.getRect(), ItemID.BreakerBlade);
                    Item.NewItem(npc.getRect(), ItemID.ClockworkAssaultRifle);
                    Item.NewItem(npc.getRect(), ItemID.LaserRifle);
                    Item.NewItem(npc.getRect(), ItemID.WallofFleshTrophy);
                    Item.NewItem(npc.getRect(), ItemID.FleshMask);
                    Item.NewItem(npc.getRect(), ItemID.DemonHeart);
                }
            }
        }

        public override bool PreAI(NPC npc)
        {
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