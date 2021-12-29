using ShardsOfAtheria.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class TrueKnife : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("True Kitchen Knife");
			Tooltip.SetDefault("'Here we are!'\n" +
				"[c/FF0000:'About time.']");
		}

		public override void SetDefaults() 
		{
			Item.damage = 99;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 34;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(gold: 7);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 0;
			Item.shoot = ModContent.ProjectileType<TrueBlade>();
			Item.shootSpeed = 15;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<KitchenKnife>())
				.AddIngredient(ModContent.ItemType<LovesKnife>())
				.AddIngredient(ModContent.ItemType<AreusDagger>())
				.AddIngredient(ModContent.ItemType<CrossDagger>())
				.AddIngredient(ItemID.BrokenHeroSword)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}