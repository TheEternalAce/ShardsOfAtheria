using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;
using SagesMania.Items.Placeable;

namespace SagesMania.Items.Potions
{
	public class ProcessedAmber : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Minor improvements to all stats\n" +
				"''Do not crush or chew, you'll break your teeth''");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.rare = ItemRarityID.Orange;
			item.maxStack = 30;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.UseSound = SoundID.Item3;
			item.useTime = 15;
			item.useAnimation = 15;
			item.consumable = true;
			item.useTurn = true;
			item.healLife = 25;
			item.buffType = BuffID.WellFed;
			item.buffTime = (8 * 60) * 60;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Amber);
			recipe.AddTile(TileID.CookingPots);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}