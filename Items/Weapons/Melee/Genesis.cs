using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class Genesis : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Left click swings an energy whip\n" +
				"Right click transforms into a spear");
		}

		public override void SetDefaults() 
		{
			Item.damage = 120;
			Item.DamageType = DamageClass.Melee;
			Item.width = 94;
			Item.height = 104;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(gold: 50);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item152;
			Item.autoReuse = false;
			Item.crit = 6;
			Item.shoot = ModContent.ProjectileType<Genesis_Whip>();
			Item.shootSpeed = 4f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FragmentSolar, 9)
				.AddIngredient(ItemID.FragmentVortex, 9)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
				Item.shoot = ModContent.ProjectileType<Genesis_Spear>();
				Item.shootSpeed = 4f;
				Item.UseSound = SoundID.Item1;
				Item.useStyle = ItemUseStyleID.Shoot;
			}
			else
			{
				Item.shoot = ModContent.ProjectileType<Genesis_Whip>();
				Item.shootSpeed = 4f;
				Item.UseSound = SoundID.Item152;
				Item.useStyle = ItemUseStyleID.Swing;
			}
            return base.CanUseItem(player);
        }
    }

	public class Genesis_Whip : WhipProjectile
	{
		public override string Texture => "ShardsOfAtheria/Projectiles/Weapon/Genesis_Whip";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Genesis (Whip)");
			ProjectileID.Sets.IsAWhip[Type] = true;
		}

        public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;

			Projectile.aiStyle = 165;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.ownerHitCheck = true;
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			WhipDefaults();
		}

        public override void WhipDefaults()
		{
			originalColor = new Color(0, 255, 200);
			whipRangeMultiplier = 1.5f;
			fallOff = 0.15f;
			whipSegments = 15;
		}
	}
}