using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class MarkedIII : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marked III");
            Description.SetDefault("Taking 50% extra damage");
        }
    }
}
