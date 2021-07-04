using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;

namespace SagesMania.Items.Weapons
{
	public class HecateII : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hécate II");
			Tooltip.SetDefault("Damage scales based on progression\n" +
				"''She's a real monster.''\n" +
				"''Just like me.''");
		}

		public override void SetDefaults() 
		{
			item.damage = 25;
			item.ranged = true;
			item.noMelee = true;
			item.width = 93;
			item.height = 20;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 8f;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item40;
			item.crit = 6;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.expert = true;
			item.value = Item.sellPrice(gold: 5);
		}
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-16, -2);
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == ProjectileID.Bullet || type == ModContent.ProjectileType<BBProjectile>())
            {
				type = ProjectileID.BulletHighVelocity;
            }
			return true;
        }

        public override void HoldItem(Player player)
        {
			player.scope = true;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.hardMode)
			{
				add += .1f;
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				add += .15f;
			}
			if (NPC.downedPlantBoss)
			{
				add += .15f;
			}
			if (NPC.downedGolemBoss)
			{
				add += .2f;
			}
			if (NPC.downedAncientCultist)
			{
				add += .5f;
			}
			if (NPC.downedMoonlord)
			{
				add += 1f;
			}
		}
	}
}