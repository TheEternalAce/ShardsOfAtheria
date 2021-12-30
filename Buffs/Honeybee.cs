using Terraria;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Minions;

namespace ShardsOfAtheria.Buffs
{
    public class Honeybee : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Honeybee");
			Description.SetDefault("The Honeybee will fight along side you\n" +
				"'Her name is Rose'");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HoneybeeMinion>()] > 0)
			{
				player.buffTime[buffIndex] = 180000;
			}
			else
			{
				player.buffTime[buffIndex] = 0;
			}
		}
	}
}