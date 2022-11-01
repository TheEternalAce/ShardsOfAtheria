using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Cooldowns
{
    public class SetBonusCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Armor Set Bonus Active Cool Down");
            Description.SetDefault("You cannot activate your armor set bonus");
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}
