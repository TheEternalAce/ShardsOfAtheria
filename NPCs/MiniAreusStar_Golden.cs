using ShardsOfAtheria.Items.Consumable;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs
{
    public class MiniAreusStar_Golden : MiniAreusStar
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPCID.Sets.GoldCrittersCollection.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.rarity = 5;
            NPC.catchItem = ModContent.ItemType<MiniStarCritter_Golden>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnChance = 0f;
            if (spawnInfo.NoInvasionOfAnyKind() && spawnInfo.Player.ZoneNormalSpace && NPC.downedBoss2)
                spawnChance += 0.08f;
            if (spawnInfo.PlayerInTown)
                spawnChance *= 2f;
            return spawnChance;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                ShardsHelpers.DustRing(NPC.Center, 5, DustID.Gold);
            }
        }
    }
}