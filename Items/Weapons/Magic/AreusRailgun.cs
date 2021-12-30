using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Terraria;
using ShardsOfAtheria.Items.Weapons.Ammo;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items.Weapons.Magic
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
			Item.damage = 100;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.width = 44;
			Item.height = 26;
			Item.useTime = 48;
			Item.useAnimation = 48;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4f;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = false;
			Item.crit = 20;
			Item.value = Item.sellPrice(gold: 25);
			Item.shoot = ItemID.PurificationPowder;
			Item.shootSpeed = 20f;
			Item.useAmmo = ModContent.ItemType<AreusRod>();

			if (!ModContent.GetInstance<Config>().areusWeaponsCostMana)
				areusResourceCost = 5;
			else Item.mana = 20;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 20)
				.AddIngredient(ItemID.SoulofMight, 7)
				.AddIngredient(ItemID.SoulofFright, 7)
				.AddTile(ModContent.TileType<AreusForge>())
				.Register();
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

			if (areusDamagePlayer.areusResourceCurrent >= areusResourceCost && player.statMana >= Item.mana)
			{
				areusDamagePlayer.areusResourceCurrent -= areusResourceCost;
				return true;
			}
			return false;
		}
	}
}