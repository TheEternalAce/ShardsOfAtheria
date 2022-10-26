using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Materials;

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

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.maxStack = 9999;

			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
			Item.useTurn = true;

			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.Red;

			Item.buffType = ModContent.BuffType<SoulInfused>();
			Item.buffTime = 14400;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<EmptyNeedle>())
				.AddRecipeGroup(ShardsRecipes.Soul, 10)
				.AddTile(TileID.Bottles)
				.Register();
        }

        public override void OnConsumeItem(Player player)
        {
            player.QuickSpawnItem(player.GetSource_FromThis(), ModContent.ItemType<EmptyNeedle>());
			player.AddBuff(ModContent.BuffType<InjectionShock>(), 300);
		}

		public override bool CanUseItem(Player player)
		{
			if (!player.HasBuff(ModContent.BuffType<InjectionShock>()))
				return true;
			else return false;
		}
	}
}