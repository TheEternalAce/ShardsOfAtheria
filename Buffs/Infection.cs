using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class Infection : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infected");
            Description.SetDefault("You are succumbing to the infection...");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 4;
            player.statDefense -= 16;
            player.allDamage *= 2;
            player.maxRunSpeed /= 2;
        }
    }
}
