using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Weapons.Ammo;
using System.Collections.Generic;

namespace ShardsOfAtheria.Items.Accessories
{
	public class BBBottle: SpecialItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BB Bottle");
			Tooltip.SetDefault("5% chance to not consume ammo\n" +
				"Fires an extra BB");
		}

		public override void SetDefaults()
		{
			Item.width = 15;
			Item.height = 22;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().BBBottle = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BB>(), 200)
				.AddIngredient(ItemID.Bottle)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}