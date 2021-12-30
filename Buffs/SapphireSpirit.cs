using Terraria;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Minions;

namespace ShardsOfAtheria.Buffs
{
    public class SapphireSpirit : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapphire Spirit");
			Description.SetDefault("The sapphire spirit will fight along side you\n" +
				"'Her name is Luna'");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] > 0 && (player.GetModPlayer<SMPlayer>().greaterSapphireCore
				|| player.GetModPlayer<SMPlayer>().superSapphireCore))
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
