using Terraria;
using Terraria.GameContent.Bestiary;
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
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.width = 98;
            NPC.height = 92;
            NPC.damage = 12;
            NPC.defense = 8;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0.4f;
            NPC.aiStyle = 14;
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("A harpy found it's way into the Underworld, it's dark magic messed up her mind and she's now in eternal pain.")
            });
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.playerSafe && spawnInfo.player.ZoneUnderworldHeight && Main.hardMode)
                return .25f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(ItemID.Hellstone);
            dropChooser.Add(ItemID.AshBlock);
            dropChooser.Add(ItemID.Obsidian);
            int choice = dropChooser;

            if (Main.rand.NextFloat() < .5f)
                Item.NewItem(NPC.getRect(), ItemID.Feather);
            if (Main.rand.NextFloat() < .2f && Main.hardMode)
                Item.NewItem(NPC.getRect(), ItemID.FireFeather);
            Item.NewItem(NPC.getRect(), choice, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat() < .1f)
                target.AddBuff(BuffID.Stoned, 60);
        }
    }
}