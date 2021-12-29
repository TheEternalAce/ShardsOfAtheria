using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
	public class HarpyCorrupt : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt Harpy");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.width = 98;
            NPC.height = 92;
            NPC.damage = 8;
            NPC.defense = 8;
            NPC.lifeMax = 50;
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
            if (!(spawnInfo.player.ZoneHallow || spawnInfo.player.ZoneCrimson || spawnInfo.player.ZoneTowerNebula
                || spawnInfo.player.ZoneTowerVortex || spawnInfo.player.ZoneTowerSolar || spawnInfo.player.ZoneTowerStardust
                || Main.pumpkinMoon || Main.snowMoon) && spawnInfo.player.ZoneOverworldHeight && spawnInfo.player.ZoneCorrupt)
                return .25f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if(Main.rand.NextFloat() < .5f)
                Item.NewItem(NPC.getRect(), ItemID.Feather);
            if (Main.hardMode)
                Item.NewItem(NPC.getRect(), ItemID.CursedFlame, Main.rand.Next(1, 3));
            Item.NewItem(NPC.getRect(), ItemID.VileMushroom, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Weak, 60);
        }
    }
}