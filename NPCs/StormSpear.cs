using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs
{
    public class StormSpear : ModNPC
    {
        public override void SetStaticDefaults()
        {
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
            NPC.width = 58;
            NPC.height = 58;
            NPC.damage = 7;
            NPC.defense = 8;
            NPC.lifeMax = 40;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0.4f;
            NPC.aiStyle = 23;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("A storm spear possessed by a malicious spirit.")
            });
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!(spawnInfo.Player.ZoneHallow || spawnInfo.Player.ZoneCrimson || spawnInfo.Player.ZoneCorrupt || Main.eclipse || spawnInfo.Player.ZoneTowerNebula || spawnInfo.Player.ZoneTowerVortex 
                || spawnInfo.Player.ZoneTowerSolar || spawnInfo.Player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || spawnInfo.PlayerSafe) && spawnInfo.Player.ZoneUndergroundDesert)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(2, 3380, ItemID.SandBlock, ItemID.Sandstone, ItemID.HardenedSand));
            npcLoot.Add(ItemDropRule.Common(ItemID.Bone, 4, 3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat() >= .5f)
                target.AddBuff(BuffID.Electrified, 10 * 60);
        }
    }
}