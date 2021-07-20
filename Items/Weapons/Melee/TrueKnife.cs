using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Melee
{
	public class TrueKnife : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("True Kitchen Knife");
			Tooltip.SetDefault("''Here we are!''\n" +
				"[c/FF0000:''About time.'']");
		}

		public override void SetDefaults() 
		{
			item.damage = 999;
			item.melee = true;
			item.width = 32;
			item.height = 34;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Red;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Slash");
			item.autoReuse = false;
			item.crit = 0;
			item.shoot = ModContent.ProjectileType<TrueBlade>();
			item.shootSpeed = 15;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<KitchenKnife>());
			recipe.AddIngredient(ModContent.ItemType<LovesKnife>());
			recipe.AddIngredient(ModContent.ItemType<AreusDagger>());
			recipe.AddIngredient(ModContent.ItemType<CrossDagger>());
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}