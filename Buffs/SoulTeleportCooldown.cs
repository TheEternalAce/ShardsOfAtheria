using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class SoulTeleportCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Teleport Cool Down");
            Description.SetDefault("You cannot use Soul Teleport right now");
            Main.debuff[Type] = true;
        }
    }
}
