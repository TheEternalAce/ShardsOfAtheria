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
			SMPlayer modPlayer = player.GetModPlayer<SMPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] > 0)
			{
				modPlayer.sapphireMinion = true;
			}
			if (!modPlayer.greaterSapphireCore)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 1;
			}
		}
	}
}
