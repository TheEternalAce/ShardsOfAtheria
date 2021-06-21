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
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.Orange;
			item.maxStack = 30;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.UseSound = SoundID.Item3;
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
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
		/*
        public override bool UseItem(Player player)
        {
			player.Hurt(PlayerDeathReason.ByOther(Player.), 20, 1, false, false, false, -1);
        }
		*/
    }
}