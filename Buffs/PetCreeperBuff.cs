using Terraria;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Pets;

namespace ShardsOfAtheria.Buffs
{
    public class PetCreeperBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Creeper Pet");
			Description.SetDefault("The creeper will follow you");
			Main.vanityPet[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<SMPlayer>().creeperPet = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<PetCreeper>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetProjectileSource_Buff(buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<PetCreeper>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
