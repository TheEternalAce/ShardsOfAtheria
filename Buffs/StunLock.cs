using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class StunLock : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stun Lock");
            Description.SetDefault("Cannot move");

            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.position = npc.oldPosition;
            npc.velocity = Vector2.Zero;
        }
    }
}
