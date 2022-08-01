using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class ShadeState : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shade State");
			Description.SetDefault("Increased damage by 15%\n" +
                "Increased defense by 20");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Generic) += .15f;
			player.statDefense += 20;
		}
	}

	public class ShadePlayer : ModPlayer
    {

    }
}
