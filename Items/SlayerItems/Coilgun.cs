using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using SagesMania.Items.Weapons.Ammo;
using Microsoft.Xna.Framework;

namespace SagesMania.Items.SlayerItems
{
	public class Coilgun : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses Rods as ammo\n" +
				"Tears through enemy armor\n" +
				"'Uses electro magnets to fire projectiles at insane velocities'\n" +
				"'Areus Railgun's older brother'");
		}

		public override void SetDefaults() 
		{
			item.damage = 150;
			item.magic = true;
			item.noMelee = true;
			item.width = 44;
			item.height = 26;
			item.useTime = 48;
			item.useAnimation = 48;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4f;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item38;
			item.autoReuse = false;
			item.crit = 20;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ItemID.PurificationPowder;
			item.shootSpeed = 20f;
			item.useAmmo = ModContent.ItemType<AreusRod>();

			item.mana = 20;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}

        public override void HoldItem(Player player)
        {
			player.armorPenetration = 20;
        }
	}
}