using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class MarkedII : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marked II");
            Description.SetDefault("Taking 20% extra damage");
        }
    }
}
