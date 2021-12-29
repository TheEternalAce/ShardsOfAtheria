using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
	public class PacBlaster : SpecialItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Pac-Blaster");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults() 
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.mana = 10;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/PacBlasterShoot");
			Item.autoReuse = true;
			Item.crit = 0;
			Item.shoot = ModContent.ProjectileType<PacBlasterShot>();
			Item.shootSpeed = 16f;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<RootBeerCan>(), 30)
				.AddIngredient(ItemID.HellstoneBar, 15)
				.AddTile(TileID.Hellforge)
				.Register();
		}
	}
}