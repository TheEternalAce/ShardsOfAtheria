using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class BaseCombat : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Knowledge Base: Combat");
            Description.SetDefault("10% increased damage and 10 defense\n" +
                "Thorns effect\n" +
                "'You excel in combat'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 10;
            player.allDamage += .1f;
            player.thorns = 1;
        }
    }
}
