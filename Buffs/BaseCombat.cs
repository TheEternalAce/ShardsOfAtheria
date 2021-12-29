using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class BaseCombat : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Knowledge Base: Combat");
            Description.SetDefault("10% increased damage and 10 defense\n" +
                "Thorns effect\n" +
                "'You excel in combat'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 10;
            player.GetDamage(DamageClass.Generic) += .1f;
            player.thorns = 1;
        }
    }
}
