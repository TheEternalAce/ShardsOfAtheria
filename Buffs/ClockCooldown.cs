using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Accessories;

namespace SagesMania.Buffs
{
    public class ClockCooldown : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Clock Cooldown");
            Description.SetDefault("You cannot use Spider's Mechanical Clock for now");
            Main.debuff[Type] = true;
        }
    }
}
