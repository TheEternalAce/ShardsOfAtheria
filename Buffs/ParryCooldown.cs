using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class ParryCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Parry Cool Down");
            Description.SetDefault("You cannot parry right now");
            Main.debuff[Type] = true;
        }
    }
}
