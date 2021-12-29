using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs
{
    public class HarpyCave : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cave Harpy");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.width = 98;
            NPC.height = 92;
            NPC.damage = 7;
            NPC.defense = 8;
            NPC.lifeMax = 40;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            NPC.aiStyle = 14;
            AIType = NPCID.Harpy;
            AnimationType = NPCID.Harpy;
            Banner = Item.NPCtoBanner(NPCID.Harpy);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (!(spawnInfo.player.ZoneHallow || spawnInfo.player.ZoneCrimson || spawnInfo.player.ZoneCorrupt || spawnInfo.player.ZoneDungeon
                || spawnInfo.player.ZoneSnow || spawnInfo.playerSafe) && spawnInfo.player.ZoneDirtLayerHeight || spawnInfo.player.ZoneRockLayerHeight)
                return .25f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(ItemID.CopperBar);
            dropChooser.Add(ItemID.TinBar);
            dropChooser.Add(ItemID.IronBar);
            dropChooser.Add(ItemID.LeadBar);
            dropChooser.Add(ItemID.SilverBar);
            dropChooser.Add(ItemID.TungstenBar);
            dropChooser.Add(ItemID.GoldBar);
            dropChooser.Add(ItemID.PlatinumBar);
            int choice = dropChooser;

            if (Main.rand.NextFloat() < .5f)
                Item.NewItem(NPC.getRect(), ItemID.Feather);
                Item.NewItem(NPC.getRect(), choice, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat() < .1f)
                target.AddBuff(BuffID.Stoned, 60);
        }
    }
}