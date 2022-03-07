using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.DecaEquipment;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Tools;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Projectiles;
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

            // Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang
            NPC.Happiness
                //Biomes
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Hate)
                //NPCs
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate);
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("He's seen your feats and has come in hopes he can find his missing daughter.")
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

                if (NPC.downedBoss2 && !ModContent.GetInstance<SoAWorld>().slayerMode)
                    return true;
            }
            return false;
        }

        public override string TownNPCName()
        {
            return "Jordan";
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            int painter = NPC.FindFirstNPC(NPCID.Painter);
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<DecaFragment>()) || Main.LocalPlayer.GetModPlayer<DecaPlayer>().modelDeca)
                chat.Add("H-Hey... I'm getting some really menacing vibes from you..");
            if (painter >= 0 && Main.rand.NextBool(6))
                chat.Add("Maybe " + Main.npc[painter].GivenName + " can make me a sprite... Huh? Oh, yes yes, enough of that, let's talk capitalism.");
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<AreusWings>()))
                return "Yikes, those wings do not meet my standards, here let me fix that for you.";
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<MicrobeAnalyzer>()))
                return "That Microbe Analyzer looks great, but it could be better. Hand it over and I can upgrade it.";
            chat.Add("HAHAHAHAHAHAHAHAHAHAH! WHAT DO YOU MEAN 'What's so funny'!?");
            chat.Add("Ey uh.. Have you seen my daughter? No..? I hope she's alright..");
            chat.Add("You know, Areus is extremely dangerous to you humans.. Wait you're no ordinary human?");
            chat.Add("Hey, if you got any spare BBs, I bet a friend of mine would love to have them. Just give 'em to me and I'll deliver them to him.");
            chat.Add("Hey! Tell the mod developer to give me a proper sprite!");
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
                return chat;
            else return "What the- I was just drinking tea and now I'm here?!";
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
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<AreusWings>()))
                {
                    SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                    Main.npcChatText = "There, I upgraded those wings.";

                    int areusWings = Main.LocalPlayer.FindItem(ModContent.ItemType<AreusWings>());

                    Main.LocalPlayer.inventory[areusWings].TurnToAir();
                    Main.LocalPlayer.QuickSpawnItem(new EntitySource_Gift(NPC), ModContent.ItemType<ChargedAreusWings>());

                    return;
                }
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<MicrobeAnalyzer>()))
                {
                    SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                    Main.npcChatText = "There, it'll be much more efficient at analyzing those Microbes.";

                    int microbeAnalyzer = Main.LocalPlayer.FindItem(ModContent.ItemType<MicrobeAnalyzer>());

                    Main.LocalPlayer.inventory[microbeAnalyzer].TurnToAir();
                    Main.LocalPlayer.QuickSpawnItem(new EntitySource_Gift(NPC), ModContent.ItemType<MicrobeAnalyzerMkII>());

                    return;
                }
                if (!Main.LocalPlayer.HasItem(ModContent.ItemType<AreusWings>()) && !Main.LocalPlayer.HasItem(ModContent.ItemType<MicrobeAnalyzer>()))
                {
                    Main.npcChatText = "Sorry pal, you don't have anything I can upgrade";

                    return;
                }
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusChargePack>());
            nextSlot++;
            if (NPC.downedBoss3)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<PhantomDrill>());
                nextSlot++;
            }
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<AreusKey>());
                nextSlot++;
            }
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