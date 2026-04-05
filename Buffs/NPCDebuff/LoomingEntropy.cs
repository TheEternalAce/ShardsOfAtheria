using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class LoomingEntropy : ModBuff
    {
        public static readonly int TagDamage = 26;
        public static readonly int DefenseReduction = 14;

        public override string Texture => SoA.DebuffTemplate;

        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsATagBuff[Type] = true;
        }
    }
}
