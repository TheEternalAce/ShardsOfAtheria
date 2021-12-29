using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Items.Potions
{
	public class RootBeerCan : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Minor improvements to all stats\n" +
				"[c/0000FF:'I TOLD YOU NOT TO STEAL MY BEER!']\n" +
				"[c/960096:'IT'S NOT BEER SIR!']\n" +
				"[c/0000FF:'SHUT UP!']");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 28;
			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 30;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.UseSound = SoundID.Item3;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.consumable = true;
			Item.useTurn = true;
			Item.buffType = BuffID.WellFed;
			Item.buffTime = (4 * 60) * 60;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddRecipeGroup("SM:CopperBars", 2)
				.AddIngredient(ItemID.Ale)
				.AddIngredient(ItemID.Wood)
				.AddTile(TileID.Bottles)
				.Register();
        }
    }
}