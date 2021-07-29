using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using SagesMania.Projectiles;

namespace SagesMania.Items.Weapons.Magic
{
	public class AreusLuger : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}

		public override void SetDefaults() 
		{
			item.damage = 40;
			item.magic = true;
			item.noMelee = true;
			item.width = 40;
			item.height = 22;
			item.scale = .85f;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item12;
			item.autoReuse = false;
			item.crit = 5;
			item.rare = ItemRarityID.Cyan;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ModContent.ProjectileType<ElectricBeam>();
			item.shootSpeed = 16f;
			if (!Config.areusWeaponsCostMana)
				areusResourceCost = 1;
			else item.mana = 5;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 5);
			recipe.AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 7);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-1, -1);
		}

		public override bool CanUseItem(Player player)
		{
			var areusDamagePlayer = player.GetModPlayer<SMPlayer>();

			if (areusDamagePlayer.areusResourceCurrent >= areusResourceCost && player.statMana >= item.mana)
			{
				areusDamagePlayer.areusResourceCurrent -= areusResourceCost;
				return true;
			}
			return false;
		}
    }
}