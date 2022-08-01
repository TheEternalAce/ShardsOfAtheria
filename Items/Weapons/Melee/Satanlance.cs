using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class Satanlance : ModItem
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("ecnalnataS");
			Tooltip.SetDefault("You feel like you can do anything\n" +
				"'!!!SOAHC SOAHC'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 54;
			Item.height = 56;

			Item.damage = 257;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 6;
			Item.crit = 100;

			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item1;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;

			Item.shootSpeed = 3.5f;
			Item.value = Item.sellPrice(0, 4, 25);
			Item.rare = ItemRarityID.Expert;
			Item.shoot = ModContent.ProjectileType<SatanlanceProjectile>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LunarBar, 20)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
    }
}