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
				"[c/960096:'Babe I promise, I don't do drugs!']");
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
				.AddRecipeGroup("SM:Souls")
				.AddTile(TileID.Bottles)
				.Register();
        }

        public override void OnConsumeItem(Player player)
        {
            player.QuickSpawnItem(ModContent.ItemType<EmptyNeedle>());
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