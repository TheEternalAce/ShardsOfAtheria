using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Magic;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
	public class AreusStaff : AreusWeapon
	{
		public override void SetDefaults() 
		{
			Item.damage = 130;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.staff[Item.type] = true;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3.75f;
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
			Item.crit = 16;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.sellPrice(0,  20);
			Item.shoot = ModContent.ProjectileType<ElectricBolt>();
			Item.shootSpeed = 16f;

			if (ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
				Item.mana = 6;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 10)
				.AddIngredient(ItemID.FragmentVortex, 7)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}