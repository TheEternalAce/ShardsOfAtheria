using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;

namespace SagesMania.Items.Potions
{
	public class RootBeerCan : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Minor improvements to all stats\n" +
				"[c/0000FF:''I TOLD YOU NOT TO STEAL MY BEER!'']\n" +
				"[c/960096:''IT'S NOT BEER SIR!'']\n" +
				"[c/0000FF:''SHUT UP!'']");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 28;
			item.value = Item.sellPrice(silver: 75);
			item.rare = ItemRarityID.Blue;
			item.maxStack = 30;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.UseSound = SoundID.Item3;
			item.useTime = 15;
			item.useAnimation = 15;
			item.consumable = true;
			item.useTurn = true;
			item.buffType = BuffID.WellFed;
			item.buffTime = (4 * 60) * 60;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SM:CopperBars", 2);
			recipe.AddIngredient(ItemID.Ale);
            recipe.AddIngredient(ItemID.Wood);
			recipe.AddTile(TileID.Bottles);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}