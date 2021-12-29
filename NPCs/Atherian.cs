using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.DecaEquipment;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Projectiles;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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
            Main.npcFrameCount[Type] = 25;
            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 4;
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

            AnimationType = NPCID.Clothier;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("Hailing from a mysterious greyscale cube world, the Example Person is here to help you understand everything about tModLoader."),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
				new FlavorTextBestiaryInfoElement("Mods.ShardsOfAtheria.Bestiary.Atherian")
            });
        }

        // The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
        // Returning false will allow you to manually draw your NPC
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            // This code slowly rotates the NPC in the bestiary
            // (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
            if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
            {
                drawModifiers.Rotation += 0.001f;

                // Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
                NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            }

            return true;
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
                default:
                    return "Jordan";
            }
        }

        public override string GetChat()
        {
            int painter = NPC.FindFirstNPC(NPCID.Painter);
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<DecaFragment>()))
            {
                return "H-Hey there... That's a Deca fragment isn't it.? In my language \"Deca\" means Death..";
            }
            if (painter >= 0 && Main.rand.NextBool(6))
            {
                return "Maybe " + Main.npc[painter].GivenName + " can make me a sprite... Huh? Oh, yes yes, enough of that, let's talk retail.";
            }
            switch (Main.rand.Next(5))
            {
                case 0:
                    return "HAHAHAHAHAHAHAHAHAHAH! WHAT DO YOU MEAN 'What's so funny'!?";
                case 1:
                    return "Ey uh.. Have you seen my daughter? No..? I hope she's alright..";
                case 2:
                    {
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
            shop.item[nextSlot].shopSpecialCurrency = ShardsOfAtheria.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<BlackAreusSword>());
            shop.item[nextSlot].shopCustomPrice = 100;
            shop.item[nextSlot].shopSpecialCurrency = ShardsOfAtheria.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusPistol>());
            shop.item[nextSlot].shopCustomPrice = 38;
            shop.item[nextSlot].shopSpecialCurrency = ShardsOfAtheria.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusStaff>());
            shop.item[nextSlot].shopCustomPrice = 38;
            shop.item[nextSlot].shopSpecialCurrency = ShardsOfAtheria.AreusCurrency;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusWings>());
            shop.item[nextSlot].shopCustomPrice = 150;
            shop.item[nextSlot].shopSpecialCurrency = ShardsOfAtheria.AreusCurrency;
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