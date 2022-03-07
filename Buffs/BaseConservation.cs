using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class BaseConservation : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Knowledge Base: Conservation");
            Description.SetDefault("10% reduced mana cost and 15% chance to not consume ammo\n" +
                "'You are efficent with your resources'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.manaCost -= .1f;
            player.GetModPlayer<SoAPlayer>().baseConservation = true;
        }
    }
}
