using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class ReactorMeltdown : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
			WeaponElements.Electric.Add(Type);
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 26;

			Item.damage = 162;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 2;
			Item.crit = 7;

			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = new SoundStyle("ShardsOfAtheria/Sounds/Item/ReactorMeltdownAlarm")
			{
				Volume = 0.9f,
				MaxInstances = 3,
			};
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;

			Item.shootSpeed = 16f;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 2, 20);
			Item.shoot = ModContent.ProjectileType<ReactorMeltdownProj>();
		}
	}
}