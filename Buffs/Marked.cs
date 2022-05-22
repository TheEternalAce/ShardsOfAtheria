using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class Marked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marked");
            Description.SetDefault("Taking 10% extra damage");
        }
    }
}
