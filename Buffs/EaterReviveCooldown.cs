using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class EaterReviveCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eater's Revive Cooldown");
            Description.SetDefault("You cannot be revived by the Eater of Worlds' soul crystal");
            Main.debuff[Type] = true;
        }
    }
}
