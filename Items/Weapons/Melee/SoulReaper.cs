using SagesMania.Items.Weapons.Ranged;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Melee
{
	public class SoulReaper : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'A weapon that once belonged to the ancient Grimm Reaper'");
		}

		public override void SetDefaults()
		{
			item.damage = 1000;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = Item.sellPrice(gold: 50);
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.useTurn = true;
			item.crit = 100;
		}

        public override void HoldItem(Player player)
        {
			player.statLifeMax2 = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BlackAreusSword>());
			recipe.AddIngredient(ModContent.ItemType<AreusDagger>());
			recipe.AddIngredient(ModContent.ItemType<CrossDagger>());
			recipe.AddIngredient(ModContent.ItemType<AreusPistol>());
			recipe.AddIngredient(ModContent.ItemType<BBGun>());
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "Special Item", "[c/FF6400:Special Item]"));
		}
	}
}