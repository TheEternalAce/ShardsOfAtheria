using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using ShardsOfAtheria.Projectiles;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
	public class AreusLuger : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}

		public override void SetDefaults() 
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.width = 40;
			Item.height = 22;
			Item.scale = .85f;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item12;
			Item.autoReuse = false;
			Item.crit = 5;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.sellPrice(gold: 25);
			Item.shoot = ModContent.ProjectileType<ElectricBeam>();
			Item.shootSpeed = 16f;
			if (!Config.areusWeaponsCostMana)
				areusResourceCost = 1;
			else Item.mana = 5;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 5)
				.AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 7)
				.AddIngredient(ItemID.HellstoneBar, 10)
				.AddTile(ModContent.TileType<AreusForge>())
				.Register();
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-1, -1);
		}

		public override bool CanUseItem(Player player)
		{
			var areusDamagePlayer = player.GetModPlayer<SMPlayer>();

			if (areusDamagePlayer.areusResourceCurrent >= areusResourceCost && player.statMana >= Item.mana)
			{
				areusDamagePlayer.areusResourceCurrent -= areusResourceCost;
				return true;
			}
			return false;
		}
    }
}