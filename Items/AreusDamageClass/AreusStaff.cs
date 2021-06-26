using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using Terraria;
using SagesMania.Projectiles;

namespace SagesMania.Items.AreusDamageClass
{
	public class AreusStaff: AreusDamageItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("''It's magic, so it won't shock you. I think.''\n" +
				"WARNING: Areus charge does get used up even if the you run out of mana and still try to use this");
		}

		public override void SafeSetDefaults() 
		{
			item.damage = 130;
			item.noMelee = true;
			Item.staff[item.type] = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.crit = 16;
			item.value = Item.sellPrice(gold: 20);
			item.shoot = ModContent.ProjectileType<ElectricBolt>();
			item.shootSpeed = 16f;
			item.mana = 6;
			areusResourceCost = 1;
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
	}
}