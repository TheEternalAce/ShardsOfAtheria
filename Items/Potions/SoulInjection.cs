using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;

namespace SagesMania.Items.Potions
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
			item.width = 48;
			item.height = 48;
			item.value = Item.sellPrice(silver: 75);
			item.rare = ItemRarityID.Red;
			item.maxStack = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 15;
			item.useAnimation = 15;
			item.consumable = true;
			item.useTurn = true;
			item.buffType = ModContent.BuffType<SoulInfused>();
			item.buffTime = (4 * 60) * 60;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<EmptyNeedle>());
			recipe.AddRecipeGroup("SM:Souls");
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override void OnConsumeItem(Player player)
        {
            player.QuickSpawnItem(ModContent.ItemType<EmptyNeedle>());
		}
    }
}