using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class NullField : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<SoAGlobalNPC>().areusNullField = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.SetImmuneTimeForAllTypes(2);
            player.immuneNoBlink = true;
            player.noKnockback = true;
            //player.GetDamage(DamageClass.Generic) *= 0;
            player.Shards().areusNullField = true;
        }
    }
}
