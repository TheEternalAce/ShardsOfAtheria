﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs
{
	public class HarpyHallowed : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Harpy");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Harpy];
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Harpy);
            npc.width = 98;
            npc.height = 92;
            npc.damage = 10;
            npc.defense = 8;
            npc.lifeMax = 50;
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
            if (!(Main.eclipse || spawnInfo.player.ZoneTowerNebula || spawnInfo.player.ZoneTowerVortex || spawnInfo.player.ZoneTowerSolar
                || spawnInfo.player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon) && spawnInfo.player.ZoneHoly
                && spawnInfo.player.ZoneOverworldHeight)
                return .25f;
            return 0f;
        }

        public override void NPCLoot()
        {
            if(Main.rand.NextFloat() < .5f)
                Item.NewItem(npc.getRect(), ItemID.Feather);
            Item.NewItem(npc.getRect(), ItemID.CrystalShard, Main.rand.Next(3, 6));
            Item.NewItem(npc.getRect(), ItemID.PixieDust, Main.rand.Next(3, 6));
            Item.NewItem(npc.getRect(), ItemID.UnicornHorn, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(164, 60);
        }
    }
}