using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using Terraria;
using SagesMania.Items.Weapons.Ammo;
using Microsoft.Xna.Framework;

namespace SagesMania.Items.Weapons.Magic
{
	public class AreusRailgun : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses Rods as ammo\n" +
				"Tears through enemy armor\n" +
				"'Leaves your enemies in an electrified pile of flesh, bones, both or scrap'");
		}

		public override void SetDefaults() 
		{
			item.damage = 100;
			item.magic = true;
			item.noMelee = true;
			item.width = 44;
			item.height = 26;
			item.useTime = 48;
			item.useAnimation = 48;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4f;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item38;
			item.autoReuse = false;
			item.crit = 20;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ItemID.PurificationPowder;
			item.shootSpeed = 20f;
			item.useAmmo = ModContent.ItemType<AreusRod>();

			if (!Config.areusWeaponsCostMana)
				areusResourceCost = 5;
			else item.mana = 20;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 15);
			recipe.AddIngredient(ItemID.SoulofMight, 7);
			recipe.AddIngredient(ItemID.SoulofFright, 7);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}

        public override void HoldItem(Player player)
        {
			player.armorPenetration = 20;
        }

        // Make sure you can't use the item if you don't have enough resource and then use 10 resource otherwise.
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