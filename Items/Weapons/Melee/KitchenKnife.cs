using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class KitchenKnife : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("[c/FF0000:'Where are the knives?']");
		}

		public override void SetDefaults() 
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 34;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 6;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddRecipeGroup(RecipeGroupID.IronBar, 10)
				.AddRecipeGroup(RecipeGroupID.Wood, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}