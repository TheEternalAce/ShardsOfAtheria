using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class Satanlance : ModItem
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("ecnalnataS");
			Tooltip.SetDefault("You feel like you can do anything\n" +
				"'!!!SOAHC SOAHC'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 257;
			Item.DamageType = DamageClass.Melee;
			Item.width = 54;
			Item.height = 56;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(0,  50);
			Item.rare = ItemRarityID.Expert;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 100;
			Item.shoot = ModContent.ProjectileType<SatanlanceProjectile>();
			Item.shootSpeed = 3.5f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LunarBar, 20)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			target.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
		}
        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
    }
}