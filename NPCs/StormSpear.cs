using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.NPCs
{
    public class StormSpear : ModNPC
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            npc.width = 58;
            npc.height = 58;
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
                || spawnInfo.player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || spawnInfo.playerSafe)
                && spawnInfo.player.ZoneUndergroundDesert)
                return .25f;
            return 0f;
        }

        public override void NPCLoot()
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(3380);
            dropChooser.Add(ItemID.SandBlock);
            dropChooser.Add(ItemID.Sandstone);
            dropChooser.Add(ItemID.HardenedSand);
            int choice = dropChooser;

            if (Main.rand.NextFloat() < .01f)
                Item.NewItem(npc.getRect(), choice, Main.rand.Next(3, 6));
            if (Main.rand.NextFloat() < .5f)
                Item.NewItem(npc.getRect(), ItemID.Bone, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat() >= .5f)
                target.AddBuff(BuffID.Electrified, 10 * 60);
        }
    }
}