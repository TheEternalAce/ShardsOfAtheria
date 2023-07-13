using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class Withering : ModBuff
    {
        public static readonly int DefenseReduction = 26;
        public static readonly float SpeedReduction = 0.1f;

        public override string Texture => SoA.DebuffTemplate;

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.StatSpeed() -= SpeedReduction;
        }
    }

    public class WitheringNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff<Withering>())
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 30;
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff<Withering>())
            {
                modifiers.Defense.Flat -= Withering.DefenseReduction;
            }
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff<Withering>())
            {
                modifiers.FinalDamage -= 0.15f;
            }
        }
    }
}
