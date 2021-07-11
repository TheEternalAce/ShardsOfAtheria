using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	public class HallowedSeal : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("5% increased magic and melee damage\n" +
				"Immunity to fall damage\n" +
				"Melee hits restore 15 mana\n" +
				"'A holy seal that once protected an underground kingdom'");
        }

        public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(gold: 7, silver: 50);
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
			item.defense = 9;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.meleeDamage += .05f;
			player.magicDamage += .05f;
			player.noFallDmg = true;
			player.GetModPlayer<SMPlayer>().hallowedSeal = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}