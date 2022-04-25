using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class Marked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Awakened Slayer");
            Description.SetDefault("Taking 10% extra damage");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.takenDamageMultiplier = 1.1f;
        }
    }
}
