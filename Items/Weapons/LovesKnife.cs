using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;

namespace SagesMania.Items.Weapons
{
	public class LovesKnife : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Tainted Love");
			Tooltip.SetDefault("This blade holds the corrupted love and jealousy of several souls\n" +
				"[c/960096:''Why won't you love me!?'']");
		}

		public override void SetDefaults() 
		{
			item.damage = 97;
			item.melee = true;
			item.width = 32;
			item.height = 34;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = Item.buyPrice(gold: 2, silver: 40);
			item.value = Item.sellPrice(gold: 1, silver: 40);
			item.rare = ItemRarityID.Red;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Slash");
			item.autoReuse = false;
			item.crit = 10;
			item.shoot = ModContent.ProjectileType<TaintedHeart>();
			item.shootSpeed = 15;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<KitchenKnife>());
			recipe.AddIngredient(ItemID.LifeCrystal, 5);
			recipe.AddIngredient(ItemID.SoulofNight, 7);
			recipe.AddIngredient(ItemID.Ectoplasm, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}