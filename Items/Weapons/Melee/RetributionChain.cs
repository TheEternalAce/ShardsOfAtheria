using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;

namespace SagesMania.Items.Weapons.Melee
{
	public class RetributionChain : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.value = Item.sellPrice(silver: 5);
			item.rare = ItemRarityID.Red;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 40;
			item.useTime = 40;
			item.knockBack = 4f;
			item.damage = 560;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<RetributionChainProjectile>();
			item.shootSpeed = 15.1f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			item.crit = 20;
			item.channel = true;
		}
	}
}