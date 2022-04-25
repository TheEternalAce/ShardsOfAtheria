using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs
{
    public class HarpyVoid : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Harpy");

            // Specify the debuffs it is immune to
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.ShadowFlame,
                    BuffID.CursedInferno,
                    BuffID.Frostburn,
                    BuffID.Frostburn2
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];

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
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.damage = 12;
            NPC.defense = 8;
            NPC.lifeMax = 50;
            NPC.lavaImmune = true;
            AIType = NPCID.Harpy;
            AnimationType = NPCID.Harpy;
            Banner = Item.NPCtoBanner(NPCID.Harpy);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("A harpy found it's way into the Underworld, it's dark magic messed up her mind and she's now in eternal pain. Killing her would be doing her a favor.")
            });
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.playerSafe && spawnInfo.player.ZoneUnderworldHeight)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule hardmode = new LeadingConditionRule(new Conditions.IsHardmode());

            npcLoot.Add(ItemDropRule.OneFromOptions(1, ItemID.Hellstone, ItemID.Obsidian));
            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 5, 3, 6));
            hardmode.OnSuccess(ItemDropRule.Common(ItemID.FireFeather, 5));

            // Finally add the leading rule
            npcLoot.Add(hardmode);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat() < .1f)
                target.AddBuff(BuffID.Stoned, 60);
        }
    }
}