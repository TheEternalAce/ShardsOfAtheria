using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Minions;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class GreaterSapphireCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("10% chance to dodge damage\n" +
				"'Calls a sapphire spirit to fight along side you'");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<SapphireCore>())
				.AddIngredient(ItemID.HellstoneBar, 10)
				.AddTile(TileID.Hellforge)
				.Register();
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<SMPlayer>().sapphireCore = true;
			player.GetModPlayer<SMPlayer>().greaterSapphireCore = true;

			player.AddBuff(ModContent.BuffType<SapphireSpirit>(), 2);
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0)
			{
				Projectile.NewProjectile(player.GetProjectileSource_Accessory(Item), player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 80, 5, player.whoAmI);
			}
		}
    }
}