using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
    public class CarrierSlime : ModNPC
    {
        public Item[] inventoryItems;
        public Item[] armor;
        public Item[] accessories;
        public int coins;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
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
            NPC.CloneDefaults(NPCID.BlueSlime);
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("This slime will hold onto all of your dropped items when you die.")
            });
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => 0f;

        public override void OnKill()
        {
            Player player = Main.LocalPlayer;
            if (player.difficulty == PlayerDifficultyID.MediumCore)
            {
                foreach (Item i in inventoryItems)
                {
                    Item.NewItem(NPC.GetItemSource_Loot(), NPC.getRect(), i.type);
                }
                foreach (Item ar in armor)
                {
                    Item.NewItem(NPC.GetItemSource_Loot(), NPC.getRect(), ar.type);
                }
                foreach (Item ac in accessories)
                {
                    Item.NewItem(NPC.GetItemSource_Loot(), NPC.getRect(), ac.type);
                }
            }
            else Item.NewItem(NPC.GetItemSource_Loot(), NPC.getRect(), ItemID.CopperCoin, coins);
        }
    }
}