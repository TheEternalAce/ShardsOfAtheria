using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Melee.AreusSwordProjs;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.Town.TheAtherian
{
    // [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class Atherian : ModNPC
    {
        public override string Texture => "ShardsOfAtheria/NPCs/Town/TheAtherian/Atherian" + (SoA.AprilFools ? "_AprilFools" : "");

        public override void SetStaticDefaults()
        {
            if (SoA.AprilFools)
            {
                Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has

                NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
                NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            }
            else
            {
                Main.npcFrameCount[Type] = 26;
                NPCID.Sets.ExtraFramesCount[Type] = 9;
                NPCID.Sets.AttackFrameCount[Type] = 5;
            }
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            List<int> buffTypes = new()
            {
                BuffID.Poisoned,
                BuffID.Confused,
                ModContent.BuffType<ElectricShock>()
            };
            NPC.SetImmuneTo(buffTypes);

            // Set Atherian's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang
            NPC.Happiness
                //Biomes
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
                //.SetBiomeAffection<>(AffectionLevel.Hate)
                //NPCs
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like);

            NPC.AddElementElec();
            NPC.ElementMultipliers(new[] { 0.5f, 1.0f, 0.8f, 2.0f });
        }

        internal void SetupShopQuotes(Mod shopQuotes)
        {
            shopQuotes.Call("AddNPC", Mod, Type);
            shopQuotes.Call("SetColor", Type, new Color(165, 140, 190));
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

            if (SoA.AprilFools)
            {
                AnimationType = NPCID.Guide;
            }
            else
            {
                AnimationType = NPCID.Stylist;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            string key = this.GetLocalizationKey("Bestiary");
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement(key)
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                    continue;
                ShardsDownedSystem shardsDowned = SoA.DownedSystem;
                if (NPC.downedBoss2 && (!(shardsDowned.slainSenterra || shardsDowned.slainGenesis || shardsDowned.slainValkyrie) ||
                    SoA.ServerConfig.cluelessNPCs) && !shardsDowned.slainAtherian)
                    return true;
            }
            return false;
        }

        public override bool PreAI()
        {
            if (!SoA.ServerConfig.cluelessNPCs)
            {
                if (SoA.DownedSystem.slainSenterra && SoA.DownedSystem.slainGenesis &&
                    SoA.DownedSystem.slainValkyrie)
                {
                    NPC.active = false;
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(DialogueKeyBase + "AllDeath"), Color.Red);
                    return false;
                }
                else if (SoA.DownedSystem.slainSenterra || SoA.DownedSystem.slainGenesis)
                {
                    NPC.active = false;
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(DialogueKeyBase + "GoddessDeath"), Color.Red);
                    return false;
                }
                else if (SoA.DownedSystem.slainValkyrie)
                {
                    NPC.active = false;
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(DialogueKeyBase + "NovaDeath"), Color.Red);
                    return false;
                }
            }
            return base.PreAI();
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() { "Jordan", "Damien", "Jason", "Kevin", "Rain", "Sage", "Archimedes" };
        }

        const string DialogueKeyBase = "Mods.ShardsOfAtheria.NPCs.Atherian.Dialogue.";
        public override string GetChat()
        {
            WeightedRandom<string> chat = new();
            Player player = Main.LocalPlayer;

            if (player.HasItem(ModContent.ItemType<GenesisAndRagnarok>()))
            {
                ShardsPlayer shardsPlayer = player.Shards();
                int upgrades = shardsPlayer.genesisRagnarockUpgrades;
                if (upgrades == 0)
                {
                    chat.AddKey(DialogueKeyBase + "BaseGenesisAndRagnarok");
                }
            }

            chat.AddKey(DialogueKeyBase + "AtheriaComment");
            if (player.HasItem(ItemID.GoldCrown, player.armor))
            {
                chat.AddKey(DialogueKeyBase + "GoldCrownCompliment");
            }
            else
            {
                chat.AddKey(DialogueKeyBase + "GoldCrownThought");
            }
            if (ShardsDownedSystem.downedValkyrie)
            {
                chat.AddKey(DialogueKeyBase + "ExComment");
            }
            int guide = NPC.FindFirstNPC(NPCID.Guide);
            if (guide >= 0)
            {
                chat.AddKey(DialogueKeyBase + "CSGOReference", Main.npc[guide].GivenName);
            }
            chat.AddKey(DialogueKeyBase + "MorshuMoment", player.name);
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Upgrade";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }
            else
            {
                ModContent.GetInstance<UpgradeUISystem>().ShowUI();
                Main.playerInventory = true;
                Main.npcChatText = "";
            }
        }

        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, "Shop")
                .Add<ValkyrieCrest>(SoAConditions.NotSlayerMode)
                .Add<AreusShard>()
                .Add<AreusArmorChip>()
                .Add<RushDrive>()
                .Add<AreusEdge>()
                .Add<AreusStriker>()
                .Add<AreusProcessor>(SoAConditions.ElementModEnabled)
                .Add<ResonatorRing>(SoAConditions.ElementModEnabled)
                .Add<AreusLance>(Condition.Hardmode)
                .Add<Bytecrusher>(Condition.DownedMechBossAny)
                .Add<AreusKey>(Condition.DownedPlantera)
                .Add<AreusPistol>(Condition.DownedPlantera)
                .Add<AreusBaton>(Condition.DownedPlantera)
                .Add<AreusStrikeChain>(Condition.DownedCultist)
                .Add<AreusEnergyCannon>(Condition.DownedCultist);
            npcShop.Register();
        }

        public override void OnKill()
        {
            Player lastPlayerToHitThisNPC = NPC.AnyInteractions() ? Main.player[NPC.lastInteraction] : null;
            if (lastPlayerToHitThisNPC != null)
            {
                if (lastPlayerToHitThisNPC.Slayer().slayerMode)
                {
                    SoA.DownedSystem.slainAtherian = true;
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