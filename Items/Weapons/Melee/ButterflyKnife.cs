using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class ButterflyKnife : SpecialItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("'SPAH CREEPIN' 'ROUND HERE!' ");
		}

		public override void SetDefaults() 
		{
			Item.damage = 1000;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.value = Item.sellPrice(gold: 80);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 96;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ItemID.LunarBar, 15)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}