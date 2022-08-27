using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Weapon.Melee.Gomorrah;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class Gomorrah : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Use RMB to throw a spear\n" +
                "Sticking 8 spears into the same target causes a large burst of damage");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 54;
			Item.height = 56;

			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 6;
			Item.crit = 8;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item1;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;

			Item.shootSpeed = 3.5f;
			Item.value = Item.sellPrice(0, 2, 25);
			Item.rare = ItemRarityID.Orange;
			Item.shoot = ModContent.ProjectileType<Gomorrah_Spear>();
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useStyle = ItemUseStyleID.Swing;

				Item.shootSpeed = 16f;
				Item.shoot = ModContent.ProjectileType<Gomorrah_Javelin>();
			}
			else
			{
				Item.useStyle = ItemUseStyleID.Shoot;

				Item.shootSpeed = 3.5f;
				Item.shoot = ModContent.ProjectileType<Gomorrah_Spear>();
			}
			return player.ownedProjectileCounts[ModContent.ProjectileType<Gomorrah_Spear>()] < 1;
		}
    }
}