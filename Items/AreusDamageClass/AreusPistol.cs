using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using SagesMania.Projectiles;
using SagesMania.Buffs;

namespace SagesMania.Items.AreusDamageClass
{
	public class AreusPistol : AreusDamageItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("100% safety guaranteed(?)");
		}

		public override void SafeSetDefaults() 
		{
			Player player = Main.LocalPlayer;
			item.damage = 120;
			item.noMelee = true;
			item.width = 32;
			item.height = 16;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item12;
			item.autoReuse = false;
			item.crit = 16;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ModContent.ProjectileType<ElectricBolt>();
			item.shootSpeed = 16f;
			areusResourceCost = 1;
		}

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(3, 4);
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusOreItem>(), 15);
			recipe.AddIngredient(ItemID.FragmentVortex, 7);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.HasBuff(ModContent.BuffType<Overdrive>()))
				type = ModContent.ProjectileType<ElectricBlast>();
			return true;
		}
	}
}