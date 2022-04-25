using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class CreeperRevenge : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Creeper Revenge");
			Description.SetDefault("20% increase to all damage");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Generic) += .2f;
		}
	}
}
