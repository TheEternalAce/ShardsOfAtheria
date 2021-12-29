using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Accessories;

namespace ShardsOfAtheria.Buffs
{
    public class ClockCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clock Cooldown");
            Description.SetDefault("You cannot use Spider's Mechanical Clock for now");
            Main.debuff[Type] = true;
        }
    }
}
