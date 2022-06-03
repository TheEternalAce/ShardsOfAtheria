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
			Tooltip.SetDefault("'Sussus Moogus'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 162;
			Item.DamageType = DamageClass.Melee;
			Item.width = 30;
			Item.height = 26;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 2, 20);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = new SoundStyle($"{nameof(ShardsOfAtheria)}/Sounds/Item/ReactorMeltdownAlarm")
			{
				Volume = 0.9f,
				PitchVariance = 0.2f,
				MaxInstances = 3,
			};
			Item.crit = 7;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<ReactorMeltdownProj>();
			Item.shootSpeed = 16f;
			Item.channel = true;
		}
	}
}