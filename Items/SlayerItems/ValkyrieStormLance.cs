using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon;

namespace ShardsOfAtheria.Items.SlayerItems
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
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 50;
			Item.height = 50;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(gold: 50);
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 6;
			Item.shoot = ModContent.ProjectileType<ValkyrieStormLanceProj>();
			Item.shootSpeed = 3.5f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Expert;
		}

        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
    }
}