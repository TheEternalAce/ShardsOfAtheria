using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class LovesKnife : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Tainted Love");
			Tooltip.SetDefault("Holds the corrupted love and jealousy of several souls");
		}

		public override void SetDefaults() 
		{
			Item.damage = 97;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 34;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(gold: 2, silver: 40);
			Item.value = Item.sellPrice(gold: 1, silver: 40);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 10;
			Item.shoot = ModContent.ProjectileType<TaintedHeart>();
			Item.shootSpeed = 15;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<KitchenKnife>())
				.AddIngredient(ItemID.LifeCrystal, 5)
				.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 7)
				.AddIngredient(ItemID.Ectoplasm, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}