using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class PlagueMark : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
    }
}
