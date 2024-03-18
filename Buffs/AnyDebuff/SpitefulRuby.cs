using ShardsOfAtheria.Buffs.NPCDebuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class SpitefulRuby : ModBuff
    {
        public static readonly int DefenseReduction = 26;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= DefenseReduction;
        }
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
            if (npc.HasBuff<SpitefulRuby>())
            {
                modifiers.Defense.Flat -= Withering.DefenseReduction;
            }
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff<SpitefulRuby>())
            {
                modifiers.FinalDamage -= 0.15f;
            }
        }
    }
}
