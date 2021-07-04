using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class ReactorMeltdown : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("'Sussus Moogus'");
		}

		public override void SetDefaults() 
		{
			item.damage = 162;
			item.melee = true;
			item.width = 30;
			item.height = 26;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 2;
			item.value = Item.sellPrice(gold: 80);
			item.rare = ItemRarityID.Red;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ReactorMeltdownAlarm");
			item.crit = 14;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<ReactorMeltdownProj>();
			item.shootSpeed = 16f;
			item.channel = true;
		}
	}
}