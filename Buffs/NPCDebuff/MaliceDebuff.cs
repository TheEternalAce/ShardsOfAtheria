using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class MaliceDebuff : ModBuff
    {
        public static readonly float DamageBonus = 0.5f;
        public static readonly float SpeedReduction = 0.1f;

        public override string Texture => SoA.DebuffTemplate;

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.StatSpeed() -= SpeedReduction;
        }
    }
}
