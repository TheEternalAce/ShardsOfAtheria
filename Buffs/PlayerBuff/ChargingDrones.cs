using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class ChargingDrones : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }
    }
}
