using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	public class TomeOfOmniscience: ModItem
	{
        public override void SetStaticDefaults()
		{
		}

        public override void SetDefaults()
		{
			item.width = 15;
			item.height = 22;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
			item.expert = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().omnicientTome = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			var list = SagesMania.TomeKey.GetAssignedKeys();
			string keyname = "Not bound";

			if (list.Count > 0)
			{
				keyname = list[0];
			}

			tooltips.Add(new TooltipLine(mod, "Damage", $"Press '[i:{keyname}]' to cycle between 3 Knowledge Bases:\n" +
				"Combat Conservation and Exploration"));
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<UnshackledTomeOfOmniscience>());
			recipe.AddIngredient(ItemID.Shackle, 2);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}