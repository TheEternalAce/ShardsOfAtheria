using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.Potions
{
	public class InfectionInjection : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Gives the Infection debuff\n" +
				"'Recommended by 0 out of 10 doctors'");
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.consumable = true;
			Item.useTurn = true;
			Item.buffType = ModContent.BuffType<Infection>();
			Item.buffTime = (4 * 60) * 60;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<EmptyNeedle>())
				.AddIngredient(ModContent.ItemType<CrystalInfection>())
				.AddTile(TileID.Bottles)
				.AddTile(TileID.Furnaces)
				.Register();
		}

		public override void OnConsumeItem(Player player)
		{
			player.QuickSpawnItem(player.GetItemSource_Misc(ModContent.ItemType<InfectionInjection>()), ModContent.ItemType<EmptyNeedle>());
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