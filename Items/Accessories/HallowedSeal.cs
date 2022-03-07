using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
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
			Item.width = 24;
			Item.height = 24;
			Item.value = Item.sellPrice(gold: 7, silver: 50);
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
			Item.defense = 9;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Melee) += .05f;
			player.GetDamage(DamageClass.Magic) += .05f;
			player.noFallDmg = true;
			player.GetModPlayer<SoAPlayer>().hallowedSeal = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 5)
				.AddIngredient(ItemID.SoulofLight, 5)
				.Register();
		}
	}
}