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
				"Tears through enemy armor");
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
			Item.crit = 6;
			Item.value = Item.sellPrice(0,  25);
			Item.shoot = ItemID.PurificationPowder;
			Item.shootSpeed = 20f;
			Item.useAmmo = ModContent.ItemType<AreusRod>();

			if (ModContent.GetInstance<ServerSideConfig>().areusWeaponsCostMana)
				Item.mana = 20;

			chargeCost = 5;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 20)
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
			player.GetArmorPenetration(DamageClass.Generic) = 20;
        }
    }
}