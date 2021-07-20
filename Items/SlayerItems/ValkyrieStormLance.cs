using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;

namespace SagesMania.Items.SlayerItems
{
	public class ValkyrieStormLance : SlayerItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Briefly shocks enemies\n" +
				"'The stolen lance of a slain Valkyrie'");
		}

		public override void SetDefaults() 
		{
			item.damage = 50;
			item.melee = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = Item.sellPrice(gold: 50);
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.crit = 6;
			item.shoot = ModContent.ProjectileType<ValkyrieStormLanceProj>();
			item.shootSpeed = 3.5f;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
			item.rare = ItemRarityID.Expert;
		}

        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
    }
}