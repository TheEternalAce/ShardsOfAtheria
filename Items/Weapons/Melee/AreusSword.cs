using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Weapon.Melee;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class AreusSword : AreusWeapon
	{
		public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Definitely wont(?) shock you");
		}

		public override void SetDefaults() 
		{
			Item.damage = 200;
			Item.DamageType = DamageClass.Melee;
			Item.width = 64;
			Item.height = 76;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.sellPrice(0,  50);
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 8;
			Item.shoot = ModContent.ProjectileType<ElectricBlade>();
			Item.shootSpeed = 10;

			if (ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
				Item.mana = 8;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 20)
				.AddIngredient(ItemID.FragmentVortex, 20)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			target.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
		}
    }
}