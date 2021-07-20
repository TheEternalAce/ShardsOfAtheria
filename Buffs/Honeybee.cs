using Terraria;
using Terraria.ModLoader;
using SagesMania.Projectiles.Minions;

namespace SagesMania.Buffs
{
    public class Honeybee : ModBuff
    {
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Honeybee");
			Description.SetDefault("The Honeybee will fight along side you\n" +
				"'Something's not quite right...'\n" +
				"'Her name is Rose'");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			SMPlayer modPlayer = player.GetModPlayer<SMPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HoneybeeMinion>()] > 0)
				modPlayer.honeybeeMinion = true;
			if (!modPlayer.honeyCrown)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
				player.buffTime[buffIndex] = 1;
		}
	}
}