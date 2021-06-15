using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;
using SagesMania.Projectiles;
using Microsoft.Xna.Framework;

namespace SagesMania.Items.Weapons
{
	public class Satanlance : ModItem
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("ecnaknataS");
			Tooltip.SetDefault("You feel like you can do anything\n" +
				"''!!!SOAHC SOAHC''");
		}

		public override void SetDefaults() 
		{
			item.damage = 257;
			item.melee = true;
			item.width = 60;
			item.height = 60;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = Item.sellPrice(gold: 50);
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.crit = 100;
			item.shoot = ModContent.ProjectileType<SatanlanceProjectile>();
			item.shootSpeed = 20;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			target.AddBuff(BuffID.Electrified, 600);
		}
	}
}