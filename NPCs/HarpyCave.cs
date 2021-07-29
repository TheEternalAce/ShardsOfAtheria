using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.NPCs
{
    public class HarpyCave : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cave Harpy");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Harpy];
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Harpy);
            npc.width = 98;
            npc.height = 92;
            npc.damage = 7;
            npc.defense = 8;
            npc.lifeMax = 40;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.aiStyle = 14;
            aiType = NPCID.Harpy;
            animationType = NPCID.Harpy;
            banner = Item.NPCtoBanner(NPCID.Harpy);
            bannerItem = Item.BannerToItem(banner);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * .5f);
            npc.damage = (int)(npc.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (!(spawnInfo.player.ZoneHoly || spawnInfo.player.ZoneCrimson || spawnInfo.player.ZoneCorrupt || spawnInfo.player.ZoneDungeon
                || spawnInfo.player.ZoneSnow || spawnInfo.playerSafe) && spawnInfo.player.ZoneDirtLayerHeight || spawnInfo.player.ZoneRockLayerHeight)
                return .25f;
            return 0f;
        }

        public override void NPCLoot()
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
                Item.NewItem(npc.getRect(), ItemID.Feather);
                Item.NewItem(npc.getRect(), choice, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat() < .1f)
                target.AddBuff(BuffID.Stoned, 60);
        }
    }
}