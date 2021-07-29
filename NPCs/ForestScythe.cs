﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.NPCs
{
    public class ForestScythe : ModNPC
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 60;
            npc.damage = 7;
            npc.defense = 8;
            npc.lifeMax = 40;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.knockBackResist = 0.4f;
            npc.aiStyle = 23;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * .5f);
            npc.damage = (int)(npc.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (!(spawnInfo.player.ZoneHoly || spawnInfo.player.ZoneCrimson || spawnInfo.player.ZoneCorrupt || Main.eclipse
                || spawnInfo.player.ZoneTowerNebula || spawnInfo.player.ZoneTowerVortex || spawnInfo.player.ZoneTowerSolar
                || spawnInfo.player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || spawnInfo.playerSafe) && Main.dayTime
                && spawnInfo.player.ZoneOverworldHeight)
                return .25f;
            return 0f;
        }

        public override void NPCLoot()
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(ItemID.Radar);
            dropChooser.Add(ItemID.Aglet);
            int choice = dropChooser;

            if (Main.rand.NextFloat() < .01f)
                Item.NewItem(npc.getRect(), choice);
            if (Main.rand.NextFloat() < .5f)
                Item.NewItem(npc.getRect(), ItemID.Wood, Main.rand.Next(10, 20));
        }
    }
}