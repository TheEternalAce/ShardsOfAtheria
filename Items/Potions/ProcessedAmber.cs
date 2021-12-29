using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Items.Potions
{
	public class ProcessedAmber : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Minor improvements to all stats\n" +
				"'Do not crush or chew, you'll break your teeth'");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 30;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item3;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.consumable = true;
			Item.useTurn = true;
			Item.healLife = 25;
			Item.buffType = BuffID.WellFed;
			Item.buffTime = (8 * 60) * 60;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ItemID.Amber)
				.AddTile(TileID.CookingPots)
				.Register();
        }
    }
}