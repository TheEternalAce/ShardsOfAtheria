using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class Infection : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infected");
            Description.SetDefault("You are succumbing to the infection...");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 16;
            player.GetDamage(DamageClass.Generic) *= 2;
            player.moveSpeed /= 2;
        }
    }
}
