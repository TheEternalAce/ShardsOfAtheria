using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class SoulInfused : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Infused");
            Description.SetDefault("You have been infused with souls");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += 50;
            player.statDefense += 10;
            player.GetDamage(DamageClass.Generic) += .15f;
        }
    }
}
