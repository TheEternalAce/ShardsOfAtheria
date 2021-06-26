using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class LCAR9 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("L-CAR 9");
			Tooltip.SetDefault("Damage scales based on progression\n" +
				"''Take the shots and move on.''");
		}

		public override void SetDefaults()
		{
			item.damage = 17;
			item.ranged = true;
			item.noMelee = true;
			item.width = 42;
			item.height = 30;
			item.scale = .85f;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item41;
			item.autoReuse = true;
			item.crit = 13;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 13f;
			item.useAmmo = AmmoID.Bullet;
			item.expert = true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(2, 4);
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