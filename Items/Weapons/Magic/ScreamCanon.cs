using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Terraria;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Magic;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
	public class ScreamCanon : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a burst of 2 shockwaves that can bounce off of tiles\n" +
                "Shockwaves get faster after each bounce\n" +
                "'I like ya cut g'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.width = 44;
			Item.height = 26;
			Item.useTime = 12;
			Item.useAnimation = 24;
			Item.reuseDelay = 36;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4f;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.crit = 6;
			Item.value = Item.sellPrice(0,  5);
			Item.shoot = ModContent.ProjectileType<ScreamShockwave>();
			Item.shootSpeed = 20f;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}
    }
}