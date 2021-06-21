using SagesMania.Items;
using SagesMania.Items.Accessories;
using SagesMania.Items.AreusDamageClass;
using SagesMania.Items.Placeable;
using SagesMania.Items.Weapons;
using SagesMania.Items.Weapons.Ammo;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SagesMania.NPCs
{
    // [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class Atherian : ModNPC
    {
        public override string Texture => "SagesMania/NPCs/Atherian";

        public override bool Autoload(ref string name)
        {
            name = "Atherian";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
            // DisplayName.SetDefault("Example Person");
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 200;
            npc.defense = 999;
            npc.lifeMax = 1000;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/AtherianHit");
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Clothier;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num = npc.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                foreach (Item item in player.inventory)
                {
                    if (item.type == ModContent.ItemType<AreusCoin>())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return "Harold";
                case 1:
                    return "Jordan";
                default:
                    return "Zero";
            }
        }

        public override void FindFrame(int frameHeight)
        {
            /*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
        }

        public override string GetChat()
        {
            int painter = NPC.FindFirstNPC(NPCID.Painter);
            if (painter >= 0 && Main.rand.NextBool(4))
            {
                return "Maybe " + Main.npc[painter].GivenName + " can make me a sprite... Huh? Oh, yes yes, enough of that, let's talk retail.";
            }
            switch (Main.rand.Next(4))
            {
                case 0:
                    return "HAHAHAHAHAHAHAHAHAHAH! WHAT DO YOU MEAN ''What's so funny''!?";
                case 1:
                    return "Ey uh.. Have you seen my son? No..? I hope he's alright..";
                case 2:
                    {
                        // Main.npcChatCornerItem shows a single item in the corner, like the Angler Quest chat.
                        //Main.npcChatCornerItem = ModContent.ItemType<>();
                        return $"You know, [i:{ModContent.ItemType<AreusOreItem>()}] is extremely dangerous to you humans.. Wait you're no ordinary human?";
                    }
                case 3:
                    {
                        return $"Hey, if you got any [i:{ModContent.ItemType<BB>()}] I bet a friend of mine would love to have them. Just give 'em to me and I'll deliver them to him.";
                    }
                default:
                    return "Hey! Tell the mod developer to give me a proper sprite!";
            }
        }

        /* 
		// Consider using this alternate approach to choosing a random thing. Very useful for a variety of use cases.
		// The WeightedRandom class needs "using Terraria.Utilities;" to use
		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			if (partyGirl >= 0 && Main.rand.NextBool(4))
			{
				chat.Add("Can you please tell " + Main.npc[partyGirl].GivenName + " to stop decorating my house with colors?");
			}
			chat.Add("Sometimes I feel like I'm different from everyone else here.");
			chat.Add("What's your favorite color? My favorite colors are white and black.");
			chat.Add("What? I don't have any arms or legs? Oh, don't be ridiculous!");
			chat.Add("This message has a weight of 5, meaning it appears 5 times more often.", 5.0);
			chat.Add("This message has a weight of 0.1, meaning it appears 10 times as rare.", 0.1);
			return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
		}
		*/

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusDagger>());
            shop.item[nextSlot].shopCustomPrice = 50;
            shop.item[nextSlot].shopSpecialCurrency = SagesMania.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<BlackAreusSword>());
            shop.item[nextSlot].shopCustomPrice = 100;
            shop.item[nextSlot].shopSpecialCurrency = SagesMania.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusPistol>());
            shop.item[nextSlot].shopCustomPrice = 38;
            shop.item[nextSlot].shopSpecialCurrency = SagesMania.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusStaff>());
            shop.item[nextSlot].shopCustomPrice = 38;
            shop.item[nextSlot].shopSpecialCurrency = SagesMania.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<RetributionChain>());
            shop.item[nextSlot].shopCustomPrice = 80;
            shop.item[nextSlot].shopSpecialCurrency = SagesMania.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusWings>());
            shop.item[nextSlot].shopCustomPrice = 150;
            shop.item[nextSlot].shopSpecialCurrency = SagesMania.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<KitchenKnife>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 30, copper: 15);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<JarOIchor>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 40);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<ButterflyKnife>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(platinum: 1, gold: 20);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<BBGun>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 4, copper: 10);
            nextSlot++;
            if (Main.LocalPlayer.HasBuff(BuffID.Lovestruck))
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<LovesKnife>());
                nextSlot++;
            }

            /*
			// Here is an example of how your npc can sell items from other mods.
			var modSummonersAssociation = ModLoader.GetMod("SummonersAssociation");
			if (modSummonersAssociation != null)
			{
				shop.item[nextSlot].SetDefaults(modSummonersAssociation.ItemType("BloodTalisman"));
				nextSlot++;
			}

			if (!Main.LocalPlayer.GetModPlayer<ExamplePlayer>().examplePersonGiftReceived && ModContent.GetInstance<ExampleConfigServer>().ExamplePersonFreeGiftList != null)
			{
				foreach (var item in ModContent.GetInstance<ExampleConfigServer>().ExamplePersonFreeGiftList)
				{
					if (item.IsUnloaded)
						continue;
					shop.item[nextSlot].SetDefaults(item.Type);
					shop.item[nextSlot].shopCustomPrice = 0;
					shop.item[nextSlot].GetGlobalItem<ExampleInstancedGlobalItem>().examplePersonFreeGift = true;
					nextSlot++;
					// TODO: Have tModLoader handle index issues.
				}
			}
			*/
        }

        public override void NPCLoot()
        {
            //Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Armor.ExampleCostume>());
        }

        // Make this Town NPC teleport to the King and/or Queen statue when triggered.
        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 200;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<TrueBlade>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}