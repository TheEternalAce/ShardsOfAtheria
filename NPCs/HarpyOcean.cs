using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs
{
	public class HarpyOcean : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Harpy");
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
            if (!(spawnInfo.player.ZoneHoly || spawnInfo.player.ZoneCrimson || spawnInfo.player.ZoneCorrupt || Main.pumpkinMoon
                || Main.snowMoon || spawnInfo.player.ZoneTowerNebula || spawnInfo.player.ZoneTowerVortex || spawnInfo.player.ZoneTowerSolar
                || spawnInfo.player.ZoneTowerStardust || spawnInfo.playerSafe)  && spawnInfo.player.ZoneBeach)
                return .25f;
            return 0f;
        }

        public override void NPCLoot()
        {
            if(Main.rand.NextFloat() < .5f)
                Item.NewItem(npc.getRect(), ItemID.Feather);
            if (Main.rand.NextFloat() < .5f)
                Item.NewItem(npc.getRect(), ItemID.Coral, Main.rand.Next(3, 6));
            if (Main.rand.NextFloat() < .5f)
                Item.NewItem(npc.getRect(), ItemID.Seashell, Main.rand.Next(3, 6));
            if (Main.rand.NextFloat() < .5f)
                Item.NewItem(npc.getRect(), ItemID.Starfish, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 60);
        }
    }
}