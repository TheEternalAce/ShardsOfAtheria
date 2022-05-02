using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class ImmunityBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Immunity Buff");
			Description.SetDefault("You are immune");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.immune = true;
		}
	}
}
