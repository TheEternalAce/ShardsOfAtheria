using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Items.Potions
{
	public class SoulInjection : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Damages you but grants the following:\n" +
				"Increased damage, movement speed and defense\n" +
				"Grants life regen\n" +
				"'Bro I promise, injecting souls directly into your bloodstream is a good idea'");
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.Red;
			Item.maxStack = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.consumable = true;
			Item.useTurn = true;
			Item.buffType = ModContent.BuffType<SoulInfused>();
			Item.buffTime = (4 * 60) * 60;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<EmptyNeedle>())
				.AddRecipeGroup(SoARecipes.Soul, 10)
				.AddTile(TileID.Bottles)
				.Register();
        }

        public override void OnConsumeItem(Player player)
        {
            player.QuickSpawnItem(player.GetItemSource_Misc(ModContent.ItemType<SoulInjection>()), ModContent.ItemType<EmptyNeedle>());
			player.AddBuff(ModContent.BuffType<InjectionShock>(), 5 * 60);
		}

		public override bool CanUseItem(Player player)
		{
			if (!player.HasBuff(ModContent.BuffType<InjectionShock>()))
				return true;
			else return false;
		}
	}
}