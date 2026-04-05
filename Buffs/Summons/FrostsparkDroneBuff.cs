using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Summons
{
    public class FrostsparkDroneBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
        }
    }
}
