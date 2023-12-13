using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class SpitefulRuby : ModBuff
    {
        public static readonly int DefenseReduction = 26;
    }

    public class SpiteRubyNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff<SpitefulRuby>())
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
