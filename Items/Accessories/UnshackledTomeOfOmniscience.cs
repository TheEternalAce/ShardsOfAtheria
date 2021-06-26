using SagesMania.Buffs;
using SagesMania;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	public class UnshackledTomeOfOmniscience: ModItem
	{
        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants effects of all Knowledge Bases at once\n" +
				"''My unlimited knowledge is too much for you.''");
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
			player.AddBuff(ModContent.BuffType<BaseCombat>(), 1);
			player.AddBuff(ModContent.BuffType<BaseConservation>(), 1);
			player.AddBuff(ModContent.BuffType<BaseExploration>(), 1);
			player.AddBuff(BuffID.Mining, 1);
			player.AddBuff(BuffID.Builder, 1);
			player.AddBuff(BuffID.Shine, 1);
			player.AddBuff(BuffID.Hunter, 1);
			player.GetModPlayer<SMPlayer>().unshackledTome = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TomeOfOmniscience>());
			recipe.AddIngredient(ItemID.ShadowKey);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}