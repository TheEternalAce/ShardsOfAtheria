using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs
{
    public class StormSpear : ModNPC
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            NPC.width = 58;
            NPC.height = 58;
            NPC.damage = 7;
            NPC.defense = 8;
            NPC.lifeMax = 40;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0.4f;
            NPC.aiStyle = 23;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (!(spawnInfo.player.ZoneHallow || spawnInfo.player.ZoneCrimson || spawnInfo.player.ZoneCorrupt || Main.eclipse
                || spawnInfo.player.ZoneTowerNebula || spawnInfo.player.ZoneTowerVortex || spawnInfo.player.ZoneTowerSolar
                || spawnInfo.player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || spawnInfo.playerSafe)
                && spawnInfo.player.ZoneUndergroundDesert)
                return .25f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(3380);
            dropChooser.Add(ItemID.SandBlock);
            dropChooser.Add(ItemID.Sandstone);
            dropChooser.Add(ItemID.HardenedSand);
            int choice = dropChooser;

            if (Main.rand.NextFloat() < .01f)
                Item.NewItem(NPC.getRect(), choice, Main.rand.Next(3, 6));
            if (Main.rand.NextFloat() < .5f)
                Item.NewItem(NPC.getRect(), ItemID.Bone, Main.rand.Next(3, 6));
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat() >= .5f)
                target.AddBuff(BuffID.Electrified, 10 * 60);
        }
    }
}