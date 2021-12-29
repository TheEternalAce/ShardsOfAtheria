using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class DoubleBow : SlayerItem
	{
		int cycleShot;

        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Alternates between Cursed Arrows and Ichor arrows");
        }

        public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 80;
			Item.value = Item.sellPrice(gold: 7, silver: 50);
			Item.rare = ItemRarityID.Expert;

			Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 2f;
			Item.crit = 10;
            Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;

			Item.shoot = ItemID.PurificationPowder;
			Item.shootSpeed = 16;
			Item.useAmmo = AmmoID.Arrow;
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				if (cycleShot == 0)
				{
					type = ProjectileID.IchorArrow;
					cycleShot = 1;
				}
				else
				{
					type = ProjectileID.CursedArrow;
					cycleShot = 0;
				}
			}
		}

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12, 2);
		}
	}
}