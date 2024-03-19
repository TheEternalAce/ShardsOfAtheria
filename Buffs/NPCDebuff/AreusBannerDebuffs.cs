using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class AreusBannerMeleeDebuff : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;
        public const int DefenseReduction = 8;
    }
    public class AreusBannerRangedDebuff : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.StatSpeed() -= 0.15f;
        }
    }
    public class AreusBannerSummonDebuff : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;
    }

    public class AreusBannerDebuffNPC : GlobalNPC
    {
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff<AreusBannerMeleeDebuff>())
            {
                modifiers.Defense -= AreusBannerMeleeDebuff.DefenseReduction;
            }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff<AreusBannerSummonDebuff>())
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 24;
                damage = 12;
            }
        }
    }
}
