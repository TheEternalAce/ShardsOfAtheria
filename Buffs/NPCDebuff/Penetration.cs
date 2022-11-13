using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class Penetration : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (NPC.downedGolemBoss)
            {
                player.statDefense -= 26;
            }
            else if (Main.hardMode)
            {
                player.statDefense -= 15;
            }
            else player.statDefense -= 5;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (NPC.downedGolemBoss)
            {
                npc.defense -= 26;
            }
            else if (Main.hardMode)
            {
                npc.defense -= 15;
            }
            else npc.defense -= 5;
        }
    }
}
