using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;
using SagesMania.Items.Placeable;
using Terraria.DataStructures;

namespace SagesMania.Items.Potions
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
			item.width = 48;
			item.height = 48;
			item.rare = ItemRarityID.Orange;
			item.maxStack = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 15;
			item.useAnimation = 15;
			item.consumable = true;
			item.useTurn = true;
			item.buffType = ModContent.BuffType<Infection>();
			item.buffTime = (4 * 60) * 60;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<EmptyNeedle>());
            recipe.AddIngredient(ModContent.ItemType<CrystalInfection>());
			recipe.AddTile(TileID.Bottles);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
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