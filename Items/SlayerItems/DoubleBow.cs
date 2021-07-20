using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.SlayerItems
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
			item.width = 42;
			item.height = 80;
			item.value = Item.sellPrice(gold: 7, silver: 50);
			item.rare = ItemRarityID.Expert;

			item.damage = 70;
            item.ranged = true;
			item.knockBack = 2f;
			item.crit = 10;
            item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;

			item.shoot = ItemID.PurificationPowder;
			item.shootSpeed = 16;
			item.useAmmo = AmmoID.Arrow;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12, 2);
		}
	}
}