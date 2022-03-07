using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
	public class HarpyHallowed : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Harpy");
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
            NPC.damage = 10;
            NPC.defense = 8;
            NPC.lifeMax = 50;
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("This harpy has found herself in the colorful land of the hallowed, which has changed her body to fit it's standards.")
            });
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!(Main.eclipse || spawnInfo.player.ZoneTowerNebula || spawnInfo.player.ZoneTowerVortex || spawnInfo.player.ZoneTowerSolar || spawnInfo.player.ZoneTowerStardust || Main.pumpkinMoon 
                || Main.snowMoon) && spawnInfo.player.ZoneHallow && spawnInfo.player.ZoneOverworldHeight)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.CrystalShard, 3, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.PixieDust, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.UnicornHorn, 3, 1, 3));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Confused, 60);
        }
    }
}