using ShardsOfAtheria.Items.Consumable;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
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

        public override void OnSpawn(IEntitySource source)
        {
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