using SagesMania.Buffs;
using SagesMania.Items;
using SagesMania.Items.Accessories;
using SagesMania.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs
{
	public class HarpyForest : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forest Harpy");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Harpy];
        }

        public override void SetDefaults()
        {
            npc.width = 98;
            npc.height = 368;
            npc.damage = 25;
            npc.defense = 8;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.CloneDefaults(NPCID.Harpy);
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
            if (!(spawnInfo.player.ZoneHoly && spawnInfo.player.ZoneCrimson && spawnInfo.player.ZoneCorrupt && Main.eclipse
                && spawnInfo.player.ZoneTowerNebula && spawnInfo.player.ZoneTowerVortex && spawnInfo.player.ZoneTowerSolar
                && spawnInfo.player.ZoneTowerStardust && Main.pumpkinMoon && Main.snowMoon && player.townNPCs <= 3) && spawnInfo.player.ZoneOverworldHeight && Main.dayTime)
                    return .25f;
            return 0f;
        }

        public override void NPCLoot()
        {
            if(Main.rand.NextFloat() < .5f)
                Item.NewItem(npc.getRect(), ItemID.Feather);
            Item.NewItem(npc.getRect(), ItemID.Mushroom, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60);
        }
    }
}