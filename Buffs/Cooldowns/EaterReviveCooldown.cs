using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Cooldowns
{
    public class EaterReviveCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eater's Revive Cool Down");
            Description.SetDefault("You cannot be revived by the Eater of Worlds' soul crystal");
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}
