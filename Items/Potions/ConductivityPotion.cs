using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.Potions
{
	public class ConductivityPotion : ModItem
	{
		public const string tip = "Increases areus weapon damage by 15% and increases duration of Electric Shock";

		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault(tip);

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 50;
			Item.maxStack = 30;

			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.useTurn = true;

			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.Cyan;

			Item.buffType = ModContent.BuffType<Conductive>();
			Item.buffTime = 14400;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>())
				.AddIngredient(ItemID.CopperOre)
				.AddIngredient(ItemID.BottledWater)
				.AddTile(TileID.Bottles)
				.Register();
		}
	}
	public class Conductive : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Conductive");
			Description.SetDefault(ConductivityPotion.tip);
		}
	}
}